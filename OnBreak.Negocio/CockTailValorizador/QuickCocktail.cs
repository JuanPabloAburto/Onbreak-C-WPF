using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Negocio;

namespace Patrones
{
    public class QuickCocktail : ICockTail
    {
        public string GetEventDetails()
        {
            return "Quick Cocktail";
        }

        //basic y custom, corresponden al tipo de ambientacion, music a la musica ambiental
        public double GetValue(string IdModalidad,int Asistentes, int PersonalAdicional, bool Basic, bool Custom, bool Music)
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
            double _basic = 0;
            double _custom = 0;
            double _music = 0;
            if ((Asistentes > 0) && (Asistentes <= 300) || (PersonalAdicional >= 0) && (PersonalAdicional <= 10) )
            {
                if ((Asistentes > 0) && (Asistentes <= 20))
                {
                    _asisUF = 4;
                }
                if ((Asistentes > 20) && (Asistentes <= 50))
                {
                    _asisUF = 6;
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
                if (Basic == true)
                {
                    _basic = 2;
                }
                if (Custom == true)
                {
                    _custom = 5;
                }
                if (Music == true)
                {
                    _music = 1;
                }
            }
            return _valorBase + _asisUF + _persUF + _basic + _custom + _music; //Devuelve el valor final en UF
            }
            else
            {
                return 0;
            }
        }
    }
}
