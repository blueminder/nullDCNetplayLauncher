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
                    lblDragInfo.Text = "Copying BIN...";
                    destpath += "data\\" + Path.GetFileName(files[0]);
                    File.Copy(path, destpath, true);
                    File.SetAttributes(destpath, FileAttributes.Normal);
                    lblDragInfo.Text = $"BIN copied to {Launcher.ExtractRelativePath(destpath)}";
                }
                else if (Path.GetExtension(path).Equals(".zip"))
                {
                    destpath += "roms\\" + Path.GetFileNameWithoutExtension(files[0]);
                    Directory.CreateDirectory(destpath);
                    lblDragInfo.Text = "Extracting ROM...";
                    if (File.Exists(path))
                    {
                        ZipArchive archive = ZipFile.OpenRead(path);
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            entry.ExtractToFile(Path.Combine(destpath, entry.FullName), true);
                        }
                    }
                    lblDragInfo.Text = $"Rom Extracted to {Launcher.ExtractRelativePath(destpath)}";
                }
            }

            
        }

    }
}
