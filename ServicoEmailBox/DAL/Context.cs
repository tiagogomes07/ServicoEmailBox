using ServicoEmailBox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Sql;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Web;


namespace ServicoEmailBox.DAL
{

    //
    //[DbConfigurationType(typeof())]

    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class Contexto : DbContext
    {
        public DbSet<RegistroEmailModel> RegistroEmail { get; set; }
        public DbSet<ConfigSmtpModel> ConfigSmtp { get; set; }
        public DbSet<ServicoEmailRemetenteModel> ServicoEmailRemente { get; set; }
        public DbSet<ClienteEmailDestinatarioModel> ClienteEmailDestinario { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }


        public Contexto() : base("name=StringRDS")
        {

        }

        public void CriarBanco()
        {
            //Entities DB = new Entities();
            this.Database.CreateIfNotExists();     
        }


    }

}