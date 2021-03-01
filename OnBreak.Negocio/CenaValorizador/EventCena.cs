using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrones
{
    public class EventCena
    {
        ICena cel, eje;

        public EventCena(IValorizador factory)
        {
            cel = factory.GetCena("Celebración");
            eje = factory.GetCena("Ejecutiva"); 
        }

        public string GetEjecutivaDetails()
        {
            return eje.GetEventDetails();
        }

        public double GetEjecutivaValue(string mod,int asi, int per, bool basic, bool custom, bool music,bool local,double otro)
        {
            return eje.GetValue(mod,asi, per, basic, custom, music,local,otro);
        }

        public string GetCelebracionDetails()
        {
            return cel.GetEventDetails();
        }

        public double GetCelebracionValue(string mod, int asi, int per, bool basic, bool custom, bool music, bool local, double otro)
        {
            return cel.GetValue(mod, asi, per, basic, custom, music, local, otro);
        }
    }
}
