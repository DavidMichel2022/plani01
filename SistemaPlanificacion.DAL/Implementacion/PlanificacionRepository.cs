using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Microsoft.EntityFrameworkCore;
using SistemaPlanificacion.DAL.DBContext;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.DAL.Implementacion
{
    public class PlanificacionRepository : GenericRepository<Planificacion>, IPlanificacionRepository
    {
        private readonly BasePlanificacionContext _DbContext;
        public PlanificacionRepository(BasePlanificacionContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext; 
        }

        public async Task<Planificacion> Registrar(Planificacion entidad)
        {
            Planificacion planificaciongenerada = new Planificacion();

            using (var transaction=_DbContext.Database.BeginTransaction()) 
            {
                try
                {
                    foreach(DetallePlanificacion dp in entidad.DetallePlanificacion)
                    {
                        PartidaPresupuestaria partida_encontrada=_DbContext.PartidaPresupuestaria.Where(p => p.IdPartida == dp.IdPartida).First();
                        partida_encontrada.Stock = partida_encontrada.Stock - dp.Cantidad;
                        _DbContext.PartidaPresupuestaria.Update(partida_encontrada);

                    }
                    await _DbContext.SaveChangesAsync();

                    NumeroCorrelativo correlativo = _DbContext.NumeroCorrelativo.Where(n => n.Gestion == "planificacion").First();

                    correlativo.Ultimonumero = correlativo.Ultimonumero + 1;
                    correlativo.FechaActualizacion=DateTime.Now;

                    _DbContext.NumeroCorrelativo.Update(correlativo);
                    await _DbContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));

                    string numeroPlanificacion = ceros + correlativo.Ultimonumero.ToString();
                    numeroPlanificacion = numeroPlanificacion.Substring(numeroPlanificacion.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);

                    entidad.NumeroPlanificacion = numeroPlanificacion;

                    await _DbContext.Planificacion.AddAsync(entidad);
                    await _DbContext.SaveChangesAsync();

                    planificaciongenerada = entidad;
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                   transaction.Rollback();
                   throw ex;
                }
            }
            return planificaciongenerada;
        }

        public async Task<List<DetallePlanificacion>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetallePlanificacion> listaResumen = await _DbContext.DetallePlanificacion
                .Include(p=>p.IdPlanificacionNavigation)
                .ThenInclude(u=>u.IdPlanificacionUsuarioNavigation)
                .Include(p => p.IdPlanificacionNavigation)
                .ThenInclude(tdp=>tdp.IdPlanificacionTipoDocumentoNavigation)
                .Where(dp=>dp.IdPlanificacionNavigation.FechaPlanificacion.Value.Date>=FechaInicio && dp.IdPlanificacionNavigation.FechaPlanificacion.Value.Date<=FechaFin).ToListAsync();
            return listaResumen;
        }
    }
}
