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
            #region RolGeneral
            CreateMap<RolGeneral, VMRolGeneral>().ReverseMap();
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
                    opt => opt.MapFrom(origen => origen.InverseIdmenuPadreNavigation)
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
                    destino.NombreDocumento,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionTipoDocumentoNavigation.Descripcion)
                )
                .ForMember(destino =>
                    destino.NombreActividad,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionActividadNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.NombreCentro,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionCentroNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.NombrePrograma,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionProgramaNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.NombreEmpresa,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionEmpresaNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.NombreUsuario,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionUsuarioNavigation.Nombre)
                )

                .ForMember(destino =>
                    destino.NombreUnidadProceso,
                    opt => opt.MapFrom(origen => origen.IdPlanificacionUnidadProcesoNavigation.Nombre)
                )


                .ForMember(destino =>
                    destino.MontoPlanificacion,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.MontoPlanificacion.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.MontoPoa,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.MontoPoa.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.MontoPresupuesto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.MontoPresupuesto.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.MontoCompra,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.MontoCompra.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.FechaPlanificacion,
                    opt => opt.MapFrom(origen => origen.FechaPlanificacion.Value.ToString("dd/MM/yyyy"))
                );
            CreateMap<VMPlanificacion, Planificacion>()
                .ForMember(destino =>
                    destino.IdPlanificacionTipoDocumentoNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.IdPlanificacionActividadNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.IdPlanificacionCentroNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.IdPlanificacionProgramaNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.IdPlanificacionEmpresaNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.IdPlanificacionUsuarioNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.MontoPlanificacion,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.MontoPlanificacion, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.MontoPoa,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.MontoPoa, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.MontoPresupuesto,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.MontoPresupuesto, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.MontoCompra,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.MontoCompra, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.FechaPlanificacion,
                    opt => opt.MapFrom(origen => origen.FechaPlanificacion.Value.ToString("dd/MM/yyyy"))
                );
            #endregion

            #region DetallePlanificacion
            CreateMap<DetallePlanificacion, VMDetallePlanificacion>()
                .ForMember(destino =>
                    destino.NombrePartida,
                    opt => opt.MapFrom(origen => origen.IdDetallePlanificacionPartidaNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.NombreActividad,
                    opt => opt.MapFrom(origen => origen.IdDetallePlanificacionActividadNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.Cantidad,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Cantidad.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                );
            CreateMap<VMDetallePlanificacion, DetallePlanificacion>()
                .ForMember(destino =>
                    destino.IdDetallePlanificacionPartidaNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.IdDetallePlanificacionActividadNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.Cantidad,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Cantidad, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
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
        }
    }
}
