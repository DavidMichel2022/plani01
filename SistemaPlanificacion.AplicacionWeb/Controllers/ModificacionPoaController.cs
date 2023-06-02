using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.AplicacionWeb.Utilidades.Response;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.Entity;
using System.Security.Claims;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class ModificacionPoaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IModificacionPoaService _modificacionPoaServicio;
        private readonly IModificacionRequerimientoService _modificacionRequerimientoServicio;
        private readonly IRequerimientoPoaService _requerimientoPoaServicio;

        public ModificacionPoaController(IMapper mapper, IModificacionPoaService modificacionPoaServicio, IModificacionRequerimientoService modificacionRequerimientoServicio, IRequerimientoPoaService requerimientoPoaServicio)
        {
            _mapper = mapper;
            _modificacionPoaServicio = modificacionPoaServicio;
            _modificacionRequerimientoServicio = modificacionRequerimientoServicio;
            _requerimientoPoaServicio = requerimientoPoaServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SolicitudModificacion()
        {
            return View();
        }
        public IActionResult ListaSolicitudModificacion()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string unidadResponsable = claimUser.Claims
                   .Where(c => c.Type == "NombreUnidadResponsable")
                   .Select(c => c.Value).SingleOrDefault();

            ViewBag.UnidadResponsable = unidadResponsable;
            return View();
        }

        public IActionResult ListaSolicitudModificacionAprobacion()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string unidadResponsable = claimUser.Claims
                   .Where(c => c.Type == "NombreUnidadResponsable")
                   .Select(c => c.Value).SingleOrDefault();

            ViewBag.UnidadResponsable = unidadResponsable;
            return View();
        }
        [HttpGet] public async Task<IActionResult> ListaMisModificacionesPoa()
        {
           List<ModificacionPoa> temp = await _modificacionPoaServicio.Lista();
           List<VMModificacionPoa> vmListaModificacionPoas= _mapper.Map<List<VMModificacionPoa>>(temp);
           return StatusCode(StatusCodes.Status200OK, new { data = vmListaModificacionPoas });
        }
        [HttpPost]
        public async Task<IActionResult> RegistrarModificacionPoa([FromBody] VMModificacionPoa modelo)
        {
            GenericResponse<VMModificacionPoa> gResponse = new();

            try
            {
                //ClaimsPrincipal claimUser = HttpContext.User;

                //string idUsuario = claimUser.Claims
                //    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                //    .Select(c => c.Value).SingleOrDefault();

                //modelo.IdUsuario = int.Parse(idUsuario);
                ClaimsPrincipal claimUser = HttpContext.User;
                string unidadResponsable = claimUser.Claims
                       .Where(c => c.Type == "IdUnidadResponsable")
                       .Select(c => c.Value).SingleOrDefault();

                int idUnidadResponsable = int.Parse(unidadResponsable);
                modelo.idUnidadResponsable = idUnidadResponsable;
                ModificacionPoa modificacionPoa_creada = await _modificacionPoaServicio.Crear(_mapper.Map<ModificacionPoa>(modelo));
                int idModificacionPoa = (int)modificacionPoa_creada.IdModificacionPoa;
                List<VMModificacionRequerimiento> listaVMModificados = modelo.DetalleModificados.ToList();

                foreach (VMModificacionRequerimiento vmModificados in listaVMModificados)
                {
                    ModificacionRequerimiento modificados = _mapper.Map<ModificacionRequerimiento>(vmModificados);
                    if (modificados != null)
                    {
                        modificados.IdModificacionPoa = idModificacionPoa;
                        _modificacionRequerimientoServicio.Crear(modificados);
                    }
                }
                modelo = _mapper.Map<VMModificacionPoa>(modificacionPoa_creada);

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
        [HttpPost]
        public async Task<IActionResult> AprobarModificacionPoa([FromBody] VMModificacionPoa modelo)
        {
            GenericResponse<VMModificacionPoa> gResponse = new();
            int idModificacionPoa = (int)modelo.IdModificacionPoa;
            try
            {
                //--Obtener Solicitud de Modificacion
                ModificacionPoa modificacionPoa = await _modificacionPoaServicio.ObtenerModificacionPoa(idModificacionPoa);
                List<DetalleModificacion> detalleModificacion = modificacionPoa.DetalleModificacions.ToList();
                int idUnidadResponsable = (int)modificacionPoa.idUnidadResponsable;
                //--Obtener Requerimiento Unidad Responsable
                RequerimientoPoa requerimientoPoa = await _requerimientoPoaServicio.ObtenerRequerimientoPoaUnidadReciente(idUnidadResponsable);
                //--Agregar Detalle Requerimiento a Requerimiento de Unidad Responsable
                int idRequerimientoPoa = requerimientoPoa.IdRequerimientoPoa;
                foreach (DetalleModificacion detalle in detalleModificacion)
                {
                    DetalleRequerimientoPoa detalleReqPoa = new DetalleRequerimientoPoa();
                    //detalleReqPoa.IdDetalleRequerimientoPoa=
                    detalleReqPoa.IdRequerimientoPoa = idRequerimientoPoa;
                    detalleReqPoa.IdPartida = detalle.IdPartida;
                    detalleReqPoa.Detalle = detalle.Detalle;
                    detalleReqPoa.Medida = detalle.Medida;
                    detalleReqPoa.Cantidad = detalle.Cantidad;
                    detalleReqPoa.Precio = detalle.Precio;
                    detalleReqPoa.Total = detalle.Total;
                    detalleReqPoa.MesEne = detalle.Total;
                    detalleReqPoa.MesFeb = detalle.MesFeb;
                    detalleReqPoa.MesMar = detalle.MesMar;
                    detalleReqPoa.MesAbr = detalle.MesAbr;
                    detalleReqPoa.MesMay = detalle.MesMay;
                    detalleReqPoa.MesJun = detalle.MesJun;
                    detalleReqPoa.MesJul = detalle.MesJul;
                    detalleReqPoa.MesAgo = detalle.MesAgo;
                    detalleReqPoa.MesSep = detalle.MesSep;
                    detalleReqPoa.MesOct = detalle.MesOct;
                    detalleReqPoa.MesNov = detalle.MesNov;
                    detalleReqPoa.MesDic = detalle.MesDic;
                    detalleReqPoa.Observacion = detalle.Observacion;
                    detalleReqPoa.CodigoActividad = detalle.CodigoActividad;
                    await _requerimientoPoaServicio.CrearDetalleRequerimiento(detalleReqPoa);
                }
                //--Actualizar Estado Solicitud Modificacion Poa
                modificacionPoa.Estado = "A";
                await _modificacionPoaServicio.Editar(modificacionPoa);
                //--Obtener Requerimientos a ser Modificados
                List<ModificacionRequerimiento> listaReqModificados = await _modificacionRequerimientoServicio.ListaModificadosSolicitud(idModificacionPoa);
                //--modificacionRequerimientoServicio.
                foreach (ModificacionRequerimiento modReq in listaReqModificados)
                {
                    int idDetalleRequerimientoPoa = modReq.IdDetalleRequerimientoPoa;
                    //--Obtener Detalle de Requerimiento
                    DetalleRequerimientoPoa det = await _requerimientoPoaServicio.ObtenerDetalleRequerimientoPoaUnidad(idDetalleRequerimientoPoa);
                    //--Actualizar Estado Requerimiento Poa Aprobados
                    det.Estado = "OBS";
                    await _requerimientoPoaServicio.ActualizarDetalleRequerimientoPoaUnidad(det);
                }
               

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
