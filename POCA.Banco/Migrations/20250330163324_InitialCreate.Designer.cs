﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POCA.Banco.Model;

#nullable disable

namespace POCA.Banco.Migrations
{
    [DbContext(typeof(DbPocaContext))]
    [Migration("20250330163324_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("POCA.Banco.Model.TbQuestoes", b =>
                {
                    b.Property<int>("IdQuestao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_questao");

                    b.Property<string>("EnunciadoQuestao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("enunciado_questao");

                    b.Property<string>("RespostacertaQuestao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("respostacerta_questao");

                    b.Property<string>("Respostaerrada1Questao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("respostaerrada1_questao");

                    b.Property<string>("Respostaerrada2Questao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("respostaerrada2_questao");

                    b.Property<string>("Respostaerrada3Questao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("respostaerrada3_questao");

                    b.HasKey("IdQuestao")
                        .HasName("PRIMARY");

                    b.ToTable("tb_questoes", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
