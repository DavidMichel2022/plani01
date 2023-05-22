using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IRequerimientoPoaService
    {
        Task<List<RequerimientoPoa>> Lista();
        Task<List<RequerimientoPoa>> ListaPoaMiUnidad(int idUnidadResponsable);
        Task<RequerimientoPoa> Crear(RequerimientoPoa entidad);
        Task<RequerimientoPoa> Editar(RequerimientoPoa entidad);
        Task<bool> Eliminar(int idRequerimiento);
        Task<List<PartidaPresupuestaria>> ObtenerPartidasRequerimiento(string busqueda);
        Task<RequerimientoPoa> Registrar(RequerimientoPoa entidad);
        Task<RequerimientoPoa> Detalle(string citeRequerimiento);
        Task<RequerimientoPoa> Anular(RequerimientoPoa entidad);
        Task<List<RequerimientoPoa>> ObtenerRequerimientos(string busqueda);
    }
}
