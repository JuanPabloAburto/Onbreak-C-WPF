using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrones
{
    public interface ICockTail
    {
        string GetEventDetails();
        double GetValue(string IdModalidad,int Asistentes, int PersonalAdicional, bool Basic, bool Custom, bool Music);
    }
}
