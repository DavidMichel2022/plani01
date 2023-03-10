using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IOperacionService
    {
        Task<List<Operacion>> Lista();
        Task<Operacion> Crear(Operacion entidad);
        Task<Operacion> Editar(Operacion entidad);
        Task<bool> Eliminar(int idOperacion);
    }
}
