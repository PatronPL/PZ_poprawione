using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pl.edu.wat.wcy.pz.tanks
{
    public class GameObject
    {
        public byte pointDirection;
        Point movePoint;
        public byte which;
        public PictureBox objectPicture;
        int speed;
    }
}
