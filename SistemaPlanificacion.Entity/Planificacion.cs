using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Planificacion
{
    public int IdPlanificacion { get; set; }

    public int? NroPlanificacion { get; set; }

    public int? IdDocumento { get; set; }

    public DateTime? FechaPlanificacion { get; set; }

    public string? CertificadoPoa { get; set; }

    public string? ReferenciaPlanificacion { get; set; }

    public int? IdPrograma { get; set; }

    public int? IdActividad { get; set; }

    public int? IdCentro { get; set; }

    public int? IdEmpresa { get; set; }

    public float? MontopoaPlanificacion { get; set; }

    public float? MontoPlanificacion { get; set; }

    public int? IdUsuario { get; set; }

    public bool? Nulo { get; set; }

    public string? UbicacionPlanificacion { get; set; }

    public virtual ICollection<Compra> Compras { get; } = new List<Compra>();

    public virtual ICollection<DetallePlanificacion> DetallePlanificacions { get; } = new List<DetallePlanificacion>();

    public virtual Actividad? IdActividadNavigation { get; set; }

    public virtual CentroSalud? IdCentroNavigation { get; set; }

    public virtual TipoDocumento? IdDocumentoNavigation { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Programa? IdProgramaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Presupuesto> Presupuestos { get; } = new List<Presupuesto>();
}
