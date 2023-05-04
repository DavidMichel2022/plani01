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
using System.Text.RegularExpressions;
using System.Linq;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class RequerimientoPoaController : Controller
    {
        private readonly ILogger<RequerimientoPoaController> _logger;

        //private readonly IPlanificacionService _planificacionServicio;
        private readonly IRequerimientoPoaService _requerimientoServicio;
        private readonly IPartidapresupuestariaService _partidaServicio;
        private readonly IUnidadResponsableService _unidadServicio;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public RequerimientoPoaController(ILogger<RequerimientoPoaController> logger, IRequerimientoPoaService requerimientoServicio,IPartidapresupuestariaService partidaServicio, IUnidadResponsableService unidadServicio,IMapper mapper, IConverter converter)
        {
                _logger = logger;
                _requerimientoServicio = requerimientoServicio;
                _mapper = mapper;
                _converter = converter;
                _partidaServicio = partidaServicio;
                _unidadServicio = unidadServicio;
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
                     IdRequerimientoPoa= int.Parse(fila.GetCell(0).ToString())//,
                     //partida = fila.GetCell(1).ToString(),
                     //detalle = fila.GetCell(2).ToString(),
                     //unidad = fila.GetCell(3).ToString(),
                     //cantidad = fila.GetCell(4).ToString(),
                     //precioUnitario = fila.GetCell(5).ToString(),
                     //precioTotal = fila.GetCell(6).ToString()
                });
            }

            return StatusCode( StatusCodes.Status200OK, lista);

        }
        [HttpPost]
        public async Task<IActionResult> EnviarDatos([FromForm] IFormFile ArchivoExcel, [FromForm] string Cite, [FromForm] string Lugar, [FromForm] string Fecha)
        {
            Stream stream = ArchivoExcel.OpenReadStream();
            IWorkbook MiExcel = null;

            //var request = HttpContext.Request.Body;

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
            int nroFila = 0;
            List<VMPlanificacion> lista = new List<VMPlanificacion>();
            for (int i = 1; i <= cantidadFila; i++)
            {
                nroFila++;               
                IRow fila = HojaExcel.GetRow(i);
                VMPlanificacion pl = new VMPlanificacion();
                pl.ReferenciaPlanificacion = fila.GetCell(0).ToString();
                pl.CitePlanificacion = Cite;
                //pl.IdCentro = 1024; // obtner centro o programa o proyec???
                string codigoUnidadResponsable = fila.GetCell(6).ToString();
                codigoUnidadResponsable = Regex.Match(codigoUnidadResponsable, @"\d+").Value;
                UnidadResponsable unidad = await _unidadServicio.ObtenerUnidadResponsableByCodigo(codigoUnidadResponsable);
                if (unidad != null)
                {
                    pl.IdUnidadResponsable = unidad.IdUnidadResponsable;
                }

                pl.IdUsuario = int.Parse(idUsuario);
                pl.Lugar = Lugar;  
                pl.FechaPlanificacion = Fecha;
                pl.NombreRegional = "Santa Cruz";
                pl.NombreEjecutora = "";
                pl.MontoPoa = Decimal.Parse(fila.GetCell(18).ToString());
                pl.EstadoCarpeta = "INI";
                VMDetallePlanificacion detalle = new VMDetallePlanificacion();
                detalle.CodigoPartida = fila.GetCell(13).ToString();
                PartidaPresupuestaria partida= await _partidaServicio.ObtenerPartidaPresupuestariaByCodigo(detalle.CodigoPartida);
                if (partida != null)
                {
                    detalle.NombrePartida = partida.Nombre;
                    detalle.IdPartida = partida.IdPartida;
                }
                //detalle.ProgramaPartida = fila.GetCell(7).ToString(); ????
                detalle.NombreItem = fila.GetCell(14).ToString();
                detalle.Medida = fila.GetCell(15).ToString();
                detalle.Cantidad = decimal.Parse(fila.GetCell(16).ToString());
                detalle.Precio = decimal.Parse(fila.GetCell(17).ToString());
                detalle.Total = decimal.Parse(fila.GetCell(18).ToString());
                detalle.CodigoActividad = int.Parse(fila.GetCell(11).ToString()); 
                detalle.Temporalidad = ""; // fila.GetCell(7).ToString();
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
                detalle.Observacion = fila.GetCell(32).ToString();

                pl.DetallePlanificacion.Add(detalle);

                lista.Add(pl);
                try
                {
                //    Planificacion planificacion_creada = await _planificacionServicio.Registrar(_mapper.Map<Planificacion>(pl));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("------ERROR-------"+nroFila.ToString());
                   // Console.WriteLine(ex.Message);
                    return StatusCode(StatusCodes.Status403Forbidden, new { mensaje = "error lectura excel en Fila: "+nroFila.ToString() });
                }

            }
            List<VMPlanificacion> listaOrdenada = lista.OrderBy(pl => pl.IdUnidadResponsable).ToList();
            int j = 0;
            var unidadResponsable = "";
            VMRequerimientoPoa requerimientosPoa = new VMRequerimientoPoa();
            foreach (var filaReq in listaOrdenada)
            {
                j++;
                
                if(unidadResponsable == filaReq.IdUnidadResponsable.ToString() && j != 1)
                {
                    //---Seguir acumulando Requerimiento a la Unidad Vigente---                   
                    VMDetalleRequerimientoPoa vmDetReqPoa = new VMDetalleRequerimientoPoa();
                    VMDetallePlanificacion vmPlani = filaReq.DetallePlanificacion.First();
                    vmDetReqPoa.IdPartida = vmPlani.IdPartida;
                    vmDetReqPoa.Detalle = vmPlani.NombreItem;
                    vmDetReqPoa.Medida = vmPlani.Medida;
                    vmDetReqPoa.Cantidad = vmPlani.Cantidad;
                    vmDetReqPoa.Precio = vmPlani.Precio;
                    vmDetReqPoa.Total = vmPlani.Total;
                    vmDetReqPoa.MesEne = vmPlani.Mes_Ene;
                    vmDetReqPoa.MesFeb = vmPlani.Mes_Feb;
                    vmDetReqPoa.MesMar = vmPlani.Mes_Mar;
                    vmDetReqPoa.MesAbr = vmPlani.Mes_Abr;
                    vmDetReqPoa.MesMay = vmPlani.Mes_May;
                    vmDetReqPoa.MesJun = vmPlani.Mes_Jun;
                    vmDetReqPoa.MesJul = vmPlani.Mes_Jul;
                    vmDetReqPoa.MesAgo = vmPlani.Mes_Ago;
                    vmDetReqPoa.MesSep = vmPlani.Mes_Sep;
                    vmDetReqPoa.MesOct = vmPlani.Mes_Oct;
                    vmDetReqPoa.MesNov = vmPlani.Mes_Nov;
                    vmDetReqPoa.MesDic = vmPlani.Mes_Dic;
                    vmDetReqPoa.Observacion = vmPlani.Observacion;
                    vmDetReqPoa.CodigoActividad = vmPlani.CodigoActividad;

                    requerimientosPoa.DetalleRequerimientoPoas.Add(vmDetReqPoa);
                   
                }
                else if(unidadResponsable != filaReq.IdUnidadResponsable.ToString() || j==1){
                    // Grabando el anterior requerimiento
                    if (requerimientosPoa.DetalleRequerimientoPoas.Count > 0)
                    {
                       RequerimientoPoa requerimiento_creada = await _requerimientoServicio.Crear(_mapper.Map<RequerimientoPoa>(requerimientosPoa));
                    }
                    //---Empezar un nuevo POA de Unidad
                    unidadResponsable = filaReq.IdUnidadResponsable.ToString();
                    requerimientosPoa = new VMRequerimientoPoa();
                    requerimientosPoa.IdUnidadResponsable = 60;// filaReq.IdUnidadResponsable;
                    requerimientosPoa.IdUsuario = filaReq.IdUsuario;
                    requerimientosPoa.IdCentro = 1005;// filaReq.IdCentro;
                    requerimientosPoa.FechaRequerimientoPoa = DateTime.Parse(filaReq.FechaPlanificacion);
                    requerimientosPoa.CiteRequerimientoPoa = filaReq.CitePlanificacion;
                    requerimientosPoa.MontoPoa = filaReq.MontoPoa;
                    requerimientosPoa.EstadoRequerimientoPoa = filaReq.EstadoCarpeta;
                    requerimientosPoa.FechaAnulacion = filaReq.FechaAnulacion;
                    requerimientosPoa.Lugar = filaReq.Lugar;
                    requerimientosPoa.NombreRegional = filaReq.NombreRegional;
                    requerimientosPoa.NombreEjecutora = filaReq.NombreEjecutora;
                    if (requerimientosPoa.DetalleRequerimientoPoas.Count > 0)
                    {                        
                        VMDetalleRequerimientoPoa vmDetReqPoa = new VMDetalleRequerimientoPoa();
                        VMDetallePlanificacion vmPlani =filaReq.DetallePlanificacion.First();
                        vmDetReqPoa.IdPartida = vmPlani.IdPartida;
                        vmDetReqPoa.Detalle = vmPlani.NombreItem;
                        vmDetReqPoa.Medida = vmPlani.Medida;
                        vmDetReqPoa.Cantidad = vmPlani.Cantidad;
                        vmDetReqPoa.Precio = vmPlani.Precio;
                        vmDetReqPoa.Total = vmPlani.Total;
                        vmDetReqPoa.MesEne =  vmPlani.Mes_Ene;
                        vmDetReqPoa.MesFeb = vmPlani.Mes_Feb;
                        vmDetReqPoa.MesMar = vmPlani.Mes_Mar;
                        vmDetReqPoa.MesAbr = vmPlani.Mes_Abr;
                        vmDetReqPoa.MesMay = vmPlani.Mes_May;
                        vmDetReqPoa.MesJun = vmPlani.Mes_Jun;
                        vmDetReqPoa.MesJul = vmPlani.Mes_Jul;
                        vmDetReqPoa.MesAgo = vmPlani.Mes_Ago;
                        vmDetReqPoa.MesSep = vmPlani.Mes_Sep;
                        vmDetReqPoa.MesOct = vmPlani.Mes_Oct;
                        vmDetReqPoa.MesNov = vmPlani.Mes_Nov;
                        vmDetReqPoa.MesDic = vmPlani.Mes_Dic;
                        vmDetReqPoa.Observacion = vmPlani.Observacion;
                        vmDetReqPoa.CodigoActividad = vmPlani.CodigoActividad;
                        requerimientosPoa.DetalleRequerimientoPoas.Add(vmDetReqPoa);
                    }
                }
                //-------------------------------------------------------
                if (requerimientosPoa.DetalleRequerimientoPoas.Count == j)
                {
                    RequerimientoPoa requerimiento_creada = await _requerimientoServicio.Crear(_mapper.Map<RequerimientoPoa>(requerimientosPoa));
                }
                Console.WriteLine("["+j.ToString()+"]--------------------------------->Unidad Responsable:"+unidadResponsable +"  --> "+filaReq.ReferenciaPlanificacion);
            }
            j = j + 5842;

            return StatusCode(StatusCodes.Status200OK, new {mensaje="ok"});

        }
    }
}
