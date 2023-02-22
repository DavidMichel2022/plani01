using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class DocmPlanificacion
{
    public int IdPlanificacion { get; set; }

    public DateTime? FechaPlanificacion { get; set; }

    public string? CertificadopoaPlanificacion { get; set; }

    public string? ReferenciaPlanificacion { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdPrograma { get; set; }

    public int? IdActividad { get; set; }

    public int? IdCentro { get; set; }

    public float? MontopoaPlanificacion { get; set; }

    public float? MontoPlanificacion { get; set; }

    public string? UbicacionPlanificacion { get; set; }

    public int? IdUsuario { get; set; }

    public bool? Nulo { get; set; }

    public virtual ICollection<DocmCompra> DocmCompras { get; } = new List<DocmCompra>();

    public virtual ICollection<DocmPresupuesto> DocmPresupuestos { get; } = new List<DocmPresupuesto>();

    public virtual Actividad? IdActividadNavigation { get; set; }

    public virtual CentroSalud? IdCentroNavigation { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Programa? IdProgramaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<MoviPlanificacion> MoviPlanificacions { get; } = new List<MoviPlanificacion>();
}
