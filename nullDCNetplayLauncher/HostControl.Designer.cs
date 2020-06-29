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
            this.btnExpandCollapse = new System.Windows.Forms.Button();
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
            this.lblRegion = new System.Windows.Forms.Label();
            this.cboRegion = new System.Windows.Forms.ComboBox();
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
            this.splitHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitHost.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitHost.IsSplitterFixed = true;
            this.splitHost.Location = new System.Drawing.Point(0, 0);
            this.splitHost.Margin = new System.Windows.Forms.Padding(2);
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
            // 
            // splitHost.Panel2
            // 
            this.splitHost.Panel2.Controls.Add(this.grpAdvanced);
            this.splitHost.Panel2Collapsed = true;
            this.splitHost.Size = new System.Drawing.Size(216, 310);
            this.splitHost.SplitterDistance = 162;
            this.splitHost.SplitterWidth = 1;
            this.splitHost.TabIndex = 72;
            // 
            // btnExpandCollapse
            // 
            this.btnExpandCollapse.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnExpandCollapse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExpandCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpandCollapse.Location = new System.Drawing.Point(-5, 129);
            this.btnExpandCollapse.Margin = new System.Windows.Forms.Padding(2);
            this.btnExpandCollapse.Name = "btnExpandCollapse";
            this.btnExpandCollapse.Size = new System.Drawing.Size(227, 22);
            this.btnExpandCollapse.TabIndex = 77;
            this.btnExpandCollapse.Text = "↑   Advanced Options   ↑";
            this.btnExpandCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExpandCollapse.UseVisualStyleBackColor = false;
            this.btnExpandCollapse.Click += new System.EventHandler(this.btnExpandCollapse_Click);
            // 
            // txtOpponentIP
            // 
            this.txtOpponentIP.Location = new System.Drawing.Point(88, 6);
            this.txtOpponentIP.Margin = new System.Windows.Forms.Padding(2);
            this.txtOpponentIP.Name = "txtOpponentIP";
            this.txtOpponentIP.Size = new System.Drawing.Size(118, 20);
            this.txtOpponentIP.TabIndex = 67;
            this.txtOpponentIP.TextChanged += new System.EventHandler(this.txtOpponentIP_TextChanged);
            // 
            // btnLaunchGame
            // 
            this.btnLaunchGame.Location = new System.Drawing.Point(6, 102);
            this.btnLaunchGame.Margin = new System.Windows.Forms.Padding(2);
            this.btnLaunchGame.Name = "btnLaunchGame";
            this.btnLaunchGame.Size = new System.Drawing.Size(200, 19);
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
            this.btnCopy.Location = new System.Drawing.Point(184, 76);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(2);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(22, 22);
            this.btnCopy.TabIndex = 70;
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtHostCode
            // 
            this.txtHostCode.Location = new System.Drawing.Point(6, 77);
            this.txtHostCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtHostCode.Name = "txtHostCode";
            this.txtHostCode.Size = new System.Drawing.Size(176, 20);
            this.txtHostCode.TabIndex = 69;
            this.txtHostCode.TextChanged += new System.EventHandler(this.txtHostCode_TextChanged);
            // 
            // lblVerifyDelay
            // 
            this.lblVerifyDelay.AutoSize = true;
            this.lblVerifyDelay.Location = new System.Drawing.Point(9, 30);
            this.lblVerifyDelay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVerifyDelay.Name = "lblVerifyDelay";
            this.lblVerifyDelay.Size = new System.Drawing.Size(63, 13);
            this.lblVerifyDelay.TabIndex = 67;
            this.lblVerifyDelay.Text = "Verify Delay";
            // 
            // lblHostnameIP
            // 
            this.lblHostnameIP.AutoSize = true;
            this.lblHostnameIP.Location = new System.Drawing.Point(9, 8);
            this.lblHostnameIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHostnameIP.Name = "lblHostnameIP";
            this.lblHostnameIP.Size = new System.Drawing.Size(67, 13);
            this.lblHostnameIP.TabIndex = 66;
            this.lblHostnameIP.Text = "Opponent IP";
            // 
            // btnGenHostCode
            // 
            this.btnGenHostCode.Location = new System.Drawing.Point(6, 53);
            this.btnGenHostCode.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenHostCode.Name = "btnGenHostCode";
            this.btnGenHostCode.Size = new System.Drawing.Size(200, 19);
            this.btnGenHostCode.TabIndex = 68;
            this.btnGenHostCode.Text = "Generate Host Code";
            this.btnGenHostCode.UseVisualStyleBackColor = true;
            this.btnGenHostCode.Click += new System.EventHandler(this.btnGenHostCode_Click);
            // 
            // btnGuess
            // 
            this.btnGuess.Location = new System.Drawing.Point(126, 27);
            this.btnGuess.Name = "btnGuess";
            this.btnGuess.Size = new System.Drawing.Size(80, 20);
            this.btnGuess.TabIndex = 74;
            this.btnGuess.Text = "Guess";
            this.btnGuess.UseVisualStyleBackColor = true;
            this.btnGuess.Click += new System.EventHandler(this.btnGuess_Click);
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(88, 28);
            this.numDelay.Margin = new System.Windows.Forms.Padding(2);
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(33, 20);
            this.numDelay.TabIndex = 73;
            this.numDelay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDelay.ValueChanged += new System.EventHandler(this.numDelay_ValueChanged);
            // 
            // grpAdvanced
            // 
            this.grpAdvanced.AutoSize = true;
            this.grpAdvanced.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpAdvanced.Controls.Add(this.cboRegion);
            this.grpAdvanced.Controls.Add(this.lblRegion);
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
            this.grpAdvanced.Margin = new System.Windows.Forms.Padding(2);
            this.grpAdvanced.Name = "grpAdvanced";
            this.grpAdvanced.Padding = new System.Windows.Forms.Padding(4);
            this.grpAdvanced.Size = new System.Drawing.Size(216, 147);
            this.grpAdvanced.TabIndex = 59;
            this.grpAdvanced.TabStop = false;
            this.grpAdvanced.Text = "Advanced Options";
            // 
            // cboMethod
            // 
            this.cboMethod.FormattingEnabled = true;
            this.cboMethod.Location = new System.Drawing.Point(83, 95);
            this.cboMethod.Margin = new System.Windows.Forms.Padding(2);
            this.cboMethod.Name = "cboMethod";
            this.cboMethod.Size = new System.Drawing.Size(105, 21);
            this.cboMethod.TabIndex = 71;
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(28, 98);
            this.lblMethod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(43, 13);
            this.lblMethod.TabIndex = 70;
            this.lblMethod.Text = "Method";
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.Image = ((System.Drawing.Image)(resources.GetObject("btnSavePreset.Image")));
            this.btnSavePreset.Location = new System.Drawing.Point(166, 24);
            this.btnSavePreset.Margin = new System.Windows.Forms.Padding(2);
            this.btnSavePreset.Name = "btnSavePreset";
            this.btnSavePreset.Size = new System.Drawing.Size(22, 22);
            this.btnSavePreset.TabIndex = 62;
            this.btnSavePreset.UseVisualStyleBackColor = true;
            this.btnSavePreset.Click += new System.EventHandler(this.btnSavePreset_Click);
            // 
            // btnDeletePreset
            // 
            this.btnDeletePreset.Image = ((System.Drawing.Image)(resources.GetObject("btnDeletePreset.Image")));
            this.btnDeletePreset.Location = new System.Drawing.Point(144, 24);
            this.btnDeletePreset.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeletePreset.Name = "btnDeletePreset";
            this.btnDeletePreset.Size = new System.Drawing.Size(22, 22);
            this.btnDeletePreset.TabIndex = 60;
            this.btnDeletePreset.UseVisualStyleBackColor = true;
            this.btnDeletePreset.Click += new System.EventHandler(this.btnDeletePreset_Click);
            // 
            // cboPresetName
            // 
            this.cboPresetName.FormattingEnabled = true;
            this.cboPresetName.Location = new System.Drawing.Point(31, 26);
            this.cboPresetName.Margin = new System.Windows.Forms.Padding(2);
            this.cboPresetName.Name = "cboPresetName";
            this.cboPresetName.Size = new System.Drawing.Size(108, 21);
            this.cboPresetName.TabIndex = 58;
            this.cboPresetName.SelectedIndexChanged += new System.EventHandler(this.cboPresetName_SelectedIndexChanged);
            // 
            // cboHostIP
            // 
            this.cboHostIP.FormattingEnabled = true;
            this.cboHostIP.Location = new System.Drawing.Point(83, 54);
            this.cboHostIP.Margin = new System.Windows.Forms.Padding(2);
            this.cboHostIP.Name = "cboHostIP";
            this.cboHostIP.Size = new System.Drawing.Size(105, 21);
            this.cboHostIP.TabIndex = 2;
            this.cboHostIP.SelectedIndexChanged += new System.EventHandler(this.cboHostIP_SelectedIndexChanged);
            // 
            // lblHostIP
            // 
            this.lblHostIP.AutoSize = true;
            this.lblHostIP.Location = new System.Drawing.Point(27, 57);
            this.lblHostIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHostIP.Name = "lblHostIP";
            this.lblHostIP.Size = new System.Drawing.Size(42, 13);
            this.lblHostIP.TabIndex = 69;
            this.lblHostIP.Text = "Host IP";
            // 
            // txtHostPort
            // 
            this.txtHostPort.Location = new System.Drawing.Point(83, 75);
            this.txtHostPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtHostPort.Name = "txtHostPort";
            this.txtHostPort.Size = new System.Drawing.Size(105, 20);
            this.txtHostPort.TabIndex = 61;
            this.txtHostPort.Text = "27886";
            // 
            // lblHostPort
            // 
            this.lblHostPort.AutoSize = true;
            this.lblHostPort.Location = new System.Drawing.Point(28, 78);
            this.lblHostPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHostPort.Name = "lblHostPort";
            this.lblHostPort.Size = new System.Drawing.Size(51, 13);
            this.lblHostPort.TabIndex = 66;
            this.lblHostPort.Text = "Host Port";
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(28, 119);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(41, 13);
            this.lblRegion.TabIndex = 73;
            this.lblRegion.Text = "Region";
            // 
            // cboRegion
            // 
            this.cboRegion.FormattingEnabled = true;
            this.cboRegion.Location = new System.Drawing.Point(83, 116);
            this.cboRegion.Name = "cboRegion";
            this.cboRegion.Size = new System.Drawing.Size(105, 21);
            this.cboRegion.TabIndex = 74;
            this.cboRegion.SelectedIndexChanged += new System.EventHandler(this.cboRegion_SelectedIndexChanged);
            // 
            // HostControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.splitHost);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(216, 500);
            this.MinimumSize = new System.Drawing.Size(216, 310);
            this.Name = "HostControl";
            this.Size = new System.Drawing.Size(216, 310);
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
        private System.Windows.Forms.ComboBox cboRegion;
        private System.Windows.Forms.Label lblRegion;
    }
}
