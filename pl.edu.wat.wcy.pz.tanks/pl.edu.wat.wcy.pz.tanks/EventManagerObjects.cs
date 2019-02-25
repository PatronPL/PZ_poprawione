using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pl.edu.wat.wcy.pz.tanks
{
    public class EventManagerObjects
    {
        public delegate void StatusUpdate();
        public event StatusUpdate OnStatusUpdate = null;

        public void Attach(ObjectsInterface gameObject)
        {
            OnStatusUpdate += new StatusUpdate(gameObject.Update);
        }
        public void Detach(ObjectsInterface gameObject)
        {
            OnStatusUpdate -= new StatusUpdate(gameObject.Update);
        }
        public void Notify()
        {
            if (OnStatusUpdate != null)
                OnStatusUpdate();
        }
    }
}
