using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IAnteproyectoPoaService
    {
        Task<List<AnteproyectoPoa>> Lista();
        Task<List<AnteproyectoPoa>> ListaPoaMiUnidad(int idUnidadResponsable);
        Task<AnteproyectoPoa> Crear(AnteproyectoPoa entidad);
        Task<AnteproyectoPoa> Editar(AnteproyectoPoa entidad);
        Task<bool> Eliminar(int idAnteproyecto);
        Task<List<PartidaPresupuestaria>> ObtenerPartidasAnteproyecto(string busqueda);
        Task<AnteproyectoPoa> Registrar(AnteproyectoPoa entidad);
        Task<AnteproyectoPoa> Detalle(string citeAnteproyecto);
        Task<AnteproyectoPoa> Anular(AnteproyectoPoa entidad);
        Task<List<AnteproyectoPoa>> ObtenerAnteproyectos(string busqueda);
    }
}
