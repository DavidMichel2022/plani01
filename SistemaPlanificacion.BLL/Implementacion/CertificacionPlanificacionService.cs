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
        private readonly IPlanificacionRepository _repositorioPlanificacion;
        public CertificacionPlanificacionService(IGenericRepository<CertificacionPlanificacion> repositorioCertificacionPlanificacion, IPlanificacionRepository repositorioPlanificacion)
        {
            _repositorioCertificacionPlanificacion = repositorioCertificacionPlanificacion;
            _repositorioPlanificacion = repositorioPlanificacion;
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
