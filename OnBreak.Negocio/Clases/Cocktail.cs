using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class Cocktail
    {
        string _DescripcionTipoAmbientacion;
        string _Contrato;

        public string Numero { get; set; }
        public Int32 IdTipoAmbientacion { get; set; }
        public Boolean Ambientacion { get; set; }
        public Boolean MusicaAmbiental { get; set; }
        public Boolean MusicaCliente { get; set; }

        public string DescripcionTipoAmbientacion  { get{ return _DescripcionTipoAmbientacion; } }
        public string Contrato { get{ return _Contrato; } }

        public Cocktail()
        {
            this.Init();
        }

        private void Init ()
        {
            Numero = string.Empty;
            IdTipoAmbientacion = 0;
            Ambientacion = false;
            MusicaAmbiental = false;
            MusicaCliente = false;

            _DescripcionTipoAmbientacion = string.Empty;
            _Contrato = string.Empty;
        }

        public void LeerTipoAmbientacion()
        {
            TipoAmbientacion tipo = new TipoAmbientacion() { IdTipoAmbientacion = IdTipoAmbientacion };
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
            Datos.Cocktail ct = new Datos.Cocktail();
            try
            {
                CommonBC.Syncronize(this, ct);
                bbdd.Cocktail.Add(ct);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                bbdd.Cocktail.Remove(ct);
                return false;
            }

        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.Cocktail Ct = bbdd.Cocktail.First(c => c.Numero == Numero);
                CommonBC.Syncronize(Ct, this);
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
                Datos.Cocktail ct = bbdd.Cocktail.First(c => c.Numero == Numero);
                CommonBC.Syncronize(this, ct);
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
                Datos.Cocktail ct = bbdd.Cocktail.First(c => c.Numero == Numero);
                bbdd.Cocktail.Remove(ct);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private List<Cocktail> GenerarListado(List<Datos.Cocktail> ListaDatos)
        {
            List<Cocktail> ListaNegocio = new List<Cocktail>();
            foreach (Datos.Cocktail Datos in ListaDatos)
            {
                Cocktail Ct = new Cocktail();
                CommonBC.Syncronize(Datos, Ct);
                ListaNegocio.Add(Ct);
            }
            return ListaNegocio;
        }

        public List<Cocktail> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                List<Datos.Cocktail> ListaDatos = bbdd.Cocktail.ToList<Datos.Cocktail>();
                List<Cocktail> ListaNegocio = GenerarListado(ListaDatos);

                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<Cocktail>();
            }
        }

    }
}
