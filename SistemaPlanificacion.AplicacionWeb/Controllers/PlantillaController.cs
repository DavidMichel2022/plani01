using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.BLL.Interfaces;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class PlantillaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INegocioService _negocioServicio;
        private readonly IPlanificacionService _planificacionServicio;

        public PlantillaController(IMapper mapper, INegocioService negocioServicio, IPlanificacionService planificacionServicio)
        {
            _mapper = mapper;
            _negocioServicio = negocioServicio;
            _planificacionServicio = planificacionServicio; 
        }


        public IActionResult EnviarClave(string correo, string clave)
        {
            ViewData["Correo"] = correo;
            ViewData["Clave"] = clave;
            ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";
            return View();
        }

        public async Task<IActionResult> PDFPlanificacion(string numeroPlanificacion)
        {
            VMPlanificacion vmPlanificacion=_mapper.Map<VMPlanificacion>(await _planificacionServicio.Detalle(numeroPlanificacion));
            VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioServicio.Obtener());

            VMPDFPlanificacion modelo = new VMPDFPlanificacion();

            modelo.negocio = vmNegocio;
            modelo.planificacion = vmPlanificacion;

            return View(modelo);
        }

        public IActionResult RestablecerClave(string clave)
        {
            ViewData["Clave"] = clave;
            return View();
        }
    }
}
