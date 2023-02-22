namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMTipoDocumento
    {
        public int IdDocumento { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? EsActivo { get; set; }
    }
}
