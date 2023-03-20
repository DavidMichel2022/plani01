using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.BLL.Interfaces;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class ReporteBaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPlanificacionService _planificacionServicio;

        public ReporteBaseController(IMapper mapper, IPlanificacionService planificacionServicio)
        {
            _mapper = mapper;
            _planificacionServicio = planificacionServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReporteVenta(string fechaInicio, string fechaFin)
        {
            List<VMReportePlanificacion> vmLista = _mapper.Map<List<VMReportePlanificacion>>(await _planificacionServicio.Reporte(fechaInicio, fechaFin));

            return StatusCode(StatusCodes.Status200OK, new {data=vmLista});
        }

    }
}
