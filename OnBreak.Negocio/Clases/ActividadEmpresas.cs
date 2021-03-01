using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Datos;

namespace OnBreak.Negocio
{
    public class ActividadEmpresas
    {
        public int IdActividadEmpresa { get; set; }
        public string Descripcion { get; set; }

        public ActividadEmpresas()
        {
            this.Init();
        }

        private void Init()
        {

            IdActividadEmpresa = 0;
            Descripcion = string.Empty;
        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.ActividadEmpresa Tipo = bbdd.ActividadEmpresa.First(ae => ae.IdActividadEmpresa == IdActividadEmpresa);
                CommonBC.Syncronize(Tipo, this);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private List<ActividadEmpresas> GenerarListado(List<Datos.ActividadEmpresa> ListaDatos)
        {
            List<ActividadEmpresas> ListaNegocio = new List<ActividadEmpresas>();

            foreach (Datos.ActividadEmpresa Datos in ListaDatos)
            {
                ActividadEmpresas AE = new ActividadEmpresas();
                CommonBC.Syncronize(Datos, AE);

                ListaNegocio.Add(AE);
            }

            return ListaNegocio;
        }

        public List<ActividadEmpresas> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.ActividadEmpresa> ListaDatos = bbdd.ActividadEmpresa.ToList<Datos.ActividadEmpresa>();
                List<ActividadEmpresas> ListaNegocio = GenerarListado(ListaDatos);

                return ListaNegocio;

            }
            catch (Exception)
            {
                return new List<ActividadEmpresas>();
            }
        }
    }
}
