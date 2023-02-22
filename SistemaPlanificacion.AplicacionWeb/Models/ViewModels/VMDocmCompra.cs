namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMDocmCompra
    {
        public int IdCompra { get; set; }

        public int? IdPlanificacion { get; set; }

        public string? CuceCompra { get; set; }

        public string? ObjcontratoCompra { get; set; }

        public string? ModalidadCompra { get; set; }

        public DateTime? FechaCompra { get; set; }

        public string? NrofacturaCompra { get; set; }

        public string? MontoadjudicadoCompra { get; set; }

        public DateTime? FechacontratoCompra { get; set; }

        public int? Nulo { get; set; }
    }
}
