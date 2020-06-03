namespace nullDCNetplayLauncher
{
    partial class JoinControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JoinControl));
            this.btnGuess = new System.Windows.Forms.Button();
            this.txtHostPort = new System.Windows.Forms.TextBox();
            this.lblDelay = new System.Windows.Forms.Label();
            this.txtGuestIP = new System.Windows.Forms.TextBox();
            this.lblHostPort = new System.Windows.Forms.Label();
            this.lblGuestIP = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.txtHostIP = new System.Windows.Forms.TextBox();
            this.lblHostIP = new System.Windows.Forms.Label();
            this.grpCodeLaunch = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLaunchGame = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.txtHostCode = new System.Windows.Forms.TextBox();
            this.btnSavePreset = new System.Windows.Forms.Button();
            this.btnDeletePreset = new System.Windows.Forms.Button();
            this.cboPresetName = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.grpCodeLaunch.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGuess
            // 
            this.btnGuess.Location = new System.Drawing.Point(171, 233);
            this.btnGuess.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuess.Name = "btnGuess";
            this.btnGuess.Size = new System.Drawing.Size(75, 25);
            this.btnGuess.TabIndex = 5;
            this.btnGuess.Text = "Guess";
            this.btnGuess.UseVisualStyleBackColor = true;
            this.btnGuess.Click += new System.EventHandler(this.btnGuess_Click);
            // 
            // txtHostPort
            // 
            this.txtHostPort.Location = new System.Drawing.Point(120, 176);
            this.txtHostPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHostPort.Name = "txtHostPort";
            this.txtHostPort.Size = new System.Drawing.Size(124, 22);
            this.txtHostPort.TabIndex = 2;
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(44, 237);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(44, 17);
            this.lblDelay.TabIndex = 42;
            this.lblDelay.Text = "Delay";
            // 
            // txtGuestIP
            // 
            this.txtGuestIP.Location = new System.Drawing.Point(120, 204);
            this.txtGuestIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGuestIP.Name = "txtGuestIP";
            this.txtGuestIP.Size = new System.Drawing.Size(124, 22);
            this.txtGuestIP.TabIndex = 3;
            // 
            // lblHostPort
            // 
            this.lblHostPort.AutoSize = true;
            this.lblHostPort.Location = new System.Drawing.Point(44, 179);
            this.lblHostPort.Name = "lblHostPort";
            this.lblHostPort.Size = new System.Drawing.Size(67, 17);
            this.lblHostPort.TabIndex = 39;
            this.lblHostPort.Text = "Host Port";
            // 
            // lblGuestIP
            // 
            this.lblGuestIP.AutoSize = true;
            this.lblGuestIP.Location = new System.Drawing.Point(44, 209);
            this.lblGuestIP.Name = "lblGuestIP";
            this.lblGuestIP.Size = new System.Drawing.Size(62, 17);
            this.lblGuestIP.TabIndex = 41;
            this.lblGuestIP.Text = "Guest IP";
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(120, 233);
            this.numDelay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(44, 22);
            this.numDelay.TabIndex = 4;
            this.numDelay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtHostIP
            // 
            this.txtHostIP.Location = new System.Drawing.Point(120, 148);
            this.txtHostIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHostIP.Name = "txtHostIP";
            this.txtHostIP.Size = new System.Drawing.Size(124, 22);
            this.txtHostIP.TabIndex = 1;
            // 
            // lblHostIP
            // 
            this.lblHostIP.AutoSize = true;
            this.lblHostIP.Location = new System.Drawing.Point(44, 151);
            this.lblHostIP.Name = "lblHostIP";
            this.lblHostIP.Size = new System.Drawing.Size(53, 17);
            this.lblHostIP.TabIndex = 48;
            this.lblHostIP.Text = "Host IP";
            // 
            // grpCodeLaunch
            // 
            this.grpCodeLaunch.Controls.Add(this.label1);
            this.grpCodeLaunch.Controls.Add(this.btnLaunchGame);
            this.grpCodeLaunch.Controls.Add(this.btnPaste);
            this.grpCodeLaunch.Controls.Add(this.txtHostCode);
            this.grpCodeLaunch.Location = new System.Drawing.Point(5, 3);
            this.grpCodeLaunch.Name = "grpCodeLaunch";
            this.grpCodeLaunch.Size = new System.Drawing.Size(280, 104);
            this.grpCodeLaunch.TabIndex = 50;
            this.grpCodeLaunch.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 51;
            this.label1.Text = "Host Code";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnLaunchGame
            // 
            this.btnLaunchGame.Location = new System.Drawing.Point(6, 66);
            this.btnLaunchGame.Name = "btnLaunchGame";
            this.btnLaunchGame.Size = new System.Drawing.Size(268, 23);
            this.btnLaunchGame.TabIndex = 9;
            this.btnLaunchGame.Text = "Launch Game";
            this.btnLaunchGame.UseVisualStyleBackColor = true;
            this.btnLaunchGame.Click += new System.EventHandler(this.btnLaunchGame_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.AutoSize = true;
            this.btnPaste.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.Location = new System.Drawing.Point(247, 38);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(27, 22);
            this.btnPaste.TabIndex = 8;
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // txtHostCode
            // 
            this.txtHostCode.Location = new System.Drawing.Point(6, 38);
            this.txtHostCode.Name = "txtHostCode";
            this.txtHostCode.Size = new System.Drawing.Size(235, 22);
            this.txtHostCode.TabIndex = 7;
            this.txtHostCode.TextChanged += new System.EventHandler(this.txtHostCode_TextChanged);
            this.txtHostCode.GotFocus += new System.EventHandler(this.txtHostCode_GotFocus);
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.Image = ((System.Drawing.Image)(resources.GetObject("btnSavePreset.Image")));
            this.btnSavePreset.Location = new System.Drawing.Point(214, 111);
            this.btnSavePreset.Name = "btnSavePreset";
            this.btnSavePreset.Size = new System.Drawing.Size(30, 27);
            this.btnSavePreset.TabIndex = 53;
            this.btnSavePreset.UseVisualStyleBackColor = true;
            this.btnSavePreset.Click += new System.EventHandler(this.btnSavePreset_Click);
            // 
            // btnDeletePreset
            // 
            this.btnDeletePreset.Image = ((System.Drawing.Image)(resources.GetObject("btnDeletePreset.Image")));
            this.btnDeletePreset.Location = new System.Drawing.Point(184, 111);
            this.btnDeletePreset.Name = "btnDeletePreset";
            this.btnDeletePreset.Size = new System.Drawing.Size(30, 27);
            this.btnDeletePreset.TabIndex = 52;
            this.btnDeletePreset.UseVisualStyleBackColor = true;
            this.btnDeletePreset.Click += new System.EventHandler(this.btnDeletePreset_Click);
            // 
            // cboPresetName
            // 
            this.cboPresetName.FormattingEnabled = true;
            this.cboPresetName.Location = new System.Drawing.Point(47, 113);
            this.cboPresetName.Name = "cboPresetName";
            this.cboPresetName.Size = new System.Drawing.Size(135, 24);
            this.cboPresetName.TabIndex = 51;
            this.cboPresetName.SelectedIndexChanged += new System.EventHandler(this.cboPresetName_SelectedIndexChanged);
            this.cboPresetName.TextChanged += new System.EventHandler(this.cboPresetName_TextChanged);
            // 
            // JoinControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSavePreset);
            this.Controls.Add(this.btnDeletePreset);
            this.Controls.Add(this.cboPresetName);
            this.Controls.Add(this.grpCodeLaunch);
            this.Controls.Add(this.txtHostIP);
            this.Controls.Add(this.lblHostIP);
            this.Controls.Add(this.btnGuess);
            this.Controls.Add(this.txtHostPort);
            this.Controls.Add(this.lblDelay);
            this.Controls.Add(this.txtGuestIP);
            this.Controls.Add(this.lblHostPort);
            this.Controls.Add(this.lblGuestIP);
            this.Controls.Add(this.numDelay);
            this.Name = "JoinControl";
            this.Size = new System.Drawing.Size(290, 275);
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.grpCodeLaunch.ResumeLayout(false);
            this.grpCodeLaunch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuess;
        private System.Windows.Forms.TextBox txtHostPort;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.TextBox txtGuestIP;
        private System.Windows.Forms.Label lblHostPort;
        private System.Windows.Forms.Label lblGuestIP;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.TextBox txtHostIP;
        private System.Windows.Forms.Label lblHostIP;
        private System.Windows.Forms.GroupBox grpCodeLaunch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLaunchGame;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.TextBox txtHostCode;
        private System.Windows.Forms.Button btnSavePreset;
        private System.Windows.Forms.Button btnDeletePreset;
        private System.Windows.Forms.ComboBox cboPresetName;
    }
}
