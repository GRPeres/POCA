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

    public virtual DbSet<TbAluno> TbAlunos { get; set; }

    public virtual DbSet<TbMateria> TbMaterias { get; set; }

    public virtual DbSet<TbProfessore> TbProfessores { get; set; }

    public virtual DbSet<TbQuesto> TbQuestoes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=db_poca");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<TbMateria>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PRIMARY");

            entity.ToTable("tb_materias");

            entity.HasIndex(e => e.IdAlunoMateria, "id_aluno_materia_idx");

            entity.HasIndex(e => e.IdProfessorMateria, "id_professor_materia_idx");

            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.IdAlunoMateria).HasColumnName("id_aluno_materia");
            entity.Property(e => e.IdProfessorMateria).HasColumnName("id_professor_materia");

            entity.HasOne(d => d.IdAlunoMateriaNavigation).WithMany(p => p.TbMateria)
                .HasForeignKey(d => d.IdAlunoMateria)
                .HasConstraintName("id_aluno_materia");

            entity.HasOne(d => d.IdProfessorMateriaNavigation).WithMany(p => p.TbMateria)
                .HasForeignKey(d => d.IdProfessorMateria)
                .HasConstraintName("id_professor_materia");
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
