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
    public class ProgramaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProgramaService _programaServicio;
        public ProgramaController(IMapper mapper, IProgramaService programaServicio)
        {
            _mapper = mapper;
            _programaServicio = programaServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMPrograma> vmProgramaLista = _mapper.Map<List<VMPrograma>>(await _programaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmProgramaLista });
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMPrograma modelo)
        {
            GenericResponse<VMPrograma> gResponse = new GenericResponse<VMPrograma>();

            try
            {
                Programa programa_creado = await _programaServicio.Crear(_mapper.Map<Programa>(modelo));
                modelo = _mapper.Map<VMPrograma>(programa_creado);
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
        public async Task<IActionResult> Editar([FromBody] VMPrograma modelo)
        {
            GenericResponse<VMPrograma> gResponse = new GenericResponse<VMPrograma>();

            try
            {
                Programa programa_editado = await _programaServicio.Editar(_mapper.Map<Programa>(modelo));
                modelo = _mapper.Map<VMPrograma>(programa_editado);
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
        public async Task<IActionResult> Eliminar(int idPrograma)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {
                gResponse.Estado = await _programaServicio.Eliminar(idPrograma);

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
