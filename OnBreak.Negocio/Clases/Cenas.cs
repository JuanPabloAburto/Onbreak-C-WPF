using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class Cenas
    {
        string _DescripcionTipoAmbientacion;
        string _Contrato;

        public string Numero { get; set; }
        public Int32 IdTipoAmbientacion { get; set; }
        public Boolean MusicaAmbiental { get; set; }
        public Boolean Local { get; set; }
        public Boolean OtroLocal { get; set; }
        public Double ValorArriendo  { get; set; }

        public string DescripcionTipoAmbientacion { get{ return _DescripcionTipoAmbientacion; } }
        public string Contrato { get { return _Contrato; } }

        public Cenas()
        {
            this.Init();
        }

        private void Init()
        {
            Numero = string.Empty;
            IdTipoAmbientacion = 0;
            MusicaAmbiental = false;
            Local = false;
            OtroLocal = false;
            ValorArriendo = 0;

            _DescripcionTipoAmbientacion = string.Empty;
            _Contrato = string.Empty;
        }

        public void LeerTipoAmbientacion()
        {
            TipoAmbientacion tipo = new TipoAmbientacion() { IdTipoAmbientacion= IdTipoAmbientacion };
            if (tipo.Read())
            {
                _DescripcionTipoAmbientacion = tipo.Descripcion;
            }
            else
            {
                _DescripcionTipoAmbientacion = string.Empty;
            }
        }

        public void LeerContrato()
        {
            Contrato con = new Contrato() { Numero = Numero };
            if (con.Read())
            {
                _Contrato = con.Numero;
            }
            else
            {
                _Contrato = string.Empty;
            }
        }

        public bool Create()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            Datos.Cenas ce = new Datos.Cenas();
            try
            {
                CommonBC.Syncronize(this, ce);
                bbdd.Cenas.Add(ce);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                bbdd.Cenas.Remove(ce);
                return false;
            }

        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.Cenas ce = bbdd.Cenas.First(c => c.Numero == Numero);
                CommonBC.Syncronize(ce, this);
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
                Datos.Cenas ce = bbdd.Cenas.First(c => c.Numero == Numero);
                CommonBC.Syncronize(this, ce);
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
                Datos.Cenas ce = bbdd.Cenas.First(c => c.Numero == Numero);
                bbdd.Cenas.Remove(ce);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private List<Cenas> GenerarListado(List<Datos.Cenas> ListaDatos)
        {
            List<Cenas> ListaNegocio = new List<Cenas>();
            foreach (Datos.Cenas Datos in ListaDatos)
            {
                Cenas Ce = new Cenas();
                CommonBC.Syncronize(Datos, Ce);
                ListaNegocio.Add(Ce);
            }
            return ListaNegocio;
        }

        public List<Cenas> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                List<Datos.Cenas> ListaDatos = bbdd.Cenas.ToList<Datos.Cenas>();
                List<Cenas> ListaNegocio = GenerarListado(ListaDatos);

                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<Cenas>();
            }
        }
    }
}
