using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Linq.Expressions;

namespace nullDCNetplayLauncher
{
    public partial class DragLoadControl : UserControl
    {
        public DragLoadControl()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new System.Windows.Forms.DragEventHandler(DragLoadControl_DragEnter);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(DragLoadControl_DragDrop);
        }

        private void DragLoadControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void DragLoadControl_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                string path = files[0];
                string destpath = Launcher.rootDir + "nulldc-1-0-4-en-win\\";

                if (Path.GetExtension(path).Equals(".bin"))
                {
                    destpath += "data\\" + Path.GetFileName(files[0]);
                    if (File.Exists(destpath))
                    {
                        File.SetAttributes(destpath, FileAttributes.Normal);
                    }
                    if (path == destpath)
                    {
                        MessageBox.Show("Your file is already in the correct place, you silly billy.");
                        return;
                    }  
                    else
                    {
                        lblDragInfo.Text = "Copying BIN...";
                        File.Copy(path, destpath, true);
                        File.SetAttributes(destpath, FileAttributes.ReadOnly);
                        lblDragInfo.Text = $"BIN copied to {Launcher.ExtractRelativePath(destpath)}";
                    }  
                }
                else if (Path.GetExtension(path).Equals(".qjc"))
                {
                    lblDragInfo.Text = "Copying QJC...";
                    destpath += "qkoJAMMA\\" + Path.GetFileName(files[0]);
                    if (File.Exists(destpath))
                    {
                        File.SetAttributes(destpath, FileAttributes.Normal);
                    }
                    File.Copy(path, destpath, true);
                    lblDragInfo.Text = $"QJC copied to {Launcher.ExtractRelativePath(destpath)}";
                }
                else if (Path.GetExtension(path).Equals(".qkc"))
                {
                    lblDragInfo.Text = "Copying QKC...";
                    destpath += "qkoJAMMA\\" + Path.GetFileName(files[0]);
                    if (File.Exists(destpath))
                    {
                        File.SetAttributes(destpath, FileAttributes.Normal);
                    }
                    File.Copy(path, destpath, true);
                    lblDragInfo.Text = $"QKC copied to {Launcher.ExtractRelativePath(destpath)}";
                }
                else if (Path.GetFileName(path) == "nullDC.cfg")
                {
                    lblDragInfo.Text = "Copying CFG...";
                    destpath += Path.GetFileName(files[0]);
                    if (File.Exists(destpath))
                    {
                        File.SetAttributes(destpath, FileAttributes.Normal);
                    }
                    File.Copy(path, destpath, true);
                    lblDragInfo.Text = $"nullDC.cfg copied to {Launcher.ExtractRelativePath(destpath)}. Restart to restore defaults.";
                }
                else if (Path.GetExtension(path).Equals(".zip"))
                {
                    bool zipHasEntry = false;
                    var folderName = Path.GetFileNameWithoutExtension(files[0]);
                    Launcher.Game GamesJsonEntry = null;
                    if (Launcher.GamesJson != null)
                    {
                        try
                        {
                            GamesJsonEntry = Launcher.GamesJson.Where(g => g.Name == folderName).First();
                            if (GamesJsonEntry != null && GamesJsonEntry.Assets.Count() > 0)
                            {
                                zipHasEntry = true;
                            }
                        }
                        catch (Exception) { };
                    }
                    
                    destpath += "roms\\" + folderName;
                    Directory.CreateDirectory(destpath);
                    lblDragInfo.Text = "Extracting ROM...";
                    if (File.Exists(path))
                    {
                        ZipArchive archive = ZipFile.OpenRead(path);
                        if (zipHasEntry)
                        {
                            Program.ShowConsoleWindow();
                            Console.Clear();
                        }
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            Launcher.Asset asset = null;

                            if (GamesJsonEntry != null)
                                asset = GamesJsonEntry.Assets.Where(a => a.Name == entry.Name).First();

                            if (zipHasEntry)
                            {
                                entry.ExtractToFile(Path.Combine(destpath, asset.LocalName()), true);
                                var verifiedString = asset.VerifyFile(Path.Combine(destpath, asset.LocalName()));
                                Console.WriteLine(verifiedString);
                            }
                            else
                            {
                                entry.ExtractToFile(Path.Combine(destpath, entry.FullName), true);
                            }
                        }
                        if (zipHasEntry)
                        {
                            Console.WriteLine("\nPress any key to continue.");
                            Console.ReadKey();
                            Program.HideConsoleWindow();
                        }
                            
                    }
                    lblDragInfo.Text = $"Rom Extracted to {Launcher.ExtractRelativePath(destpath)}";
                }
            }

            
        }

    }
}
