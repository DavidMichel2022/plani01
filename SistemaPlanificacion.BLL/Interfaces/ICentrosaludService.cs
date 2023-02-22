using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface ICentrosaludService
    {
        Task<List<CentroSalud>> Lista();
        Task<CentroSalud> Crear(CentroSalud entidad);
        Task<CentroSalud> Editar(CentroSalud entidad);
        Task<bool> Eliminar(int idCentro);
    }
}
