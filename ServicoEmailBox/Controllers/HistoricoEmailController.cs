using ServicoEmailBox.DAL;
using ServicoEmailBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;

namespace ServicoEmailBox.Controllers
{
    public class HistoricoEmailController : Controller
    {
        

        // GET: HistoricoEmail
        public ActionResult Index()
        {
            using (Contexto Db = new Contexto())
            {

                return View(Db.RegistroEmail.ToList());


            }            
        }

        [HttpPost]
        public ActionResult ListaDeletar(int[] deletingList)
        {           

            RegistroEmailModel registroEmailModel;
            try
            {
                using (Contexto Db = new Contexto())
                {
                    foreach (var idDeletingItem in deletingList)
                    {
                        registroEmailModel = Db.RegistroEmail.Where(x => x.Id == idDeletingItem).First();

                        Db.RegistroEmail.Remove(registroEmailModel);

                    }

                    Db.SaveChanges();

                }
            }
            catch (Exception ex){
               
            }

            return RedirectToAction("Index");
        }


        public int qtdItens()
        {
            using (Contexto Db = new Contexto())
            {

                int iensqtd = Db.RegistroEmail.Count();

                return (iensqtd);
            }
        }

        public String ExibicaoPaginas(int[] arraPaginas)
        {
            using (Contexto Db = new Contexto())
            {
                int inicial = arraPaginas[0];
                int final = arraPaginas[1];

                dynamic lista;

                if (inicial == 1)
                {
                    var listaResumida = Db.RegistroEmail.Take(20).ToList();
                    lista = listaResumida;
                }
                else
                {
                    var listaResumida = Db.RegistroEmail.OrderBy( c => c.Id).Skip(inicial - 1).Take(final - inicial).ToList();
                    lista = listaResumida;
                }             

               
                

                var o = JsonConvert.SerializeObject(lista);

                 return o;

               // return RedirectToAction("Index");
            }
        }


    }
}