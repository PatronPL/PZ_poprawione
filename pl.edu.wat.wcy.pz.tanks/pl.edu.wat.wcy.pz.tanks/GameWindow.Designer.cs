namespace pl.edu.wat.wcy.pz.tanks
{
    partial class GameWindow
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.exit = new System.Windows.Forms.Button();
            this.buttonClient = new System.Windows.Forms.Button();
            this.buttonHost = new System.Windows.Forms.Button();
            this.socketButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.timerHost = new System.Windows.Forms.Timer(this.components);
            this.timerSend = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.exit.Location = new System.Drawing.Point(117, 103);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(75, 23);
            this.exit.TabIndex = 11;
            this.exit.Text = "button1";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // buttonClient
            // 
            this.buttonClient.Enabled = false;
            this.buttonClient.Location = new System.Drawing.Point(222, 323);
            this.buttonClient.Name = "buttonClient";
            this.buttonClient.Size = new System.Drawing.Size(90, 30);
            this.buttonClient.TabIndex = 10;
            this.buttonClient.Text = "buttonClient";
            this.buttonClient.UseVisualStyleBackColor = true;
            this.buttonClient.Visible = false;
            this.buttonClient.Click += new System.EventHandler(this.buttonClient_Click);
            // 
            // buttonHost
            // 
            this.buttonHost.Enabled = false;
            this.buttonHost.Location = new System.Drawing.Point(126, 323);
            this.buttonHost.Name = "buttonHost";
            this.buttonHost.Size = new System.Drawing.Size(90, 30);
            this.buttonHost.TabIndex = 9;
            this.buttonHost.Text = "buttonHost";
            this.buttonHost.UseVisualStyleBackColor = true;
            this.buttonHost.Visible = false;
            this.buttonHost.Click += new System.EventHandler(this.buttonHost_Click);
            // 
            // socketButton
            // 
            this.socketButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.socketButton.Location = new System.Drawing.Point(365, 159);
            this.socketButton.Name = "socketButton";
            this.socketButton.Size = new System.Drawing.Size(112, 74);
            this.socketButton.TabIndex = 8;
            this.socketButton.Text = "Start Online";
            this.socketButton.UseVisualStyleBackColor = false;
            this.socketButton.Click += new System.EventHandler(this.socketButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.loadButton.Location = new System.Drawing.Point(365, 103);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(150, 50);
            this.loadButton.TabIndex = 7;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = false;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.startButton.Location = new System.Drawing.Point(209, 103);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(150, 100);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start Local";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 16;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // timerHost
            // 
            this.timerHost.Interval = 16;
            this.timerHost.Tag = "Host";
            this.timerHost.Tick += new System.EventHandler(this.timerHost_Tick);
            // 
            // timerSend
            // 
            this.timerSend.Interval = 16;
            this.timerSend.Tick += new System.EventHandler(this.timerSend_Tick);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 507);
            this.ControlBox = false;
            this.Controls.Add(this.exit);
            this.Controls.Add(this.buttonClient);
            this.Controls.Add(this.buttonHost);
            this.Controls.Add(this.socketButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.startButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(625, 545);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(625, 545);
            this.Name = "GameWindow";
            this.Text = "Tanks";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown_Listner);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp_Listner);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exit;
        public System.Windows.Forms.Button buttonClient;
        public System.Windows.Forms.Button buttonHost;
        private System.Windows.Forms.Button socketButton;
        private System.Windows.Forms.Button loadButton;
        public System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer timerHost;
        private System.Windows.Forms.Timer timerSend;
    }
}

