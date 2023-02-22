using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaPlanificacion.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.DAL.Implementacion;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.BLL.Implementacion;
using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BasePlanificacionContext>(Options =>
            {
                Options.UseSqlServer(configuration.GetConnectionString("CadenaSQL"));
            });
            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));

            //services.AddScoped<IPlanificacionRepository, PlanificacionRepository>();

            services.AddScoped<ICorreoService, CorreoService>();
            services.AddScoped<IFireBaseService, FireBaseService>();
            services.AddScoped<IUtilidadesService, UtilidadesService>();
            services.AddScoped<IRolService, RolService>();

            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IProgramaService, ProgramaService>();
            services.AddScoped<IActividadService, ActividadService>();
            services.AddScoped<ICentrosaludService, CentrosaludService>();
            services.AddScoped<IPartidapresupuestariaService, PartidapresupuestariaService>();
            services.AddScoped<ITipodocumentoService, TipodocumentoService>();
        }
    }
}
