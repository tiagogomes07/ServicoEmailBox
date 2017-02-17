using ServicoEmailBox.DAL;
using ServicoEmailBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicoEmailBox.Controllers
{
    public class ConfigSmtpController : BaseController
    {
        Contexto DB = DbHelper.GetContext();

        // GET: ConfigSmtp
        public ActionResult Lista()
        {
            var Lista = DB.ConfigSmtp.ToList();
            return View(Lista);
        }

        // GET: ConfigSmtp/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            ConfigSmtpModel smtp = new ConfigSmtpModel();
            smtp.Nome = form["Nome"];
            smtp.Porta = Convert.ToInt32( form["Porta"]);

            //SSL
            if (form["SSL"] == "Sim")
            {
                smtp.SSL = true;
            }
            else if (form["SSL"] == "Não")
            {
                smtp.SSL = false;
            }
            
            smtp.Host = form["Host"];

            if (form["Tipo Delivery"] == "Network")
            {
                smtp.DeliveryMethod = 0;
            }
            else if (form["Tipo Delivery"] == "SpecifiedPickupDirectory")
            {
                smtp.DeliveryMethod = 1;
            }
            else if(form["Tipo Delivery"] == "PickupDirectoryFromIis")
            {
                smtp.DeliveryMethod = 2;
            }


            //
            if (form["UseDefaultCredentials"] == "Sim")
            {
                smtp.UseDefaultCredentials = true;
            }
            else if (form["UseDefaultCredentials"] == "Não")
            {
                smtp.UseDefaultCredentials = false;
            }
            

            try
            {
                DB.ConfigSmtp.Add(smtp);
                DB.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction("Lista");
            }
            catch
            {
                return View();
            }

            return RedirectToAction("Lista");
        }



        // GET: ConfigSmtp/Edit/5
        public ActionResult Edit(int id)
        {
            return View(DB.ConfigSmtp.FirstOrDefault(x => x.Id == id));
        }

        // POST: ConfigSmtp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            ConfigSmtpModel smtp = new ConfigSmtpModel();

            smtp.Nome = form["Nome"];
            smtp.Porta = Convert.ToInt32(form["Porta"]);

            //SSL
            if (form["SSL"] == "Sim")
            {
                smtp.SSL = true;
            }
            else if (form["SSL"] == "Não")
            {
                smtp.SSL = false;
            }

            smtp.Host = form["Host"];

            if (form["Tipo Delivery"] == "Network")
            {
                smtp.DeliveryMethod = 0;
            }
            else if (form["Tipo Delivery"] == "SpecifiedPickupDirectory")
            {
                smtp.DeliveryMethod = 1;
            }
            else if (form["Tipo Delivery"] == "PickupDirectoryFromIis")
            {
                smtp.DeliveryMethod = 2;
            }

            //
            if (form["UseDefaultCredentials"] == "Sim")
            {
                smtp.UseDefaultCredentials = true;
            }
            else if (form["UseDefaultCredentials"] == "Não")
            {
                smtp.UseDefaultCredentials = false;
            }


            try
            {
                var SMTP = from c in DB.ConfigSmtp where c.Id == id select c;
                var objetoSMTP = SMTP.FirstOrDefault();
                objetoSMTP.Nome = smtp.Nome;
                objetoSMTP.Porta = smtp.Porta;
                objetoSMTP.SSL = (smtp.SSL == true) ? true : false;
                objetoSMTP.Host = smtp.Host;
                objetoSMTP.DeliveryMethod = smtp.DeliveryMethod;
                objetoSMTP.UseDefaultCredentials = smtp.UseDefaultCredentials;


                DB.SaveChanges();


                return RedirectToAction("Lista");
            }
            catch
            {
                return View();
            }
        }

        // GET: ConfigSmtp/Delete/5
        public ActionResult Delete(int id)
        {
            var deletar = DB.ConfigSmtp.FirstOrDefault(x => x.Id == id);


            return View(deletar);
        }

        // POST: ConfigSmtp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var deletar = DB.ConfigSmtp.FirstOrDefault(x => x.Id == id);
                DB.ConfigSmtp.Remove(deletar);
                DB.SaveChanges();

                return RedirectToAction("Lista");
            }
            catch
            {
                return View();
            }
        }
    }
}
