using Microsoft.AspNetCore.Mvc;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
