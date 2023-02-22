using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMCompra
    {
        public int IdPlanificacion { get; set; }

        public int? NroPlanificacion { get; set; }

        public string? CuceCompra { get; set; }

        public string? ObjContratocompra { get; set; }

        public string? ModalidadCompra { get; set; }

        public DateTime? FechaCompra { get; set; }

        public string? NrofacturaCompra { get; set; }

        public string? MontoadjudicadoCompra { get; set; }

        public DateTime? FechabienContratado { get; set; }

        public int? Nulo { get; set; }
    }
}
