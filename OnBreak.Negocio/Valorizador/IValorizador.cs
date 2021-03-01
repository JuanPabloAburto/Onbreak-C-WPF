using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrones
{
    //Abstract Factory
    public interface IValorizador
    {
        ICoffeeBreak GetCoffeeBreak(string name);
        ICockTail GetCockTail(string name);
        ICena GetCena(string name);
    }
}
