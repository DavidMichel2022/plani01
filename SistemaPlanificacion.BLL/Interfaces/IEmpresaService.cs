using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IEmpresaService
    {
        Task<List<Empresa>> Lista();
        Task<Empresa> Crear(Empresa entidad);
        Task<Empresa> Editar(Empresa entidad);
        Task<bool> Eliminar(int idEmpresa);
    }
}
