using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IProgramaService
    {
        Task<List<Programa>> Lista();
        Task<Programa> Crear(Programa entidad);
        Task<Programa> Editar(Programa entidad);
        Task<bool> Eliminar(int idPrograma);
    }
}
