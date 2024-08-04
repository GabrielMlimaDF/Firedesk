﻿// <auto-generated />
using System;
using FireDesk.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FireDesk.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240804152715_UsuarioSenha")]
    partial class UsuarioSenha
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FireDesk.Models.TecnicosModel", b =>
                {
                    b.Property<int>("TecnicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TecnicoId"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeTecnico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TecnicoId");

                    b.ToTable("Tecnicos");
                });

            modelBuilder.Entity("FireDesk.Models.TicketsModel", b =>
                {
                    b.Property<int>("TicketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketID"));

                    b.Property<string>("Assunto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataProv")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TecnicoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioAberturaId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioFechamentoId")
                        .HasColumnType("int");

                    b.HasKey("TicketID");

                    b.HasIndex("TecnicoId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("FireDesk.Models.UsuarioModel", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"));

                    b.Property<string>("UsuarioCPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("UsuarioEmail")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("UsuarioName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("UsuarioSenha")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            UsuarioId = 1,
                            UsuarioCPF = "73282146191",
                            UsuarioEmail = "gabrieldevbrasilia@gmail.com",
                            UsuarioName = "Gabriel Matos Lima",
                            UsuarioSenha = "123"
                        });
                });

            modelBuilder.Entity("FireDesk.Models.TicketsModel", b =>
                {
                    b.HasOne("FireDesk.Models.TecnicosModel", "Tecnicos")
                        .WithMany()
                        .HasForeignKey("TecnicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tecnicos");
                });
#pragma warning restore 612, 618
        }
    }
}
