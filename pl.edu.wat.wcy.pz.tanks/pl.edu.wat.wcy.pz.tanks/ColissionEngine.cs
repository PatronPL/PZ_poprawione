using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace pl.edu.wat.wcy.pz.tanks
{
    class ColissionEngine
    {
        static public bool WallColission(byte where, Tank tank = null, Bullet bullet = null)
        {
            bool colission = false;
            int X, Y, width, height;
            int col = 0;
            int row = 0;
            int iteracja = -1;
            int temp;

            if (bullet == null)
            {
                X = tank.objectPicture .Location.X;
                Y = tank.objectPicture.Location.Y;
                width = tank.objectPicture.Width;
                height = tank.objectPicture.Height;
            }
            else
            {
                X = bullet.objectPicture.Location.X;
                Y = bullet.objectPicture.Location.Y;
                width = bullet.objectPicture.Width;
                height = bullet.objectPicture.Height;
            }

            temp = X - X % 50;
            do
            {
                temp = temp - 50;
                iteracja++;
            } while (temp >= 0);
            col = iteracja;
            iteracja = -1;
            temp = Y - Y % 50;
            do
            {
                temp = temp - 50;
                iteracja++;
            } while (temp >= 0);
            row = iteracja;
            switch (where)
            {
                case Initialise.KUp:
                    if ((Y % 50 == 0) || (bullet != null && (Y % 50 == 0 || Y % 50 == 1 || Y % 50 == 2)))
                    {
                        if (Initialise.map1[row, col + 1] == 1)
                        {
                            colission = true;
                        }
                        else if (Initialise.map1[row, col + 2] == 1 && X % 50 > (50 - width))
                        {
                            colission = true;
                        }
                    }
                    break;
                case Initialise.KRight:
                    if ((X % 50 == 50 - width) || (bullet != null && (X % 50 == 50 - width || X % 50 == 49 - width || X % 50 == 48 - width)))
                    {
                        if (Initialise.map1[row + 1, col + 2] == 1)
                        {
                            colission = true;
                        }
                        else if (Initialise.map1[row + 2, col + 2] == 1 && Y % 50 > (50 - height))
                        {
                            colission = true;
                        }
                    }
                    break;
                case Initialise.KDown:
                    if ((Y % 50 == 50 - height) || (bullet != null && (Y % 50 == 50 - height || Y % 50 == 49 - height || Y % 50 == 48 - height)))
                    {
                        if (Initialise.map1[row + 2, col + 1] == 1)
                        {
                            colission = true;
                        }
                        else if (Initialise.map1[row + 2, col + 2] == 1 && X % 50 > (50 - width))
                        {
                            colission = true;
                        }
                    }
                    break;
                case Initialise.KLeft:
                    if ((X % 50 == 0) || (bullet != null && (X % 50 == 0 || X % 50 == 1 || X % 50 == 2)))
                    {
                        if (Initialise.map1[row + 1, col] == 1)
                        {
                            colission = true;
                        }
                        else if (Initialise.map1[row + 2, col] == 1 && Y % 50 > (50 - height))
                        {
                            colission = true;
                        }
                    }
                    break;
            }
            return colission;
        }

        static public bool TankColission(byte where, Tank tank)
        {
            bool onSwamp = false;
            for (int i = 0; i < GameWindow.listSwamp.Count; i++)
            {
                if (tank.objectPicture.Bounds.IntersectsWith(GameWindow.listSwamp[i].Bounds))
                {
                    onSwamp = true;
                    if (GameWindow.listBonus.Count>=1 && tank.objectPicture.Bounds.IntersectsWith(GameWindow.listBonus[0].Bounds))
                    {
                        tank.superLoaded = true;
                        GameWindow.panel1.Controls.Remove(GameWindow.listBonus[0]);
                        GameWindow.listBonus.RemoveAt(0);
                        if (!GameWindow.online)
                        Initialise.GenerateBonus();
                    }
                    break;
                }
            }
            if (onSwamp)
            {
                tank.OnSwamp();
            }
            else
            {
                tank.OffSwamp();
            }
            if (GameWindow.tank1.objectPicture.Bounds.IntersectsWith(GameWindow.tank2.objectPicture.Bounds))
            {
                tank.keyTank[where] = false;
                tank.ChangeDirection((byte)((where + 2) % 4));
                tank.TankMove((byte)((where + 2) % 4));
                tank.TankMove((byte)((where + 2) % 4));
                return true;
            }
            else
            {

                if (WallColission(where, tank) == true)
                {
                    return true;
                }
            }
            return false;
        }

        static public bool BulletColission(byte where, Bullet bullet)
        {
            if (WallColission(bullet.pointDirection, null, bullet) == true)
            {
                GameWindow.panel1.Controls.Remove(bullet.objectPicture);
                if (bullet.which == 1)
                {
                    GameWindow.tank1.shoot = false;

                }
                else
                { 
                    GameWindow.tank2.shoot = false;
                }
                return true;
            } 
            else
            {
                if (bullet.objectPicture.Bounds.IntersectsWith(GameWindow.tank1Pic.Bounds) && bullet.which == 2)
                {
                    bullet.Explode();
                    GameWindow.tank2.shoot = false;
                    GameWindow.panel1.Controls.Remove(bullet.objectPicture);
                    GameWindow.tank1.life -= 10;
                    GameWindow.lifeT1.Refresh();
                    if (GameWindow.tank1.Destroy() == false)
                        GameWindow.lifeT1.Value = GameWindow.tank1.life;
                    return true;
                }

                if (bullet.objectPicture.Bounds.IntersectsWith(GameWindow.tank2Pic.Bounds) && bullet.which == 1)
                {
                    bullet.objectPicture.Location = new Point(bullet.objectPicture.Location.X - 3, bullet.objectPicture.Location.Y - 3);
                    GameWindow.tank1.shoot = false;
                    GameWindow.panel1.Controls.Remove(bullet.objectPicture);
                    GameWindow.tank2.life -= 10;
                    GameWindow.lifeT2.Refresh();
                    if (GameWindow.tank2.Destroy() == false)
                        GameWindow.lifeT2.Value = GameWindow.tank2.life;
                    return true;
                }
            }
            return false;
        }
    }
}
