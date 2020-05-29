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
            this.btnGuess = new System.Windows.Forms.Button();
            this.txtHostPort = new System.Windows.Forms.TextBox();
            this.lblDelay = new System.Windows.Forms.Label();
            this.txtGuestIP = new System.Windows.Forms.TextBox();
            this.lblHostPort = new System.Windows.Forms.Label();
            this.lblGuestIP = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.txtHostIP = new System.Windows.Forms.TextBox();
            this.lblHostIP = new System.Windows.Forms.Label();
            this.btnGenHostCode = new System.Windows.Forms.Button();
            this.grpCodeLaunch = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLaunchGame = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txtHostCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.grpCodeLaunch.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGuess
            // 
            this.btnGuess.Location = new System.Drawing.Point(171, 98);
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
            this.txtHostPort.Location = new System.Drawing.Point(120, 41);
            this.txtHostPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHostPort.Name = "txtHostPort";
            this.txtHostPort.Size = new System.Drawing.Size(124, 22);
            this.txtHostPort.TabIndex = 2;
            this.txtHostPort.Text = "27886";
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(44, 102);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(44, 17);
            this.lblDelay.TabIndex = 42;
            this.lblDelay.Text = "Delay";
            // 
            // txtGuestIP
            // 
            this.txtGuestIP.Location = new System.Drawing.Point(120, 69);
            this.txtGuestIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGuestIP.Name = "txtGuestIP";
            this.txtGuestIP.Size = new System.Drawing.Size(124, 22);
            this.txtGuestIP.TabIndex = 3;
            this.txtGuestIP.Text = "0.0.0.0";
            // 
            // lblHostPort
            // 
            this.lblHostPort.AutoSize = true;
            this.lblHostPort.Location = new System.Drawing.Point(44, 44);
            this.lblHostPort.Name = "lblHostPort";
            this.lblHostPort.Size = new System.Drawing.Size(67, 17);
            this.lblHostPort.TabIndex = 39;
            this.lblHostPort.Text = "Host Port";
            // 
            // lblGuestIP
            // 
            this.lblGuestIP.AutoSize = true;
            this.lblGuestIP.Location = new System.Drawing.Point(44, 74);
            this.lblGuestIP.Name = "lblGuestIP";
            this.lblGuestIP.Size = new System.Drawing.Size(62, 17);
            this.lblGuestIP.TabIndex = 41;
            this.lblGuestIP.Text = "Guest IP";
            // 
            // numDelay
            // 
            this.numDelay.Location = new System.Drawing.Point(120, 98);
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
            this.txtHostIP.Location = new System.Drawing.Point(120, 13);
            this.txtHostIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHostIP.Name = "txtHostIP";
            this.txtHostIP.Size = new System.Drawing.Size(124, 22);
            this.txtHostIP.TabIndex = 1;
            this.txtHostIP.Text = "0.0.0.0";
            // 
            // lblHostIP
            // 
            this.lblHostIP.AutoSize = true;
            this.lblHostIP.Location = new System.Drawing.Point(44, 16);
            this.lblHostIP.Name = "lblHostIP";
            this.lblHostIP.Size = new System.Drawing.Size(53, 17);
            this.lblHostIP.TabIndex = 48;
            this.lblHostIP.Text = "Host IP";
            // 
            // btnGenHostCode
            // 
            this.btnGenHostCode.Location = new System.Drawing.Point(10, 131);
            this.btnGenHostCode.Name = "btnGenHostCode";
            this.btnGenHostCode.Size = new System.Drawing.Size(273, 23);
            this.btnGenHostCode.TabIndex = 6;
            this.btnGenHostCode.Text = "Generate Host Code";
            this.btnGenHostCode.UseVisualStyleBackColor = true;
            this.btnGenHostCode.Click += new System.EventHandler(this.btnGenHostCode_Click);
            // 
            // grpCodeLaunch
            // 
            this.grpCodeLaunch.Controls.Add(this.label1);
            this.grpCodeLaunch.Controls.Add(this.btnLaunchGame);
            this.grpCodeLaunch.Controls.Add(this.btnCopy);
            this.grpCodeLaunch.Controls.Add(this.txtHostCode);
            this.grpCodeLaunch.Location = new System.Drawing.Point(5, 160);
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
            // btnCopy
            // 
            this.btnCopy.AutoSize = true;
            this.btnCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.Location = new System.Drawing.Point(247, 38);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(27, 22);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtHostCode
            // 
            this.txtHostCode.Location = new System.Drawing.Point(6, 38);
            this.txtHostCode.Name = "txtHostCode";
            this.txtHostCode.Size = new System.Drawing.Size(235, 22);
            this.txtHostCode.TabIndex = 7;
            // 
            // HostControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpCodeLaunch);
            this.Controls.Add(this.btnGenHostCode);
            this.Controls.Add(this.txtHostIP);
            this.Controls.Add(this.lblHostIP);
            this.Controls.Add(this.btnGuess);
            this.Controls.Add(this.txtHostPort);
            this.Controls.Add(this.lblDelay);
            this.Controls.Add(this.txtGuestIP);
            this.Controls.Add(this.lblHostPort);
            this.Controls.Add(this.lblGuestIP);
            this.Controls.Add(this.numDelay);
            this.Name = "HostControl";
            this.Size = new System.Drawing.Size(290, 271);
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
        private System.Windows.Forms.Button btnGenHostCode;
        private System.Windows.Forms.GroupBox grpCodeLaunch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLaunchGame;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtHostCode;
    }
}
