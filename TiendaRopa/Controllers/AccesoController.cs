using Entidades;
using Negocio;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace TiendaRopa.Controllers
{
    public class AccesoController : Controller
    {
        NegocioUsuario _negocioUsuario = new NegocioUsuario();
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave() //Formulario para cambiar clave Con mail
        {
            return View();
        }

        public ActionResult RecuperarContra()//Verificacion de codigo enviado
        {
            return View();
        }

        public ActionResult Restablecer() //Formulario para completat nueva clave
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave) //Login
        {
            Usuario Usu = new Usuario();

            Usu = new NegocioUsuario().Listar().Where(u => u.EmailUsuario == correo && u.ContrasenaUsuario == NegocioRecursos.ConvertirSha64(clave)).FirstOrDefault();
            if (Usu == null)
            {
                ViewBag.Error = "Correo o contraseña incorrecta";
                return View();
            }
            else
            {
                if (Usu.RolUsuario)
                {
                    FormsAuthentication.SetAuthCookie(Usu.ContrasenaUsuario, false);//autentica al usuario
                    ViewBag.Error = null;//limpia el mensaje de error
                    return RedirectToAction("Index", "Home");//redirecciona a la vista principal del administrador

                }
                /*else
                {
                    //Vista CLIENTE
                    
                     FormsAuthentication.SetAuthCookie(Usu.ContrasenaUsuario, false);
                     ViewBag.Error = null;
                     return RedirectToAction("Index", "Tienda");
                     
                }*/
                return View();
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string correo) //Recuperar Contraseña con Mail
        {
            Usuario Usu = new NegocioUsuario().Listar().Where(u => u.EmailUsuario == correo).FirstOrDefault();

            if (Usu != null)
            {
                string codigo = GenerarCodigo();

                bool guardado = _negocioUsuario.GuardarCodigoRecuperacion(correo, codigo);//guardar codigo en una session

                if (guardado)
                {
                    bool enviado = NegocioRecursos.EnviarCorreo(correo, "Recuperación de contraseña", $"Tu código de recuperacion es: <b>{codigo}</b>");

                    if (enviado)
                    {
                        ViewBag.Error = null;//limpia el mensaje de error
                        Session["correoRecuperacion"] = correo;
                        return RedirectToAction("RecuperarContra", "Acceso");

                    }
                    else
                    {
                        ViewBag.Error = "Error al enviar el correo";
                    }
                }
            }
            else
            {
                ViewBag.Error = "Mail no encontrado";
            }

            return CambiarClave();
        }

        public static string GenerarCodigo(int length = 6)
        {
            var bytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            var code = new StringBuilder();
            foreach (var b in bytes)
                code.Append((b % 10).ToString());
            return code.ToString();
        }

        [HttpPost]

        public ActionResult RecuperarContra(string codigo)
{
    string correo = Session["correoRecuperacion"]?.ToString();

    int esValido = _negocioUsuario.VerificarCodigoRecuperacion(correo, codigo);
    if (esValido != 0)
    {
        return RedirectToAction("Restablecer", "Acceso");
    }
    else
    {
        ViewBag.Error = "Código incorrecto";
        return View();
    }
}


        [HttpPost]

        public ActionResult Restablecer(string actual, string nueva, string confirmarclave) //Formulario para completar nueva clave
        {
            //cambiar la clave
            Usuario Usu = new NegocioUsuario().Listar().Where(u => u.ContrasenaUsuario == actual).FirstOrDefault();//busca el usuario con la clave actual
            bool resultado = false;

            if (Usu == null)
            {
                ViewData["Vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta.";
                return View();

            }
            else if (nueva != confirmarclave)
            {
                ViewData["Vclave"] = actual;
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View();
            }

            ViewData["Vclave"] = "";//limpia el campo de la clave actual

            nueva = NegocioRecursos.ConvertirSha64(nueva);

            string mensaje = string.Empty;

            resultado = new NegocioUsuario().CambiarClave(Usu.IdUsuario, nueva, out mensaje);

            if (resultado)
            {
                ViewBag.Mensaje = "CambioExitoso";
                return View();
            }
            else
            {
                ViewBag.Error = mensaje;
            }

            return View();
        }

        public ActionResult Cerrarsesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }

    }
}