using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServicoEmailBox.Models;
using System.Threading;

namespace UnitTestProject1
{
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [TestClass]
    public class UnitTest1
    {
        RegistroEmailModel email;

        [TestInitialize]
        public void Iniciar()
        {

        }


        [TestMethod]
        public void EnviarEmailCliente()
        {
            var hora = DateTime.Now;
            email = new RegistroEmailModel("Email Teste " + hora.ToString(),
            "Tiago",
            "",
            "",
            "ListaFotos",
            "pousadaCampos");
            email.enviar(email);
        }

        [TestMethod]
        public void EnviarEmailAvulso()
        {
            var hora = DateTime.Now;
            email = new RegistroEmailModel("Email Teste " + hora.ToString(),
            "Tiago",
            "cpostal.box@gmail.com",
            "cpostal.box@gmail.com",
            "Apenas um teste ",
            "Cliente aleatorio");
            email.enviar(email);
        }


        [TestMethod]
        public void EnviarEmailAvulsoThread()
        {
            for (int i = 0; i < 40; i++)
            {
                var hora = DateTime.Now;
                email = new RegistroEmailModel("Email Teste " + hora.ToString(),
                "Tiago",
                "cpostal.box@gmail.com",
                "cpostal.box@gmail.com",
                "Apenas um teste" + i + "de 40",
                "Cliente aleatorio");
                email.enviar(email);


            }


        }




        [TestMethod]
        public void SalvarBanco()
        {
            email.salvarBanco(email);
        }



    }
}
