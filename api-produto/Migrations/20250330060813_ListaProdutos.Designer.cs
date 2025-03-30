﻿// <auto-generated />
using ApiProduto.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace api_produto.Migrations
{
    [DbContext(typeof(ProdutoDbContext))]
    [Migration("20250330060813_ListaProdutos")]
    partial class ListaProdutos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Estoque")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Produtos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Estoque = 23,
                            Nome = "Notebook Asus",
                            Valor = 3800.00m
                        },
                        new
                        {
                            Id = 2,
                            Estoque = 51,
                            Nome = "Mouse Logitech",
                            Valor = 150.00m
                        },
                        new
                        {
                            Id = 3,
                            Estoque = 19,
                            Nome = "Teclado Logitech",
                            Valor = 380.00m
                        },
                        new
                        {
                            Id = 4,
                            Estoque = 15,
                            Nome = "Monitor AOC",
                            Valor = 560.00m
                        },
                        new
                        {
                            Id = 5,
                            Estoque = 3,
                            Nome = "Impressora",
                            Valor = 890.00m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
