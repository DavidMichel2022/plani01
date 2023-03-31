using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaPlanificacion.Entity;

  namespace SistemaPlanificacion.BLL.Interfaces
    {
        public interface ICertificacionPlanificacionService
        {
            Task<CertificacionPlanificacion> ObtenerCertificacion(int idPlanificacion);
            Task<CertificacionPlanificacion> Registrar(CertificacionPlanificacion entidad);
        }
    }
