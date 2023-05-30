using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.DAL.Interfaces
{

    public interface IRequerimientoPoaRepository : IGenericRepository<RequerimientoPoa>
    {
        Task<RequerimientoPoa> Registrar(RequerimientoPoa entidad);
        Task<List<DetalleRequerimientoPoa>> Reporte(DateTime FechaInicio, DateTime FechaFin);
        Task<bool> EliminarDetalleRequerimientoPoa(DetalleRequerimientoPoa entidad);
        Task<DetalleRequerimientoPoa> AgregarDetalleRequerimientoPoa(DetalleRequerimientoPoa entidad);
    }
}
