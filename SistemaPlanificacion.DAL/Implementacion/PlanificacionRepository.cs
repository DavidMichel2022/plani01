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
        private readonly BasePlanificacionContext _dbContext;
        public PlanificacionRepository(BasePlanificacionContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task<Planificacion> Registrar(Planificacion entidad)
        {
            Planificacion planificacionGenerada = new Planificacion();

            using (var transaction = _dbContext.Database.BeginTransaction()) 
            {
                try
                {
                    foreach(DetallePlanificacion dp in entidad.DetallePlanificacions)
                    {
                        PartidaPresupuestaria partida_encontrada = _dbContext.PartidaPresupuestaria.Where(p => p.IdPartida == dp.IdPartida).First();
                        partida_encontrada.Stock = partida_encontrada.Stock - dp.Cantidad;
                        _dbContext.PartidaPresupuestaria.Update(partida_encontrada);

                    }
                    await _dbContext.SaveChangesAsync();

                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(n => n.Gestion == "planificacion").First();

                    correlativo.Ultimonumero = correlativo.Ultimonumero + 1;
                    correlativo.FechaActualizacion=DateTime.Now;

                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));

                    string numeroPlanificacion = ceros + correlativo.Ultimonumero.ToString();
                    numeroPlanificacion = numeroPlanificacion.Substring(numeroPlanificacion.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);

                    entidad.NumeroPlanificacion = numeroPlanificacion;

                    await _dbContext.Planificacions.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    planificacionGenerada = entidad;
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                   transaction.Rollback();
                   throw ex;
                }
            }
            return planificacionGenerada;
        }

        public async Task<List<DetallePlanificacion>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetallePlanificacion> listaResumen = await _dbContext.DetallePlanificacions
                .Include(p => p.IdPlanificacionNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(p => p.IdPlanificacionNavigation)
                .ThenInclude(tdp => tdp.IdDocumentoNavigation)
                .Where(dp => dp.IdPlanificacionNavigation.FechaPlanificacion.Value.Date>=FechaInicio && dp.IdPlanificacionNavigation.FechaPlanificacion.Value.Date<=FechaFin).ToListAsync();
            return listaResumen;
        }
    }
}
