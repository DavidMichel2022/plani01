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
    public class ActividadController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IActividadService _actividadServicio;
        public ActividadController(IMapper mapper, IActividadService actividadServicio)
        {
            _mapper = mapper;
            _actividadServicio = actividadServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMActividad> vmActividadLista = _mapper.Map<List<VMActividad>>(await _actividadServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmActividadLista });
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody]VMActividad modelo)
        {
            GenericResponse<VMActividad> gResponse = new GenericResponse<VMActividad>();

            try
            {
                Actividad actividad_creada = await _actividadServicio.Crear(_mapper.Map<Actividad>(modelo));
                modelo = _mapper.Map<VMActividad>(actividad_creada);
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
        public async Task<IActionResult> Editar([FromBody] VMActividad modelo)
        {
            GenericResponse<VMActividad> gResponse = new GenericResponse<VMActividad>();

            try
            {
                Actividad actividad_editada = await _actividadServicio.Editar(_mapper.Map<Actividad>(modelo));
                modelo = _mapper.Map<VMActividad>(actividad_editada);
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
        public async Task<IActionResult> Eliminar(int idActividad)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {
                gResponse.Estado = await _actividadServicio.Eliminar(idActividad);

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
