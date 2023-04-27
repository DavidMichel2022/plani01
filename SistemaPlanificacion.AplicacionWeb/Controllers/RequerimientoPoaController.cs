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
        public async Task<IActionResult> EnviarDatos([FromForm] IFormFile ArchivoExcel)
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

                pl.CitePlanificacion = "POA2022";

              //  pl.NumeroPlanificacion = fila.GetCell(0).ToString();

                pl.IdDocumento = 3;

                pl.IdCentro = 1024;

               // pl.IdUnidadResponsable = "";

                pl.IdUsuario = int.Parse(idUsuario); 

               // pl.Lugar = "";

                //pl.CertificadoPoa = "";

                //pl.ReferenciaPlanificacion = "";

                pl.NombreRegional = fila.GetCell(7).ToString();

                pl.NombreEjecutora = fila.GetCell(6).ToString();

                //pl.MontoPlanificacion = "";

                pl.MontoPoa = Decimal.Parse(fila.GetCell(18).ToString());

                //pl.MontoPresupuesto = "";

                //pl.MontoCompra = "";

                //pl.UnidadProceso = "";

                pl.EstadoCarpeta = "INI";

                VMDetallePlanificacion detalle = new VMDetallePlanificacion();
                //detalle.IdPlanificacion = fila.GetCell(7).ToString();
                detalle.IdPartida = 1;// fila.GetCell(13).ToString(); 
                detalle.CodigoPartida = fila.GetCell(13).ToString();
                detalle.NombrePartida = fila.GetCell(13).ToString();
                //detalle.ProgramaPartida = fila.GetCell(7).ToString();
                detalle.NombreItem = fila.GetCell(14).ToString();
                detalle.Medida = fila.GetCell(15).ToString();
                detalle.Cantidad = int.Parse(fila.GetCell(16).ToString());
                detalle.Precio = decimal.Parse(fila.GetCell(17).ToString());
                detalle.Total = decimal.Parse(fila.GetCell(18).ToString());
                detalle.CodigoActividad = int.Parse(fila.GetCell(0).ToString()); // revisar
                detalle.Temporalidad = ""; // fila.GetCell(7).ToString();
                detalle.Observacion = fila.GetCell(32).ToString();

                detalle.Mes_Ene = decimal.Parse(fila.GetCell(20).ToString());
                detalle.Mes_Feb = decimal.Parse(fila.GetCell(21).ToString());
                detalle.Mes_Mar = decimal.Parse(fila.GetCell(22).ToString());
                detalle.Mes_Abr = decimal.Parse(fila.GetCell(23).ToString());
                detalle.Mes_May = decimal.Parse(fila.GetCell(24).ToString());
                detalle.Mes_Jun = decimal.Parse(fila.GetCell(25).ToString());
                detalle.Mes_Jul = decimal.Parse(fila.GetCell(26).ToString());
                detalle.Mes_Ago = decimal.Parse(fila.GetCell(27).ToString());
                detalle.Mes_Sep = decimal.Parse(fila.GetCell(28).ToString());
                detalle.Mes_Oct = decimal.Parse(fila.GetCell(29).ToString());
                detalle.Mes_Nov = decimal.Parse(fila.GetCell(30).ToString());
                detalle.Mes_Dic = decimal.Parse(fila.GetCell(31).ToString());
                pl.DetallePlanificacion.Add(detalle);

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
