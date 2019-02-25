using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pl.edu.wat.wcy.pz.tanks
{
    static class SocketConnect
    {

        public static string serverIp = Dns.GetHostName();
        public static IPAddress iphost = Dns.GetHostEntry(serverIp).AddressList[0];
        public static string dnsClient;
        public static int port;
        public static TcpListener server = new TcpListener(iphost, 8080);
        public static TcpClient client = default(TcpClient);
        public static ManualResetEvent connectDone = new ManualResetEvent(false);


        public static void HostTimer()
        {
            client = server.AcceptTcpClient();
            byte[] receivedBuff = new byte[7];
            NetworkStream stream = client.GetStream();

            stream.Read(receivedBuff, 0, 7);


            for (int i = 0; i < 4; i++)
            {
                if (receivedBuff[i] == 1)
                {
                    if (GameWindow.onlinehost)
                        GameWindow.tank2.keyTank[i] = true;
                    if (!GameWindow.onlinehost)
                        GameWindow.tank1.keyTank[i] = true;
                }
                if (receivedBuff[i] == 0)
                {
                    if (GameWindow.onlinehost)
                        GameWindow.tank2.keyTank[i] = false;
                    if (!GameWindow.onlinehost)
                        GameWindow.tank1.keyTank[i] = false;
                }
            }
            if (receivedBuff[4] == 1)
            {
                if (!GameWindow.onlinehost && GameWindow.tank1.shoot == false && GameWindow.tank1.isDestroyed == false)
                {
                    PictureBox picbullet1 = new PictureBox();
                    GameWindow.bullet1 = new Bullet(ref GameWindow.tank1, GameWindow.tank1.pointDirection, ref picbullet1);
                    GameWindow.panel1.Controls.Add(picbullet1);
                    picbullet1.BringToFront();
                    GameWindow.tank1.shoot = true;
                    GameWindow.allEvents.gameEvent.Attach(GameWindow.bullet1);
                }
                if (GameWindow.onlinehost && GameWindow.tank2.shoot == false && GameWindow.tank2.isDestroyed == false)
                {
                    PictureBox picbullet2 = new PictureBox();
                    GameWindow.bullet2 = new Bullet(ref GameWindow.tank2, GameWindow.tank2.pointDirection, ref picbullet2);
                    GameWindow.panel1.Controls.Add(picbullet2);
                    picbullet2.BringToFront();
                    GameWindow.tank2.shoot = true;
                    GameWindow.allEvents.gameEvent.Attach(GameWindow.bullet2);
                }
            }
            if (receivedBuff[5] == 1)
            {
                GameWindow.Timers_off();
                MessageBox.Show("Your partner disconnected. Restarting game.", "Oooops!");
                Application.Restart();
            }

            if (!GameWindow.onlinehost)
                GameWindow.bonusLocation = receivedBuff[6];
            if (GameWindow.listBonus.Count < 1)
                Initialise.GenerateBonus(GameWindow.bonusLocation);

        }
        static public void SendTimer()
        {
            try
            {
                try
                {
                    client = new TcpClient(dnsClient, port);
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.ToString());
                }


                byte[] sendData = new byte[7];
                sendData[Initialise.KUp] = 0;
                sendData[Initialise.KRight] = 0;
                sendData[Initialise.KDown] = 0;
                sendData[Initialise.KLeft] = 0;
                sendData[4] = 0;
                sendData[5] = 0;
                sendData[6] = GameWindow.bonusLocation;
                for (int i = 0; i < 4; i++)
                {
                    if (GameWindow.tank1.keyTank[i] == true && GameWindow.onlinehost)
                        sendData[i] = 1;
                    if (GameWindow.tank2.keyTank[i] == true && !GameWindow.onlinehost)
                        sendData[i] = 1;
                }
                if (GameWindow.tank2.shoot == true && GameWindow.tank2.isDestroyed == false && !GameWindow.onlinehost)
                {
                    sendData[4] = 1;
                }
                if (GameWindow.tank1.shoot == true && GameWindow.tank1.isDestroyed == false && GameWindow.onlinehost)
                {
                    sendData[4] = 1;
                }
                if (GameWindow.disconect == true)
                {
                    sendData[5] = 1;
                }


                NetworkStream stream = SocketConnect.client.GetStream();


                stream.Write(sendData, 0, 7);
                stream.Close();
                client.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        static public void HostClientStart(int port1, int port2, bool isHost)
        {
            dnsClient = GameWindow.secondIP.Text;
            Console.WriteLine(dnsClient);
            GameWindow.secondIP.Enabled = false;
            GameWindow.secondIP.Visible = false;
            GameWindow.ipLabel.Enabled = false;
            GameWindow.ipLabel.Visible = false;
            server = new TcpListener(SocketConnect.iphost, port2);
            port = port1;
            GameWindow.onlinehost = isHost;
            try
            {
                SocketConnect.server.Start();
                Console.WriteLine("Server started");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
