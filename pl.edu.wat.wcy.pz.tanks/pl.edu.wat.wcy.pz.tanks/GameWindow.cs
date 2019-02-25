using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace pl.edu.wat.wcy.pz.tanks
{
    public partial class GameWindow : Form
    {
        static public Label ipLabel = new Label();
        static public TextBox secondIP = new TextBox();
        static public bool onlinehost = false;
        static public bool online = false;
        static bool timersOff = false;
        static public bool disconect;
        static public byte counter = 1;

        public static Panel panel1 = new Panel();
        public static List<PictureBox> listWall = new List<PictureBox>();
        public static List<PictureBox> listSwamp = new List<PictureBox>();
        public static List<PictureBox> listBonus = new List<PictureBox>();
        public static PictureBox tank1Pic = new PictureBox();
        public static PictureBox tank2Pic = new PictureBox();
        public static ProgressBar lifeT1 = new ProgressBar();
        public static ProgressBar lifeT2 = new ProgressBar();
        public static Bullet bullet1;
        public static Bullet bullet2;
        public static Tank tank1;
        public static Tank tank2;
        public static byte bonusLocation;
        

        static public EventsClass allEvents = new EventsClass();
        
        

        public GameWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialise.SetLocationAndSize(ref startButton, new Point(200, 200), new Size(150, 70));
            startButton.Text = "Start Local";
            Initialise.SetVisibilityEnable(ref startButton, true);

            Initialise.SetLocationAndSize(ref socketButton, new Point(200, 300), new Size(150, 70));
            socketButton.Text = "Start Online";
            Initialise.SetVisibilityEnable(ref socketButton, true);

            Initialise.SetLocationAndSize(ref loadButton, new Point(200, 400), new Size(150, 50));
            loadButton.Text = "Load";
            Initialise.SetVisibilityEnable(ref loadButton, false);

            Initialise.SetLocationAndSize(ref exit, new Point(200, 450), new Size(150, 50));
            exit.Text = "Exit";
            Initialise.SetVisibilityEnable(ref exit, true);
        }

        private void StartGame()
        {
            if(!online)
            MessageBox.Show("Controls: \n-Player one: Arrows + Space  \n-Player two: WSAD + F   \n-Addition: Escape");
            tank1Pic.Location = new Point(250, 400);
            tank2Pic.Location = new Point(250, 56);
            panel1.Location = new Point(0, 0);
            panel1.Size = new Size(500, 500);
            panel1.BackColor = Color.FromArgb(111, 99, 91);
            this.Controls.Add(panel1);

            Initialise.GenerateMap(ref panel1, ref listWall, ref listSwamp, ref listBonus);
            Bitmap background = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(background, panel1.DisplayRectangle);
            panel1.BackgroundImage = background;

            Initialise.CreateTanks(ref tank1, ref tank2, ref tank1Pic, ref tank2Pic);
            Controls.Add(GameWindow.lifeT1);
            Controls.Add(GameWindow.lifeT2);
            allEvents.gameEvent.Attach(tank1);
            allEvents.gameEvent.Attach(tank2);
            gameTimer.Enabled = true;
        }


        private void LoadGame(Point tank1p, Point tank2p)
        {
            tank1Pic.Location = tank1p;
            tank2Pic.Location = tank2p;
            StartGame();
        }






        private void startButton_Click(object sender, EventArgs e)
        {
            Initialise.SetVisibilityEnable(ref startButton, false);
            Initialise.SetVisibilityEnable(ref loadButton, false);
            Initialise.SetVisibilityEnable(ref socketButton, false);
            Initialise.SetVisibilityEnable(ref exit, false);
            if (online == true)
            {
                timerHost.Enabled = true;
                timerSend.Enabled = true;
            }
            disconect = false;
            StartGame();
        }

        private void socketButton_Click(object sender, EventArgs e)
        {
            Initialise.SetVisibilityEnable(ref startButton, false);
            Initialise.SetVisibilityEnable(ref loadButton, false);
            Initialise.SetVisibilityEnable(ref socketButton, false);
            Initialise.SetVisibilityEnable(ref exit, false);
            buttonClient.Text = "Start Client";
            buttonHost.Text = "Start Host";
            Initialise.SetVisibilityEnable(ref buttonHost, true);
            Initialise.SetVisibilityEnable(ref buttonClient, true);
            online = true;


            ipLabel.Text = "Yours DNS: " + SocketConnect.serverIp;
            //iphost.MapToIPv4().ToString() +
            ipLabel.Location = new Point(200, 100);
            ipLabel.Size = new Size(200, 50);
            ipLabel.Enabled = true;
            ipLabel.Visible = true;
            this.Controls.Add(ipLabel);

            secondIP.Location = new Point(200, 150);
            ipLabel.Size = new Size(200, 50);
            secondIP.Text = "Insert DNS";
            secondIP.Enabled = true;
            secondIP.Visible = true;
            this.Controls.Add(secondIP);

        }
        private void loadButton_Click(object sender, EventArgs e)
        {
            Initialise.SetVisibilityEnable(ref startButton, false);
            Initialise.SetVisibilityEnable(ref loadButton, false);
            Initialise.SetVisibilityEnable(ref socketButton, false);
            LoadGame(new Point(2, 2), new Point(452, 452));
        }

        private void buttonHost_Click(object sender, EventArgs e)
        {
            SocketConnect.HostClientStart(8080, 8181, true);

            Initialise.SetVisibilityEnable(ref buttonHost, false);
            Initialise.SetVisibilityEnable(ref buttonClient, false);

            startButton.Text = "Start when both players ready";
            Initialise.SetVisibilityEnable(ref startButton, true);
        }
        private void buttonClient_Click(object sender, EventArgs e)
        {
            SocketConnect.HostClientStart(8181, 8080, false);

            Initialise.SetVisibilityEnable(ref buttonHost, false);
            Initialise.SetVisibilityEnable(ref buttonClient, false);

            startButton.Text = "Start when both players ready";
            Initialise.SetVisibilityEnable(ref startButton, true);

        }
        private void exit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Game by Damian TOMASIK", "Thank you for playing!");
            Application.Exit();
        }

        public static void Timers_off()
        {
            timersOff = true;
        }

        




        


        private void KeyUp_Listner(object sender, KeyEventArgs e)
        {
            if (onlinehost == true || online == false)
            {
                tank1.SetDirection(e, false);
                if (e.KeyCode == Keys.Space && tank1.shoot == false && tank1.isDestroyed == false)
                {
                    tank1.shoot = true;

                    PictureBox picbullet1 = new PictureBox();
                    bullet1 = new Bullet(ref tank1, tank1.pointDirection, ref picbullet1);
                    panel1.Controls.Add(picbullet1);
                    picbullet1.BringToFront();
                    allEvents.gameEvent.Attach(bullet1);
                }
            }
            
            if (onlinehost == false || online == false)
            {
                tank2.SetDirection(e, false);

                if (e.KeyCode == Keys.F && tank2.shoot == false && tank2.isDestroyed == false)
                {
                    tank2.shoot = true;
                    PictureBox picbullet2 = new PictureBox();
                    bullet2 = new Bullet(ref tank2, tank2.pointDirection, ref picbullet2);
                    panel1.Controls.Add(picbullet2);
                    picbullet2.BringToFront();
                    allEvents.gameEvent.Attach(bullet2);
                }
            }
        }

        private void KeyDown_Listner(object sender, KeyEventArgs e)
        {
            if (onlinehost == true || online == false)
            {
                tank1.SetDirection(e, true);
            }

            if (onlinehost == false || online == false)
            {
                tank2.SetDirection(e, true);
            }

            if (e.KeyCode == Keys.Escape)
            {
                Timers_off();
                gameTimer.Enabled = false;
                disconect = true;
                if (online)
                {
                    SocketConnect.SendTimer();
                    online = false;
                }
                Application.Restart();
            }
        }






        //Timery głównej logiki gry

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (counter == 6)
                counter = 0;
            counter++;
            allEvents.gameEvent.Notify();
            allEvents.gameEvent.Notify(); // podwójne wystápienie w celu przyspieszenia gry
        }

        private void timerHost_Tick(object sender, EventArgs e)
        {
            if (timersOff == true)
            {
                timersOff = false;
                timerHost.Enabled = false;
                timerSend.Enabled = false;
                gameTimer.Enabled = false;
            }
            SocketConnect.HostTimer();
        }

        private void timerSend_Tick(object sender, EventArgs e)
        {
            if (timersOff == true)
            {
                timersOff = false;
                timerHost.Enabled = false;
                timerSend.Enabled = false;
                gameTimer.Enabled = false;
            }
            if(onlinehost && listBonus.Count<1)
                bonusLocation=(byte)Initialise.random.Next(0, listSwamp.Count - 1); // Przyszła lokacja bonusu
            SocketConnect.SendTimer();
        }
    }
}
