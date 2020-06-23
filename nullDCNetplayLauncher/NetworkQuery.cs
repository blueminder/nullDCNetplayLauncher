using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

    }
}
