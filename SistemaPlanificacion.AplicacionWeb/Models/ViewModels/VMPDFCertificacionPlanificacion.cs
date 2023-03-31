namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMPDFCertificacionPlanificacion
    {
        
        public VMNegocio? negocio { get; set; }
        public VMPlanificacion? planificacion { get; set; }
        public VMCertificacionPlanificacion? certificacionPlanificacion{ get; set; }
    }
}
