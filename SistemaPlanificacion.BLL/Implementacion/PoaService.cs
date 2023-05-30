using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class PoaService
    {
        private readonly IGenericRepository<PartidaPresupuestaria> _repositorioPartida;
        private readonly IGenericRepository<DetalleRequerimientoPoa> _repositorioDetalle;
        private readonly IAnteproyectoPoaRepository _repositorioRequerimientoPoa;
        private readonly IPartidapresupuestariaService _partidapresupuestariaServicio;

    }
}
