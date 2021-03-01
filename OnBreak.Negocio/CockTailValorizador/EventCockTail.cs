using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrones
{
    public class EventCockTail
    {
        ICockTail quick, ambient;

        public EventCockTail(IValorizador factory)
        {
            quick = factory.GetCockTail("Quick Cocktail");
            ambient = factory.GetCockTail("Ambient Cocktail");     
        }

        public string GetQuickDetails()
        {
            return quick.GetEventDetails();
        }

        public double GetQuickValue(string mod,int asi, int per,bool basic, bool custom,bool music)
        {
            return quick.GetValue(mod,asi, per,basic,custom,music);
        }

        public string GetAmbientDetails()
        {
            return ambient.GetEventDetails();
        }

        public double GetAmbientValue(string mod, int asi, int per, bool basic, bool custom, bool music)
        {
            return ambient.GetValue(mod, asi, per, basic, custom, music);
        }
    }
}
