using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.AplicacionWeb.Utilidades.Response;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.Entity;

using Microsoft.AspNetCore.Authorization;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    [Authorize]
    public class PartidapresupuestariaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPartidapresupuestariaService _partidapresupuestariaServicio;
        public PartidapresupuestariaController(IMapper mapper, IPartidapresupuestariaService partidapresupuestariaServicio)
        {
            _mapper = mapper;
            _partidapresupuestariaServicio = partidapresupuestariaServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMPartidaPresupuestaria> vmPartidaPresupuestariaLista = _mapper.Map<List<VMPartidaPresupuestaria>>(await _partidapresupuestariaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmPartidaPresupuestariaLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMPartidaPresupuestaria modelo)
        {
            GenericResponse<VMPartidaPresupuestaria> gResponse = new GenericResponse<VMPartidaPresupuestaria>();

            try
            {
                PartidaPresupuestaria partidapresupuestaria_creada = await _partidapresupuestariaServicio.Crear(_mapper.Map<PartidaPresupuestaria>(modelo));
                modelo = _mapper.Map<VMPartidaPresupuestaria>(partidapresupuestaria_creada);
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
        public async Task<IActionResult> Editar([FromBody] VMPartidaPresupuestaria modelo)
        {
            GenericResponse<VMPartidaPresupuestaria> gResponse = new GenericResponse<VMPartidaPresupuestaria>();

            try
            {
                PartidaPresupuestaria partidapresupuestaria_editada = await _partidapresupuestariaServicio.Editar(_mapper.Map<PartidaPresupuestaria>(modelo));
                modelo = _mapper.Map<VMPartidaPresupuestaria>(partidapresupuestaria_editada);
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
        public async Task<IActionResult> Eliminar(int idPartida)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {
                gResponse.Estado = await _partidapresupuestariaServicio.Eliminar(idPartida);

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