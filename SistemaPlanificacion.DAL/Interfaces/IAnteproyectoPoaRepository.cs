using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.DAL.Interfaces
{
    public interface IAnteproyectoPoaRepository : IGenericRepository<AnteproyectoPoa>
    {
        Task<AnteproyectoPoa> Registrar(AnteproyectoPoa entidad);
        Task<List<DetalleAnteproyectoPoa>> Reporte(DateTime FechaInicio, DateTime FechaFin);
        Task<bool> EliminarDetalleAnteproyectoPoa(DetalleAnteproyectoPoa entidad);
        Task<DetalleAnteproyectoPoa> AgregarDetalleAnteproyectoPoa(DetalleAnteproyectoPoa entidad);
    }
}
