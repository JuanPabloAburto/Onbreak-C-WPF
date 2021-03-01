using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class SaveContrato
    {
        public string Numero { get; set; }
        public DateTime Creacion { get; set; }
        public DateTime Termino { get; set; }
        public string RutContacto { get; set; }
        public string IdModalidad { get; set; }
        public Int32 IdTipoEvento { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraTermino { get; set; }
        public Int32 Asistentes { get; set; }
        public Int32 PersonalAdicional { get; set; }
        public bool Realizado { get; set; }
        public double ValorTotalContrato { get; set; }
        public string Observaciones { get; set; }
        
        public SaveContrato (string sNumero, DateTime sCreacion, DateTime sTermino, string sRutContacto, 
            string sIdModalidad, Int32 sIdTipoEvento, DateTime sFechaHoraInicio, DateTime sFechaHoraTermino,
            Int32 sAsistentes, Int32 sPersonalAdicional, bool sRealizado, double sValorTotalContrato, string sObservaciones)
        {
            this.Numero = sNumero;
            this.Creacion = sCreacion;
            this.Termino = sTermino;
            this.RutContacto = sRutContacto;
            this.IdModalidad = sIdModalidad;
            this.IdTipoEvento = sIdTipoEvento;
            this.FechaHoraInicio = sFechaHoraInicio;
            this.FechaHoraTermino = sFechaHoraTermino;
            this.Asistentes = sAsistentes;
            this.PersonalAdicional = sPersonalAdicional;
            this.Realizado = sRealizado;
            this.ValorTotalContrato = sValorTotalContrato;
            this.Observaciones = sObservaciones;

        }

    }
}
