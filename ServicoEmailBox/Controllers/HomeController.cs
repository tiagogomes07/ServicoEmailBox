using ServicoEmailBox.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicoEmailBox.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Login()
        {
            ViewBag.Title = "Seja bem vindo";
            Contexto Db = new Contexto();
            Db.Database.CreateIfNotExists();


            return View();
        }

        public ActionResult Index()
        {

            return View();
        }

    }
}
