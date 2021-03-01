using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class Contacto
    {
        string _DescripcionActividadEmpresa;
        string _DescripcionTipoEmpresa;

        public string RutContacto { get; set; }
        public string RazonSocial { get; set; }
        public string NombreContacto { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdActividadEmpresa { get; set; }
        public int IdTipoEmpresa { get; set; }
        public string DescripcionActividadEmpresa { get { return _DescripcionActividadEmpresa; } }
        public string DescripcionTipoEmpresa { get { return _DescripcionTipoEmpresa; } }

        public Contacto()
        {
            this.Init();
        }

        private void Init()
        {
            RutContacto = string.Empty;
            RazonSocial = string.Empty;
            NombreContacto = string.Empty;
            Correo = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
            IdActividadEmpresa = 0;
            IdTipoEmpresa = 0;

            _DescripcionActividadEmpresa = string.Empty;
            _DescripcionTipoEmpresa = string.Empty;
        }

        public bool Create()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            Datos.Contacto cli = new Datos.Contacto();
            try
            {
                CommonBC.Syncronize(this, cli);
                bbdd.Cliente.Add(cli);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                bbdd.Cliente.Remove(cli);
                return false;
            }

        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                Datos.Contacto cli = bbdd.Cliente.First(c => c.RutContacto == RutContacto);
                CommonBC.Syncronize(cli, this);
                LeerIdActividadEmpresa();
                LeerIdTipoEmpresa();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                Datos.Contacto cli = bbdd.Cliente.First(c => c.RutContacto == RutContacto);
                CommonBC.Syncronize(this, cli);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                Datos.Contacto cli = bbdd.Cliente.First(c => c.RutContacto == RutContacto);
                bbdd.Cliente.Remove(cli);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void LeerIdActividadEmpresa()
        {
            ActividadEmpresas tipo = new ActividadEmpresas() { IdActividadEmpresa = IdActividadEmpresa };
            if (tipo.Read())
            {
                _DescripcionActividadEmpresa = tipo.Descripcion;
            }
            else
            {
                _DescripcionActividadEmpresa = string.Empty;
            }
        }

        public void LeerIdTipoEmpresa()
        {
            TipoEmpresas tipo = new TipoEmpresas() { IdTipoEmpresa = IdTipoEmpresa };

            if (tipo.Read())
            {
                _DescripcionTipoEmpresa = tipo.Descripcion;
            }
            else
            {
                _DescripcionTipoEmpresa = string.Empty;
            }
        }

        private List<Contacto> GenerarListado(List<Datos.Contacto> ListaDatos)
        {
            List<Contacto> ListaNegocio = new List<Contacto>();
            foreach (Datos.Contacto datos in ListaDatos)
            {
                Contacto cli = new Contacto();
                CommonBC.Syncronize(datos, cli);
                cli.LeerIdActividadEmpresa();
                cli.LeerIdTipoEmpresa();

                ListaNegocio.Add(cli);
            }
            return ListaNegocio;
        }

        public List<Contacto> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                List<Datos.Contacto> ListaDatos = bbdd.Cliente.ToList<Datos.Contacto>();
                List<Contacto> ListaNegocio = GenerarListado(ListaDatos);

                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<Contacto>();
            }
        }

        public List<Contacto> ReadAllByRut(string RutCliente)
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.Contacto> ListaDatos =
                    bbdd.Cliente.Where(c => c.RutContacto == RutCliente).ToList<Datos.Contacto>();

                List<Contacto> ListaNegocio = GenerarListado(ListaDatos);
                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<Contacto>();
            }
        }

        public List<Contacto> ReadAllByTipoEmpresa(int IdTipoEmpresa)
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.Contacto> ListaDatos =
                    bbdd.Cliente.Where(c => c.IdTipoEmpresa == IdTipoEmpresa).ToList<Datos.Contacto>();

                List<Contacto> ListaNegocio = GenerarListado(ListaDatos);
                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<Contacto>();
            }
        }

        public List<Contacto> ReadAllByActividad(int IdActividadEmpresa)
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.Contacto> ListaDatos =
                    bbdd.Cliente.Where(c => c.IdActividadEmpresa == IdActividadEmpresa).ToList<Datos.Contacto>();

                List<Contacto> ListaNegocio = GenerarListado(ListaDatos);
                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<Contacto>();
            }
        }

    }
}
