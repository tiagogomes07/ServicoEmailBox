using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicoEmailBox.DAL
{
    public class DbHelper
    {
        private static Contexto context;

        public static Contexto GetContext()
        {
            

            if (context == null  )
            {
                context = new Contexto();
                
            }

            var estado = context.Database.Connection.State.ToString();

            if (estado == "Closed")
            {
                context = new Contexto();
            }

            return context;
        }

    }
}