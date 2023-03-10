using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.AplicacionWeb.Utilidades.Response;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    [Authorize]
    public class PlanificacionController : Controller
    {
        private readonly ITipodocumentoService _tipoDocumentoServicio;
        private readonly IActividadService _actividadServicio;
        private readonly IOperacionService _operacionServicio;
        private readonly IObjetivoService _objetivoServicio;
        private readonly ICentrosaludService _centroSaludServicio;
        private readonly ITablaaceService _tablaAceServicio;
        private readonly IPlanificacionService _planificacionServicio;
        private readonly IUnidadresponsableService _unidadResponsableServicio;
        private readonly IProgramaService _programaServicio;
        private readonly IMapper _mapper;

        public PlanificacionController(ITipodocumentoService tipoDocumentoServicio, IActividadService actividadServicio, IOperacionService operacionServicio, IObjetivoService objetivoServicio, ICentrosaludService centroSaludServicio, ITablaaceService tablaAceServicio, IPlanificacionService planificacionServicio, IUnidadresponsableService unidadResponsableServicio, IProgramaService programaServicio, IMapper mapper)
        {
            _tipoDocumentoServicio = tipoDocumentoServicio;
            _actividadServicio = actividadServicio;
            _operacionServicio = operacionServicio;
            _objetivoServicio = objetivoServicio;
            _centroSaludServicio = centroSaludServicio;
            _tablaAceServicio = tablaAceServicio;
            _unidadResponsableServicio = unidadResponsableServicio;
            _programaServicio = programaServicio;

            _planificacionServicio = planificacionServicio;
            _mapper = mapper;
        }

        public IActionResult NuevaPlanificacion()
        {
            return View();
        }
        public IActionResult Historial()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> HistorialPlanificacionAsync(string numeroPlanificacion, string fechaInicio, string fechaFin)
        {
            if (fechaInicio != null)
                fechaInicio = fechaInicio.Trim();
            if (fechaFin != null)
                fechaFin = fechaFin.Trim();
            var service = await _planificacionServicio.Historial(numeroPlanificacion, fechaInicio, fechaFin);
            List<VMPlanificacion> vmHistorialPlanificacion= _mapper.Map<List<VMPlanificacion>>(service);
            return StatusCode(StatusCodes.Status200OK, vmHistorialPlanificacion);

        }

        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumento()
        {
            List<VMTipoDocumento> vmListaTipoDocumentos = _mapper.Map<List<VMTipoDocumento>>(await _tipoDocumentoServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaTipoDocumentos);
        }

        [HttpGet]
        public async Task<IActionResult> ListaActividad()
        {
            List<VMActividad> vmListaActividades = _mapper.Map<List<VMActividad>>(await _actividadServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaActividades);
        }

        [HttpGet]
        public async Task<IActionResult> ListaOperacion()
        {
            List<VMOperacion> vmListaOperaciones = _mapper.Map<List<VMOperacion>>(await _operacionServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaOperaciones);
        }

        [HttpGet]
        public async Task<IActionResult> ListaObjetivo()
        {
            List<VMObjetivo> vmListaObjetivos = _mapper.Map<List<VMObjetivo>>(await _objetivoServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaObjetivos);
        }

        [HttpGet]
        public async Task<IActionResult> ListaCentrosalud()
        {
            List<VMCentroSalud> vmListaCentrossalud = _mapper.Map<List<VMCentroSalud>>(await _centroSaludServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaCentrossalud);
        }

        [HttpGet]
        public async Task<IActionResult> ListaTablaace()
        {
            List<VMTablaAce> vmListaTablaaces = _mapper.Map<List<VMTablaAce>>(await _tablaAceServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaTablaaces);
        }

        [HttpGet]
        public async Task<IActionResult> ListaUnidadresponsable()
        {
            List<VMUnidadResponsable> vmListaUnidadesResponsables = _mapper.Map<List<VMUnidadResponsable>>(await _unidadResponsableServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaUnidadesResponsables);
        }

        [HttpGet]
        public async Task<IActionResult> ListaPrograma()
        {
            List<VMPrograma> vmListaProgramas = _mapper.Map<List<VMPrograma>>(await _programaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaProgramas);
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
        
        [HttpGet]
        public async Task<IActionResult> Historial_Busqueda(string numeroPlanificacion, string fechaInicio, string fechaFin)
        {
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                if (fechaInicio != null)
                    fechaInicio = fechaInicio.Trim();
                if (fechaFin != null)
                    fechaFin = fechaFin.Trim();
                //var service = await _capetaService.Historial(numeroCarpeta, fechaInicio, fechaFin);
                //List<VMCarpetaRequerimiento> vmHistorialCarpeta = _mapper.Map<List<VMCarpetaRequerimiento>>(service);
                //return StatusCode(StatusCodes.Status200OK, vmHistorialCarpeta);
                List<VMPlanificacion> vmHistorialPlanificacion = _mapper.Map<List<VMPlanificacion>>(await _planificacionServicio.Historial(numeroPlanificacion, fechaInicio, fechaFin));
                return StatusCode(StatusCodes.Status200OK, vmHistorialPlanificacion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }



          
        }

    }
}
