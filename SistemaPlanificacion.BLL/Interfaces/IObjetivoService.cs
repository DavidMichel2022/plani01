using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface IObjetivoService
    {
        Task<List<Objetivo>> Lista();
        Task<Objetivo> Crear(Objetivo entidad);
        Task<Objetivo> Editar(Objetivo entidad);
        Task<bool> Eliminar(int idObjetivo);
    }
}
