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
using Firebase.Auth;
using System.Collections.Generic;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class RequerimientoPoaController : Controller
    {
        private readonly ILogger<RequerimientoPoaController> _logger;

        private readonly ICentrosaludService _centroSaludServicio;
        private readonly IPartidapresupuestariaService _partidaServicio;
        private readonly IUnidadResponsableService _unidadServicio;

        private readonly IRequerimientoPoaService _requerimientopoaServicio;

        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public RequerimientoPoaController(ILogger<RequerimientoPoaController> logger, ICentrosaludService centroSaludServicio, IPartidapresupuestariaService partidaServicio,
               IUnidadResponsableService unidadServicio, IRequerimientoPoaService requerimientopoaServicio, IMapper mapper, IConverter converter)
        {
            _logger = logger;
            _centroSaludServicio = centroSaludServicio;
            _partidaServicio = partidaServicio;
            _unidadServicio = unidadServicio;
            _requerimientopoaServicio = requerimientopoaServicio;
            _mapper = mapper;
            _converter = converter;
        }
        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string unidadResponsable = claimUser.Claims
                   .Where(c => c.Type == "NombreUnidadResponsable")
                   .Select(c => c.Value).SingleOrDefault();

            ViewBag.UnidadResponsable = unidadResponsable;
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

        public IActionResult ListadoRequerimientosPoa()
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
            List<VMRequerimientoPoa> vmRequerimientosLista = _mapper.Map<List<VMRequerimientoPoa>>(await _requerimientopoaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmRequerimientosLista);
        }

        [HttpGet]
        public async Task<IActionResult> ListaMisRequerimientosPoa()
        {
            List<VMRequerimientoPoa> vmListaRequerimientos = _mapper.Map<List<VMRequerimientoPoa>>(await _requerimientopoaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmListaRequerimientos });
        }

        [HttpGet]
        public async Task<IActionResult> ListaPoaMiUnidad()
        {
             try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string userUnidadResponsable = claimUser.Claims
                    .Where(c => c.Type == "IdUnidadResponsable")
                    .Select(c => c.Value).SingleOrDefault();
                int idUnidadResponsable = int.Parse(userUnidadResponsable);


                List<VMRequerimientoPoa> vmRequerimientosPoaLista = _mapper.Map<List<VMRequerimientoPoa>>(await _requerimientopoaServicio.ListaPoaMiUnidad(idUnidadResponsable));
                List<VMReporteRequerimientoPoa> lista = new();

                for (int i = 0; i < vmRequerimientosPoaLista.Count; i++)
                {
                    VMRequerimientoPoa reqPoaUnidad = vmRequerimientosPoaLista.ElementAt<VMRequerimientoPoa>(i);
                    List<VMDetalleRequerimientoPoa> listaDetalle = reqPoaUnidad.DetalleRequerimientoPoas.ToList<VMDetalleRequerimientoPoa>();
                    foreach (VMDetalleRequerimientoPoa detalle in listaDetalle)
                    {
                        VMReporteRequerimientoPoa reporte= new();
                        reporte.IdRequerimientoPoa = reqPoaUnidad.IdRequerimientoPoa;
                        reporte.IdUnidadResponsable = reqPoaUnidad.IdUnidadResponsable;
                        reporte.NombreUnidadResponsable = reqPoaUnidad.NombreUnidadResponsable;
                        reporte.IdUsuario = reqPoaUnidad.IdUsuario;
                        reporte.NombreUsuario = reqPoaUnidad.NombreUsuario;
                        reporte.IdCentro = reqPoaUnidad.IdCentro;
                        reporte.NombreCentro = reqPoaUnidad.NombreCentro;
                        reporte.FechaRequerimientoPoa = DateTime.Parse(reqPoaUnidad.FechaRequerimientoPoa);
                        reporte.CiteRequerimientoPoa = reqPoaUnidad.CiteRequerimientoPoa;
                        reporte.MontoPoa = reqPoaUnidad.MontoPoa;
                        reporte.EstadoRequerimientoPoa = reqPoaUnidad.EstadoRequerimientoPoa;
                        reporte.FechaAnulacion = reqPoaUnidad.FechaAnulacion;
                        reporte.Lugar = reqPoaUnidad.Lugar;
                        reporte.NombreRegional = reqPoaUnidad.NombreRegional;
                        reporte.NombreEjecutora = reqPoaUnidad.NombreEjecutora;
                        reporte.IdDetalleRequerimientoPoa = detalle.IdDetalleRequerimientoPoa;
                        reporte.IdPartida = detalle.IdPartida;
                        reporte.CodigoPartida = detalle.CodigoPartida.Trim();
                        reporte.NombrePartida = detalle.NombrePartida;
                        reporte.ProgramaPartida = detalle.ProgramaPartida;
                        reporte.Detalle = detalle.Detalle;
                        reporte.Medida = detalle.Medida;
                        reporte.Cantidad = detalle.Cantidad;
                        reporte.Precio = detalle.Precio;
                        reporte.Total = detalle.Total;
                        reporte.MesEne = detalle.MesEne;
                        reporte.MesFeb = detalle.MesFeb;
                        reporte.MesMar = detalle.MesMar;
                        reporte.MesAbr = detalle.MesAbr;
                        reporte.MesMay = detalle.MesMay;
                        reporte.MesJun = detalle.MesJun;
                        reporte.MesJul = detalle.MesJul;
                        reporte.MesAgo = detalle.MesAgo;
                        reporte.MesSep = detalle.MesSep;
                        reporte.MesOct = detalle.MesOct;
                        reporte.MesNov = detalle.MesNov;
                        reporte.MesDic = detalle.MesDic;
                        reporte.Observacion = detalle.Observacion;
                        reporte.CodigoActividad = detalle.CodigoActividad;
                        lista.Add(reporte);
                    }
                    
                }
                return StatusCode(StatusCodes.Status200OK, new { data = lista });
            }
            catch (Exception ex)
            {
                List<VMRequerimientoPoa> vmRequerimientosPoaLista = _mapper.Map<List<VMRequerimientoPoa>>(await _requerimientopoaServicio.Lista());               
                return StatusCode(StatusCodes.Status200OK, new { data = vmRequerimientosPoaLista });
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRequerimientosPoaMiUnidad()
        {
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string userUnidadResponsable = claimUser.Claims
                    .Where(c => c.Type == "IdUnidadResponsable")
                    .Select(c => c.Value).SingleOrDefault();
                int idUnidadResponsable = int.Parse(userUnidadResponsable);


                List<VMRequerimientoPoa> vmRequerimientosPoaLista = _mapper.Map<List<VMRequerimientoPoa>>(await _requerimientopoaServicio.ListaPoaMiUnidad(idUnidadResponsable));
                List<VMReporteRequerimientoPoa> lista = new();

                for (int i = 0; i < vmRequerimientosPoaLista.Count; i++)
                {
                    VMRequerimientoPoa reqPoaUnidad = vmRequerimientosPoaLista.ElementAt<VMRequerimientoPoa>(i);
                    List<VMDetalleRequerimientoPoa> listaDetalle = reqPoaUnidad.DetalleRequerimientoPoas.ToList<VMDetalleRequerimientoPoa>();
                    foreach (VMDetalleRequerimientoPoa detalle in listaDetalle)
                    {
                        VMReporteRequerimientoPoa reporte = new();
                        reporte.IdRequerimientoPoa = reqPoaUnidad.IdRequerimientoPoa;
                        reporte.IdUnidadResponsable = reqPoaUnidad.IdUnidadResponsable;
                        reporte.NombreUnidadResponsable = reqPoaUnidad.NombreUnidadResponsable;
                        reporte.IdUsuario = reqPoaUnidad.IdUsuario;
                        reporte.NombreUsuario = reqPoaUnidad.NombreUsuario;
                        reporte.IdCentro = reqPoaUnidad.IdCentro;
                        reporte.NombreCentro = reqPoaUnidad.NombreCentro;
                        reporte.FechaRequerimientoPoa = DateTime.Parse(reqPoaUnidad.FechaRequerimientoPoa);
                        reporte.CiteRequerimientoPoa = reqPoaUnidad.CiteRequerimientoPoa;
                        reporte.MontoPoa = reqPoaUnidad.MontoPoa;
                        reporte.EstadoRequerimientoPoa = reqPoaUnidad.EstadoRequerimientoPoa;
                        reporte.FechaAnulacion = reqPoaUnidad.FechaAnulacion;
                        reporte.Lugar = reqPoaUnidad.Lugar;
                        reporte.NombreRegional = reqPoaUnidad.NombreRegional;
                        reporte.NombreEjecutora = reqPoaUnidad.NombreEjecutora;
                        reporte.IdDetalleRequerimientoPoa = detalle.IdDetalleRequerimientoPoa;
                        reporte.IdPartida = detalle.IdPartida;
                        reporte.CodigoPartida = detalle.CodigoPartida.Trim();
                        reporte.NombrePartida = detalle.NombrePartida;
                        reporte.ProgramaPartida = detalle.ProgramaPartida;
                        reporte.Detalle = detalle.Detalle;
                        reporte.Medida = detalle.Medida;
                        reporte.Cantidad = detalle.Cantidad;
                        reporte.Precio = detalle.Precio;
                        reporte.Total = detalle.Total;
                        reporte.MesEne = detalle.MesEne;
                        reporte.MesFeb = detalle.MesFeb;
                        reporte.MesMar = detalle.MesMar;
                        reporte.MesAbr = detalle.MesAbr;
                        reporte.MesMay = detalle.MesMay;
                        reporte.MesJun = detalle.MesJun;
                        reporte.MesJul = detalle.MesJul;
                        reporte.MesAgo = detalle.MesAgo;
                        reporte.MesSep = detalle.MesSep;
                        reporte.MesOct = detalle.MesOct;
                        reporte.MesNov = detalle.MesNov;
                        reporte.MesDic = detalle.MesDic;
                        reporte.Observacion = detalle.Observacion;
                        reporte.CodigoActividad = detalle.CodigoActividad;
                        lista.Add(reporte);
                    }

                }
                return StatusCode(StatusCodes.Status200OK,lista );
            }
            catch (Exception ex)
            {
                List<VMRequerimientoPoa> vmRequerimientosPoaLista = _mapper.Map<List<VMRequerimientoPoa>>(await _requerimientopoaServicio.Lista());
                return StatusCode(StatusCodes.Status200OK, vmRequerimientosPoaLista );
            }

        }

        [HttpGet]
        public async Task<IActionResult> ListaCentrosalud()
        {
            List<VMCentroSalud> vmListaCentrosalud = _mapper.Map<List<VMCentroSalud>>(await _centroSaludServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaCentrosalud);
        }

        [HttpGet]
        public async Task<IActionResult> ListaUnidadResponsable()
        {
            List<VMUnidadResponsable> vmListaUnidadresponsable = _mapper.Map<List<VMUnidadResponsable>>(await _unidadServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaUnidadresponsable);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPartidasRequerimiento(string busqueda)
        {
            List<VMPartidaPresupuestaria> vmListaPartidas = _mapper.Map<List<VMPartidaPresupuestaria>>(await _requerimientopoaServicio.ObtenerPartidasRequerimiento(busqueda));
            return StatusCode(StatusCodes.Status200OK, vmListaPartidas);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRequerimientos(string citeRequerimientoPoa)
        {
            List<VMRequerimientoPoa> vmBusquedaRequerimientoPoa = _mapper.Map<List<VMRequerimientoPoa>>(await _requerimientopoaServicio.ObtenerRequerimientos(citeRequerimientoPoa));
            return StatusCode(StatusCodes.Status200OK, vmBusquedaRequerimientoPoa);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarRequerimientoPoa([FromBody] VMRequerimientoPoa modelo)
        {
            GenericResponse<VMRequerimientoPoa> gResponse = new();

            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                modelo.IdUsuario = int.Parse(idUsuario);

                RequerimientoPoa requerimientopoa_creada = await _requerimientopoaServicio.Registrar(_mapper.Map<RequerimientoPoa>(modelo));

                modelo = _mapper.Map<VMRequerimientoPoa>(requerimientopoa_creada);

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

            List<VMReporteRequerimientoPoa> lista = new();

            for(int i=1; i<=cantidadFila; i++)
            {
                IRow fila = HojaExcel.GetRow(i);
                lista.Add(new VMReporteRequerimientoPoa
                {
                     IdRequerimientoPoa= int.Parse(fila.GetCell(0).ToString()),
                     CiteRequerimientoPoa=i.ToString(),
                     CodigoPartida = fila.GetCell(13).ToString(),
                     Medida = fila.GetCell(15).ToString(),
                     NombreUnidadResponsable = fila.GetCell(7).ToString(),
                     Cantidad = decimal.Parse(fila.GetCell(16).ToString()),
                     Precio = decimal.Parse(fila.GetCell(17).ToString()),
                     MontoPoa = decimal.Parse(fila.GetCell(18).ToString()),
                     MesEne = decimal.Parse(fila.GetCell(20).ToString()),
                     MesFeb = decimal.Parse(fila.GetCell(21).ToString()),
                     MesMar = decimal.Parse(fila.GetCell(22).ToString()),
                     MesAbr = decimal.Parse(fila.GetCell(23).ToString()),
                     MesMay = decimal.Parse(fila.GetCell(24).ToString()),
                     MesJun = decimal.Parse(fila.GetCell(25).ToString()),
                     MesJul = decimal.Parse(fila.GetCell(26).ToString()),
                     MesAgo = decimal.Parse(fila.GetCell(27).ToString()),
                     MesSep = decimal.Parse(fila.GetCell(28).ToString()),
                     MesOct = decimal.Parse(fila.GetCell(29).ToString()),
                     MesNov = decimal.Parse(fila.GetCell(30).ToString()),
                     MesDic = decimal.Parse(fila.GetCell(31).ToString()),
                     Observacion = fila.GetCell(32).ToString(),
                     Detalle = fila.GetCell(14).ToString()
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
          
            List<VMRequerimientoPoa> lista = new();
            for (int i = 1; i <= cantidadFila; i++)
            {
                nroFila++;               
                IRow fila = HojaExcel.GetRow(i);
                VMRequerimientoPoa pl = new();
                //pl. = fila.GetCell(0).ToString();                
                pl.CiteRequerimientoPoa = Cite;
                var codUe = fila.GetCell(1).ToString();
                codUe = Regex.Match(codUe, @"\d+").Value;
                var codProg= fila.GetCell(2).ToString();
                codProg = Regex.Match(codProg, @"\d+").Value;
                var codProy = fila.GetCell(3).ToString();
                codProy = Regex.Match(codProy, @"\d+").Value;
                var codAct = fila.GetCell(4).ToString();
                codAct = Regex.Match(codAct, @"\d+").Value;
                var codigoCompuesto = codProg + "0" + codUe + codProy.Substring(1, 3) + codAct;
                CentroSalud cSalud = await _centroSaludServicio.ObtenerCentroByCodigo(codigoCompuesto);
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
                    pl.NombreUnidadResponsable = unidad.Nombre;
                }

                pl.IdUsuario = int.Parse(idUsuario);
                pl.Lugar = Lugar;  
                pl.FechaRequerimientoPoa = DateTime.Now.ToString();
                pl.NombreRegional = "Santa Cruz";
                pl.NombreEjecutora = "";
                pl.MontoPoa = Decimal.Parse(fila.GetCell(18).ToString());
                pl.EstadoRequerimientoPoa = "INI";
                VMDetalleRequerimientoPoa detalle = new();
                detalle.CodigoPartida = fila.GetCell(13).ToString();
                PartidaPresupuestaria partida= await _partidaServicio.ObtenerPartidaPresupuestariaByCodigo(detalle.CodigoPartida);
                if (partida != null)
                {
                    detalle.NombrePartida = partida.Nombre;
                    detalle.IdPartida = partida.IdPartida;
                }
                //detalle.ProgramaPartida = fila.GetCell(7).ToString(); ????
                detalle.Detalle = fila.GetCell(14).ToString();
                detalle.Medida = fila.GetCell(15).ToString();
                detalle.Cantidad = decimal.Parse(fila.GetCell(16).ToString());
                detalle.Precio = decimal.Parse(fila.GetCell(17).ToString());
                detalle.Total = decimal.Parse(fila.GetCell(18).ToString());
                detalle.CodigoActividad = int.Parse(fila.GetCell(11).ToString()); 
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

                pl.DetalleRequerimientoPoas.Add(detalle);

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
            List<VMRequerimientoPoa> listaOrdenada = lista.OrderBy(pl => pl.IdUnidadResponsable).ToList();
            int j = 0;
            var unidadResponsable = "";
            int nroCite = 0;
            VMRequerimientoPoa requerimientosPoa = new();
            foreach (var filaReq in listaOrdenada)
            {
                j++;                
                VMDetalleRequerimientoPoa vmPlaniTMP = filaReq.DetalleRequerimientoPoas.First();               
                if (unidadResponsable == filaReq.IdUnidadResponsable.ToString() && j != 1)
                {
                    //---Seguir acumulando Requerimiento a la Unidad Vigente---                   
                    VMDetalleRequerimientoPoa vmDetReqPoa = new();
                    VMDetalleRequerimientoPoa vmPlani = filaReq.DetalleRequerimientoPoas.First();
                    vmDetReqPoa.IdPartida = vmPlani.IdPartida;
                    vmDetReqPoa.Detalle = vmPlani.Detalle;
                    vmDetReqPoa.Medida = vmPlani.Medida;
                    vmDetReqPoa.Cantidad = vmPlani.Cantidad;
                    vmDetReqPoa.Precio = vmPlani.Precio;
                    vmDetReqPoa.Total = vmPlani.Total;
                    vmDetReqPoa.Observacion = vmPlani.Observacion;
                    vmDetReqPoa.CodigoActividad = vmPlani.CodigoActividad;
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
                    requerimientosPoa.DetalleRequerimientoPoas.Add(vmDetReqPoa);
                   
                }
                else if(unidadResponsable != filaReq.IdUnidadResponsable.ToString() || j==1){
                    // Grabando el anterior requerimiento
                    if (requerimientosPoa.DetalleRequerimientoPoas.Count > 0)
                    {
                        nroCite++;
                        string citePoa = Cite + "-" + nroCite.ToString();
                        requerimientosPoa.CiteRequerimientoPoa = citePoa;
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
                    requerimientosPoa.FechaRequerimientoPoa = filaReq.FechaRequerimientoPoa;
                    requerimientosPoa.CiteRequerimientoPoa = filaReq.CiteRequerimientoPoa;
                    requerimientosPoa.MontoPoa = filaReq.MontoPoa;
                    requerimientosPoa.EstadoRequerimientoPoa = filaReq.EstadoRequerimientoPoa;
                    requerimientosPoa.FechaAnulacion = filaReq.FechaAnulacion;
                    requerimientosPoa.Lugar = filaReq.Lugar;
                    requerimientosPoa.NombreRegional = filaReq.NombreRegional;
                    requerimientosPoa.NombreEjecutora = filaReq.NombreUnidadResponsable;
                    if (filaReq.DetalleRequerimientoPoas.Count > 0)
                    {                        
                        VMDetalleRequerimientoPoa vmDetReqPoa = new();
                        VMDetalleRequerimientoPoa vmPlani =filaReq.DetalleRequerimientoPoas.First();
                        vmDetReqPoa.IdPartida = vmPlani.IdPartida;
                        vmDetReqPoa.Detalle = vmPlani.Detalle;
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
                }
               
                Console.WriteLine("["+j.ToString()+"]--------------------------------->Unidad Responsable:"+unidadResponsable +"  --> "+filaReq.CiteRequerimientoPoa);
            }
            if (requerimientosPoa.DetalleRequerimientoPoas.Count >0)
            {
                nroCite++;
                string citePoa = Cite + "-" + nroCite.ToString();
                requerimientosPoa.CiteRequerimientoPoa = citePoa;
                RequerimientoPoa requerimiento_creada = await _requerimientopoaServicio.Crear(_mapper.Map<RequerimientoPoa>(requerimientosPoa));
            }

            return StatusCode(StatusCodes.Status200OK, new {mensaje="ok"});

        }

        public IActionResult MostrarPDFRequerimientoPoa(string citeRequerimiento)
        {
            string urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFRequerimientoPoa?citeRequerimientoPoa={citeRequerimiento}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        Page=urlPlantillaVista
                    }
                }
            };

            var archivoPDF = _converter.Convert(pdf);

            return File(archivoPDF, "application/pdf");
        }

        [HttpPut]
        public async Task<IActionResult> Anular([FromBody] VMRequerimientoPoa modelo)
        {
            GenericResponse<VMRequerimientoPoa> gResponse = new();

            try
            {
                RequerimientoPoa requerimientopoa_anulada = await _requerimientopoaServicio.Anular(_mapper.Map<RequerimientoPoa>(modelo));
                modelo = _mapper.Map<VMRequerimientoPoa>(requerimientopoa_anulada);
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
        public async Task<IActionResult> Editar([FromBody] VMRequerimientoPoa modelo)
        {
            GenericResponse<VMRequerimientoPoa> gResponse = new();

            try
            {
                RequerimientoPoa requerimientopoa_editada = await _requerimientopoaServicio.Editar(_mapper.Map<RequerimientoPoa>(modelo));
                modelo = _mapper.Map<VMRequerimientoPoa>(requerimientopoa_editada);
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
    }
}
