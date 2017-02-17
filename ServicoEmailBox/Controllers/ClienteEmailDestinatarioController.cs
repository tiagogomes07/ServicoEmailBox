using ServicoEmailBox.DAL;
using ServicoEmailBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicoEmailBox.Controllers;

namespace ServicoEmailBox.Controllers
{
    public class ClienteEmailDestinatarioController : BaseController
    {
        Contexto DB = DbHelper.GetContext();

        // GET:List
        public ActionResult Index()
        {
            
            return View(DB.ClienteEmailDestinario.ToList());
        }

        // GET:List
        public ActionResult Create()
        {
            ViewData["ListaRemetente"] = DB.ServicoEmailRemente.ToList();
            
            return View();
        }

        // POST: ClienteEmailDestinatario/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ViewData["ListaRemetente"] = DB.ServicoEmailRemente.ToList();
            ClienteEmailDestinatarioModel cli = new ClienteEmailDestinatarioModel();
            cli.Nome = collection["Nome"];
            cli.Email = collection["Email"];
            double numeroCel;
            try
            {
                numeroCel = Convert.ToDouble(collection["celular"].ToString());
            }
            catch (Exception)
            {
                numeroCel = 0;
                
            }

            
            cli.Celular = numeroCel;
            string nomeRemetente = collection["emailRemetente"];
            cli.Remetente = DB.ServicoEmailRemente.FirstOrDefault(x => x.Nome == nomeRemetente);



            try
            {
                DB.ClienteEmailDestinario.Add(cli);
                DB.SaveChanges();
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View("falhou");
            }
        }

        // GET: ClienteEmailDestinatario/Edit/5
        public ActionResult Edit(int ID)
        {   
            ViewData["ListaRemetente"] = DB.ServicoEmailRemente.ToList();

            return View(DB.ClienteEmailDestinario.FirstOrDefault(x => x.Id == ID));
        }

        // POST: ClienteEmailDestinatario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
            var cliEdit = DB.ClienteEmailDestinario.FirstOrDefault(x => x.Id == id);
            cliEdit.Nome = collection["Nome"];
            cliEdit.Email = collection["Email"];
            cliEdit.Celular = Convert.ToDouble( collection["Celular"]);
            string nomeRemetente = collection["nomeRemetente"];
            cliEdit.Remetente = DB.ServicoEmailRemente.FirstOrDefault(x => x.Nome == nomeRemetente);            


                DB.SaveChanges();
                ViewBag["mensagem"] = "Não foi possivel Salvar a edição";
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
                ViewBag["salvo"] = "Salvo com sucesso!";
            }
        }

        // GET: ClienteEmailDestinatario/Delete/5
        public ActionResult Delete(int id)
        {
            var clienteDeletar = DB.ClienteEmailDestinario.FirstOrDefault(x => x.Id ==id);

            return View(clienteDeletar);


        }

        // POST: ClienteEmailDestinatario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var clienteEdicao = DB.ClienteEmailDestinario.FirstOrDefault(x => x.Id == id);
                DB.ClienteEmailDestinario.Remove(clienteEdicao);
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
