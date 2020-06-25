using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using WebClient = System.Net.WebClient;

namespace nullDCNetplayLauncher
{
    public class NetworkQuery
    {
        public Dictionary<String, String> LocalIPsByNetwork { get; set; }

        public NetworkQuery()
        {
            LocalIPsByNetwork = GetIPsByNetwork();
        }

        // https://stackoverflow.com/questions/11412956/what-is-the-best-way-of-validating-an-ip-address
        public static bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        /// <summary> 
        /// This utility function displays all the IPv4 addresses of the local computer. 
        /// </summary> 
        /// https://blog.stephencleary.com/2009/05/getting-local-ip-addresses.html
        public static Dictionary<String, String> GetIPsByNetwork()
        {
            var IPsByNetwork = new Dictionary<String, String>();

            var externalIP = GetExternalIP();
            if (externalIP != null)
                IPsByNetwork.Add("External", externalIP);

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface network in networkInterfaces)
            {
                IPInterfaceProperties properties = network.GetIPProperties();
                foreach (IPAddressInformation address in properties.UnicastAddresses)
                {
                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                        continue;

                    if (IPAddress.IsLoopback(address.Address))
                        continue;

                    IPsByNetwork[network.Name] = address.Address.ToString();
                }
            }

            return IPsByNetwork;
        }

        public static string GetExternalIP()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return null; }
        }

        public static String GetRadminHostIP()
        {
            String radminHostIP;
            try
            {
                radminHostIP = GetIPsByNetwork()["Radmin VPN"];
            }
            catch
            {
                radminHostIP = null;
            }
            return radminHostIP;
        }

        public static void DownloadReferenceUrl(Launcher.Game game)
        {
            string displayName;
            if (game.Name.Length > 0)
                displayName = game.Name;
            else
                displayName = game.ID;

            Console.WriteLine($"Downloading {displayName}...");

            string workingDir = "";
            if (game.Root == "roms" || game.Root == "data")
            {
                workingDir = Path.Combine(Launcher.rootDir, "nulldc-1-0-4-en-win", game.Root);
            }
            else
            {
                Console.WriteLine("no valid root found in reference entry");
                return;
            }
            var di = new DirectoryInfo(workingDir);
            di.Attributes |= FileAttributes.Normal;
            var zipPath = Path.Combine(workingDir, $"{game.ID}.zip");

            if (!File.Exists(zipPath))
            {
                var referenceUri = new Uri(game.ReferenceUrl);
                if (referenceUri.Host == "mega.nz")
                {
                    MegaApiClient client = new MegaApiClient();
                    client.LoginAnonymous();

                    INodeInfo node = client.GetNodeFromLink(referenceUri);

                    Console.WriteLine($"Downloading {node.Name}");
                    client.DownloadFile(referenceUri, zipPath);

                    client.Logout();
                }
                else
                {
                    using (WebClient client = new WebClient())
                    {
                        Console.WriteLine($"Downloading {Path.GetFileName(referenceUri.LocalPath)}");
                        client.DownloadFile(referenceUri,
                                            zipPath);
                    }
                }
            }

            Console.WriteLine($"Download Complete");
            Console.WriteLine($"Extracting...\n");

            string extractPath;
            if (game.Root == "roms")
            {
                extractPath = Path.Combine(workingDir, displayName);
                Directory.CreateDirectory(extractPath);
            }
            else
            {
                extractPath = workingDir;
            }
                
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                List<Launcher.Asset> files = game.Assets;
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    try
                    {
                        var fileEntry = files.Where(f => f.Name == entry.Name).First();
                        if (fileEntry != null)
                        {
                            var destinationFile = Path.Combine(extractPath, fileEntry.LocalName());
                            entry.ExtractToFile(destinationFile, true);
                            Console.WriteLine(fileEntry.VerifyFile(destinationFile));
                        }

                    }
                    catch (Exception) { }
                }
            }
            File.Delete(zipPath);

            Console.WriteLine($"\nPress any key to continue.");
            Console.ReadKey(); 
        }

    }
}
