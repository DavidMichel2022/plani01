using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    [Authorize]
    public class ContactosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
