using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMPlanificacion
    {
        public int IdPlanificacion { get; set; }
        public int IdPDocumento { get; set; }

        public string? NroPlanificacion { get; set; }

        public string? FechaPlanificacion { get; set; }

        public string? CertificadoPoa { get; set; }

        public string? ReferenciaPlanificacion { get; set; }

        public int? IdPrograma { get; set; }
        public string? NombrePrograma { get; set; }

        public int? IdActividad { get; set; }
        public string? NombreActividad { get; set; }

        public int? IdCentro { get; set; }
        public string? NombreCentro { get; set; }

        public string? MontoPoaPlanificacion { get; set; }

        public string? MontoPlanificacion { get; set; }

        public int? IdUsuario { get; set; }
        public string? Usuario { get; set; }

        public int? Nulo { get; set; }

        public string? UbicacionPlanificacion { get; set; }
    }
}
