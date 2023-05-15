using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.AplicacionWeb.Utilidades.Response;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using DinkToPdf;
using DinkToPdf.Contracts;
using SistemaPlanificacion.BLL.Implementacion;


namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    [Authorize]
    public class PlanificacionController : Controller
    {
        private readonly ITipodocumentoService _tipoDocumentoServicio;
        private readonly ICentrosaludService _centroSaludServicio;
        private readonly IPlanificacionService _planificacionServicio;
        private readonly ICertificacionPlanificacionService _certificacionPlanificacionServicio;
        private readonly IUnidadResponsableService _unidadResponsableServicio;

        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public PlanificacionController(ITipodocumentoService tipoDocumentoServicio, ICentrosaludService centroSaludServicio, IPlanificacionService planificacionServicio, ICertificacionPlanificacionService certificacionPlanificacionServicio, IUnidadResponsableService unidadResponsableServicio, IMapper mapper, IConverter converter)
        {
            _tipoDocumentoServicio = tipoDocumentoServicio;
            _centroSaludServicio = centroSaludServicio;
            _unidadResponsableServicio = unidadResponsableServicio;
            _planificacionServicio = planificacionServicio;
            _certificacionPlanificacionServicio = certificacionPlanificacionServicio;
            _mapper = mapper;
            _converter = converter;
        }

        public IActionResult NuevaPlanificacion()
        {
            return View();
        }
        public IActionResult HistorialPlanificacion()
        {
            return View();
        }
        public IActionResult ListadoPlanificacion()
        {
            return View();
        }
        public IActionResult ListadoCarpetas()
        {
            return View();
        }

        public IActionResult ListadoCarpetasUsuario()
        {
            return View();
        }

        public string ObtenerHora() {
            return DateTime.Now.Date.ToString("yyyy-MM-dd");
        }
        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumento()
        {
            List<VMTipoDocumento> vmListaTipoDocumentos = _mapper.Map<List<VMTipoDocumento>>(await _tipoDocumentoServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaTipoDocumentos);
        }

        [HttpGet]
        public async Task<IActionResult> ListaCentrosalud()
        {
            List<VMCentroSalud> vmListaCentrossalud = _mapper.Map<List<VMCentroSalud>>(await _centroSaludServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaCentrossalud);
        }

        [HttpGet]
        public async Task<IActionResult> ListaUnidadResponsable()
        {
            List<VMUnidadResponsable> vmListaUnidadesResponsables = _mapper.Map<List<VMUnidadResponsable>>(await _unidadResponsableServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaUnidadesResponsables);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPartidas(string busqueda)
        {
            List<VMPartidaPresupuestaria> vmListaPartidas = _mapper.Map<List<VMPartidaPresupuestaria>>(await _planificacionServicio.ObtenerPartidas(busqueda));
            return StatusCode(StatusCodes.Status200OK, vmListaPartidas);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPlanificacion([FromBody] VMPlanificacion modelo)
        {
            GenericResponse<VMPlanificacion> gResponse = new GenericResponse<VMPlanificacion>();

            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                modelo.IdUsuario = int.Parse(idUsuario);

                Planificacion planificacion_creada = await _planificacionServicio.Registrar(_mapper.Map<Planificacion>(modelo));

                modelo = _mapper.Map<VMPlanificacion>(planificacion_creada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch(Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje=ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMPlanificacion modelo)
        {
            GenericResponse<VMPlanificacion> gResponse = new GenericResponse<VMPlanificacion>();

            try
            {
                Planificacion planificacion_editada = await _planificacionServicio.Editar(_mapper.Map<Planificacion>(modelo));
                modelo = _mapper.Map<VMPlanificacion>(planificacion_editada);
                gResponse.Estado = true;
                gResponse.Objeto = modelo;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Anular([FromBody] VMPlanificacion modelo)
        {
            GenericResponse<VMPlanificacion> gResponse = new GenericResponse<VMPlanificacion>();

            try
            {
                Planificacion planificacion_anulada = await _planificacionServicio.Anular(_mapper.Map<Planificacion>(modelo));
                modelo = _mapper.Map<VMPlanificacion>(planificacion_anulada);
                gResponse.Estado = true;
                gResponse.Objeto = modelo;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idPlanificacion)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {
                gResponse.Estado = await _planificacionServicio.Eliminar(idPlanificacion);

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Historial(string numeroPlanificacion, string fechaInicio, string fechaFin)
        {
            List<VMPlanificacion> vmHistorialPlanificacion = _mapper.Map<List<VMPlanificacion>>(await _planificacionServicio.Historial(numeroPlanificacion, fechaInicio, fechaFin));

            return StatusCode(StatusCodes.Status200OK, vmHistorialPlanificacion);
        }

        public IActionResult MostrarPDFPlanificacion(string numeroPlanificacion)
        {
            string urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFPlanificacion?numeroPlanificacion={numeroPlanificacion}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        Page=urlPlantillaVista
                    }
                }
            };

            var archivoPDF = _converter.Convert(pdf);

            return File(archivoPDF,"application/pdf");
        }
        public IActionResult MostrarPDFCertificacionPlanificacion(string numeroPlanificacion)
        {
            string urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFCertificacionPlanificacion?numeroPlanificacion={numeroPlanificacion}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        Page=urlPlantillaVista
                    }
                }
            };

            var archivoPDF = _converter.Convert(pdf);

            return File(archivoPDF, "application/pdf");
        }
        [HttpGet]
        public async Task<IActionResult> ListaMisCarpetas()
        {
            List<VMPlanificacion> vmListaCarpetas = _mapper.Map<List<VMPlanificacion>>(await _planificacionServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmListaCarpetas });
        }

        [HttpGet]
        public async Task<IActionResult> ListaCarpetasxUsuario(int idUsuarioActivo)
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            string idUsuario = claimUser.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault();

            idUsuarioActivo = int.Parse(idUsuario);

            List<VMPlanificacion> vmListaCarpetas = _mapper.Map<List<VMPlanificacion>>(await _planificacionServicio.ListaCarpetasxUsuario(idUsuarioActivo));
            return StatusCode(StatusCodes.Status200OK, new { data = vmListaCarpetas });
        }

        [HttpGet]
        public async Task<IActionResult> ListCarpetasCertificarPlanificacion()
        {
            List<VMPlanificacion> vmListaCarpetas = _mapper.Map<List<VMPlanificacion>>(await _planificacionServicio.ListaCertificarPlanificacion());
            return StatusCode(StatusCodes.Status200OK, new { data = vmListaCarpetas });
        }
        [HttpGet]
        public async Task<IActionResult> CertificarPlanificacion(string numeroCarpeta)
        {
            VMPlanificacion vmCarpeta = _mapper.Map<VMPlanificacion>(await _planificacionServicio.Detalle(numeroCarpeta));
            VMCertificacionPlanificacion vmCertificacionPlanificacion = _mapper.Map<VMCertificacionPlanificacion>(await _certificacionPlanificacionServicio.ObtenerCertificacion(vmCarpeta.IdPlanificacion));
            VMPDFCertificacionPlanificacion modelo = new VMPDFCertificacionPlanificacion();
            modelo.planificacion = vmCarpeta;
            modelo.certificacionPlanificacion = vmCertificacionPlanificacion;

            return View(modelo);
        }
        [HttpGet]
        public async Task<IActionResult> EditarPlanificacion(string numeroCarpeta)
        {
            VMPlanificacion vmCarpeta = _mapper.Map<VMPlanificacion>(await _planificacionServicio.Detalle(numeroCarpeta));
            VMPDFPlanificacion modelo = new VMPDFPlanificacion();
            modelo.Planificacion = vmCarpeta;

            return View(modelo);
        }
        [HttpPost]
        public async Task<IActionResult> RegistrarCertificacionPlanificacion([FromBody] VMCertificacionPlanificacion modelo)
        {
            GenericResponse<VMCertificacionPlanificacion> gResponse = new GenericResponse<VMCertificacionPlanificacion>();

            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                modelo.IdUsuario = int.Parse(idUsuario);
                modelo.FechaRegistro = DateTime.Now;
                modelo.EstadoCertificacion = "INI";
                await _certificacionPlanificacionServicio.ActualizarEstadoCertificacionIdPlanificacion("OBS",modelo.IdPlanificacion);

                CertificacionPlanificacion planificacion_creada = await _certificacionPlanificacionServicio.Registrar(_mapper.Map<CertificacionPlanificacion>(modelo));

                modelo = _mapper.Map<VMCertificacionPlanificacion>(planificacion_creada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
