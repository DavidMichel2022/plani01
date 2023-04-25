using Microsoft.AspNetCore.Mvc;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.Entity;
using AutoMapper;
using DinkToPdf.Contracts;
using System.Security.Claims;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class RequerimientoPoaController : Controller
    {
        private readonly ILogger<RequerimientoPoaController> _logger;

        private readonly IPlanificacionService _planificacionServicio;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public RequerimientoPoaController(ILogger<RequerimientoPoaController> logger, IPlanificacionService planificacionServicio, IMapper mapper, IConverter converter)
        {
            _logger = logger;
            _planificacionServicio = planificacionServicio;
              _mapper = mapper;
            _converter = converter;
            
    }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MostrarDatos([FromForm] IFormFile ArchivoExcel)
        {
            Stream stream = ArchivoExcel.OpenReadStream();
            IWorkbook MiExcel = null;

            if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
            {
                MiExcel = new XSSFWorkbook(stream);
            }
            else
            {
                MiExcel = new HSSFWorkbook(stream);
            }

            ISheet HojaExcel = MiExcel.GetSheetAt(0);
            int cantidadFila = HojaExcel.LastRowNum;

            List<VMRequerimientoPoa> lista = new List<VMRequerimientoPoa>();

            for(int i=1; i<=cantidadFila; i++)
            {
                IRow fila = HojaExcel.GetRow(i);
                lista.Add(new VMRequerimientoPoa
                {
                     numero = fila.GetCell(0).ToString(),
                     partida = fila.GetCell(1).ToString(),
                     detalle = fila.GetCell(2).ToString(),
                     unidad = fila.GetCell(3).ToString(),
                     cantidad = fila.GetCell(4).ToString(),
                     precioUnitario = fila.GetCell(5).ToString(),
                     precioTotal = fila.GetCell(6).ToString()
                });
            }

            return StatusCode( StatusCodes.Status200OK, lista);

        }
        [HttpPost]
        public async Task<IActionResult> EnviarDatosAsync([FromForm] IFormFile ArchivoExcel)
        {
            Stream stream = ArchivoExcel.OpenReadStream();
            IWorkbook MiExcel = null;

            if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
            {
                MiExcel = new XSSFWorkbook(stream);
            }
            else
            {
                MiExcel = new HSSFWorkbook(stream);
            }

            ClaimsPrincipal claimUser = HttpContext.User;

            string idUsuario = claimUser.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault();

            ISheet HojaExcel = MiExcel.GetSheetAt(0);
            int cantidadFila = HojaExcel.LastRowNum;


            List<VMPlanificacion> lista = new List<VMPlanificacion>();

            for (int i = 1; i <= cantidadFila; i++)
            {
                IRow fila = HojaExcel.GetRow(i);
                VMPlanificacion pl = new VMPlanificacion();

                pl.CitePlanificacion = "";

              //  pl.NumeroPlanificacion = fila.GetCell(0).ToString();

             //   pl.IdDocumento = "";

                pl.IdCentro = "";

                pl.IdUnidadResponsable = "";

                pl.IdUsuario = int.Parse(idUsuario); 

               // pl.Lugar = "";

                //pl.CertificadoPoa = "";

                //pl.ReferenciaPlanificacion = "";

                pl.NombreRegional = "";

                pl.NombreEjecutora = "";

                pl.MontoPlanificacion = "";

                pl.MontoPoa = "";

                pl.MontoPresupuesto = "";

                pl.MontoCompra = "";

                pl.UnidadProceso = "";

                pl.EstadoCarpeta = "";s
                
                lista.Add(pl);
                try
                {
                    Planificacion planificacion_creada = await _planificacionServicio.Registrar(_mapper.Map<Planificacion>(pl));
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { mensaje = "error lectura excel" });
                }
            }
           
            return StatusCode(StatusCodes.Status200OK, new {mensaje="ok"});

        }
    }
}
