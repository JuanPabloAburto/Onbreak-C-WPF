using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class TipoEmpresas
    {
        public Int32 IdTipoEmpresa { get; set; }
        public string Descripcion { get; set; }

        public TipoEmpresas()
        {
            this.Init();
        }

        private void Init()
        {
            IdTipoEmpresa = 0;
            Descripcion = string.Empty;
        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.TipoEmpresa Tipo = bbdd.TipoEmpresa.First(te => te.IdTipoEmpresa == IdTipoEmpresa);
                CommonBC.Syncronize(Tipo, this);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private List<TipoEmpresas> GenerarListado(List<Datos.TipoEmpresa> ListaDatos)
        {
            List<TipoEmpresas> ListadoNegocio = new List<TipoEmpresas>();
            foreach (Datos.TipoEmpresa Datos in ListaDatos)
            {
                TipoEmpresas TE = new TipoEmpresas();
                CommonBC.Syncronize(Datos, TE);

                ListadoNegocio.Add(TE);
            }
            return ListadoNegocio;
        }

        public List<TipoEmpresas> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.TipoEmpresa> ListaDatos = bbdd.TipoEmpresa.ToList<Datos.TipoEmpresa>();
                List<TipoEmpresas> ListaNegocio = GenerarListado(ListaDatos);

                return ListaNegocio;

            }
            catch (Exception)
            {
                return new List<TipoEmpresas>();
            }
        }
    }
}
