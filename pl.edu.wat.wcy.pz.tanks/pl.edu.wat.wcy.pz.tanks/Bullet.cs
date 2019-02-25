using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace pl.edu.wat.wcy.pz.tanks
{
    public class Bullet: GameObject, ObjectsInterface
    {
        public byte pointDirection;
        Point shootPoint;
        Point movePoint;
        public byte which;
        public PictureBox objectPicture;
        public int bulletSize = 4;
        int speed = 3;
        public bool super;
        int x, y;
        Tank tank;
        public Bullet(ref Tank tank, byte where, ref PictureBox picture)
        {
            this.tank = tank;
            objectPicture = picture;
            which = tank.GetWhich();
            switch (where)
            {
                case Initialise.KUp:
                    y = -speed;
                    shootPoint = new Point(tank.objectPicture.Location.X + 20 - (bulletSize / 2), tank.objectPicture.Location.Y + bulletSize);
                    break;
                case Initialise.KRight:
                    x = speed;
                    shootPoint = new Point(tank.objectPicture.Location.X + 40 - bulletSize, tank.objectPicture.Location.Y + 20 - (bulletSize / 2));
                    break;
                case Initialise.KDown:
                    y = speed;
                    shootPoint = new Point(tank.objectPicture.Location.X + 20 - (bulletSize / 2), tank.objectPicture.Location.Y + 40 - bulletSize);
                    break;
                case Initialise.KLeft:
                    x = -speed;
                    shootPoint = new Point(tank.objectPicture.Location.X + bulletSize, tank.objectPicture.Location.Y + 20 - (bulletSize / 2));
                    break;
            }
            if (tank.superLoaded == true)
            {
                super = true;
                tank.superLoaded = false;
            }
            movePoint = shootPoint;
            pointDirection = where;

            objectPicture.BackColor = Color.Orange;
            objectPicture.Size = new Size(bulletSize, bulletSize);
            objectPicture.Location = movePoint;
            objectPicture.Enabled = true;

        }


    
        private Point BulletMove()
            {
                movePoint.X += x;
                movePoint.Y += y;
                objectPicture.Location = movePoint;
                return movePoint;
            }
            
        private Point BulletMove(byte where)
        {
            switch (where)
            {
                case Initialise.KUp:
                    movePoint.Y += -speed;
                    pointDirection = Initialise.KUp;
                    break;
                case Initialise.KRight:
                    movePoint.X += speed;
                    pointDirection = Initialise.KRight;
                    break;
                case Initialise.KDown:
                    movePoint.Y += speed;
                    pointDirection = Initialise.KDown;
                    break;
                case Initialise.KLeft:
                    movePoint.X += -speed;
                    pointDirection = Initialise.KLeft;
                    break;
            }
            objectPicture.Location = movePoint;
            return movePoint;
        }

            public void Explode()
            {
                PictureBox explosion = new PictureBox();
                explosion.BackColor = Color.Red;
                explosion.Location = new Point(this.objectPicture.Location.X - 3, this.objectPicture.Location.Y - 3);
                explosion.Size = new Size(10, 10);
                GameWindow.panel1.Controls.Add(explosion);
                explosion.BringToFront();
                explosion.Visible = false;
            }

        public void Update()
        {
            if (tank.shoot)
            if (ColissionEngine.BulletColission(pointDirection, this) == false)
            {
                    if (!super)
                    {
                        BulletMove();
                    }
                    else
                    {
                        objectPicture.BackColor = Color.LightCyan;
                        BulletMove(tank.pointDirection);
                    }
                
            }
            else
            {
                GameWindow.allEvents.gameEvent.Detach(this);
                super = false;
            }
              

        }

    }
    
}
