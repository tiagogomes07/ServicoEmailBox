﻿using ServicoEmailBox.Reposiotiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicoEmailBox.Filtros
{
 [HandleError]
 [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,Inherited=true,AllowMultiple=true)]
public class AutorizacaoDeAcesso : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext FiltroDeContexto)
        {
            var Controller = FiltroDeContexto.ActionDescriptor.ControllerDescriptor.ControllerName;
            var Action = FiltroDeContexto.ActionDescriptor.ActionName;
            if (Controller != "Home" || Action != "Login")
            { if (RepositoriosUsuarios.VerificaSeOUsuarioEstaLogado() == null)
                { FiltroDeContexto.RequestContext.HttpContext.
                        Response.Redirect("/Home/Login?Url=" + 
                        FiltroDeContexto.HttpContext.Request.Url.LocalPath); } }
        }
    }
}