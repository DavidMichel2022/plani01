using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public interface IVMPartidaPresupuestaria
    {
        string? Codigo { get; set; }
        int? EsActivo { get; set; }
        int IdPartida { get; set; }
        int? IdPrograma { get; set; }
        string? Nombre { get; set; }
        string? NombrePrograma { get; set; }
        int? Stock { get; set; }
    }
}