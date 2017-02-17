using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServicoEmailBox.Models
{
    [Table("ClienteEmailDestino")]
    public class ClienteEmailDestinatarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public double Celular { get; set; }
        public virtual ServicoEmailRemetenteModel Remetente { get; set; }

    }
}