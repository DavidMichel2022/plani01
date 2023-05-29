using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.Entity;
using System.Security.Claims;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class ModificacionPoaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IModificacionPoaService _modificacionPoaServicio;

        public ModificacionPoaController(IMapper mapper, IModificacionPoaService modificacionPoaServicio)
        {
            _mapper = mapper;
            _modificacionPoaServicio = modificacionPoaServicio;
        }
        public IActionResult Index()
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

    }
}
