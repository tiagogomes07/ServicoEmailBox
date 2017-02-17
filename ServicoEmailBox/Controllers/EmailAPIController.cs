using ServicoEmailBox.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace ServicoEmailBox.Controllers
{
    public class EmailAPIController : ApiController
    {

        [HttpPost]
        [ResponseType(typeof(RegistroEmailModel))]
        public IHttpActionResult Post(RegistroEmailModel email)
        {
            RegistroEmailModel sender = new RegistroEmailModel();
            sender.enviar(email);
            sender.salvarBanco(email);
            return Ok(new
            {
                Result = "OK"
            });
        }



        [HttpGet]
        [ResponseType(typeof(RegistroEmailModel))]
        public IHttpActionResult Get()
        {
            return Ok("Get bem sucessido");
        }

    }


}
