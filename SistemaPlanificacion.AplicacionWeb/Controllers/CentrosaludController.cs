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
    public class CentrosaludController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICentrosaludService _centrosaludServicio;
        public CentrosaludController(IMapper mapper, ICentrosaludService centrosaludServicio)
        {
            _mapper = mapper;
            _centrosaludServicio = centrosaludServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMCentroSalud> vmCentroSaludLista = _mapper.Map<List<VMCentroSalud>>(await _centrosaludServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmCentroSaludLista });
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMCentroSalud modelo)
        {
            GenericResponse<VMCentroSalud> gResponse = new GenericResponse<VMCentroSalud>();

            try
            {
                CentroSalud centro_creado = await _centrosaludServicio.Crear(_mapper.Map<CentroSalud>(modelo));
                modelo = _mapper.Map<VMCentroSalud>(centro_creado);
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
        public async Task<IActionResult> Editar([FromBody] VMCentroSalud modelo)
        {
            GenericResponse<VMCentroSalud> gResponse = new GenericResponse<VMCentroSalud>();

            try
            {
                CentroSalud centro_editado = await _centrosaludServicio.Editar(_mapper.Map<CentroSalud>(modelo));
                modelo = _mapper.Map<VMCentroSalud>(centro_editado);
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
        public async Task<IActionResult> Eliminar(int idCentro)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {
                gResponse.Estado = await _centrosaludServicio.Eliminar(idCentro);

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
