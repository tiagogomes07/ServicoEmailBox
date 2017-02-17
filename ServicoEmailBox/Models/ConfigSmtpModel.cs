using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Web;
using ServicoEmailBox.DAL;

namespace ServicoEmailBox.Models
{
    //[Table("ConfigSmtp")]
    public class ConfigSmtpModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Porta { get; set; }
        public bool SSL { get; set; }
        public string Host { get; set; }
        public int DeliveryMethod { get; set; }
        public bool UseDefaultCredentials { get; set; }


        //public static SmtpClient conexaoGmail()
        //{
        //    SmtpClient client = new SmtpClient();
        //    client.Port = 587;
        //    client.Host = "smtp.gmail.com";
        //    client.EnableSsl = true;

        //    var smtp0 = SmtpDeliveryMethod.Network;
        //    var smtp1 = SmtpDeliveryMethod.PickupDirectoryFromIis;
        //    var smtp3 = SmtpDeliveryMethod.SpecifiedPickupDirectory;

        //    client.Timeout = 10000;
        //    client.DeliveryMethod = smtp0;
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = new System.Net.NetworkCredential("cpostal.box@gmail.com", "5ff3f546");
        //    return client;
        //}

        //Novo método para clientes SMTP, informar o nome do server SMTP, e o cliente remetente
        public static SmtpClient Conexao(string nomeSMTP, ServicoEmailRemetenteModel remetente)
        {
            Contexto DB = DbHelper.GetContext();
            var clienteSMTP = DB.ConfigSmtp.FirstOrDefault(x => x.Nome == nomeSMTP);


            SmtpClient client = new SmtpClient();
            client.Port = clienteSMTP.Porta;
            client.Host = clienteSMTP.Host;
            client.EnableSsl = clienteSMTP.SSL;


            client.Timeout = 10000;
            client.DeliveryMethod = (SmtpDeliveryMethod)clienteSMTP.DeliveryMethod;
            client.UseDefaultCredentials = clienteSMTP.UseDefaultCredentials;
            client.Credentials = new System.Net.NetworkCredential(remetente.Email, remetente.Password);
            return client;
        }

    }
}