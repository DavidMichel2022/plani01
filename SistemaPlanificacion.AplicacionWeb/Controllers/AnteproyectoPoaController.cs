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
using System.Xml.Linq;
using NPOI.SS.Formula.Functions;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class AnteproyectoPoaController : Controller
    {
        private readonly ILogger<AnteproyectoPoaController> _logger;

        private readonly ICentrosaludService _centroSaludServicio;
        private readonly IPartidapresupuestariaService _partidaServicio;
        private readonly IUnidadResponsableService _unidadServicio;

        private readonly IAnteproyectoPoaService _anteproyectopoaServicio;

        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public AnteproyectoPoaController(ILogger<AnteproyectoPoaController> logger, ICentrosaludService centroSaludServicio, IPartidapresupuestariaService partidaServicio, IUnidadResponsableService unidadServicio, IAnteproyectoPoaService anteproyectopoaServicio, IMapper mapper, IConverter converter)
        {
            _logger = logger;
            _centroSaludServicio = centroSaludServicio;
            _partidaServicio = partidaServicio;
            _unidadServicio = unidadServicio;
            _anteproyectopoaServicio = anteproyectopoaServicio;
            _mapper = mapper;
            _converter = converter;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string unidadResponsable = claimUser.Claims.Where(c => c.Type == "NombreUnidadResponsable").Select(c => c.Value).SingleOrDefault();

            ViewBag.UnidadResponsable = unidadResponsable;
            return View();
        }

        public IActionResult NuevoAnteproyectoPoa()
        {
            return View();
        }

        public IActionResult ExcelPoa()
        {
            return View();
        }
        public IActionResult ListadoAnteproyectosPoa()
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
            List<VMAnteproyectoPoa> vmAnteproyectosLista = _mapper.Map<List<VMAnteproyectoPoa>>(await _anteproyectopoaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmAnteproyectosLista);
        }

        [HttpGet]
        public async Task<IActionResult> ListaMisAnteproyectosPoa()
        {
            List<VMAnteproyectoPoa> vmListaAnteproyectos = _mapper.Map<List<VMAnteproyectoPoa>>(await _anteproyectopoaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmListaAnteproyectos });
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


                List<VMAnteproyectoPoa> vmAnteproyectosPoaLista = _mapper.Map<List<VMAnteproyectoPoa>>(await _anteproyectopoaServicio.ListaPoaMiUnidad(idUnidadResponsable));
                List<VMReporteAnteproyectoPoa> lista = new();

                for (int i = 0; i < vmAnteproyectosPoaLista.Count; i++)
                {
                    VMAnteproyectoPoa antPoaUnidad = vmAnteproyectosPoaLista.ElementAt<VMAnteproyectoPoa>(i);
                    List<VMDetalleAnteproyectoPoa> listaDetalle = antPoaUnidad.DetalleAnteproyectoPoas.ToList<VMDetalleAnteproyectoPoa>();
                    foreach (VMDetalleAnteproyectoPoa detalle in listaDetalle)
                    {
                        VMReporteAnteproyectoPoa reporte = new();
                        reporte.IdAnteproyecto = antPoaUnidad.IdAnteproyecto;
                        reporte.IdUnidadResponsable = antPoaUnidad.IdUnidadResponsable;
                        reporte.NombreUnidadResponsable = antPoaUnidad.NombreUnidadResponsable;
                        reporte.IdUsuario = antPoaUnidad.IdUsuario;
                        reporte.NombreUsuario = antPoaUnidad.NombreUsuario;
                        reporte.IdCentro = antPoaUnidad.IdCentro;
                        reporte.NombreCentro = antPoaUnidad.NombreCentro;
                        reporte.FechaAnteproyecto = DateTime.Parse(antPoaUnidad.FechaAnteproyecto);
                        reporte.CiteAnteproyecto = antPoaUnidad.CiteAnteproyecto;
                        reporte.MontoAnteproyecto = antPoaUnidad.MontoAnteproyecto;
                        reporte.EstadoAnteproyecto = antPoaUnidad.EstadoAnteproyecto;
                        reporte.FechaAnulacion = antPoaUnidad.FechaAnulacion;
                        reporte.Lugar = antPoaUnidad.Lugar;
                        reporte.NombreRegional = antPoaUnidad.NombreRegional;
                        reporte.NombreEjecutora = antPoaUnidad.NombreEjecutora;
                        reporte.IdDetalleAnteproyecto = detalle.IdDetalleAnteproyecto;
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
            catch 
            {
                List<VMAnteproyectoPoa> vmAnteproyectosPoaLista = _mapper.Map<List<VMAnteproyectoPoa>>(await _anteproyectopoaServicio.Lista());
                return StatusCode(StatusCodes.Status200OK, new { data = vmAnteproyectosPoaLista });
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
        public async Task<IActionResult> ObtenerPartidasAnteproyecto(string busqueda)
        {
            List<VMPartidaPresupuestaria> vmListaPartidas = _mapper.Map<List<VMPartidaPresupuestaria>>(await _anteproyectopoaServicio.ObtenerPartidasAnteproyecto(busqueda));
            return StatusCode(StatusCodes.Status200OK, vmListaPartidas);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAnteproyectos(string citeAnteproyecto)
        {
            List<VMAnteproyectoPoa> vmBusquedaAnteproyectoPoa = _mapper.Map<List<VMAnteproyectoPoa>>(await _anteproyectopoaServicio.ObtenerAnteproyectos(citeAnteproyecto));
            return StatusCode(StatusCodes.Status200OK, vmBusquedaAnteproyectoPoa);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarAnteproyectoPoa([FromBody] VMAnteproyectoPoa modelo)
        {
            GenericResponse<VMAnteproyectoPoa> gResponse = new();

            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                modelo.IdUsuario = int.Parse(idUsuario);

                AnteproyectoPoa anteproyectopoa_creada = await _anteproyectopoaServicio.Registrar(_mapper.Map<AnteproyectoPoa>(modelo));

                modelo = _mapper.Map<VMAnteproyectoPoa>(anteproyectopoa_creada);

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

            List<VMReporteAnteproyectoPoa> lista = new();

            for (int i = 1; i <= cantidadFila; i++)
            {
                IRow fila = HojaExcel.GetRow(i);
                lista.Add(new VMReporteAnteproyectoPoa
                {
                    //IdAnteproyecto = int.Parse(fila.GetCell(0).ToString()),
                    IdAnteproyecto = 0,
                    CiteAnteproyecto = i.ToString(),
                    CodigoPartida = fila.GetCell(13).ToString(),
                    Medida = fila.GetCell(15).ToString(),
                    NombreUnidadResponsable = fila.GetCell(7).ToString(),
                    Cantidad = decimal.Parse(fila.GetCell(16).ToString()),
                    Precio = decimal.Parse(fila.GetCell(17).ToString()),
                    MontoAnteproyecto = decimal.Parse(fila.GetCell(18).ToString()),
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

            return StatusCode(StatusCodes.Status200OK, lista);

        }

        [HttpPost]
        public async Task<IActionResult> EnviarDatos([FromForm] IFormFile ArchivoExcel, [FromForm] string Cite, [FromForm] string Lugar, [FromForm] string Fecha)
        {
            var codigoIdCentro = "";
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

            List<VMAnteproyectoPoa> lista = new();
            for (int i = 1; i <= cantidadFila; i++)
            {
                nroFila++;
                IRow fila = HojaExcel.GetRow(i);
                VMAnteproyectoPoa pl = new();
                //pl. = fila.GetCell(0).ToString();                
                pl.CiteAnteproyecto = Cite;
                var codUe = fila.GetCell(1).ToString();
                codUe = Regex.Match(codUe, @"\d+").Value;
                var codProg = fila.GetCell(2).ToString();
                codProg = Regex.Match(codProg, @"\d+").Value;
                var codProy = fila.GetCell(3).ToString();
                codProy = Regex.Match(codProy, @"\d+").Value;
                var codAct = fila.GetCell(4).ToString();
                codAct = Regex.Match(codAct, @"\d+").Value;
                var codigoCompuesto = codProg + "0" + codUe + codProy.Substring(1, 3) + codAct;
                CentroSalud cSalud = await _centroSaludServicio.ObtenerCentroByCodigo(codigoCompuesto);
                pl.IdCentro = 1032; // No existe el codigo asociado
                codigoIdCentro = pl.IdCentro.ToString();
                if (cSalud != null)
                {
                    pl.IdCentro = cSalud.IdCentro;
                    codigoIdCentro = pl.IdCentro.ToString();

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
                //pl.FechaAnteproyecto = DateTime.Now.ToString();
                pl.FechaAnteproyecto = Fecha;
                pl.NombreRegional = "Santa Cruz";
                pl.NombreEjecutora = "";
                pl.MontoAnteproyecto = Decimal.Parse(fila.GetCell(18).ToString());
                pl.EstadoAnteproyecto = "INI";
                VMDetalleAnteproyectoPoa detalle = new();
                detalle.CodigoPartida = fila.GetCell(13).ToString();
                PartidaPresupuestaria partida = await _partidaServicio.ObtenerPartidaPresupuestariaByCodigo(detalle.CodigoPartida);
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
                detalle.Observacion = fila.GetCell(18).ToString();

                pl.DetalleAnteproyectoPoas.Add(detalle);

                lista.Add(pl);
                try
                {
                    //    Planificacion planificacion_creada = await _planificacionServicio.Registrar(_mapper.Map<Planificacion>(pl));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("------ERROR-------" + nroFila.ToString());
                    // Console.WriteLine(ex.Message);
                    return StatusCode(StatusCodes.Status403Forbidden, new { mensaje = "error lectura excel en Fila: " + nroFila.ToString() });
                }

            }
            List<VMAnteproyectoPoa> listaOrdenada = lista.OrderBy(pl => pl.IdUnidadResponsable).ToList();
            int j = 0;
            var unidadResponsable = "";
            int nroCite = 0;
            //float importeTotal = 0;
            VMAnteproyectoPoa anteproyectosPoa = new();
            foreach (var filaAnt in listaOrdenada)
            {
                j++;
                if (unidadResponsable == filaAnt.IdUnidadResponsable.ToString() && j != 1)
                {
                    //---Seguir acumulando Requerimiento a la Unidad Vigente---                   
                    VMDetalleAnteproyectoPoa vmDetAntPoa = new();
                    VMDetalleAnteproyectoPoa vmPlani = filaAnt.DetalleAnteproyectoPoas.First();
                    vmDetAntPoa.IdPartida = vmPlani.IdPartida;
                    vmDetAntPoa.Detalle = vmPlani.Detalle;
                    vmDetAntPoa.Medida = vmPlani.Medida;
                    vmDetAntPoa.Cantidad = vmPlani.Cantidad;
                    vmDetAntPoa.Precio = vmPlani.Precio;
                    vmDetAntPoa.Total = vmPlani.Total;
                    vmDetAntPoa.Observacion = vmPlani.Observacion;
                    vmDetAntPoa.CodigoActividad = vmPlani.CodigoActividad;
                    vmDetAntPoa.Total = vmPlani.Total;
                    vmDetAntPoa.MesEne = vmPlani.MesEne;
                    vmDetAntPoa.MesFeb = vmPlani.MesFeb;
                    vmDetAntPoa.MesMar = vmPlani.MesMar;
                    vmDetAntPoa.MesAbr = vmPlani.MesAbr;
                    vmDetAntPoa.MesMay = vmPlani.MesMay;
                    vmDetAntPoa.MesJun = vmPlani.MesJun;
                    vmDetAntPoa.MesJul = vmPlani.MesJul;
                    vmDetAntPoa.MesAgo = vmPlani.MesAgo;
                    vmDetAntPoa.MesSep = vmPlani.MesSep;
                    vmDetAntPoa.MesOct = vmPlani.MesOct;
                    vmDetAntPoa.MesNov = vmPlani.MesNov;
                    vmDetAntPoa.MesDic = vmPlani.MesDic;

                    anteproyectosPoa.DetalleAnteproyectoPoas.Add(vmDetAntPoa);

                }
                else if (unidadResponsable != filaAnt.IdUnidadResponsable.ToString() || j == 1)
                {
                    // Grabando el anterior requerimiento
                    if (anteproyectosPoa.DetalleAnteproyectoPoas.Count > 0)
                    {
                        nroCite++;
                        //string citePoa = Cite + "-" + nroCite.ToString();
                        string citePoa = Cite + "-" + anteproyectosPoa.IdUnidadResponsable.ToString();
                        anteproyectosPoa.CiteAnteproyecto = citePoa;
                        AnteproyectoPoa anteproyecto_creada = await _anteproyectopoaServicio.Crear(_mapper.Map<AnteproyectoPoa>(anteproyectosPoa));
                    }
                    //---Empezar un nuevo POA de Unidad
                    unidadResponsable = filaAnt.IdUnidadResponsable.ToString();
                    anteproyectosPoa = new VMAnteproyectoPoa();
                    anteproyectosPoa.IdUnidadResponsable = filaAnt.IdUnidadResponsable;
                    if (unidadResponsable == "")
                        anteproyectosPoa.IdUnidadResponsable = 1002;
                    anteproyectosPoa.IdUsuario = filaAnt.IdUsuario;
                    anteproyectosPoa.IdCentro = filaAnt.IdCentro;
                    anteproyectosPoa.FechaAnteproyecto = filaAnt.FechaAnteproyecto;
                    anteproyectosPoa.CiteAnteproyecto = filaAnt.CiteAnteproyecto;
                    anteproyectosPoa.MontoAnteproyecto = filaAnt.MontoAnteproyecto;
                    anteproyectosPoa.EstadoAnteproyecto = filaAnt.EstadoAnteproyecto;
                    anteproyectosPoa.FechaAnulacion = filaAnt.FechaAnulacion;
                    anteproyectosPoa.Lugar = filaAnt.Lugar;
                    anteproyectosPoa.NombreRegional = filaAnt.NombreRegional;
                    anteproyectosPoa.NombreEjecutora = filaAnt.NombreUnidadResponsable;
                    if (filaAnt.DetalleAnteproyectoPoas.Count > 0)
                    {
                        VMDetalleAnteproyectoPoa vmDetAntPoa = new();
                        VMDetalleAnteproyectoPoa vmPlani = filaAnt.DetalleAnteproyectoPoas.First();
                        vmDetAntPoa.IdPartida = vmPlani.IdPartida;
                        vmDetAntPoa.Detalle = vmPlani.Detalle;
                        vmDetAntPoa.Medida = vmPlani.Medida;
                        vmDetAntPoa.Cantidad = vmPlani.Cantidad;
                        vmDetAntPoa.Precio = vmPlani.Precio;
                        vmDetAntPoa.Total = vmPlani.Total;

                        vmDetAntPoa.MesEne = vmPlani.MesEne;
                        vmDetAntPoa.MesFeb = vmPlani.MesFeb;
                        vmDetAntPoa.MesMar = vmPlani.MesMar;
                        vmDetAntPoa.MesAbr = vmPlani.MesAbr;
                        vmDetAntPoa.MesMay = vmPlani.MesMay;
                        vmDetAntPoa.MesJun = vmPlani.MesJun;
                        vmDetAntPoa.MesJul = vmPlani.MesJul;
                        vmDetAntPoa.MesAgo = vmPlani.MesAgo;
                        vmDetAntPoa.MesSep = vmPlani.MesSep;
                        vmDetAntPoa.MesOct = vmPlani.MesOct;
                        vmDetAntPoa.MesNov = vmPlani.MesNov;
                        vmDetAntPoa.MesDic = vmPlani.MesDic;

                        vmDetAntPoa.Observacion = vmPlani.Observacion;
                        vmDetAntPoa.CodigoActividad = vmPlani.CodigoActividad;

                        anteproyectosPoa.DetalleAnteproyectoPoas.Add(vmDetAntPoa);
                    }
                    //importeTotal = importeTotal + (VMPlanificacion.)
                }

                Console.WriteLine("[" + j.ToString() + "]--------------------------------->Unidad Responsable:" + unidadResponsable + "  --> " + filaAnt.CiteAnteproyecto);
            }
            if (anteproyectosPoa.DetalleAnteproyectoPoas.Count > 0)
            {
                nroCite++;
                //string citePoa = Cite + "-" + nroCite.ToString();
                string citePoa = Cite + "-" + anteproyectosPoa.IdUnidadResponsable.ToString();
                anteproyectosPoa.CiteAnteproyecto = citePoa;
                AnteproyectoPoa anteproyecto_creada = await _anteproyectopoaServicio.Crear(_mapper.Map<AnteproyectoPoa>(anteproyectosPoa));
            }

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

        }

        [HttpPut]
        public IActionResult MostrarPDFAnteproyectoPoa(string citeAnteproyecto)
        {
            string urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFAnteproyectoPoa?citeAnteproyecto={citeAnteproyecto}";

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
        public async Task<IActionResult> Anular([FromBody] VMAnteproyectoPoa modelo)
        {
            GenericResponse<VMAnteproyectoPoa> gResponse = new();

            try
            {
                AnteproyectoPoa anteproyectopoa_anulada = await _anteproyectopoaServicio.Anular(_mapper.Map<AnteproyectoPoa>(modelo));
                modelo = _mapper.Map<VMAnteproyectoPoa>(anteproyectopoa_anulada);
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
        public async Task<IActionResult> Editar([FromBody] VMAnteproyectoPoa modelo)
        {
            GenericResponse<VMAnteproyectoPoa> gResponse = new();


            try
            {
                AnteproyectoPoa anteproyectopoa_editada = await _anteproyectopoaServicio.Editar(_mapper.Map<AnteproyectoPoa>(modelo));
                modelo = _mapper.Map<VMAnteproyectoPoa>(anteproyectopoa_editada);
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
