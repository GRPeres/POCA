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

    public virtual DbSet<TbAtividade> TbAtividades { get; set; }

    public virtual DbSet<TbMateria> TbMaterias { get; set; }

    public virtual DbSet<TbPessoa> TbPessoas { get; set; }

    public virtual DbSet<TbProfessore> TbProfessores { get; set; }

    public virtual DbSet<TbQuesto> TbQuestoes { get; set; }

    public virtual DbSet<TbResposta> TbRespostas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=db_poca");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAluno>(entity =>
        {
            entity.HasKey(e => e.IdAluno).HasName("PRIMARY");

            entity.ToTable("tb_alunos");

            entity.HasIndex(e => e.IdAluno, "id_aluno_UNIQUE").IsUnique();

            entity.HasIndex(e => e.NomeAluno, "nome_aluno_UNIQUE").IsUnique();

            entity.Property(e => e.IdAluno).HasColumnName("id_aluno");
            entity.Property(e => e.ContatoAluno)
                .HasMaxLength(45)
                .HasColumnName("contato_aluno");
            entity.Property(e => e.NascimentoAluno).HasColumnName("nascimento_aluno");
            entity.Property(e => e.NomeAluno)
                .HasMaxLength(45)
                .HasColumnName("nome_aluno");
            entity.Property(e => e.EmailAluno)
                .HasMaxLength(100)
                .HasColumnName("email_aluno");
            entity.Property(e => e.ProgressoAluno).HasColumnName("progresso_aluno");

            entity.HasMany(d => d.TbMateriasIdMateria).WithMany(p => p.TbAlunosIdAlunos)
                .UsingEntity<Dictionary<string, object>>(
                    "TbAlunosHasTbMateria",
                    r => r.HasOne<TbMateria>().WithMany()
                        .HasForeignKey("TbMateriasIdMateria")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_alunos_has_tb_materias_tb_materias1"),
                    l => l.HasOne<TbAluno>().WithMany()
                        .HasForeignKey("TbAlunosIdAluno")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_alunos_has_tb_materias_tb_alunos1"),
                    j =>
                    {
                        j.HasKey("TbAlunosIdAluno", "TbMateriasIdMateria").HasName("PRIMARY");
                        j.ToTable("tb_alunos_has_tb_materias");
                        j.HasIndex(new[] { "TbAlunosIdAluno" }, "fk_tb_alunos_has_tb_materias_tb_alunos1_idx");
                        j.HasIndex(new[] { "TbMateriasIdMateria" }, "fk_tb_alunos_has_tb_materias_tb_materias1_idx");
                        j.IndexerProperty<int>("TbAlunosIdAluno").HasColumnName("tb_alunos_id_aluno");
                        j.IndexerProperty<int>("TbMateriasIdMateria").HasColumnName("tb_materias_id_materia");
                    });
        });

        modelBuilder.Entity<TbAtividade>(entity =>
        {
            entity.HasKey(e => e.IdAtividade).HasName("PRIMARY");

            entity.ToTable("tb_atividades");

            entity.HasIndex(e => e.IdAtividade, "id_atividade_UNIQUE").IsUnique();

            entity.HasIndex(e => e.NomeAtividade, "nome_atividade_UNIQUE").IsUnique();

            entity.Property(e => e.IdAtividade).HasColumnName("id_atividade");
            entity.Property(e => e.NomeAtividade)
                .HasMaxLength(45)
                .HasColumnName("nome_atividade");

            entity.HasMany(d => d.TbMateriasIdMateria).WithMany(p => p.TbAtividadesIdAtividades)
                .UsingEntity<Dictionary<string, object>>(
                    "TbAtividadesHasTbMateria",
                    r => r.HasOne<TbMateria>().WithMany()
                        .HasForeignKey("TbMateriasIdMateria")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_atividades_has_tb_materias_tb_materias1"),
                    l => l.HasOne<TbAtividade>().WithMany()
                        .HasForeignKey("TbAtividadesIdAtividade")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_atividades_has_tb_materias_tb_atividades1"),
                    j =>
                    {
                        j.HasKey("TbAtividadesIdAtividade", "TbMateriasIdMateria").HasName("PRIMARY");
                        j.ToTable("tb_atividades_has_tb_materias");
                        j.HasIndex(new[] { "TbAtividadesIdAtividade" }, "fk_tb_atividades_has_tb_materias_tb_atividades1_idx");
                        j.HasIndex(new[] { "TbMateriasIdMateria" }, "fk_tb_atividades_has_tb_materias_tb_materias1_idx");
                        j.IndexerProperty<int>("TbAtividadesIdAtividade").HasColumnName("tb_atividades_id_atividade");
                        j.IndexerProperty<int>("TbMateriasIdMateria").HasColumnName("tb_materias_id_materia");
                    });
        });

        modelBuilder.Entity<TbMateria>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PRIMARY");

            entity.ToTable("tb_materias");

            entity.HasIndex(e => e.IdMateria, "id_materia_UNIQUE").IsUnique();

            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.NomeMateria)
                .HasMaxLength(45)
                .HasColumnName("nome_materia");
        });

        modelBuilder.Entity<TbResposta>(entity =>
        {
            entity.HasKey(e => e.IdResposta).HasName("PRIMARY");

            entity.ToTable("tb_respostas");

            entity.HasIndex(e => e.IdResposta, "id_resposta_UNIQUE").IsUnique();

            entity.Property(e => e.IdResposta)
                  .HasColumnName("id_resposta");

            entity.Property(e => e.FinalResposta)
                  .HasMaxLength(500)
                  .HasColumnName("final_resposta");

            entity.Property(e => e.IdAluno)
                  .HasColumnName("tb_alunos_id_aluno");

            entity.Property(e => e.IdAtividade)
                  .HasColumnName("tb_atividades_id_atividade");

            entity.Property(e => e.IdQuestao)
                  .HasColumnName("tb_questoes_id_questao");

            entity.HasOne(d => d.Aluno)
                  .WithMany(p => p.TbRespostasIdRespostas)
                  .HasForeignKey(d => d.IdAluno)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_tb_respostas_tb_alunos");

            entity.HasOne(d => d.Atividade)
                  .WithMany(p => p.TbRespostasIdRespostas)
                  .HasForeignKey(d => d.IdAtividade)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_tb_respostas_tb_atividades");

            entity.HasOne(d => d.Questao)
                  .WithMany(p => p.TbRespostasIdRespostas)
                  .HasForeignKey(d => d.IdQuestao)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_tb_respostas_tb_questoes");
        });


        modelBuilder.Entity<TbPessoa>(entity =>
        {
            entity.HasKey(e => e.IdPessoa).HasName("PRIMARY");

            entity.ToTable("tb_pessoas");

            entity.HasIndex(e => e.IdPessoa, "id_pessoa_UNIQUE").IsUnique();

            entity.HasIndex(e => e.LoginPessoa, "login_pessoa_UNIQUE").IsUnique();

            entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");
            entity.Property(e => e.BoolProfessorPessoa).HasColumnName("bool_professor_pessoa");
            entity.Property(e => e.LoginPessoa)
                .HasMaxLength(45)
                .HasColumnName("login_pessoa");
            entity.Property(e => e.SenhaPessoa)
                .HasMaxLength(45)
                .HasColumnName("senha_pessoa");

            entity.HasMany(d => d.TbAlunosIdAlunos).WithMany(p => p.TbPessoasIdPessoas)
                .UsingEntity<Dictionary<string, object>>(
                    "TbPessoasHasTbAluno",
                    r => r.HasOne<TbAluno>().WithMany()
                        .HasForeignKey("TbAlunosIdAluno")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_pessoas_has_tb_alunos_tb_alunos1"),
                    l => l.HasOne<TbPessoa>().WithMany()
                        .HasForeignKey("TbPessoasIdPessoa")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_pessoas_has_tb_alunos_tb_pessoas1"),
                    j =>
                    {
                        j.HasKey("TbPessoasIdPessoa", "TbAlunosIdAluno").HasName("PRIMARY");
                        j.ToTable("tb_pessoas_has_tb_alunos");
                        j.HasIndex(new[] { "TbAlunosIdAluno" }, "fk_tb_pessoas_has_tb_alunos_tb_alunos1_idx");
                        j.HasIndex(new[] { "TbPessoasIdPessoa" }, "fk_tb_pessoas_has_tb_alunos_tb_pessoas1_idx");
                        j.IndexerProperty<int>("TbPessoasIdPessoa").HasColumnName("tb_pessoas_id_pessoa");
                        j.IndexerProperty<int>("TbAlunosIdAluno").HasColumnName("tb_alunos_id_aluno");
                    });
        });

        modelBuilder.Entity<TbProfessore>(entity =>
        {
            entity.HasKey(e => e.IdProfessor).HasName("PRIMARY");

            entity.ToTable("tb_professores");

            entity.HasIndex(e => e.ContatoProfessor, "contato_professor_UNIQUE").IsUnique();

            entity.HasIndex(e => e.IdProfessor, "id_professor_UNIQUE").IsUnique();

            entity.HasIndex(e => e.NomeProfessor, "nome_professor_UNIQUE").IsUnique();

            entity.Property(e => e.IdProfessor).HasColumnName("id_professor");
            entity.Property(e => e.ContatoProfessor)
                .HasMaxLength(45)
                .HasColumnName("contato_professor");
            entity.Property(e => e.MateriaProfessor)
                .HasMaxLength(45)
                .HasColumnName("materia_professor");
            entity.Property(e => e.NomeProfessor)
                .HasMaxLength(45)
                .HasColumnName("nome_professor");

            entity.HasMany(d => d.TbMateriasIdMateria).WithMany(p => p.TbProfessoresIdProfessors)
                .UsingEntity<Dictionary<string, object>>(
                    "TbProfessoresHasTbMateria",
                    r => r.HasOne<TbMateria>().WithMany()
                        .HasForeignKey("TbMateriasIdMateria")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_professores_has_tb_materias_tb_materias1"),
                    l => l.HasOne<TbProfessore>().WithMany()
                        .HasForeignKey("TbProfessoresIdProfessor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_professores_has_tb_materias_tb_professores"),
                    j =>
                    {
                        j.HasKey("TbProfessoresIdProfessor", "TbMateriasIdMateria").HasName("PRIMARY");
                        j.ToTable("tb_professores_has_tb_materias");
                        j.HasIndex(new[] { "TbMateriasIdMateria" }, "fk_tb_professores_has_tb_materias_tb_materias1_idx");
                        j.HasIndex(new[] { "TbProfessoresIdProfessor" }, "fk_tb_professores_has_tb_materias_tb_professores_idx");
                        j.IndexerProperty<int>("TbProfessoresIdProfessor").HasColumnName("tb_professores_id_professor");
                        j.IndexerProperty<int>("TbMateriasIdMateria").HasColumnName("tb_materias_id_materia");
                    });

            entity.HasMany(d => d.TbPessoasIdPessoas).WithMany(p => p.TbProfessoresIdProfessors)
                .UsingEntity<Dictionary<string, object>>(
                    "TbProfessoresHasTbPessoa",
                    r => r.HasOne<TbPessoa>().WithMany()
                        .HasForeignKey("TbPessoasIdPessoa")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_professores_has_tb_pessoas_tb_pessoas1"),
                    l => l.HasOne<TbProfessore>().WithMany()
                        .HasForeignKey("TbProfessoresIdProfessor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_professores_has_tb_pessoas_tb_professores1"),
                    j =>
                    {
                        j.HasKey("TbProfessoresIdProfessor", "TbPessoasIdPessoa").HasName("PRIMARY");
                        j.ToTable("tb_professores_has_tb_pessoas");
                        j.HasIndex(new[] { "TbPessoasIdPessoa" }, "fk_tb_professores_has_tb_pessoas_tb_pessoas1_idx");
                        j.HasIndex(new[] { "TbProfessoresIdProfessor" }, "fk_tb_professores_has_tb_pessoas_tb_professores1_idx");
                        j.IndexerProperty<int>("TbProfessoresIdProfessor").HasColumnName("tb_professores_id_professor");
                        j.IndexerProperty<int>("TbPessoasIdPessoa").HasColumnName("tb_pessoas_id_pessoa");
                    });
        });

        modelBuilder.Entity<TbQuesto>(entity =>
        {
            entity.HasKey(e => e.IdQuestao).HasName("PRIMARY");

            entity.ToTable("tb_questoes");

            entity.HasIndex(e => e.IdQuestao, "id_questao_UNIQUE").IsUnique();

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

            entity.HasMany(d => d.TbAtividadesIdAtividades).WithMany(p => p.TbQuestoesIdQuestoes)
                .UsingEntity<Dictionary<string, object>>(
                    "TbQuestoesHasTbAtividade",
                    r => r.HasOne<TbAtividade>().WithMany()
                        .HasForeignKey("TbAtividadesIdAtividade")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_questoes_has_tb_atividades_tb_atividades1"),
                    l => l.HasOne<TbQuesto>().WithMany()
                        .HasForeignKey("TbQuestoesIdQuestoes")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_tb_questoes_has_tb_atividades_tb_questoes1"),
                    j =>
                    {
                        j.HasKey("TbQuestoesIdQuestoes", "TbAtividadesIdAtividade").HasName("PRIMARY");
                        j.ToTable("tb_questoes_has_tb_atividades");
                        j.HasIndex(new[] { "TbAtividadesIdAtividade" }, "fk_tb_questoes_has_tb_atividades_tb_atividades1_idx");
                        j.HasIndex(new[] { "TbQuestoesIdQuestoes" }, "fk_tb_questoes_has_tb_atividades_tb_questoes1_idx");
                        j.IndexerProperty<int>("TbQuestoesIdQuestoes").HasColumnName("tb_questoes_id_questao");
                        j.IndexerProperty<int>("TbAtividadesIdAtividade").HasColumnName("tb_atividades_id_atividade");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
