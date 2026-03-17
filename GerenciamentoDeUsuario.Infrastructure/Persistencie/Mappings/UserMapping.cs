using GerenciamentoDeUsuario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciamentoDeUsuario.Infrastructure.Persistencie.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Perfil)
            .IsRequired();

        builder.Property(u => u.CriadoEm)
            .IsRequired();

        builder.Property(u => u.IsAtivo)
            .IsRequired();

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Endereco)
                .HasColumnName("Email")
                .IsRequired();
        });

        builder.OwnsOne(u => u.SenhaHash, senha =>
        {
            senha.Property(s => s.Hash)
                .HasColumnName("SenhaHash")
                .IsRequired();
        });

        builder.HasMany(u => u.Tokens)
            .WithOne()
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(u => u.Tokens)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        
    }
}
