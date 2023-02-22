using Microsoft.AspNetCore.Mvc;

using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.BLL.Implementacion;
using SistemaPlanificacion.Entity;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using SistemaPlanificacion.BLL.Interfaces;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;
        public AccesoController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult RestablecerClave()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMUsuarioLogin modelo)
        {
            Usuario usuario_encontrado = await _usuarioServicio.ObtenerPorCredenciales(modelo.Correo, modelo.Clave);
            if(usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No Se Encontraron Coincidencias";
                return View();
            }
            ViewData["Mensaje"] = null;

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.Nombre),
                new Claim(ClaimTypes.NameIdentifier, usuario_encontrado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuario_encontrado.IdRol.ToString()),
                new Claim("UrlFoto", usuario_encontrado.UrlFoto),
            }; 

            ClaimsIdentity claimsIdentity=new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = modelo.MantenerSesion
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]

        public async Task<IActionResult> RestablecerClave(VMUsuarioLogin modelo)
        {
            try
            {
                string urlPlantilllaCorreo = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/RestablecerClave?clave=[clave]";
                bool Resultado = await _usuarioServicio.RestablecerClave(modelo.Correo, urlPlantilllaCorreo);

                if (Resultado)
                {
                    ViewData["Mensaje"] = "Su Contraseña Fue Restablecida. Revise Su Correo";
                    ViewData["MensajeError"] = null;
                }
                else
                {
                    ViewData["MensajeError"] = "Tenemos Problemas. Por Favor Intentelo De Nuevo Mas Tarde.";
                    ViewData["Mensaje"] = null;
                }
            }
            catch(Exception ex)
            {
                ViewData["MensajeError"] = ex.Message;
                ViewData["Mensaje"] = null;
            }
            return View();
        }
    }
}
