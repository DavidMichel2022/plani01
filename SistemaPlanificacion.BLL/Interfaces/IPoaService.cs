using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IPoaService
    {
        Task<List<RequerimientoPoa>> ObtenerPartidas(string busqueda);
        Task<RequerimientoPoa> Registrar(RequerimientoPoa entidad);
        Task<RequerimientoPoa> Editar(RequerimientoPoa entidad);
        Task<RequerimientoPoa> Anular(RequerimientoPoa entidad);
        Task<bool> Eliminar(int idRequerimientoPoa);
        Task<bool> EliminarDetalles(int idRequerimientoPoa);
        Task<List<RequerimientoPoa>> Historial(string numeroRequerimientoPoa, string fechaInicio, string fechaFin);
        Task<RequerimientoPoa> Detalle(string numeroRequerimientoPoa);
        Task<List<RequerimientoPoa>> Lista();
        Task<List<RequerimientoPoa>> ListaCarpetasxServicio(int idCentro);
    }
}
