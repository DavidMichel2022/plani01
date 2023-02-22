using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface ITipodocumentoService
    {
        Task<List<TipoDocumento>> Lista();
        Task<TipoDocumento> Crear(TipoDocumento entidad);
        Task<TipoDocumento> Editar(TipoDocumento entidad);
        Task<bool> Eliminar(int idDocumento);
    }
}
