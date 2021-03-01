using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrones
{
    public class DataType : IValorizador
    {
        public ICena GetCena(string name)
        {
            if (ICena.Equals("Ejecutiva", name))
            {
                return new Ejecutiva();
            }
            else
            {
                return new Celebracion();
            }
        }

        public ICockTail GetCockTail(string name)
        {
            if (ICockTail.Equals("Quick Cocktail",name))
            {
                return new QuickCocktail();
            }
            else
            {
                return new AmbientCocktail();
            }
        }

        public ICoffeeBreak GetCoffeeBreak(string name)
        {
            if (ICoffeeBreak.Equals("Light Break", name))
            {
                return new LightBreak();
            }
            else if(ICoffeeBreak.Equals("Journal Break", name))
            {
                 return new JournalBreak();
            }
            else 
            {
                return new DayBreak();
            }

        }


    }
}
