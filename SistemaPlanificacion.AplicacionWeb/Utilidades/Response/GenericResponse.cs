using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;

namespace SistemaPlanificacion.AplicacionWeb.Utilidades.Response
{
    public class GenericResponse<TObject>
    {
        public bool Estado { get; set; }
        public string? Mensaje { get; set; }
        public TObject? Objeto { get; set; }
        public List<TObject>? ListaObjeto { get; set; }

        public static implicit operator GenericResponse<TObject>(GenericResponse<VMActividad> v)
        {
            throw new NotImplementedException();
        }
    }
}
