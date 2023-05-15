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
        private readonly IRequerimientoPoaService _requerimientopoaServicio;
        private readonly ICertificacionPlanificacionService _certificacionPlanificacionServicio;

        public PlantillaController(IMapper mapper, INegocioService negocioServicio, IPlanificacionService planificacionServicio, ICertificacionPlanificacionService certificacionPlanificacionServicio, IRequerimientoPoaService requerimientopoaServicio)
        {
            _mapper = mapper;
            _negocioServicio = negocioServicio;
            _planificacionServicio = planificacionServicio;
            _requerimientopoaServicio = requerimientopoaServicio;
            _certificacionPlanificacionServicio = certificacionPlanificacionServicio;
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

            modelo.Negocio = vmNegocio;
            modelo.Planificacion = vmPlanificacion;

            return View(modelo);
        }

        public async Task<IActionResult> PDFRequerimientoPoa(string citeRequerimiento)
        {
            VMRequerimientoPoa vmRequerimientoPoa = _mapper.Map<VMRequerimientoPoa>(await _requerimientopoaServicio.Detalle(citeRequerimiento));
            VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioServicio.Obtener());

            VMPDFRequerimientoPoa modelo = new VMPDFRequerimientoPoa();

            modelo.Negocio = vmNegocio;
            modelo.RequerimientoPoa = vmRequerimientoPoa;

            return View(modelo);
        }
        public async Task<IActionResult> PDFCertificacionPlanificacion(string numeroPlanificacion)
        {
            VMPlanificacion vmPlanificacion = _mapper.Map<VMPlanificacion>(await _planificacionServicio.Detalle(numeroPlanificacion));
            VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioServicio.Obtener());
            VMCertificacionPlanificacion vmCertificacionPlanificacion = _mapper.Map<VMCertificacionPlanificacion>(await _certificacionPlanificacionServicio.ObtenerCertificacion(vmPlanificacion.IdPlanificacion));


            VMPDFCertificacionPlanificacion modelo = new VMPDFCertificacionPlanificacion();

            modelo.negocio = vmNegocio;
            modelo.planificacion = vmPlanificacion;
            modelo.certificacionPlanificacion = vmCertificacionPlanificacion;
            return View(modelo);
        }

        public IActionResult RestablecerClave(string clave)
        {
            ViewData["Clave"] = clave;
            return View();
        }
    }
}
