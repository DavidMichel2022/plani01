namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMDocmPresupuesto
    {
        public int IdPresupuesto { get; set; }

        public int? IdPlanificacion { get; set; }

        public string? CertPresupuesto { get; set; }

        public int? IdPrograma { get; set; }

        public int? IdActividad { get; set; }

        public DateTime? FechaPresupuesto { get; set; }

        public string? MontoPresupuesto { get; set; }

        public int? Nulo { get; set; }
    }
}
