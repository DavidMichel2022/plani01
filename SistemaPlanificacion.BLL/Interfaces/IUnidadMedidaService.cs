using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IUnidadMedidaService
    {
        Task<List<UnidadMedida>> Lista();
        Task<UnidadMedida> Crear(UnidadMedida entidad);
        Task<UnidadMedida> Editar(UnidadMedida entidad);
        Task<bool> Eliminar(int idUnidad);
    }
}
