using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.AplicacionWeb.Utilidades.Response;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.BLL.Implementacion;
using SistemaPlanificacion.Entity;
using AutoMapper;
using DinkToPdf;

using DinkToPdf.Contracts;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Linq;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class RequerimientoPoaController : Controller
    {
        private readonly ILogger<RequerimientoPoaController> _logger;

        private readonly ICentrosaludService _centroServicio;
        private readonly ITipodocumentoService _tipoDocumentoServicio;
        private readonly IPartidapresupuestariaService _partidaServicio;
        private readonly IUnidadResponsableService _unidadServicio;

        private readonly IRequerimientoPoaService _requerimientopoaServicio;

        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public RequerimientoPoaController(ILogger<RequerimientoPoaController> logger, ICentrosaludService centroServicio, ITipodocumentoService tipoDocumentoServicio, IPartidapresupuestariaService partidaServicio,
               IUnidadResponsableService unidadServicio, IRequerimientoPoaService requerimientopoaServicio, IMapper mapper, IConverter converter)
        {
            _logger = logger;
            _centroServicio = centroServicio;
            _tipoDocumentoServicio = tipoDocumentoServicio;
            _partidaServicio = partidaServicio;
            _unidadServicio = unidadServicio;
            _requerimientopoaServicio = requerimientopoaServicio;
            _mapper = mapper;
            _converter = converter;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NuevoRequerimientoPoa()
        {
            return View();
        }
        public IActionResult ExcelPoa()
        {
            return View();
        }
        public string ObtenerHora()
        {
            return DateTime.Now.Date.ToString("yyyy-MM-dd");
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMRequerimientoPoa> vmEmpresaLista = _mapper.Map<List<VMRequerimientoPoa>>(await _requerimientopoaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmEmpresaLista });
        }

        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumento()
        {
            List<VMTipoDocumento> vmListaTipoDocumentos = _mapper.Map<List<VMTipoDocumento>>(await _tipoDocumentoServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmListaTipoDocumentos });
        }

        [HttpGet]
        public async Task<IActionResult> ListaCentrosalud()
        {
            List<VMCentroSalud> vmListaCentrosalud = _mapper.Map<List<VMCentroSalud>>(await _centroServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmListaCentrosalud });
        }

        [HttpGet]
        public async Task<IActionResult> ListaUnidadResponsable()
        {
            List<VMUnidadResponsable> vmListaUnidadresponsable = _mapper.Map<List<VMUnidadResponsable>>(await _unidadServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmListaUnidadresponsable });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPartidas(string busqueda)
        {
            List<VMPartidaPresupuestaria> vmListaPartidas = _mapper.Map<List<VMPartidaPresupuestaria>>(await _requerimientopoaServicio.ObtenerPartidas(busqueda));
            return StatusCode(StatusCodes.Status200OK, new { data = vmListaPartidas });
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
                var codUe = fila.GetCell(1).ToString();
                codUe = Regex.Match(codUe, @"\d+").Value;
                var codProg= fila.GetCell(2).ToString();
                codProg = Regex.Match(codProg, @"\d+").Value;
                var codProy = fila.GetCell(3).ToString();
                codProy = Regex.Match(codProy, @"\d+").Value;
                var codAct = fila.GetCell(4).ToString();
                codAct = Regex.Match(codAct, @"\d+").Value;
                var codigoCompuesto = codProg + "0" + codUe + codProy.Substring(1, 3) + codAct;
                CentroSalud cSalud = await _centroServicio.ObtenerCentroByCodigo(codigoCompuesto);
                pl.IdCentro = 1032; // No existe el codigo asociado
                
                if (cSalud != null)
                {
                    pl.IdCentro = cSalud.IdCentro;
                }
                string codigoUnidadResponsable = fila.GetCell(6).ToString();
                codigoUnidadResponsable = Regex.Match(codigoUnidadResponsable, @"\d+").Value;
                UnidadResponsable unidad = await _unidadServicio.ObtenerUnidadResponsableByCodigo(codigoUnidadResponsable);
                if (unidad != null)
                {
                    pl.IdUnidadResponsable = unidad.IdUnidadResponsable;
                    //if (unidad.IdUnidadResponsable == 1004)
                    //{
                    //    var jk = 0;
                    //    jk=jk + 10;
                    //    jk = jk - 10;

                    //}
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
                detalle.MesEne = decimal.Parse(fila.GetCell(20).ToString());
                detalle.MesFeb = decimal.Parse(fila.GetCell(21).ToString());
                detalle.MesMar = decimal.Parse(fila.GetCell(22).ToString());
                detalle.MesAbr = decimal.Parse(fila.GetCell(23).ToString());
                detalle.MesMay = decimal.Parse(fila.GetCell(24).ToString());
                detalle.MesJun = decimal.Parse(fila.GetCell(25).ToString());
                detalle.MesJul = decimal.Parse(fila.GetCell(26).ToString());
                detalle.MesAgo = decimal.Parse(fila.GetCell(27).ToString());
                detalle.MesSep = decimal.Parse(fila.GetCell(28).ToString());
                detalle.MesOct = decimal.Parse(fila.GetCell(29).ToString());
                detalle.MesNov = decimal.Parse(fila.GetCell(30).ToString());
                detalle.MesDic = decimal.Parse(fila.GetCell(31).ToString());
                detalle.Observacion = fila.GetCell(0).ToString();

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
                //if (filaReq.IdUnidadResponsable == 1004)
                //{
                //    j = j + 1;
                //    j = j - 1;

                //}
                VMDetallePlanificacion vmPlaniTMP = filaReq.DetallePlanificacion.First();
                if (vmPlaniTMP.Observacion == "182")
                {
                    //j = j + 1;
                    //j = j - 1;

                    j++;
                    j--;

                }

                if (unidadResponsable == filaReq.IdUnidadResponsable.ToString() && j != 1)
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
                    vmDetReqPoa.MesEne = vmPlani.MesEne;
                    vmDetReqPoa.MesFeb = vmPlani.MesFeb;
                    vmDetReqPoa.MesMar = vmPlani.MesMar;
                    vmDetReqPoa.MesAbr = vmPlani.MesAbr;
                    vmDetReqPoa.MesMay = vmPlani.MesMay;
                    vmDetReqPoa.MesJun = vmPlani.MesJun;
                    vmDetReqPoa.MesJul = vmPlani.MesJul;
                    vmDetReqPoa.MesAgo = vmPlani.MesAgo;
                    vmDetReqPoa.MesSep = vmPlani.MesSep;
                    vmDetReqPoa.MesOct = vmPlani.MesOct;
                    vmDetReqPoa.MesNov = vmPlani.MesNov;
                    vmDetReqPoa.MesDic = vmPlani.MesDic;
                    vmDetReqPoa.Observacion = vmPlani.Observacion;
                    vmDetReqPoa.CodigoActividad = vmPlani.CodigoActividad;

                    requerimientosPoa.DetalleRequerimientoPoas.Add(vmDetReqPoa);
                   
                }
                else if(unidadResponsable != filaReq.IdUnidadResponsable.ToString() || j==1){
                    // Grabando el anterior requerimiento
                    if (requerimientosPoa.DetalleRequerimientoPoas.Count > 0)
                    {
                       RequerimientoPoa requerimiento_creada = await _requerimientopoaServicio.Crear(_mapper.Map<RequerimientoPoa>(requerimientosPoa));
                    }
                    //---Empezar un nuevo POA de Unidad
                    unidadResponsable = filaReq.IdUnidadResponsable.ToString();
                    requerimientosPoa = new VMRequerimientoPoa();
                    requerimientosPoa.IdUnidadResponsable = filaReq.IdUnidadResponsable;
                    if (unidadResponsable == "")
                        requerimientosPoa.IdUnidadResponsable = 1002;
                    requerimientosPoa.IdUsuario = filaReq.IdUsuario;
                    requerimientosPoa.IdCentro = filaReq.IdCentro;
                    requerimientosPoa.FechaRequerimientoPoa = DateTime.Parse(filaReq.FechaPlanificacion);
                    requerimientosPoa.CiteRequerimientoPoa = filaReq.CitePlanificacion;
                    requerimientosPoa.MontoPoa = filaReq.MontoPoa;
                    requerimientosPoa.EstadoRequerimientoPoa = filaReq.EstadoCarpeta;
                    requerimientosPoa.FechaAnulacion = filaReq.FechaAnulacion;
                    requerimientosPoa.Lugar = filaReq.Lugar;
                    requerimientosPoa.NombreRegional = filaReq.NombreRegional;
                    requerimientosPoa.NombreEjecutora = filaReq.NombreUnidadResponsable;
                    if (filaReq.DetallePlanificacion.Count > 0)
                    {                        
                        VMDetalleRequerimientoPoa vmDetReqPoa = new VMDetalleRequerimientoPoa();
                        VMDetallePlanificacion vmPlani =filaReq.DetallePlanificacion.First();
                        vmDetReqPoa.IdPartida = vmPlani.IdPartida;
                        vmDetReqPoa.Detalle = vmPlani.NombreItem;
                        vmDetReqPoa.Medida = vmPlani.Medida;
                        vmDetReqPoa.Cantidad = vmPlani.Cantidad;
                        vmDetReqPoa.Precio = vmPlani.Precio;
                        vmDetReqPoa.Total = vmPlani.Total;
                        vmDetReqPoa.MesEne =  vmPlani.MesEne;
                        vmDetReqPoa.MesFeb = vmPlani.MesFeb;
                        vmDetReqPoa.MesMar = vmPlani.MesMar;
                        vmDetReqPoa.MesAbr = vmPlani.MesAbr;
                        vmDetReqPoa.MesMay = vmPlani.MesMay;
                        vmDetReqPoa.MesJun = vmPlani.MesJun;
                        vmDetReqPoa.MesJul = vmPlani.MesJul;
                        vmDetReqPoa.MesAgo = vmPlani.MesAgo;
                        vmDetReqPoa.MesSep = vmPlani.MesSep;
                        vmDetReqPoa.MesOct = vmPlani.MesOct;
                        vmDetReqPoa.MesNov = vmPlani.MesNov;
                        vmDetReqPoa.MesDic = vmPlani.MesDic;
                        vmDetReqPoa.Observacion = vmPlani.Observacion;
                        vmDetReqPoa.CodigoActividad = vmPlani.CodigoActividad;
                        requerimientosPoa.DetalleRequerimientoPoas.Add(vmDetReqPoa);
                    }
                }
               
                Console.WriteLine("["+j.ToString()+"]--------------------------------->Unidad Responsable:"+unidadResponsable +"  --> "+filaReq.ReferenciaPlanificacion);
            }
            if (requerimientosPoa.DetalleRequerimientoPoas.Count >0)
            {
                RequerimientoPoa requerimiento_creada = await _requerimientopoaServicio.Crear(_mapper.Map<RequerimientoPoa>(requerimientosPoa));
            }
            //j = j + 5842;

            return StatusCode(StatusCodes.Status200OK, new {mensaje="ok"});

        }
    }
}
