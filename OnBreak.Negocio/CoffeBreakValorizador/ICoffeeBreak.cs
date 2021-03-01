using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrones
{
    //Abstract Product
    public interface ICoffeeBreak
    {
        string GetEventDetails();
        double GetValue(string IdModalidad, int Asistentes, int PersonalAdicional);
        
    }
}
