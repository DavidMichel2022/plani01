using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.Entity;
using System.Globalization;
using AutoMapper;

namespace SistemaPlanificacion.AplicacionWeb.Utilidades.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
                CreateMap<Rol, VMRol>().ReverseMap();
            #endregion
            #region Actividad
            CreateMap<Actividad, VMActividad>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<VMActividad, Actividad>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion
            #region Empresa
            CreateMap<Empresa, VMEmpresa>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<VMEmpresa, Empresa>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion
            #region TipoDocumento
            CreateMap<TipoDocumento, VMTipoDocumento>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<VMTipoDocumento, TipoDocumento>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion
            #region Programa
            CreateMap<Programa, VMPrograma>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<VMPrograma, Programa>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion
            #region CentroSalud
            CreateMap<CentroSalud, VMCentroSalud>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<VMCentroSalud, CentroSalud>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion
            #region Usuario
            CreateMap<Usuario, VMUsuario>()
                .ForMember(destino => 
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(destino =>
                    destino.NombreRol,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation.Descripcion)
                );
            CreateMap<VMUsuario, Usuario>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                    destino.IdRolNavigation,
                    opt => opt.Ignore()
                );
            #endregion
            #region PartidaPresupuestaria
            CreateMap<PartidaPresupuestaria, VMPartidaPresupuestaria>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(destino =>
                    destino.NombrePrograma,
                    opt => opt.MapFrom(origen => origen.IdProgramaNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.Stock,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Stock.Value, new CultureInfo("es-PE")))
                );
            CreateMap<VMPartidaPresupuestaria, PartidaPresupuestaria>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                    destino.IdProgramaNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.Stock,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Stock, new CultureInfo("es-PE")))
                );
            #endregion
            #region Menu
            CreateMap<Menu, VMMenu>()
                .ForMember(destino =>
                    destino.SubMenus,
                    opt => opt.MapFrom(origen => origen.InverseIdMenuPadreNavigation)
                );
            #endregion
            #region Negocio
            CreateMap<Negocio, VMNegocio>()
                .ForMember(destino =>
                    destino.PorcentajeImpuesto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.PorcentajeImpuesto.Value, new CultureInfo("es-PE")))
                );
            CreateMap<VMNegocio, Negocio>()
                .ForMember(destino =>
                    destino.PorcentajeImpuesto,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PorcentajeImpuesto, new CultureInfo("es-PE")))
                );
            #endregion

            #region Planificacion
             CreateMap<Planificacion, VMPlanificacion>()
                 .ForMember(destino =>
                     destino.DetallePlanificacion,
                     opt => opt.MapFrom(origen => origen.DetallePlanificacions)
                 ).ForMember(destino =>
                     destino.NombreDocumento,
                     opt => opt.MapFrom(origen => origen.IdDocumentoNavigation.Descripcion)
                 )
                 .ForMember(destino =>
                     destino.NombreUsuario,
                     opt => opt.MapFrom(origen => origen.IdUsuarioNavigation.Nombre)
                 )
                 .ForMember(destino =>
                     destino.NombreCentro,
                     opt => opt.MapFrom(origen => origen.IdCentroNavigation.Nombre)
                 )
                 .ForMember(destino =>
                     destino.NombreUnidadResponsable,
                     opt => opt.MapFrom(origen => origen.IdUnidadResponsableNavigation.Nombre)
                 )
                .ForMember(destino =>
                    destino.MontoPlanificacion,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.MontoPlanificacion.Value, new CultureInfo("es-PE")))
                )
                 .ForMember(destino =>
                     destino.FechaPlanificacion,
                     opt => opt.MapFrom(origen => origen.FechaPlanificacion.Value.ToString("dd/MM/yyyy"))
                 );
             CreateMap<VMPlanificacion, Planificacion>()
                 .ForMember(destino =>
                     destino.DetallePlanificacions,
                     opt => opt.MapFrom(origen => origen.DetallePlanificacion)
                 )
                 .ForMember(destino =>
                     destino.IdDocumentoNavigation,
                     opt => opt.Ignore()
                 )
                 .ForMember(destino =>
                     destino.IdUsuarioNavigation,
                     opt => opt.Ignore()
                 )
                 .ForMember(destino =>
                     destino.IdCentroNavigation,
                     opt => opt.Ignore()
                 )
                 .ForMember(destino =>
                     destino.IdUnidadResponsableNavigation,
                     opt => opt.Ignore()
                 )
                .ForMember(destino =>
                    destino.MontoPlanificacion,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.MontoPlanificacion, new CultureInfo("es-PE")))
                )
                 .ForMember(destino =>
                     destino.FechaPlanificacion,
                     opt => opt.MapFrom(origen => origen.FechaPlanificacion)
                 );
            #endregion

            #region DetallePlanificacion
            CreateMap<DetallePlanificacion, VMDetallePlanificacion>()
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Cantidad,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Cantidad.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.CodigoPartida,
                    opt => opt.MapFrom(origen => origen.IdPartidaNavigation.Codigo)
                )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                );
            CreateMap<VMDetallePlanificacion, DetallePlanificacion>()
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Cantidad,
                    opt => opt.MapFrom(origen => Convert.ToInt32(origen.Cantidad, new CultureInfo("es-PE")))
                )                
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
                );

            CreateMap<DetallePlanificacion, VMReportePlanificacion>()
                .ForMember(destino =>
                    destino.FechaPlanificacion,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionNavigation.FechaPlanificacion.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                    destino.NumeroPlanificacion,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionNavigation.NumeroPlanificacion)
                )
                .ForMember(destino =>
                    destino.NombreTipoDocumento,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionNavigation.IdDocumentoNavigation.Descripcion)
                )
                .ForMember(destino =>
                    destino.NombreCentro,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionNavigation.IdCentroNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.NombreUnidadResponsable,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionNavigation.IdUnidadResponsableNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.CitePlanificacion,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionNavigation.CitePlanificacion)
                )
                .ForMember(destino =>
                    destino.MontoPlanificacion,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdPlanificacionNavigation.MontoPlanificacion.Value))
                )
                .ForMember(destino =>
                    destino.NombrePartida,
                    opt => opt.MapFrom(origen => origen.IdPartidaNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.NombreItem,
                    opt => opt.MapFrom(origen => origen.NombreItem)
                )
                .ForMember(destino =>
                    destino.Medida,
                    opt => opt.MapFrom(origen => origen.Medida)
                )
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Cantidad,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Cantidad.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                );
            #endregion

            #region Operacion
            CreateMap<Operacion, VMOperacion>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<VMOperacion, Operacion>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion

            #region Objetivo
            CreateMap<Objetivo, VMObjetivo>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<VMObjetivo, Objetivo>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion

            #region TablaAce
            CreateMap<TablaAce, VMTablaAce>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<VMTablaAce, TablaAce>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion

            #region UnidadResponsable
            CreateMap<UnidadResponsable, VMUnidadResponsable>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<VMUnidadResponsable, UnidadResponsable>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion

            #region UnidadProceso
            CreateMap<UnidadProceso, VMUnidadProceso>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<VMUnidadProceso, UnidadProceso>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );
            #endregion

            #region CertificacionPlanificacion
            CreateMap<CertificacionPlanificacion, VMCertificacionPlanificacion>().ReverseMap();
            #endregion
            #region DetalleCertificacionPlanifiacion
                CreateMap<DetalleCertificacionPlanificacion, VMDetalleCertificacionPlanificacion>().ReverseMap();
            #endregion
        }
    }
}
