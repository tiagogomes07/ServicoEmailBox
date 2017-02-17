using ServicoEmailBox.DAL;
using ServicoEmailBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ServicoEmailBox.Controllers
{
    public class EmailRemetenteController : BaseController
    {
        Contexto DB = DbHelper.GetContext();

        // GET: EmailRemetente
        [HttpGet]
        public ActionResult Index()
        {                   

           return View(DB.ServicoEmailRemente.ToList());
        }


        // GET: EmailRemetente/Create
        public ActionResult Create()
        {
            ViewData["ListaSMTP"] = DB.ConfigSmtp.ToList();
            return View();
        }

        // POST: EmailRemetente/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ServicoEmailRemetenteModel EmailRemetente = new ServicoEmailRemetenteModel();
            try
            {
                EmailRemetente.Nome = collection["nome"];
                EmailRemetente.Email = collection["email"];
                EmailRemetente.Password = collection["Password"];

                string nomeServer = collection["Server"];
                var server = DB.ConfigSmtp.FirstOrDefault(x => x.Nome == nomeServer);
                EmailRemetente.ID_SMTP = server;

                DB.ServicoEmailRemente.Add(EmailRemetente);
                DB.SaveChanges();

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmailRemetente/Edit/5
        public ActionResult Edit(int id)
        {            
            ViewData["ListaSMTP"] = DB.ConfigSmtp.ToList();

            return View(DB.ServicoEmailRemente.FirstOrDefault(x => x.Id == id));
        }

        // POST: EmailRemetente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            ServicoEmailRemetenteModel EmailRemetente = new ServicoEmailRemetenteModel();
            EmailRemetente.Nome = collection["nome"];
            EmailRemetente.Email = collection["email"];
            EmailRemetente.Password = collection["Password"];
            string nomeServer = collection["Server"];
            var server = DB.ConfigSmtp.FirstOrDefault(x => x.Nome == nomeServer);
            EmailRemetente.ID_SMTP = server;

            try
            {
                var editar = DB.ServicoEmailRemente.FirstOrDefault(x => x.Id == id);
                editar.Nome = EmailRemetente.Nome;
                editar.Email = EmailRemetente.Email;
                editar.Password = EmailRemetente.Password;
                editar.ID_SMTP = EmailRemetente.ID_SMTP;
                DB.SaveChanges();


                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmailRemetente/Delete/5
        public ActionResult Delete(int id)
        {
            
            return View(DB.ServicoEmailRemente.FirstOrDefault(x => x.Id == id));
        }

        // POST: EmailRemetente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var deletar = DB.ServicoEmailRemente.FirstOrDefault(x => x.Id == id);
                DB.ServicoEmailRemente.Remove(deletar);
                DB.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
