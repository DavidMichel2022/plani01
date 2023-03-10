using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface ITablaaceService
    {
        Task<List<TablaAce>> Lista();
        Task<TablaAce> Crear(TablaAce entidad);
        Task<TablaAce> Editar(TablaAce entidad);
        Task<bool> Eliminar(int idAce);
    }
}
