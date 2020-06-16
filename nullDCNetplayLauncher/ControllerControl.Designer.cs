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
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnDetectController = new System.Windows.Forms.Button();
            this.btnShowKeyboard = new System.Windows.Forms.Button();
            this.btnDPad = new System.Windows.Forms.Button();
            this.btnAnalog = new System.Windows.Forms.Button();
            this.btnEnableGamepadMapper = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picArcadeStick)).BeginInit();
            this.SuspendLayout();
            // 
            // lblController
            // 
            this.lblController.Location = new System.Drawing.Point(29, 231);
            this.lblController.Name = "lblController";
            this.lblController.Size = new System.Drawing.Size(381, 92);
            this.lblController.TabIndex = 0;
            this.lblController.Text = "Plug in your controller to continue.";
            this.lblController.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picArcadeStick
            // 
            this.picArcadeStick.Image = global::nullDCNetplayLauncher.Properties.Resources.base_full;
            this.picArcadeStick.Location = new System.Drawing.Point(29, 12);
            this.picArcadeStick.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picArcadeStick.Name = "picArcadeStick";
            this.picArcadeStick.Size = new System.Drawing.Size(381, 217);
            this.picArcadeStick.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picArcadeStick.TabIndex = 1;
            this.picArcadeStick.TabStop = false;
            // 
            // btnSetup
            // 
            this.btnSetup.AutoSize = true;
            this.btnSetup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSetup.Location = new System.Drawing.Point(29, 324);
            this.btnSetup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(55, 27);
            this.btnSetup.TabIndex = 2;
            this.btnSetup.Text = "Setup";
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Location = new System.Drawing.Point(349, 324);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(61, 27);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSkip
            // 
            this.btnSkip.AutoSize = true;
            this.btnSkip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSkip.Location = new System.Drawing.Point(197, 325);
            this.btnSkip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(45, 27);
            this.btnSkip.TabIndex = 4;
            this.btnSkip.Text = "Skip";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // btnDetectController
            // 
            this.btnDetectController.AutoSize = true;
            this.btnDetectController.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDetectController.Location = new System.Drawing.Point(156, 263);
            this.btnDetectController.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDetectController.Name = "btnDetectController";
            this.btnDetectController.Size = new System.Drawing.Size(124, 27);
            this.btnDetectController.TabIndex = 6;
            this.btnDetectController.Text = "Detect Controller";
            this.btnDetectController.UseVisualStyleBackColor = true;
            this.btnDetectController.Visible = false;
            this.btnDetectController.Click += new System.EventHandler(this.btnDetectController_Click);
            // 
            // btnShowKeyboard
            // 
            this.btnShowKeyboard.AutoSize = true;
            this.btnShowKeyboard.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnShowKeyboard.Location = new System.Drawing.Point(129, 326);
            this.btnShowKeyboard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowKeyboard.Name = "btnShowKeyboard";
            this.btnShowKeyboard.Size = new System.Drawing.Size(175, 27);
            this.btnShowKeyboard.TabIndex = 7;
            this.btnShowKeyboard.Text = "Show Keyboard Mapping";
            this.btnShowKeyboard.UseVisualStyleBackColor = true;
            this.btnShowKeyboard.Visible = false;
            this.btnShowKeyboard.Click += new System.EventHandler(this.btnShowKeyboard_Click);
            // 
            // btnDPad
            // 
            this.btnDPad.AutoSize = true;
            this.btnDPad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDPad.Location = new System.Drawing.Point(87, 324);
            this.btnDPad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDPad.Name = "btnDPad";
            this.btnDPad.Size = new System.Drawing.Size(57, 27);
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
            this.btnAnalog.Location = new System.Drawing.Point(287, 324);
            this.btnAnalog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAnalog.Name = "btnAnalog";
            this.btnAnalog.Size = new System.Drawing.Size(62, 27);
            this.btnAnalog.TabIndex = 9;
            this.btnAnalog.Text = "Analog";
            this.btnAnalog.UseVisualStyleBackColor = true;
            this.btnAnalog.Visible = false;
            this.btnAnalog.Click += new System.EventHandler(this.btnAnalog_Click);
            // 
            // btnEnableGamepadMapper
            // 
            this.btnEnableGamepadMapper.AutoSize = true;
            this.btnEnableGamepadMapper.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEnableGamepadMapper.Location = new System.Drawing.Point(128, 295);
            this.btnEnableGamepadMapper.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEnableGamepadMapper.Name = "btnEnableGamepadMapper";
            this.btnEnableGamepadMapper.Size = new System.Drawing.Size(180, 27);
            this.btnEnableGamepadMapper.TabIndex = 10;
            this.btnEnableGamepadMapper.Text = "Enable Gamepad Mapper";
            this.btnEnableGamepadMapper.UseVisualStyleBackColor = true;
            this.btnEnableGamepadMapper.Visible = false;
            this.btnEnableGamepadMapper.Click += new System.EventHandler(this.btnEnableGamepadMapper_Click);
            // 
            // ControllerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAnalog);
            this.Controls.Add(this.btnDPad);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSetup);
            this.Controls.Add(this.picArcadeStick);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.btnShowKeyboard);
            this.Controls.Add(this.btnEnableGamepadMapper);
            this.Controls.Add(this.btnDetectController);
            this.Controls.Add(this.lblController);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ControllerControl";
            this.Size = new System.Drawing.Size(441, 391);
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
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnDetectController;
        private System.Windows.Forms.Button btnShowKeyboard;
        private System.Windows.Forms.Button btnDPad;
        private System.Windows.Forms.Button btnAnalog;
        private System.Windows.Forms.Button btnEnableGamepadMapper;
    }
}
