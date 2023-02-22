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
            CreateMap<Actividad,VMActividad>().ReverseMap();
            #endregion
            #region Empresa
            CreateMap<Empresa, VMEmpresa>().ReverseMap();
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
            CreateMap<Programa, VMPrograma>().ReverseMap();
            #endregion
            #region CentroSalud
            CreateMap<CentroSalud, VMCentroSalud>().ReverseMap();
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
                    destino.Stock,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Stock.Value, new CultureInfo("es-BO")))
                );
            CreateMap<VMPartidaPresupuestaria, PartidaPresupuestaria>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                    destino.Stock,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Stock, new CultureInfo("es-BO")))
                );
            #endregion

            #region Menu
            CreateMap<Menu, VMMenu>()
                .ForMember(destino =>
                    destino.SubMenus,
                    opt => opt.MapFrom(origen => origen.InverseIdmenuPadreNavigation)
                );
            #endregion
        }
    }
}
