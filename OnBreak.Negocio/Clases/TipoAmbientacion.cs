using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class TipoAmbientacion
    {
        public Int32 IdTipoAmbientacion { get; set; }
        public string Descripcion { get; set; }

        public TipoAmbientacion()
        {
            this.Init();
        }

        private void Init()
        {
            IdTipoAmbientacion = 0;
            Descripcion = string.Empty;

        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.TipoAmbientacion tipo = bbdd.TipoAmbientacion.First(t => t.IdTipoAmbientacion == IdTipoAmbientacion);
                CommonBC.Syncronize(tipo, this);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private List<TipoAmbientacion> GenerarListado(List<Datos.TipoAmbientacion> ListaDatos)
        {
            List<TipoAmbientacion> ListaNegocio = new List<TipoAmbientacion>();
            foreach (Datos.TipoAmbientacion Datos in ListaDatos)
            {
                TipoAmbientacion ta = new TipoAmbientacion();
                CommonBC.Syncronize(Datos, ta);
                ListaNegocio.Add(ta);
            }
            return ListaNegocio;
        }

        public List<TipoAmbientacion> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                List<Datos.TipoAmbientacion> ListaDatos = bbdd.TipoAmbientacion.ToList<Datos.TipoAmbientacion>();
                List<TipoAmbientacion> ListaNegocio = GenerarListado(ListaDatos);

                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<TipoAmbientacion>();
            }
        }
    }
}
