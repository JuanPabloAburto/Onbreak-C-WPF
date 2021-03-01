using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Negocio;

namespace Patrones
{
    public class Celebracion : ICena
    {
        public string GetEventDetails()
        {
            return "Celebración";
        }

        //basic y custom, corresponden al tipo de ambientacion, music a la musica ambiental, Local corresponde al
        //local onbreak, otro corresponde a un lugar otorgado por onbreak, de lo contrario es sin costo adicional
        public double GetValue(string IdModalidad, int Asistentes, int PersonalAdicional, bool Basic, bool Custom, bool Music, bool Local, double otro)
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
                double _local = 0;
                double _otro = 0;
                if ((Asistentes > 0) && (Asistentes <= 300) || (PersonalAdicional >= 0) && (PersonalAdicional <= 10))
                {
                    if ((Asistentes > 0) && (Asistentes <= 20))
                    {
                        _asisUF = 1.5 * Asistentes;
                    }
                    if ((Asistentes > 20) && (Asistentes <= 50))
                    {
                        _asisUF = 1.2 * Asistentes;
                    }
                    if (Asistentes > 50)
                    {

                        _asisUF = 1 * Asistentes;
                    }
                    if (PersonalAdicional == 2)
                    {
                        _persUF = 3;
                    }
                    if (PersonalAdicional == 3)
                    {
                        _persUF = 4;
                    }
                    if (PersonalAdicional == 4)
                    {
                        _persUF = 5;
                    }
                    if (PersonalAdicional > 4)
                    {
                        double resultado = PersonalAdicional - 4;
                        _persUF = (resultado * 0.5) + 5;
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
                    if (Local == true)
                    {
                        _local = 9;
                    }
                    if (otro > 0)
                    {
                        _otro = otro * 1.05;
                    }
                }
                return _valorBase + _asisUF + _persUF + _basic + _custom + _music + _local + _otro; //Devuelve el valor final en UF
            }
            else
            {
                return 0;
            }
        }
    }
}
