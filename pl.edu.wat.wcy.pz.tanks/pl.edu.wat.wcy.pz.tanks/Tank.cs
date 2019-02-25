using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace pl.edu.wat.wcy.pz.tanks
{
    public class Tank: GameObject, ObjectsInterface
    {
        public byte pointDirection;
        Point movePoint;
        public int life;
        public byte which;
        public PictureBox objectPicture;
        private Image tankPicutre;
        int speed = 1;
        public bool shoot=false;
        public bool isDestroyed = false;
        public bool[] keyTank = new bool[4] { false, false, false, false };
        public bool[] tankColision = new bool[4] { false, false, false, false };
        private Keys[] whichKeys;
        bool onSwamp = false;
        public bool superLoaded = false;
        


        public Tank(byte which, ref PictureBox picTank)
        {
            if (which == 1)
            {
                whichKeys = new Keys[4] { Keys.Up, Keys.Right, Keys.Down, Keys.Left };
                this.which = 1;
                pointDirection = Initialise.KUp;
                movePoint.X = picTank.Location.X;
                movePoint.Y = picTank.Location.Y;
                tankPicutre = Image.FromFile(@"res\czołgV1.png");
                picTank.Image = (Image)tankPicutre.Clone();
            }
            if (which == 2)
            {
                whichKeys = new Keys[4] { Keys.W, Keys.D, Keys.S, Keys.A };
                this.which = 2;
                pointDirection = Initialise.KDown;
                movePoint.X = picTank.Location.X;
                movePoint.Y = picTank.Location.Y;
                tankPicutre = Image.FromFile(@"res\czołgV2.png");
                picTank.Image = (Image)tankPicutre.Clone();
                picTank.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            life = 100;
            this.objectPicture = picTank;
        }

        public Point TankMove(byte where)
        {
            if (pointDirection == where)
                switch (where)
                {
                    case Initialise.KUp:
                        movePoint.Y -= speed;
                        break;
                    case Initialise.KRight:
                        movePoint.X += speed;
                        break;
                    case Initialise.KDown:
                        movePoint.Y += speed;
                        break;
                    case Initialise.KLeft:
                        movePoint.X -= speed;
                        break;
                }
            objectPicture.Location = movePoint;
            objectPicture.Refresh();
            return movePoint;
        }

        public void ChangeDirection(byte where)
        {
            switch (where)
            {
                case Initialise.KUp:
                    pointDirection = where;
                    objectPicture.Image = (Image)tankPicutre.Clone();
                    objectPicture.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;
                case Initialise.KRight:
                    pointDirection = where;
                    objectPicture.Image = (Image)tankPicutre.Clone();
                    objectPicture.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case Initialise.KDown:
                    pointDirection = where;
                    objectPicture.Image = (Image)tankPicutre.Clone();
                    objectPicture.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case Initialise.KLeft:
                    pointDirection = where;
                    objectPicture.Image = (Image)tankPicutre.Clone();
                    objectPicture.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
        }

        public bool Destroy()
        {
            if (life < 0)
            {
                objectPicture.Enabled = false;
                objectPicture.Visible = false;
                objectPicture.Location = new Point(-50, -50);
                objectPicture.Refresh();
                isDestroyed = true;
                MessageBox.Show("End of game! Press Escape");
                return true;
            }
            return false;
        }

        public void OnSwamp()
        {
            onSwamp = true;
        }
        public void OffSwamp()
        {
            onSwamp = false;   
        }


        public byte GetWhich()
        {
            return which;
        }

        public byte GetWhere()
        {
            byte count = 0;
            byte where = 5;
            if (keyTank[Initialise.KUp])
            {
                count++;
                where = Initialise.KUp;
            }
            if (keyTank[Initialise.KRight])
            {
                count++;
                where = Initialise.KRight;
            }
            if (keyTank[Initialise.KDown])
            {
                count++;
                where = Initialise.KDown;
            }
            if (keyTank[Initialise.KLeft])
            {
                count++;
                where = Initialise.KLeft;
            }
            if (count > 1)
                return 5;

            return where;
        }

        public void SetDirection(KeyEventArgs key, bool tf)
        {
            if (key.KeyCode == whichKeys[Initialise.KUp])
            {
                keyTank[Initialise.KUp] = tf;
            }
            if (key.KeyCode == whichKeys[Initialise.KRight])
            {
                keyTank[Initialise.KRight] = tf;
            }
            if (key.KeyCode == whichKeys[Initialise.KDown])
            {
                keyTank[Initialise.KDown] = tf;
            }
            if (key.KeyCode == whichKeys[Initialise.KLeft])
            {
                keyTank[Initialise.KLeft] = tf;
            }
        }

        public void Update()
        {
            if (!onSwamp || GameWindow.counter%2==0)
            if(GetWhere()<5)
            if (pointDirection == GetWhere())
            {
                if (ColissionEngine.TankColission(GetWhere(), this) == false)
                {
                    tankColision[GetWhere()] = false;
                    TankMove(GetWhere());
                }
                else
                {
                        if(GetWhere()<5)
                    tankColision[GetWhere()] = true;
                    objectPicture.Refresh();
                }
            }
            else
            {
                ChangeDirection(GetWhere());
            }
        }
    }
}
