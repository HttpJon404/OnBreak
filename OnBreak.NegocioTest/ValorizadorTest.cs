using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnBreak.Negocio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace OnBreak.NegocioTest
{
    [TestClass]
    public class ValorizadorTest
    {
        [TestMethod()]
        public void TestCalcularPersonalAdicionalNegativo()
        {
            float ValorBase = 3;
            int Asistentes = 5;
            int PersonalAdicional = -5;

            try
            {

                Valorizador CalculoEvento = new Valorizador();
                double resultado;
                resultado = CalculoEvento.CalcularValorEvento(ValorBase, Asistentes, PersonalAdicional);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "Personal adicional ingresado negativo. Valor Rechazado.");
                return;
            }
        }

        [TestMethod()]
        public void TestCalcularAsistentesNegativo()
        {
            float ValorBase = 5;
            int Asistentes = -2;
            int PersonalAdicional = 4;

            try
            {
                Valorizador CalculoEvento = new Valorizador();
                double resultado;
                resultado = CalculoEvento.CalcularValorEvento(ValorBase, Asistentes, PersonalAdicional);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "Asistentes ingresado negativo. Valor Rechazado.");
                return;
            }
        }

        [TestMethod()]
        public void TestCalcularValorBaseNegativo()
        {
            float ValorBase = -1;
            int Asistentes = 3;
            int PersonalAdicional = 5;

            try
            {
                Valorizador CalculoEvento = new Valorizador();
                double resultado;
                resultado = CalculoEvento.CalcularValorEvento(ValorBase, Asistentes, PersonalAdicional);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "Valor Base ingresado negativo. Valor Rechazado");
                return;
            }
        }

        [TestMethod()]
        public void TestValorBaseIgualCero()
        {
            float ValorBase = 0;
            int Asistentes = 4;
            int PersonalAdicional = 2;

            try
            {
                Valorizador CalculoEvento = new Valorizador();
                double resultado;
                resultado = CalculoEvento.CalcularValorEvento(ValorBase, Asistentes, PersonalAdicional);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "Valor Base ingresado es igual a 0. Valor Rechazado.");
                return;
            }
        }

        [TestMethod()]
        public void TestCalcularAsistentesSobreMaximaCapacidad()
        {
            float ValorBase = 10;
            int Asistentes = 305;
            int PersonalAdicional = 3;

            try
            {
                Valorizador CalculoEvento = new Valorizador();
                double resultado;
                resultado = CalculoEvento.CalcularValorEvento(ValorBase, Asistentes, PersonalAdicional);
            }
            catch(Exception ex)
            {
                StringAssert.Contains(ex.Message, "Asistentes ingresados superan el limite. Valor Rechazado.");
                return;
            }
        }

        [TestMethod()]
        public void TestCalcularAsistentesIgualCero()
        {
            float ValorBase = 9;
            int Asistentes = 0;
            int PersonalAdicional = 6;

            try
            {
                Valorizador CalculoEvento = new Valorizador();
                double resultado;
                resultado = CalculoEvento.CalcularValorEvento(ValorBase, Asistentes, PersonalAdicional);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "Asistentes ingresados igual a 0. Valor Rechazado.");
                return;
            }
        }

    }
}

