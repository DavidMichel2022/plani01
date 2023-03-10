using Microsoft.AspNetCore.Mvc;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class ReporteBaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
