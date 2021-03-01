using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cliente.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Negocio;

namespace Cliente.Negocio.Tests
{
    [TestClass()]
    public class ClienteNTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var Cli = new Contacto()
            {
                RutContacto = "22223",
                RazonSocial = "PC",
                NombreContacto = "GENOS",
                Direccion = "CITY Z",
                Correo = "SAITAMA@A.CL",
                Telefono = "+56912345698",
                IdActividadEmpresa = 3,
                IdTipoEmpresa = 30,
            };

            var Resultado = Cli.Create();

            Assert.IsTrue(Resultado);
        }
        [TestMethod()]

        public void ReadTest()
        {
            var Cli = new Contacto()
            {
                RutContacto = "22223",
            };

            var Resultado = Cli.Read();

            Assert.IsTrue(Resultado);

        }

        [TestMethod()]
        public void UpdateTest()
        {
            var Cli = new Contacto()
            {
                RutContacto = "22223",
            };
            string rut = "22223";
            if (rut == Cli.RutContacto)
            {
                Cli.RutContacto = "22223";
                Cli.RazonSocial = "ANIME";
                Cli.NombreContacto = "SAITAMA";
                Cli.Direccion = "SANTIAGO";
                Cli.Correo = "B@B.CL";
                Cli.Telefono = "+56912345655";
                Cli.IdActividadEmpresa = 3;
                Cli.IdTipoEmpresa = 30;
            }
            var Resultado = Cli.Update();

            Assert.IsTrue(Resultado);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var Cli = new Contacto()
            {
                RutContacto = "22223",
            };
            var Resultado = Cli.Delete();

            Assert.IsTrue(Resultado);
        }
    }
}