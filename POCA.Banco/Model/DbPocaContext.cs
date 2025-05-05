using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public virtual DbSet<TbAluno> TbAlunos { get; set; }

    public virtual DbSet<TbProfessore> TbProfessores { get; set; }

    public virtual DbSet<TbQuesto> TbQuestoes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySQL(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<TbAluno>(entity =>
        {
            entity.HasKey(e => e.IdAluno).HasName("PRIMARY");

            entity.ToTable("tb_alunos");

            entity.HasIndex(e => e.NomeAluno, "nome_aluno_UNIQUE").IsUnique();

            entity.Property(e => e.IdAluno).HasColumnName("id_aluno");
            entity.Property(e => e.ContatoAluno)
                .HasMaxLength(45)
                .HasColumnName("contato_aluno");
            entity.Property(e => e.IdadeAluno).HasColumnName("idade_aluno");
            entity.Property(e => e.LoginAluno)
                .HasMaxLength(45)
                .HasColumnName("login_aluno");
            entity.Property(e => e.NomeAluno)
                .HasMaxLength(45)
                .HasColumnName("nome_aluno");
            entity.Property(e => e.ProgressoAluno).HasColumnName("progresso_aluno");
            entity.Property(e => e.SenhaAluno)
                .HasMaxLength(45)
                .HasColumnName("senha_aluno");
        });

        modelBuilder.Entity<TbProfessore>(entity =>
        {
            entity.HasKey(e => e.IdProfessor).HasName("PRIMARY");

            entity.ToTable("tb_professores");

            entity.HasIndex(e => e.ContatoProfessor, "contato_professor_UNIQUE").IsUnique();

            entity.HasIndex(e => e.LoginProfessor, "login_professor_UNIQUE").IsUnique();

            entity.HasIndex(e => e.NomeProfessor, "nome_professor_UNIQUE").IsUnique();

            entity.Property(e => e.IdProfessor).HasColumnName("id_professor");
            entity.Property(e => e.ContatoProfessor)
                .HasMaxLength(45)
                .HasColumnName("contato_professor");
            entity.Property(e => e.LoginProfessor)
                .HasMaxLength(45)
                .HasColumnName("login_professor");
            entity.Property(e => e.MateriaProfessor)
                .HasMaxLength(45)
                .HasColumnName("materia_professor");
            entity.Property(e => e.NomeProfessor)
                .HasMaxLength(45)
                .HasColumnName("nome_professor");
            entity.Property(e => e.SenhaProfessor)
                .HasMaxLength(45)
                .HasColumnName("senha_professor");
        });

        modelBuilder.Entity<TbQuesto>(entity =>
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
