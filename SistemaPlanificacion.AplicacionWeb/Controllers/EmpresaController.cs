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
    public class EmpresaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaService _empresaServicio;

        public EmpresaController(IMapper mapper, IEmpresaService empresaServicio)
        {
            _mapper = mapper;
            _empresaServicio= empresaServicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMEmpresa> vmEmpresaLista = _mapper.Map<List<VMEmpresa>>(await _empresaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK,new {data=vmEmpresaLista});
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody]VMEmpresa modelo)
        {
            GenericResponse<VMEmpresa> gResponse = new GenericResponse<VMEmpresa>();

            try
            {
                Empresa empresa_creada=await _empresaServicio.Crear(_mapper.Map<Empresa>(modelo));
                modelo = _mapper.Map<VMEmpresa>(empresa_creada);
                gResponse.Estado = true;
                gResponse.Objeto = modelo;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje=ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK,gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMEmpresa modelo)
        {
            GenericResponse<VMEmpresa> gResponse = new GenericResponse<VMEmpresa>();

            try
            {
                Empresa empresa_editada = await _empresaServicio.Editar(_mapper.Map<Empresa>(modelo));
                modelo = _mapper.Map<VMEmpresa>(empresa_editada);
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
        public async Task<IActionResult> Eliminar(int idEmpresa)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {
                gResponse.Estado = await _empresaServicio.Eliminar(idEmpresa);

            }
            catch(Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
