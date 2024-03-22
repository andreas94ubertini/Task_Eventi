using System;
using System.Collections.Generic;

namespace Task_Eventi.Models;

public partial class Risorsa
{
    public int RisorsaId { get; set; }

    public string Tipo { get; set; } = null!;

    public int Qt { get; set; }

    public decimal Costo { get; set; }

    public string Fornitore { get; set; } = null!;

    public int EventoRif { get; set; }

    public virtual Evento EventoRifNavigation { get; set; } = null!;
}
