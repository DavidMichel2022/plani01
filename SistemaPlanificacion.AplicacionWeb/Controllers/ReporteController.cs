using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.BLL.Interfaces;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class ReporteController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPlanificacionService _planificacionServicio;

        public ReporteController(IMapper mapper, IPlanificacionService planificacionServicio)
        {
            _mapper = mapper;
            _planificacionServicio = planificacionServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReportePlanificacion(string fechaInicio, string fechaFin)
        {
            List<VMReportePlanificacion> vmLista = _mapper.Map<List<VMReportePlanificacion>>(await _planificacionServicio.Reporte(fechaInicio, fechaFin));

            return StatusCode(StatusCodes.Status200OK, new { data = vmLista });
        }
    }
}
