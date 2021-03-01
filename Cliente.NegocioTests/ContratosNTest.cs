using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnBreak.Negocio;

namespace Cliente.NegocioTests
{
   
    [TestClass()]
    public class ContratosNTest
    {

        [TestMethod()]
        public void CreateConTest()
        {
            var Con = new Contrato()
            {
            Numero = "201111111111",
            RutContacto = "22223",
            Creacion = DateTime.Now,
            Termino = DateTime.Now,
            FechaHoraInicio = DateTime.Now,
            FechaHoraTermino = DateTime.Now,
            Asistentes = 10,
            PersonalAdicional = 5,
            IdModalidad = "CB001",
            IdTipoEvento = 10,
            Realizado = false,
            ValorTotalContrato = 45,
            Observaciones = "registrado"
             };

            var Resultado = Con.Create();

            Assert.IsTrue(Resultado);

        }


        [TestMethod()]

        public void ReadConTest()


        {

            var Con = new Contrato()

            {
                Numero = "201111111111"
            };

            var Resultado = Con.Read();

            Assert.IsTrue(Resultado);

        }

        [TestMethod()]

        public void UpdateConTest()

        {


            var Con = new Contrato()

            {
                Numero = "201111111111"
            };


            string NuevoNumero = "201111111111";


            if (NuevoNumero == Con.Numero)

            {
                Con.Numero = "201111111111";
                Con.RutContacto = "22223";
                Con.Creacion = DateTime.Now;
                Con.Termino = DateTime.Now;
                Con.FechaHoraInicio = DateTime.Now;
                Con.FechaHoraTermino = DateTime.Now;
                Con.Asistentes = 5;
                Con.PersonalAdicional = 5;
                Con.IdModalidad = "CB001";
                Con.IdTipoEvento = 10;
                Con.Realizado = false;
                Con.ValorTotalContrato = 7;
                Con.Observaciones = "ejooo";
            }

            var resultado = Con.Update();

            Assert.IsTrue(resultado);
        }

        [TestMethod()]

        public void DeleteConTest()

        {
            var Con = new Contrato()

            {
                Numero = "201111111111"
            };

            var resultado = Con.Delete();

            Assert.IsTrue(resultado);

        }


        
    }
}

