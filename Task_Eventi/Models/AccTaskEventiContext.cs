using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Task_Eventi.Models;

public partial class AccTaskEventiContext : DbContext
{
    public AccTaskEventiContext()
    {
    }

    public AccTaskEventiContext(DbContextOptions<AccTaskEventiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Partecipante> Partecipantes { get; set; }

    public virtual DbSet<Risorsa> Risorsas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-BFLNTHU7\\SQLEXPRESS;Database=acc_Task_Eventi;User Id=academy;Password=academy!;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.EventoId).HasName("PK__Evento__DE07229C2F4E8DCB");

            entity.ToTable("Evento");

            entity.Property(e => e.EventoId).HasColumnName("eventoID");
            entity.Property(e => e.CapMax).HasColumnName("capMax");
            entity.Property(e => e.DataEvento).HasColumnName("dataEvento");
            entity.Property(e => e.Descrizione)
                .HasColumnType("text")
                .HasColumnName("descrizione");
            entity.Property(e => e.Luogo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("luogo");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Partecipante>(entity =>
        {
            entity.HasKey(e => e.PartecipanteId).HasName("PK__Partecip__59BAFC0EBCB32585");

            entity.ToTable("Partecipante");

            entity.HasIndex(e => new { e.CodFis, e.EventoRif }, "UQ__Partecip__463736D66EA0DBE9").IsUnique();

            entity.Property(e => e.PartecipanteId).HasColumnName("partecipanteID");
            entity.Property(e => e.CodFis)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("codFis");
            entity.Property(e => e.Cognome)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("cognome");
            entity.Property(e => e.Contatto)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("contatto");
            entity.Property(e => e.EventoRif).HasColumnName("eventoRIF");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome");

            entity.HasOne(d => d.EventoRifNavigation).WithMany(p => p.Partecipantes)
                .HasForeignKey(d => d.EventoRif)
                .HasConstraintName("FK__Partecipa__event__3D5E1FD2");
        });

        modelBuilder.Entity<Risorsa>(entity =>
        {
            entity.HasKey(e => e.RisorsaId).HasName("PK__Risorsa__31473C997B64D464");

            entity.ToTable("Risorsa");

            entity.Property(e => e.RisorsaId).HasColumnName("risorsaID");
            entity.Property(e => e.Costo)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("costo");
            entity.Property(e => e.EventoRif).HasColumnName("eventoRIF");
            entity.Property(e => e.Fornitore)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("fornitore");
            entity.Property(e => e.Qt).HasColumnName("qt");
            entity.Property(e => e.Tipo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.EventoRifNavigation).WithMany(p => p.Risorsas)
                .HasForeignKey(d => d.EventoRif)
                .HasConstraintName("FK__Risorsa__eventoR__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
