using Newtonsoft.Json;
using ServicoEmailBox.DAL;
using ServicoEmailBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ServicoEmailBox.Controllers
{
    public class DisparoEmailController : Controller
    {
        // GET: DisparoEmail
        public ActionResult Index()
        {
            ViewData["ListaEmail"] = DbHelper.GetContext().ServicoEmailRemente.ToList();

            return View();
        }


        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                RegistroEmailModel email = new RegistroEmailModel();
                email.emailRemetente = form["remetente"];
                email.destinatario = form["destinatario"];
                email.assunto = form["assunto"];
                email.mensagem = form["mensagem"];

                int repetir = 1;

                if (form["repetir"] != "")
                {
                    repetir = Convert.ToInt32(form["repetir"]);
                }

                repetir = Convert.ToInt32(form["repetir"]);

                for (int i = 0; i < repetir; i++)
                {
                    email.enviar(email);
                }

                email.salvarBanco(email);

                return View("Sucesso");

            }
            catch (Exception)
            {
                ViewData["ErroSolicitacao"] = "Falha de dentativa de envio";
                return View("Index");
            }

           
            
        }

        public ActionResult Sucesso()
        {
            ViewData["ListaEmail"] = DbHelper.GetContext().ServicoEmailRemente.ToList();

            return View();
        }


        public JsonResult getLista()
        {
            var lista = DbHelper.GetContext().ServicoEmailRemente.ToList();
            string json = JsonConvert.SerializeObject(lista);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

    }
}