using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pl.edu.wat.wcy.pz.tanks
{
    public class EventsClass
    {
        public EventManagerObjects gameEvent;
        public EventManagerObjects senderNotify;
        public EventsClass()
        {
            gameEvent = new EventManagerObjects();
            senderNotify = new EventManagerObjects();
        }
    }
}
