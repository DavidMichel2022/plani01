using Microsoft.EntityFrameworkCore;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class CertificacionPlanificacionService : ICertificacionPlanificacionService
    {
        private readonly IGenericRepository<CertificacionPlanificacion> _repositorioCertificacionPlanificacion;
        private readonly IPlanificacionRepository _repositorioPlanificacion;
        public CertificacionPlanificacionService(IGenericRepository<CertificacionPlanificacion> repositorioCertificacionPlanificacion, IPlanificacionRepository repositorioPlanificacion)
        {
            _repositorioCertificacionPlanificacion = repositorioCertificacionPlanificacion;
            _repositorioPlanificacion = repositorioPlanificacion;
        }

        public async Task<bool> ActualizarEstadoCertificacionIdPlanificacion(string estado, int idPlanificacion)
        {
            try
            {
                                
                IQueryable<CertificacionPlanificacion> query = await _repositorioCertificacionPlanificacion.Consultar(c => c.IdPlanificacion == idPlanificacion);
                foreach (CertificacionPlanificacion certificacion in query)
                {
                    certificacion.EstadoCertificacion = estado;
                   
                }
                 await _repositorioCertificacionPlanificacion.Actualizar();
               


                //  Task<List<CertificacionPlanificacion>> Lista = query.ToList<CertificacionPlanificacion>;

                // Task<List<CertificacionPlanificacion>> certificaciones_encontradas = await _repositorioCertificacionPlanificacion.Consultar(c => c.IdPlanificacion == idPlanificacion);
                /*  IQueryable<CertificacionPlanificacion> query = await _repositorioCertificacionPlanificacion.Consultar();
                  return query
                      .Include(tdp => tdp.IdDocumentoNavigation)
                      .Include(c => c.IdCentroNavigation)
                      .Include(ur => ur.IdUnidadResponsableNavigation)
                      .Include(dp => dp.DetallePlanificacions)
                      .ThenInclude(dpp => dpp.IdPartidaNavigation)
                      //.Include(dp => dp.DetallePlanificacions).ThenInclude(dpp => dpp.IdPartidaNavigation)
                      .ToList();
                  certificacion_encontrada.EstadoCertificacion = estado; 
                  bool respuesta = await _repositorio.Editar(empresa_encontrada);
                  return Task<true>;*/
                return await Task.FromResult(true);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CertificacionPlanificacion> ObtenerCertificacion(int idPlanificacion)
        {
            // IQueryable<CertificacionPlanificacion> query = await _repositorioCertificacionPlanificacion.Consultar(p => p.IdPlanificacion == idPlanificacion);
            //return query.FirstOrDefault();
            IQueryable<CertificacionPlanificacion> query = await _repositorioCertificacionPlanificacion.Consultar();

            return query.Where(p => p.IdPlanificacion == idPlanificacion && p.EstadoCertificacion == "INI" )
                //.Include(dp => dp.DetalleCertificacionPlanificacions);
                .Include(dp=>dp.DetalleCertificacionPlanificacions)
                .FirstOrDefault();
                

        }

        public async Task<CertificacionPlanificacion> Registrar(CertificacionPlanificacion entidad)
        {
            try
            {
                Planificacion planificacion_encontrada = await _repositorioPlanificacion.Obtener(c => c.IdPlanificacion == entidad.IdPlanificacion);
                planificacion_encontrada.EstadoCarpeta = "PLA";
                bool respuesta = await _repositorioPlanificacion.Editar(planificacion_encontrada);

                return await _repositorioCertificacionPlanificacion.Crear(entidad);


            }
            catch
            {
                throw;
            }

        }
    }
}
