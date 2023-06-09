﻿// <auto-generated />
using Certificados.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Certificados.Infra.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Certificados.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "super@super.com",
                            Nome = "super",
                            Salt = "754ab257a6ca7b2b29236cf2f899dffd8719efa9a4b81eb3e0fe2f2fe079512dbc2fa979e8866ea9a340ee2c3c971ee51b45b751846406a28741f4b5110523b6",
                            Senha = "639f80627cbccf696bc1c0f959c82c4f6bc5b441ad047f8c4450b6346c20cf1f"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
