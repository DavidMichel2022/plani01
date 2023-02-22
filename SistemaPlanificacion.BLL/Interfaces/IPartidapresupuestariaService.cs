using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IPartidapresupuestariaService
    {
        Task<List<PartidaPresupuestaria>> Lista();
        Task<PartidaPresupuestaria> Crear(PartidaPresupuestaria entidad);
        Task<PartidaPresupuestaria> Editar(PartidaPresupuestaria entidad);
        Task<bool> Eliminar(int idPartida);
    }
}
