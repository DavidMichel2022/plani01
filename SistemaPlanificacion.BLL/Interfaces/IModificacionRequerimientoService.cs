using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IModificacionRequerimientoService
    {
        Task<List<ModificacionRequerimiento>> Lista();
        Task<ModificacionRequerimiento> Crear(ModificacionRequerimiento entidad);
        Task<ModificacionRequerimiento> Editar(ModificacionRequerimiento entidad);
        Task<bool> Eliminar(int idModificacionRequerimiento);
    }
}
