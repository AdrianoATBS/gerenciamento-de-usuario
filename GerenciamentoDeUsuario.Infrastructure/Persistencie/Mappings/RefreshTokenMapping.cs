using GerenciamentoDeUsuario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciamentoDeUsuario.Infrastructure.Persistencie.Mappings;

public class RefreshTokenMapping : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Token)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(rt => rt.Expiration)
            .IsRequired();

        builder.Property(rt => rt.Revogado)
            .IsRequired();

        builder.Property(rt => rt.UserId)
            .IsRequired();
    }
}
