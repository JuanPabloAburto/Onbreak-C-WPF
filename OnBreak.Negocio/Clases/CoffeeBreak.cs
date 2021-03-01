using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class CoffeeBreak
    {
        string _Contrato;

        public string Numero { get; set; }
        public Boolean Vegetariana { get; set; }

        public string Contrato { get{ return _Contrato; } }

        public CoffeeBreak()
        {
            this.Init();
        }

        private void Init()
        {
            Numero = string.Empty;
            Vegetariana = false;

            _Contrato = string.Empty;
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
            Datos.CoffeeBreak cb = new Datos.CoffeeBreak();
            try
            {
                CommonBC.Syncronize(this, cb);
                bbdd.CoffeeBreak.Add(cb);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                bbdd.CoffeeBreak.Remove(cb);
                return false;
            }

        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.CoffeeBreak Cb = bbdd.CoffeeBreak.First(c => c.Numero == Numero);
                CommonBC.Syncronize(Cb, this);
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
                Datos.CoffeeBreak cb = bbdd.CoffeeBreak.First(c => c.Numero == Numero);
                CommonBC.Syncronize(this, cb);
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
                Datos.CoffeeBreak cb = bbdd.CoffeeBreak.First(c => c.Numero == Numero);
                bbdd.CoffeeBreak.Remove(cb);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private List<CoffeeBreak> GenerarListado(List<Datos.CoffeeBreak> ListaDatos)
        {
            List<CoffeeBreak> ListaNegocio = new List<CoffeeBreak>();
            foreach (Datos.CoffeeBreak Datos in ListaDatos)
            {
                CoffeeBreak Cb = new CoffeeBreak();
                CommonBC.Syncronize(Datos, Cb);
                ListaNegocio.Add(Cb);
            }
            return ListaNegocio;
        }

        public List<CoffeeBreak> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            try
            {
                List<Datos.CoffeeBreak> ListaDatos = bbdd.CoffeeBreak.ToList<Datos.CoffeeBreak>();
                List<CoffeeBreak> ListaNegocio = GenerarListado(ListaDatos);

                return ListaNegocio;
            }
            catch (Exception)
            {
                return new List<CoffeeBreak>();
            }
        }
    }
}
