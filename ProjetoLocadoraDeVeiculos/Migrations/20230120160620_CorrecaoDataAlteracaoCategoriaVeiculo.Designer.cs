﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoLocadoraDeVeiculos.Data;

#nullable disable

namespace ProjetoLocadoraDeVeiculos.Migrations
{
    [DbContext(typeof(ProjetoLocadoraDeVeiculosContext))]
    [Migration("20230120160620_CorrecaoDataAlteracaoCategoriaVeiculo")]
    partial class CorrecaoDataAlteracaoCategoriaVeiculo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.CategoriaVeiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CategoriaVeiculo");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cnh")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.Locacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataEntrega")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataEntregaOriginal")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataLocacao")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal?>("Desconto")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("QtdDiasAlugados")
                        .HasColumnType("int");

                    b.Property<int?>("QtdRenovacoes")
                        .HasColumnType("int");

                    b.Property<int>("StatusLocacaoId")
                        .HasColumnType("int");

                    b.Property<int>("TemporadaId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorDiaria")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("ValorMultaDiaria")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("ValorMultaFixa")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("ValorTotal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("VeiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("StatusLocacaoId");

                    b.HasIndex("TemporadaId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("Locacao");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.StatusLocacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<bool?>("Internal")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("StatusLocacao");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.StatusVeiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<bool?>("Internal")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("StatusVeiculo");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.Temporada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("PercentualAcrescerDiaria")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PercentualAcrescerMultaDiaria")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PercentualAcrescerMultaFixa")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Temporada");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoriaVeiculoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("StatusVeiculoId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorDiaria")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("ValorMultaDiaria")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("ValorMultaFixa")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaVeiculoId");

                    b.HasIndex("StatusVeiculoId");

                    b.ToTable("Veiculo");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.Locacao", b =>
                {
                    b.HasOne("ProjetoLocadoraDeVeiculos.Models.Cliente", "Cliente")
                        .WithMany("Locacoes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoLocadoraDeVeiculos.Models.StatusLocacao", "StatusLocacao")
                        .WithMany("Veiculos")
                        .HasForeignKey("StatusLocacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoLocadoraDeVeiculos.Models.Temporada", "Temporada")
                        .WithMany("Locacoes")
                        .HasForeignKey("TemporadaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoLocadoraDeVeiculos.Models.Veiculo", "Veiculo")
                        .WithMany("Locacoes")
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("StatusLocacao");

                    b.Navigation("Temporada");

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.Veiculo", b =>
                {
                    b.HasOne("ProjetoLocadoraDeVeiculos.Models.CategoriaVeiculo", "CategoriaVeiculo")
                        .WithMany("Veiculos")
                        .HasForeignKey("CategoriaVeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoLocadoraDeVeiculos.Models.StatusVeiculo", "StatusVeiculo")
                        .WithMany("Veiculos")
                        .HasForeignKey("StatusVeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoriaVeiculo");

                    b.Navigation("StatusVeiculo");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.CategoriaVeiculo", b =>
                {
                    b.Navigation("Veiculos");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.Cliente", b =>
                {
                    b.Navigation("Locacoes");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.StatusLocacao", b =>
                {
                    b.Navigation("Veiculos");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.StatusVeiculo", b =>
                {
                    b.Navigation("Veiculos");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.Temporada", b =>
                {
                    b.Navigation("Locacoes");
                });

            modelBuilder.Entity("ProjetoLocadoraDeVeiculos.Models.Veiculo", b =>
                {
                    b.Navigation("Locacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
