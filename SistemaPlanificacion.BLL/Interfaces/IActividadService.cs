using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IActividadService
    {
        Task<List<Actividad>> Lista();
        Task<Actividad> Crear(Actividad entidad);
        Task<Actividad> Editar(Actividad entidad);
        Task<bool> Eliminar(int idActividad);
    }
}
