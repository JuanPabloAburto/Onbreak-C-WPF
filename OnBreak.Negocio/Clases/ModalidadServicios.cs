using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class ModalidadServicios
    {
        string _DescripcionTipoEvento;
        string _Contrato;

        public string IdModalidad { get; set; }
        public Int32 IdTipoEvento { get; set; }
        public string Nombre { get; set; }
        public double ValorBase { get; set; }
        public Int32 PersonalBase { get; set; }
        public string DescripcionTipoEvento { get { return _DescripcionTipoEvento; } }
        public string Contrato { get { return _Contrato; } }

        public ModalidadServicios()
        {
            this.Init();
        }

        private void Init()
        {
            IdModalidad = string.Empty;
            IdTipoEvento = 0;
            Nombre = string.Empty;
            ValorBase = 0;
            PersonalBase = 0;
            _DescripcionTipoEvento = string.Empty;

        }

        public void LeerTipoEvento()
        {
            TipoEventos tipo = new TipoEventos() { IdTipoEvento = IdTipoEvento };
            if (tipo.Read())
            {
                _DescripcionTipoEvento = tipo.Descripcion;
            }
            else
            {
                _DescripcionTipoEvento = string.Empty;
            }
        }

        public void LeerContrato()
        {
            Contrato tipo = new Contrato() { IdTipoEvento = IdTipoEvento };
            if (tipo.Read())
            {
                _Contrato = tipo.Numero;
            }
            else
            {
                _Contrato = string.Empty;
            }
        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                Datos.ModalidadServicio Mod = bbdd.ModalidadServicio.First(ms => ms.IdModalidad == IdModalidad);
                CommonBC.Syncronize(Mod, this);
                LeerTipoEvento();
                LeerContrato();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private List<ModalidadServicios> GenerarListado(List<Datos.ModalidadServicio> ListaDatos)
        {
            List<ModalidadServicios> ListaNegocio = new List<ModalidadServicios>();
            foreach (Datos.ModalidadServicio Datos in ListaDatos)
            {
                ModalidadServicios ms = new ModalidadServicios();
                CommonBC.Syncronize(Datos, ms);
                ListaNegocio.Add(ms);
            }
            return ListaNegocio;
        }

        public List<ModalidadServicios> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                List<Datos.ModalidadServicio> ListaDatos = bbdd.ModalidadServicio.ToList<Datos.ModalidadServicio>();
                List<ModalidadServicios> ListaNegocio = GenerarListado(ListaDatos);

                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<ModalidadServicios>();
            }
        }

        public List<ModalidadServicios> ReadAllByModalidad(int IdTipoEvento)
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.ModalidadServicio> ListaDatos =
                    bbdd.ModalidadServicio.Where(c => c.IdTipoEvento == IdTipoEvento).ToList<Datos.ModalidadServicio>();

                List<ModalidadServicios> ListaNegocio = GenerarListado(ListaDatos);
                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<ModalidadServicios>();
            }
        }

    }
}
