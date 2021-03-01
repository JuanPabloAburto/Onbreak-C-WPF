using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class TipoEventos
    {
        public Int32 IdTipoEvento { get; set; }
        public string Descripcion { get; set; }

        public TipoEventos()
        {
            this.Init();
        }

        private void Init()
        {
            IdTipoEvento = 0;
            Descripcion = string.Empty;
        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.TipoEvento tipo = bbdd.TipoEvento.First(t => t.IdTipoEvento == IdTipoEvento);
                CommonBC.Syncronize(tipo, this);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private List<TipoEventos> GenerarListado(List<Datos.TipoEvento> ListaDatos)
        {
            List<TipoEventos> ListaNegocio = new List<TipoEventos>();
            foreach (Datos.TipoEvento Datos in ListaDatos)
            {
                TipoEventos te = new TipoEventos();
                CommonBC.Syncronize(Datos, te);
                ListaNegocio.Add(te);
            }
            return ListaNegocio;
        }

        public List<TipoEventos> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                List<Datos.TipoEvento> ListaDatos = bbdd.TipoEvento.ToList<Datos.TipoEvento>();
                List<TipoEventos> ListaNegocio = GenerarListado(ListaDatos);

                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<TipoEventos>();
            }
        }
    }
}
