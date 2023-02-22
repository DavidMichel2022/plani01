using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMCierreCarpeta
    {
        public int IdPlanificacion { get; set; }

        public string? NroPlanificacion { get; set; }

        public string? PartidaPresuesto { get; set; }

        public string? SaldoFinal { get; set; }

        public string? TipoCierre { get; set; }

        public int? Nulo { get; set; }
    }
}
