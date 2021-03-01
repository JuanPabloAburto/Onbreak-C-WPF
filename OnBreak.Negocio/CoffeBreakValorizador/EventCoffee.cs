using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Negocio;

namespace Patrones
{
    public class EventCoffee
    {

        ICoffeeBreak light, journal, day;
        
        public EventCoffee(IValorizador factory)
        {

            light = factory.GetCoffeeBreak("Light Break");
            journal = factory.GetCoffeeBreak("Journal Break");
            day = factory.GetCoffeeBreak("Day Break");
        }

        public string GetLightDetails()
        {

            return light.GetEventDetails();
        }

        public double GetLightValue(string mod,int asi , int per)
        {
           
            return light.GetValue(mod,asi, per);
        }



        public string GetJournalDetails()
        {

            return journal.GetEventDetails();
        }

        public double GetJournalValue(string mod,int asi, int per)
        {
            return journal.GetValue(mod,asi, per);
        }

        public string GetDayDetails()
        {

            return day.GetEventDetails();
        }

        public double GetDayValue(string mod,int asi, int per)
        {
            return day.GetValue(mod,asi, per);
        }


    }
}
