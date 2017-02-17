using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using ServicoEmailBox.DAL;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace ServicoEmailBox.Models
{
    //[Table("RegistroEmail")]
    public class RegistroEmailModel
    {
        public int Id { get; set; }
        public string nomeRemetente { get; set; }
        public string emailRemetente { get; set; }
        public string destinatario { get; set; }        
        public string assunto { get; set; }
        public string mensagem { get; set; }
        public string nomeCliente { get; set; }
        public virtual ClienteEmailDestinatarioModel ID_destinatario { get; set; }

        SmtpClient smtpCliente = new SmtpClient();
        Contexto DB = DbHelper.GetContext();
        //ClienteEmailDestinatarioModel EmailDestino;



        public RegistroEmailModel()
        {
        }
        
        public RegistroEmailModel(string _Assunto, 
                    string _nomeRemetente, 
                    string _emailRemetetene, 
                    string _Destinatario, 
                    string _Menssagem,
                    string _nomeCliente)
        {            
            this.assunto = _nomeRemetente + ": " + _Assunto;
            this.nomeRemetente = _nomeRemetente;
            this.emailRemetente = _emailRemetetene;
            this.destinatario = _Destinatario;            
            this.mensagem = _Menssagem;
            this.nomeCliente = _nomeCliente;     
        }


        public void enviar(RegistroEmailModel email)
        {
            MailMessage mail = new MailMessage();

            /*
             A propriedade nomeCliente indica qual foi o site que fez a requisição.
             Através desta informação é feita uma busca no banco onde se recupera qual será o email remetente
             que fará o envio e qual sera o SMTP usado.
             */
            var emailClienteDestino = DB.ClienteEmailDestinario.FirstOrDefault(x => x.Nome == email.nomeCliente);

            if (emailClienteDestino != null)
            {
                mail.From = new MailAddress(emailClienteDestino.Remetente.Email.ToString());
                mail.To.Add(emailClienteDestino.Email.ToString());
                string nomeSMTP = emailClienteDestino.Remetente.ID_SMTP.Nome;
                smtpCliente = ConfigSmtpModel.Conexao(nomeSMTP, emailClienteDestino.Remetente);

            }
            else
            {
                
                var emailRemetente = DB.ServicoEmailRemente.Where( x => x.Email == email.emailRemetente).FirstOrDefault();
                
                
                //to do

                //Precisa ser um objeto do tipo MailAdress
                mail.From = new MailAddress(email.emailRemetente);


                //Precisa de uma configuração de SMTP
                //para pegar um smtp do banco é preciso do nomeSMTP que é a identificação dele e também de
                // um objeto do banco do clienteEmailDestino que é de onde vem a senha de acesso
                smtpCliente = ConfigSmtpModel.Conexao(emailRemetente.ID_SMTP.Nome, emailRemetente);


                string destino = email.destinatario;
                var ListaDestino = Regex.Split(destino, "; ").ToList();

                foreach (var item in ListaDestino)
                {
                    mail.To.Add(item);
                }                
            }                      
            mail.Subject = email.emailRemetente + "-" + email.nomeRemetente + "-" + email.assunto + "-" + System.DateTime.Now.ToString();
            mail.Body = email.mensagem;

            try
            {
                this.smtpCliente.Send(mail);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
            email.ID_destinatario = emailClienteDestino;
            
            
        }

        public void salvarBanco(RegistroEmailModel e)
        {
            
            
            //DB.Database.CreateIfNotExists();
            //var IDDestino = DB.ClienteEmailDestinario.FirstOrDefault(x => x.Nome == e.nomeCliente);
            //e.ID_destinatario = EmailDestino;
            DB.RegistroEmail.Add(e);
            DB.SaveChanges();
        }

    }
}