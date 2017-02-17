using ServicoEmailBox.DAL;
using ServicoEmailBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ServicoEmailBox.Reposiotiro
{
    public class RepositoriosUsuarios
    {
        public static bool AutenticarUsuario(string Login, string Senha)
        {
            var SenhaCriptogragada = FormsAuthentication.
                HashPasswordForStoringInConfigFile(Senha, "sha1");

            try
            {
                using (Contexto DB = new Contexto())
                {
                    var QueryAutenticaUsuarios = DB.Usuarios.
                        Where(x => x.Login == Login && x.Senha == Senha).
                        SingleOrDefault();

                    if (QueryAutenticaUsuarios == null)
                    {
                        return false;                    }
                    else
                    {
                        RepositorioCookies.RegistraCookieAutenticacao(
                            QueryAutenticaUsuarios.Id);
                        return true;
                    }
                }                
            }catch(Exception)
            {

                return false;
            }
        }

        public static Usuarios RecuperaUsuarioPorID(long IDUsuario)
        {
            try { using (Contexto db = new Contexto())
                    { var Usuario = db.Usuarios.Where(u => u.Id == IDUsuario).
                        SingleOrDefault();
                    return Usuario;
                    }
                }
            catch (Exception)
            { return null;
              }
        }        

        public static Usuarios VerificaSeOUsuarioEstaLogado()
        {
            var Usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];

            if (Usuario == null)
            {
                return null;
            }
            else
            {
                long IDUsuario = Convert.ToInt64((Usuario.Values["Id"]));
                var UsuarioRetornado = RecuperaUsuarioPorID(IDUsuario);
                return UsuarioRetornado;

                //long IDUsuario = Convert.ToInt64(RepositorioCriptografia.Descriptografar(Usuario.Values["Id"]));
                //var UsuarioRetornado = RecuperaUsuarioPorID(IDUsuario); return UsuarioRetornado;
            }
        }

    }
}