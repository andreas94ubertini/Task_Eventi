using System;
using System.Collections.Generic;

namespace Task_Eventi.Models;

public partial class Partecipante
{
    public int PartecipanteId { get; set; }

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public string Contatto { get; set; } = null!;

    public string CodFis { get; set; } = null!;

    public int EventoRif { get; set; }

    public virtual Evento EventoRifNavigation { get; set; } = null!;
}
