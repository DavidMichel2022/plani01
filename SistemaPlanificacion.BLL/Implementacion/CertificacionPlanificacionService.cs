using Microsoft.EntityFrameworkCore;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class CertificacionPlanificacionService : ICertificacionPlanificacionService
    {
        private readonly IGenericRepository<CertificacionPlanificacion> _repositorioCertificacionPlanificacion;
        //private readonly IPlanificacionRepository _repositorioPlanificacion;
        public CertificacionPlanificacionService(IGenericRepository<CertificacionPlanificacion> repositorioCertificacionPlanificacion)
        {
            _repositorioCertificacionPlanificacion = repositorioCertificacionPlanificacion;
        }
        public async Task<CertificacionPlanificacion> ObtenerCertificacion(int idPlanificacion)
        {
            IQueryable<CertificacionPlanificacion> query = await _repositorioCertificacionPlanificacion.Consultar(p => p.IdPlanificacion == idPlanificacion);
            return query.FirstOrDefault();
        }

        public async Task<CertificacionPlanificacion> Registrar(CertificacionPlanificacion entidad)
        {
            try
            {
                return await _repositorioCertificacionPlanificacion.Crear(entidad);
            }
            catch
            {
                throw;
            }

        }
    }
}
