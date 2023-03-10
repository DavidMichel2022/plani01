using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class PartidaProgramaService : IPartidaProgramaService
    {
        private readonly IGenericRepository<Programa> _repositorio;
        public PartidaProgramaService(IGenericRepository<Programa> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Programa>> Lista()
        {
            IQueryable<Programa> query = await _repositorio.Consultar();
            return query.ToList();

        }
    }
}
