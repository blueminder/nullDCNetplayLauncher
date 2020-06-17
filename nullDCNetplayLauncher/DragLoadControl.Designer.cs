namespace nullDCNetplayLauncher
{
    partial class DragLoadControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picDragLoad = new System.Windows.Forms.PictureBox();
            this.lblDragInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picDragLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // picDragLoad
            // 
            this.picDragLoad.Image = global::nullDCNetplayLauncher.Properties.Resources.outline_highlight_alt_black_48dp;
            this.picDragLoad.InitialImage = global::nullDCNetplayLauncher.Properties.Resources.outline_highlight_alt_black_48dp;
            this.picDragLoad.Location = new System.Drawing.Point(36, 18);
            this.picDragLoad.Name = "picDragLoad";
            this.picDragLoad.Size = new System.Drawing.Size(126, 120);
            this.picDragLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDragLoad.TabIndex = 0;
            this.picDragLoad.TabStop = false;
            // 
            // lblDragInfo
            // 
            this.lblDragInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDragInfo.Location = new System.Drawing.Point(12, 150);
            this.lblDragInfo.Name = "lblDragInfo";
            this.lblDragInfo.Size = new System.Drawing.Size(172, 71);
            this.lblDragInfo.TabIndex = 1;
            this.lblDragInfo.Text = "Drag NAOMI ROMs && BIOS Files Here";
            this.lblDragInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DragLoadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDragInfo);
            this.Controls.Add(this.picDragLoad);
            this.Name = "DragLoadControl";
            this.Size = new System.Drawing.Size(199, 221);
            ((System.ComponentModel.ISupportInitialize)(this.picDragLoad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picDragLoad;
        private System.Windows.Forms.Label lblDragInfo;
    }
}
