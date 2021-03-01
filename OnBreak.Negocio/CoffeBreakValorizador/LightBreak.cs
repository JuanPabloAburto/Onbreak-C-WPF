using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Negocio;

namespace Patrones
{
    public class LightBreak : ICoffeeBreak
    {
        public string GetEventDetails()
        {
            return "Light Break";
        }

       
        public double GetValue(string IdModalidad, int Asistentes, int PersonalAdicional)
        {
            ModalidadServicios mod = new ModalidadServicios()
            {
                IdModalidad = IdModalidad
            };
            if (mod.Read())
            {
                double _valorBase = mod.ValorBase;
                double _asisUF = 0; //Valor UF por Asistente
                double _persUF = 0;  // Valor  Uf por Personal Adiccional
                if ((Asistentes > 0) && (Asistentes <= 300) || (PersonalAdicional >= 0) && (PersonalAdicional <= 10) )
                {
                    if ((Asistentes > 0) && (Asistentes <= 20))
                    {
                        _asisUF = 3;
                    }
                    if ((Asistentes > 20) && (Asistentes <= 50))
                    {
                        _asisUF = 5;
                    }
                    if (Asistentes > 50)
                    {
                        double resultado = (Asistentes - 50) / 20 + 1;
                        Math.Round(resultado);
                        _asisUF = (resultado * 2) + 5;
                    }
                    if ((PersonalAdicional == 2) || (PersonalAdicional == 3))
                    {
                        _persUF = PersonalAdicional;
                    }
                    if (PersonalAdicional == 4)
                    {
                        _persUF = 3.5;
                    }
                    if (PersonalAdicional > 4)
                    {
                        double resultado = PersonalAdicional - 4;
                        _persUF = (resultado * 0.5) + 3.5;
                    }
                }
                return _valorBase + _asisUF + _persUF; //Devuelve el valor final en UF
            }
            else
            {
                return 0;
            }
        }
}
}
