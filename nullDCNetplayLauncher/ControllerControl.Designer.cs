namespace nullDCNetplayLauncher
{
    partial class ControllerControl
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
            this.lblController = new System.Windows.Forms.Label();
            this.picArcadeStick = new System.Windows.Forms.PictureBox();
            this.btnSetup = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDPad = new System.Windows.Forms.Button();
            this.btnAnalog = new System.Windows.Forms.Button();
            this.chkForceMapper = new System.Windows.Forms.CheckBox();
            this.btnSkip = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picArcadeStick)).BeginInit();
            this.SuspendLayout();
            // 
            // lblController
            // 
            this.lblController.Location = new System.Drawing.Point(22, 188);
            this.lblController.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblController.Name = "lblController";
            this.lblController.Size = new System.Drawing.Size(286, 75);
            this.lblController.TabIndex = 0;
            this.lblController.Text = "Plug in your controller to continue.";
            this.lblController.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picArcadeStick
            // 
            this.picArcadeStick.Image = global::nullDCNetplayLauncher.Properties.Resources.base_full;
            this.picArcadeStick.Location = new System.Drawing.Point(22, 10);
            this.picArcadeStick.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.picArcadeStick.Name = "picArcadeStick";
            this.picArcadeStick.Size = new System.Drawing.Size(286, 176);
            this.picArcadeStick.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picArcadeStick.TabIndex = 1;
            this.picArcadeStick.TabStop = false;
            // 
            // btnSetup
            // 
            this.btnSetup.AutoSize = true;
            this.btnSetup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSetup.Location = new System.Drawing.Point(83, 251);
            this.btnSetup.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(45, 23);
            this.btnSetup.TabIndex = 2;
            this.btnSetup.Text = "Setup";
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Location = new System.Drawing.Point(200, 251);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(50, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDPad
            // 
            this.btnDPad.AutoSize = true;
            this.btnDPad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDPad.Location = new System.Drawing.Point(82, 251);
            this.btnDPad.Margin = new System.Windows.Forms.Padding(2);
            this.btnDPad.Name = "btnDPad";
            this.btnDPad.Size = new System.Drawing.Size(46, 23);
            this.btnDPad.TabIndex = 8;
            this.btnDPad.Text = "Digital";
            this.btnDPad.UseVisualStyleBackColor = true;
            this.btnDPad.Visible = false;
            this.btnDPad.Click += new System.EventHandler(this.btnDPad_Click);
            // 
            // btnAnalog
            // 
            this.btnAnalog.AutoSize = true;
            this.btnAnalog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAnalog.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnAnalog.Location = new System.Drawing.Point(202, 251);
            this.btnAnalog.Margin = new System.Windows.Forms.Padding(2);
            this.btnAnalog.Name = "btnAnalog";
            this.btnAnalog.Size = new System.Drawing.Size(50, 23);
            this.btnAnalog.TabIndex = 9;
            this.btnAnalog.Text = "Analog";
            this.btnAnalog.UseVisualStyleBackColor = true;
            this.btnAnalog.Visible = false;
            this.btnAnalog.Click += new System.EventHandler(this.btnAnalog_Click);
            // 
            // chkForceMapper
            // 
            this.chkForceMapper.AutoSize = true;
            this.chkForceMapper.Location = new System.Drawing.Point(97, 288);
            this.chkForceMapper.Margin = new System.Windows.Forms.Padding(2);
            this.chkForceMapper.Name = "chkForceMapper";
            this.chkForceMapper.Size = new System.Drawing.Size(140, 17);
            this.chkForceMapper.TabIndex = 11;
            this.chkForceMapper.Text = "Force Keyboard Mapper";
            this.chkForceMapper.UseVisualStyleBackColor = true;
            this.chkForceMapper.CheckedChanged += new System.EventHandler(this.chkForceMapper_CheckedChanged);
            // 
            // btnSkip
            // 
            this.btnSkip.AutoSize = true;
            this.btnSkip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSkip.Enabled = false;
            this.btnSkip.Location = new System.Drawing.Point(145, 251);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(38, 23);
            this.btnSkip.TabIndex = 12;
            this.btnSkip.Text = "Skip";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Visible = false;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // ControllerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.chkForceMapper);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.picArcadeStick);
            this.Controls.Add(this.btnSetup);
            this.Controls.Add(this.btnDPad);
            this.Controls.Add(this.btnAnalog);
            this.Controls.Add(this.lblController);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "ControllerControl";
            this.Size = new System.Drawing.Size(331, 318);
            this.Load += new System.EventHandler(this.ControllerControl_Load);
            this.HandleDestroyed += new System.EventHandler(this.ControllerControl_Close);
            ((System.ComponentModel.ISupportInitialize)(this.picArcadeStick)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblController;
        private System.Windows.Forms.PictureBox picArcadeStick;
        private System.Windows.Forms.Button btnSetup;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDPad;
        private System.Windows.Forms.Button btnAnalog;
        private System.Windows.Forms.CheckBox chkForceMapper;
        private System.Windows.Forms.Button btnSkip;
    }
}
