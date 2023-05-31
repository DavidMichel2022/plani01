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

        public ModificacionPoaController(IMapper mapper, IModificacionPoaService modificacionPoaServicio, IModificacionRequerimientoService modificacionRequerimientoServicio)
        {
            _mapper = mapper;
            _modificacionPoaServicio = modificacionPoaServicio;
            _modificacionRequerimientoServicio = modificacionRequerimientoServicio;
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

    }
}
