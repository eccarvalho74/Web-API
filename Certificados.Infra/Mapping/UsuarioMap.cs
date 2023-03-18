using Certificados.Domain.Entities;
using Certificados.Infra.Integration.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certificados.Infra.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .ToTable("Usuario")
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(u => u.Nome)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder
                .Property(u => u.Email)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder
                .Property(u => u.Senha)
                .HasColumnType("nvarchar(128)")
                .IsRequired();

            builder
                .Property(u => u.Salt)
                .HasColumnType("nvarchar(128)")
                .IsRequired();

            var _salt = GeradorDeSenha.GerarSalt(64);
            builder
                .HasData(new Usuario
                {
                    Id = 1,
                    Nome = "super",
                    Email = "super@super.com",
                    Salt = _salt,
                    Senha = GeradorDeSenha.ObterHash("super", _salt)
                });

          
        }
    }
}
