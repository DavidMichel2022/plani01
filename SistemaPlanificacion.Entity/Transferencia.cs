using System;
using System.Collections.Generic;

namespace SistemaPlanificacion.Entity;

public partial class Transferencia
{
    public int IdTransferencia { get; set; }

    public string? IdOrigen { get; set; }

    public string? IdDestino { get; set; }

    public string? Referencia { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
