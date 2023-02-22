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
    public class TipodocumentoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITipodocumentoService _tipodocumentoServicio;
        public TipodocumentoController(IMapper mapper, ITipodocumentoService tipodocumentoServicio)
        {
            _mapper = mapper;
            _tipodocumentoServicio = tipodocumentoServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMTipoDocumento> vmTipoDocumentoLista = _mapper.Map<List<VMTipoDocumento>>(await _tipodocumentoServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmTipoDocumentoLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMTipoDocumento modelo)
        {
            GenericResponse<VMTipoDocumento> gResponse = new GenericResponse<VMTipoDocumento>();

            try
            {
                TipoDocumento tipodocumento_creada = await _tipodocumentoServicio.Crear(_mapper.Map<TipoDocumento>(modelo));
                modelo = _mapper.Map<VMTipoDocumento>(tipodocumento_creada);
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
        public async Task<IActionResult> Editar([FromBody] VMTipoDocumento modelo)
        {
            GenericResponse<VMTipoDocumento> gResponse = new GenericResponse<VMTipoDocumento>();

            try
            {
                TipoDocumento tipodocumento_editada = await _tipodocumentoServicio.Editar(_mapper.Map<TipoDocumento>(modelo));
                modelo = _mapper.Map<VMTipoDocumento>(tipodocumento_editada);
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
        public async Task<IActionResult> Eliminar(int idDocumento)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {
                gResponse.Estado = await _tipodocumentoServicio.Eliminar(idDocumento);

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