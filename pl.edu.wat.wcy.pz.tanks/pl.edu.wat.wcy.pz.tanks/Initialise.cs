using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pl.edu.wat.wcy.pz.tanks
{
    static class Initialise
    {
        static public Random random = new Random();
        static Panel panel1;
        public static List<PictureBox> listWall = new List<PictureBox>();
        public static List<PictureBox> listSwamp = new List<PictureBox>();
        public static List<PictureBox> listBonus = new List<PictureBox>();

        public const byte KUp = 0;
        public const byte KRight = 1;
        public const byte KDown = 2;
        public const byte KLeft = 3;


        public static int[,] map1 = new int[12, 12]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,1,0,2,0,0,0,0,1,0,1},
            {1,0,2,1,0,1,1,0,2,1,0,1},
            {1,2,2,0,0,0,0,0,1,0,0,1},
            {1,0,1,1,2,0,0,0,2,2,0,1},
            {1,0,0,0,2,1,0,0,0,2,0,1},
            {1,0,0,1,2,0,0,1,1,0,0,1},
            {1,2,0,0,1,0,2,1,0,0,1,1},
            {1,2,1,0,1,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1}
        };
        static public void GenerateMap(ref Panel panel, ref List<PictureBox> listWall1, ref List<PictureBox> listSwamp1, ref List<PictureBox> bonusList)
        {
            panel1 = panel;
            listWall = listWall1;
            listSwamp = listSwamp1;
            listBonus = bonusList;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (map1[i, j] == 1)
                        CreateWall(j, i);
                    if (map1[i, j] == 0)
                        CreateFloor(j, i);
                    if (map1[i, j] == 2)
                        CreateSwamp(j, i);
                }
            }
            if (!GameWindow.online)
                GenerateBonus();
        }

        static public void CreateTanks(ref Tank tank1, ref Tank tank2, ref PictureBox tank1Pic, ref PictureBox tank2Pic)
        {
            tank1 = new Tank(1, ref tank1Pic);
            tank2 = new Tank(2, ref tank2Pic);
            tank1.objectPicture.Size = new Size(40, 40);
            tank2.objectPicture.Size = new Size(40, 40);
            tank1.objectPicture.BackColor = Color.Transparent;
            tank2.objectPicture.BackColor = Color.Transparent;
            tank1.objectPicture.Tag = "Tank";
            tank2.objectPicture.Tag = "Tank";
            panel1.Controls.Add(tank1Pic);
            panel1.Controls.Add(tank2.objectPicture);
            tank1.objectPicture.BringToFront();
            tank2.objectPicture.BringToFront();
            GameWindow.lifeT1.Location = new Point(505, 40);
            GameWindow.lifeT2.Location = new Point(505, 60);
            GameWindow.lifeT1.Size = new Size(100, 10);
            GameWindow.lifeT2.Size = new Size(100, 10);
            GameWindow.lifeT1.Enabled = true;
            GameWindow.lifeT2.Enabled = true;
            GameWindow.lifeT1.Style = ProgressBarStyle.Continuous;
            GameWindow.lifeT2.Style = ProgressBarStyle.Continuous;
            GameWindow.lifeT1.ForeColor = Color.FromArgb(58, 127, 2);
            GameWindow.lifeT2.ForeColor = Color.FromArgb(90, 63, 1);
            GameWindow.lifeT1.Value = tank1.life;
            GameWindow.lifeT2.Value = tank2.life;
        }

        static public void CreateWall(int column, int row)
        {
            PictureBox wall = new PictureBox();
            wall.Tag = "Wall";
            wall.Location = new Point(-50 + column * 50, -50 + row * 50);
            wall.Size = new Size(50, 50);
            wall.Image = Image.FromFile(@"res\walle.png");
            listWall.Add(wall);
            panel1.Controls.Add(wall);
        }

        static public void CreateFloor(int column, int row)
        {
            PictureBox floor = new PictureBox();
            floor.Tag = "floor";
            floor.Location = new Point(-50 + column * 50, -50 + row * 50);
            floor.Size = new Size(50, 50);
            floor.Image = Image.FromFile(@"res\floor.png");
            floor.Image.RotateFlip((RotateFlipType)random.Next(0, 7));
            panel1.Controls.Add(floor);
        }
        public static void CreateSwamp(int column, int row)
        {
            PictureBox swamp = new PictureBox();
            swamp.Tag = "swamp";
            swamp.Location = new Point(-50 + column * 50, -50 + row * 50);
            swamp.Size = new Size(50, 50);
            swamp.Image = Image.FromFile(@"res\swamp.png");
            swamp.Image.RotateFlip((RotateFlipType)random.Next(0, 7));
            listSwamp.Add(swamp);
            panel1.Controls.Add(swamp);
        }

        public static void GenerateBonus()
        {
            int swampNumber;
            PictureBox bonus = new PictureBox();
            bonus.Tag = "bonus";
            bonus.Size = new Size(10, 10);
            bonus.BackColor = Color.Transparent;
            bonus.Image = Image.FromFile(@"res\Bonus.png");
            swampNumber = random.Next(0, listSwamp.Count - 1);
            GameWindow.bonusLocation = (byte)swampNumber;
            bonus.Location = new Point(listSwamp[swampNumber].Location.X + 20, listSwamp[swampNumber].Location.Y + 20);
            bonus.Enabled = true;
            bonus.Visible = true;
            listBonus.Add(bonus);
            panel1.Controls.Add(bonus);
            bonus.BringToFront();
        }

        public static void GenerateBonus(byte bonusLocation)
        {
            int swampNumber;
            PictureBox bonus = new PictureBox();
            bonus.Tag = "bonus";
            bonus.Size = new Size(10, 10);
            bonus.BackColor = Color.Transparent;
            bonus.Image = Image.FromFile(@"res\Bonus.png");
            swampNumber = (int)bonusLocation;
            bonus.Location = new Point(listSwamp[swampNumber].Location.X + 20, listSwamp[swampNumber].Location.Y + 20);
            bonus.Enabled = true;
            bonus.Visible = true;
            listBonus.Add(bonus);
            panel1.Controls.Add(bonus);
            bonus.BringToFront();
        }


        public static void SetVisibilityEnable(ref Button button, bool tf)
        {
            button.Visible = tf;
            button.Enabled = tf;
        }
        public static void SetLocationAndSize(ref Button button, Point location, Size size)
        {
            button.Location = location;
            button.Size = size;
        }



    }
}
