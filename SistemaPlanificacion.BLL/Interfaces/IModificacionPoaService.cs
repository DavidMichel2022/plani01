using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IModificacionPoaService
    {
        Task<List<ModificacionPoa>> Lista();
        Task<ModificacionPoa> Crear(ModificacionPoa entidad);
        Task<ModificacionPoa> Editar(ModificacionPoa entidad);
        Task<bool> Eliminar(int idModificacionPoa);
        Task<ModificacionPoa> ObtenerModificacionPoa(int idModificacionPoa);
        
    }
}
