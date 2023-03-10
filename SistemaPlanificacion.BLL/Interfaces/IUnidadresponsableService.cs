using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IUnidadresponsableService
    {
        Task<List<UnidadResponsable>> Lista();
        Task<UnidadResponsable> Crear(UnidadResponsable entidad);
        Task<UnidadResponsable> Editar(UnidadResponsable entidad);
        Task<bool> Eliminar(int idUnidad);
    }
}
