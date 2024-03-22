using System;
using System.Collections.Generic;

namespace Task_Eventi.Models;

public partial class Evento
{
    public int EventoId { get; set; }

    public string Nome { get; set; } = null!;

    public string Descrizione { get; set; } = null!;

    public DateOnly DataEvento { get; set; }

    public string Luogo { get; set; } = null!;

    public int CapMax { get; set; }

    public virtual ICollection<Partecipante> Partecipantes { get; set; } = new List<Partecipante>();

    public virtual ICollection<Risorsa> Risorsas { get; set; } = new List<Risorsa>();
}
