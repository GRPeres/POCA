using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace POCA.Banco.Model;

public partial class DbPocaContext : DbContext
{
    public DbPocaContext()
    {
    }

    public DbPocaContext(DbContextOptions<DbPocaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<TbQuestoes> TbQuestoes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=db_poca");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<TbQuestoes>(entity =>
        {
            entity.HasKey(e => e.IdQuestao).HasName("PRIMARY");

            entity.ToTable("tb_questoes");

            entity.Property(e => e.IdQuestao).HasColumnName("id_questao");
            entity.Property(e => e.DificuldadeQuestao)
                .HasColumnType("enum('Fácil','Médio','Difícil')")
                .HasColumnName("dificuldade_questao");
            entity.Property(e => e.EnunciadoQuestao)
                .HasMaxLength(200)
                .HasColumnName("enunciado_questao");
            entity.Property(e => e.RespostacertaQuestao)
                .HasMaxLength(100)
                .HasColumnName("respostacerta_questao");
            entity.Property(e => e.Respostaerrada1Questao)
                .HasMaxLength(100)
                .HasColumnName("respostaerrada1_questao");
            entity.Property(e => e.Respostaerrada2Questao)
                .HasMaxLength(100)
                .HasColumnName("respostaerrada2_questao");
            entity.Property(e => e.Respostaerrada3Questao)
                .HasMaxLength(100)
                .HasColumnName("respostaerrada3_questao");
            entity.Property(e => e.TemaQuestao)
                .HasColumnType("enum('Teoria','Programação')")
                .HasColumnName("tema_questao");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
