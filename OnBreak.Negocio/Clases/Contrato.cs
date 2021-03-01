using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class Contrato
    {
        string _Contacto;
        string _ModalidadServicio;
        string _TipoEvento;
        string _Realizado;

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
        public string Contacto { get { return _Contacto; } }
        public string DescripcionRealizado { get { return _Realizado; } }
        public string ModalidadServicio { get { return _ModalidadServicio; } }
        public string TipoEvento { get { return _TipoEvento; } }


        public Contrato()
        {
            this.Init();
        }

        private void Init()
        {
            Numero = string.Empty;
            Creacion = DateTime.Now;
            Termino = DateTime.Now;
            RutContacto = string.Empty;
            IdModalidad = string.Empty;
            IdTipoEvento = 0;
            FechaHoraInicio = DateTime.Now;
            FechaHoraTermino = DateTime.Now;
            Asistentes = 0;
            PersonalAdicional = 0;
            Realizado = false;
            ValorTotalContrato = 0;
            Observaciones = string.Empty;
            _Contacto = string.Empty;
            _ModalidadServicio = string.Empty;
            _TipoEvento = string.Empty;
        }

        public void LeerRazonCliente()
        {
            Contacto cli = new Contacto() { RutContacto = RutContacto };
            if (cli.Read())
            {
                _Contacto = cli.RazonSocial;
            }
            else
            {
                _Contacto = string.Empty;
            }
        }

        public void LeerModalidad()
        {
            ModalidadServicios mod = new ModalidadServicios() { IdModalidad = IdModalidad };
            if (mod.Read())
            {
                _ModalidadServicio = mod.Nombre;
            }
            else
            {
                _ModalidadServicio = string.Empty;
            }
        }

        public void LeerIdTipoEvento()
        {
            TipoEventos tipo = new TipoEventos() { IdTipoEvento = IdTipoEvento };

            if (tipo.Read())
            {
                _TipoEvento = tipo.Descripcion;
            }
            else
            {
                _TipoEvento = string.Empty;
            }
        }

        public void LeerRealizado()
        {
            if (Realizado) { _Realizado = "Realizado"; }
            else { _Realizado = "No realizado"; }
        }

        public bool Create()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            Datos.Contrato con = new Datos.Contrato();
            try
            {
                CommonBC.Syncronize(this, con);
                bbdd.Contrato.Add(con);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                bbdd.Contrato.Remove(con);
                return false;
            }

        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                Datos.Contrato con = bbdd.Contrato.First(c => c.Numero == Numero);
                CommonBC.Syncronize(con, this);
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
                Datos.Contrato con = bbdd.Contrato.First(c => c.Numero == Numero);
                CommonBC.Syncronize(this, con);
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
                Datos.Contrato con = bbdd.Contrato.First(c => c.Numero == Numero);
                bbdd.Contrato.Remove(con);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private List<Contrato> GenerarListado(List<Datos.Contrato> ListaDatos)
        {
            List<Contrato> ListaNegocio = new List<Contrato>();
            foreach (Datos.Contrato datos in ListaDatos)
            {
                Contrato con = new Contrato();
                CommonBC.Syncronize(datos, con);
                con.LeerRealizado();
                con.LeerRazonCliente();
                con.LeerModalidad();
                con.LeerIdTipoEvento();

                ListaNegocio.Add(con);
            }
            return ListaNegocio;
        }

        public List<Contrato> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                List<Datos.Contrato> ListaDatos = bbdd.Contrato.ToList<Datos.Contrato>();
                List<Contrato> ListaNegocio = GenerarListado(ListaDatos);
                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }

        public List<Contrato> ReadAllByNumero(string Numero)
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.Contrato> ListaDatos =
                    bbdd.Contrato.Where(c => c.Numero == Numero).ToList<Datos.Contrato>();

                List<Contrato> ListaNegocio = GenerarListado(ListaDatos);
                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }

        public List<Contrato> ReadAllByRut(string RutCliente)
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.Contrato> ListaDatos =
                    bbdd.Contrato.Where(c => c.RutContacto == RutCliente).ToList<Datos.Contrato>();

                List<Contrato> ListaNegocio = GenerarListado(ListaDatos);
                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }

        public List<Contrato> ReadAllByTipo(int IdTipoEvento)
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.Contrato> ListaDatos =
                    bbdd.Contrato.Where(c => c.IdTipoEvento == (Int32)IdTipoEvento).ToList<Datos.Contrato>();

                List<Contrato> ListaNegocio = GenerarListado(ListaDatos);
                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }

        public List<Contrato> ReadAllByModalidad(string IdModalidad)
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            ;
            try
            {
                List<Datos.Contrato> ListaDatos =
                    bbdd.Contrato.Where(c => c.IdModalidad == IdModalidad).ToList<Datos.Contrato>();

                List<Contrato> ListaNegocio = GenerarListado(ListaDatos);
                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }

        public double LeerPrecio()
        {
            ModalidadServicios mod = new ModalidadServicios() { IdModalidad = IdModalidad };
            if (mod.Read())
            {
                return ValorTotalContrato = mod.ValorBase;
            }
            else
            {
                return ValorTotalContrato = 0; ;
            }
        }

        public SaveContrato CreateSave (Contrato con)
        {
            return new SaveContrato(con.Numero, con.Creacion, con.Termino, con.RutContacto, con.IdModalidad,
                con.IdTipoEvento, con.FechaHoraInicio, con.FechaHoraTermino, con.Asistentes, con.PersonalAdicional, 
                con.Realizado, con.ValorTotalContrato, con.Observaciones);
        }

        public void Remember(SaveContrato Save)
        {
            this.Numero = Save.Numero;
            this.Creacion = Save.Creacion;
            this.Termino = Save.Termino;
            this.RutContacto = Save.RutContacto;
            this.IdModalidad = Save.IdModalidad;
            this.IdTipoEvento = Save.IdTipoEvento;
            this.FechaHoraInicio = Save.FechaHoraInicio;
            this.FechaHoraTermino = Save.FechaHoraTermino;
            this.Asistentes = Save.Asistentes;
            this.PersonalAdicional = Save.PersonalAdicional;
            this.Realizado = Save.Realizado;
            this.ValorTotalContrato = Save.ValorTotalContrato;
            this.Observaciones = Save.Observaciones;
        }
    }
}
