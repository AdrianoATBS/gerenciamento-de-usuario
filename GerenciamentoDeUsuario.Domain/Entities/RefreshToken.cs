using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeUsuario.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public string Token { get; private set; }
    public DateTime Expiration { get; private set; }
    public bool Revogado { get; private set; }
    public Guid UserId { get; private set; }

    public RefreshToken(string token, DateTime expiration, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token é obrigatório.", nameof(token));
        
        if (expiration <= DateTime.UtcNow)
            throw new ArgumentException("Data de expiração deve ser futura.", nameof(expiration));
        
        Id = Guid.NewGuid();
        Token = token;
        Expiration = expiration;
        Revogado = false;
        UserId = userId;
    }

    public void Revogar()
    {
        if(Revogado)
            throw new InvalidOperationException("Token já está revogado.");
        
        Revogado = true;
    }
}
