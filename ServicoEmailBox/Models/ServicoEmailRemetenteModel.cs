using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServicoEmailBox.Models
{
    //[Table("ServicoEmailRemetente")]
    public class ServicoEmailRemetenteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ConfigSmtpModel ID_SMTP { get; set; }        

    }
}