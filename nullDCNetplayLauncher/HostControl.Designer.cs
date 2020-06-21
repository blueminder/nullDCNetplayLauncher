namespace nullDCNetplayLauncher
{
    partial class HostControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostControl));
            this.splitHost = new System.Windows.Forms.SplitContainer();
            this.txtOpponentIP = new System.Windows.Forms.TextBox();
            this.btnLaunchGame = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txtHostCode = new System.Windows.Forms.TextBox();
            this.lblVerifyDelay = new System.Windows.Forms.Label();
            this.lblHostnameIP = new System.Windows.Forms.Label();
            this.btnGenHostCode = new System.Windows.Forms.Button();
            this.btnGuess = new System.Windows.Forms.Button();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.grpAdvanced = new System.Windows.Forms.GroupBox();
            this.cboMethod = new System.Windows.Forms.ComboBox();
            this.lblMethod = new System.Windows.Forms.Label();
            this.btnSavePreset = new System.Windows.Forms.Button();
            this.btnDeletePreset = new System.Windows.Forms.Button();
            this.cboPresetName = new System.Windows.Forms.ComboBox();
            this.cboHostIP = new System.Windows.Forms.ComboBox();
            this.lblHostIP = new System.Windows.Forms.Label();
            this.txtHostPort = new System.Windows.Forms.TextBox();
            this.lblHostPort = new System.Windows.Forms.Label();
            this.btnExpandCollapse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitHost)).BeginInit();
            this.splitHost.Panel1.SuspendLayout();
            this.splitHost.Panel2.SuspendLayout();
            this.splitHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.grpAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitHost
            // 
            this.splitHost.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.splitHost.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitHost.Location = new System.Drawing.Point(9, 6);
            this.splitHost.Name = "splitHost";
            this.splitHost.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitHost.Panel1
            // 
            this.splitHost.Panel1.Controls.Add(this.btnExpandCollapse);
            this.splitHost.Panel1.Controls.Add(this.txtOpponentIP);
            this.splitHost.Panel1.Controls.Add(this.btnLaunchGame);
            this.splitHost.Panel1.Controls.Add(this.btnCopy);
            this.splitHost.Panel1.Controls.Add(this.txtHostCode);
            this.splitHost.Panel1.Controls.Add(this.lblVerifyDelay);
            this.splitHost.Panel1.Controls.Add(this.lblHostnameIP);
            this.splitHost.Panel1.Controls.Add(this.btnGenHostCode);
            this.splitHost.Panel1.Controls.Add(this.btnGuess);
            this.splitHost.Panel1.Controls.Add(this.numDelay);
            this.splitHost.Panel1MinSize = 185;
            // 
            // splitHost.Panel2
            // 
            this.splitHost.Panel2.Controls.Add(this.grpAdvanced);
            this.splitHost.Panel2MinSize = 0;
            this.splitHost.Size = new System.Drawing.Size(275, 360);
            this.splitHost.SplitterDistance = 185;
            this.splitHost.SplitterWidth = 1;
            this.splitHost.TabIndex = 72;
            // 
            // txtOpponentIP
            // 
            this.txtOpponentIP.Location = new System.Drawing.Point(114, 7);
            this.txtOpponentIP.Name = "txtOpponentIP";
            this.txtOpponentIP.Size = new System.Drawing.Size(158, 22);
            this.txtOpponentIP.TabIndex = 72;
            // 
            // btnLaunchGame
            // 
            this.btnLaunchGame.Location = new System.Drawing.Point(5, 126);
            this.btnLaunchGame.Name = "btnLaunchGame";
            this.btnLaunchGame.Size = new System.Drawing.Size(267, 23);
            this.btnLaunchGame.TabIndex = 71;
            this.btnLaunchGame.Text = "Launch Game";
            this.btnLaunchGame.UseVisualStyleBackColor = true;
            this.btnLaunchGame.Click += new System.EventHandler(this.btnLaunchGame_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.AutoSize = true;
            this.btnCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.Location = new System.Drawing.Point(245, 96);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(27, 22);
            this.btnCopy.TabIndex = 70;
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtHostCode
            // 
            this.txtHostCode.Location = new System.Drawing.Point(5, 96);
            this.txtHostCode.Name = "txtHostCode";
            this.txtHostCode.Size = new System.Drawing.Size(234, 22);
            this.txtHostCode.TabIndex = 69;
            // 
            // lblVerifyDelay
            // 
            this.lblVerifyDelay.AutoSize = true;
            this.lblVerifyDelay.Location = new System.Drawing.Point(9, 37);
            this.lblVerifyDelay.Name = "lblVerifyDelay";
            this.lblVerifyDelay.Size = new System.Drawing.Size(84, 17);
            this.lblVerifyDelay.TabIndex = 67;
            this.lblVerifyDelay.Text = "Verify Delay";
            // 
            // lblHostnameIP
            // 
            this.lblHostnameIP.AutoSize = true;
            this.lblHostnameIP.Location = new System.Drawing.Point(9, 10);
            this.lblHostnameIP.Name = "lblHostnameIP";
            this.lblHostnameIP.Size = new System.Drawing.Size(87, 17);
            this.lblHostnameIP.TabIndex = 66;
            this.lblHostnameIP.Text = "Opponent IP";
            // 
            // btnGenHostCode
            // 
            this.btnGenHostCode.Location = new System.Drawing.Point(5, 65);
            this.btnGenHostCode.Name = "btnGenHostCode";
            this.btnGenHostCode.Size = new System.Drawing.Size(267, 23);
            this.btnGenHostCode.TabIndex = 68;
            this.btnGenHostCode.Text = "Generate Host Code";
            this.btnGenHostCode.UseVisualStyleBackColor = true;
            this.btnGenHostCode.Click += new System.EventHandler(this.btnGenHostCode_Click);
            // 
            // btnGuess
            // 
            this.btnGuess.Location = new System.Drawing.Point(165, 33);
            this.btnGuess.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuess.Name = "btnGuess";
            this.btnGuess.Size = new System.Drawing.Size(107, 25);
            this.btnGuess.TabIndex = 74;
            this.btnGuess.Text = "Guess";
            this.btnGuess.UseVisualStyleBackColor = true;
            this.btnGuess.Click += new System.EventHandler(this.btnGuess_Click);
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(114, 35);
            this.numDelay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(44, 22);
            this.numDelay.TabIndex = 73;
            this.numDelay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // grpAdvanced
            // 
            this.grpAdvanced.AutoSize = true;
            this.grpAdvanced.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpAdvanced.Controls.Add(this.cboMethod);
            this.grpAdvanced.Controls.Add(this.lblMethod);
            this.grpAdvanced.Controls.Add(this.btnSavePreset);
            this.grpAdvanced.Controls.Add(this.btnDeletePreset);
            this.grpAdvanced.Controls.Add(this.cboPresetName);
            this.grpAdvanced.Controls.Add(this.cboHostIP);
            this.grpAdvanced.Controls.Add(this.lblHostIP);
            this.grpAdvanced.Controls.Add(this.txtHostPort);
            this.grpAdvanced.Controls.Add(this.lblHostPort);
            this.grpAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAdvanced.Location = new System.Drawing.Point(0, 0);
            this.grpAdvanced.Name = "grpAdvanced";
            this.grpAdvanced.Padding = new System.Windows.Forms.Padding(5);
            this.grpAdvanced.Size = new System.Drawing.Size(275, 174);
            this.grpAdvanced.TabIndex = 59;
            this.grpAdvanced.TabStop = false;
            this.grpAdvanced.Text = "Advanced Options";
            // 
            // cboMethod
            // 
            this.cboMethod.FormattingEnabled = true;
            this.cboMethod.Location = new System.Drawing.Point(111, 118);
            this.cboMethod.Name = "cboMethod";
            this.cboMethod.Size = new System.Drawing.Size(126, 24);
            this.cboMethod.TabIndex = 71;
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(37, 123);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(55, 17);
            this.lblMethod.TabIndex = 70;
            this.lblMethod.Text = "Method";
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.Image = ((System.Drawing.Image)(resources.GetObject("btnSavePreset.Image")));
            this.btnSavePreset.Location = new System.Drawing.Point(207, 30);
            this.btnSavePreset.Name = "btnSavePreset";
            this.btnSavePreset.Size = new System.Drawing.Size(30, 27);
            this.btnSavePreset.TabIndex = 62;
            this.btnSavePreset.UseVisualStyleBackColor = true;
            this.btnSavePreset.Click += new System.EventHandler(this.btnSavePreset_Click);
            // 
            // btnDeletePreset
            // 
            this.btnDeletePreset.Image = ((System.Drawing.Image)(resources.GetObject("btnDeletePreset.Image")));
            this.btnDeletePreset.Location = new System.Drawing.Point(179, 30);
            this.btnDeletePreset.Name = "btnDeletePreset";
            this.btnDeletePreset.Size = new System.Drawing.Size(30, 27);
            this.btnDeletePreset.TabIndex = 60;
            this.btnDeletePreset.UseVisualStyleBackColor = true;
            this.btnDeletePreset.Click += new System.EventHandler(this.btnDeletePreset_Click);
            // 
            // cboPresetName
            // 
            this.cboPresetName.FormattingEnabled = true;
            this.cboPresetName.Location = new System.Drawing.Point(38, 32);
            this.cboPresetName.Name = "cboPresetName";
            this.cboPresetName.Size = new System.Drawing.Size(135, 24);
            this.cboPresetName.TabIndex = 58;
            this.cboPresetName.SelectedIndexChanged += new System.EventHandler(this.cboPresetName_SelectedIndexChanged);
            // 
            // cboHostIP
            // 
            this.cboHostIP.FormattingEnabled = true;
            this.cboHostIP.Location = new System.Drawing.Point(111, 66);
            this.cboHostIP.Name = "cboHostIP";
            this.cboHostIP.Size = new System.Drawing.Size(126, 24);
            this.cboHostIP.TabIndex = 2;
            // 
            // lblHostIP
            // 
            this.lblHostIP.AutoSize = true;
            this.lblHostIP.Location = new System.Drawing.Point(37, 71);
            this.lblHostIP.Name = "lblHostIP";
            this.lblHostIP.Size = new System.Drawing.Size(53, 17);
            this.lblHostIP.TabIndex = 69;
            this.lblHostIP.Text = "Host IP";
            // 
            // txtHostPort
            // 
            this.txtHostPort.Location = new System.Drawing.Point(111, 93);
            this.txtHostPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHostPort.Name = "txtHostPort";
            this.txtHostPort.Size = new System.Drawing.Size(126, 22);
            this.txtHostPort.TabIndex = 61;
            this.txtHostPort.Text = "27886";
            // 
            // lblHostPort
            // 
            this.lblHostPort.AutoSize = true;
            this.lblHostPort.Location = new System.Drawing.Point(37, 98);
            this.lblHostPort.Name = "lblHostPort";
            this.lblHostPort.Size = new System.Drawing.Size(67, 17);
            this.lblHostPort.TabIndex = 66;
            this.lblHostPort.Text = "Host Port";
            // 
            // btnExpandCollapse
            // 
            this.btnExpandCollapse.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExpandCollapse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExpandCollapse.Location = new System.Drawing.Point(-9, 155);
            this.btnExpandCollapse.Name = "btnExpandCollapse";
            this.btnExpandCollapse.Size = new System.Drawing.Size(293, 25);
            this.btnExpandCollapse.TabIndex = 77;
            this.btnExpandCollapse.Text = "▲                    ▲";
            this.btnExpandCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExpandCollapse.UseVisualStyleBackColor = false;
            this.btnExpandCollapse.Click += new System.EventHandler(this.btnExpandCollapse_Click);
            // 
            // HostControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.splitHost);
            this.Name = "HostControl";
            this.Size = new System.Drawing.Size(293, 371);
            this.Load += new System.EventHandler(this.HostControl_Load);
            this.splitHost.Panel1.ResumeLayout(false);
            this.splitHost.Panel1.PerformLayout();
            this.splitHost.Panel2.ResumeLayout(false);
            this.splitHost.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitHost)).EndInit();
            this.splitHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.grpAdvanced.ResumeLayout(false);
            this.grpAdvanced.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitHost;
        private System.Windows.Forms.TextBox txtOpponentIP;
        private System.Windows.Forms.Button btnLaunchGame;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtHostCode;
        private System.Windows.Forms.Label lblVerifyDelay;
        private System.Windows.Forms.Label lblHostnameIP;
        private System.Windows.Forms.Button btnGenHostCode;
        private System.Windows.Forms.Button btnGuess;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.GroupBox grpAdvanced;
        private System.Windows.Forms.ComboBox cboMethod;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.Button btnSavePreset;
        private System.Windows.Forms.Button btnDeletePreset;
        private System.Windows.Forms.ComboBox cboPresetName;
        private System.Windows.Forms.ComboBox cboHostIP;
        private System.Windows.Forms.Label lblHostIP;
        private System.Windows.Forms.TextBox txtHostPort;
        private System.Windows.Forms.Label lblHostPort;
        private System.Windows.Forms.Button btnExpandCollapse;
    }
}
