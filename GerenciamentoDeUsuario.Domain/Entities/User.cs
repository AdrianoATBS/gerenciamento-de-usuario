using GerenciamentoDeUsuario.Domain.Enums;
using GerenciamentoDeUsuario.Domain.ValueObjects;

namespace GerenciamentoDeUsuario.Domain.Entities;

public class User
{
    private readonly List<RefreshToken> _tokens = new();
    public IReadOnlyCollection<RefreshToken> Tokens => _tokens;

    public Guid Id { get; private set; } 
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public Senha SenhaHash { get; private set; }
    public UserPerfil Perfil { get; private set; }
    public DateTime CriadoEm { get; private set; } 
    public bool IsAtivo { get; private set; } 

    public User(string nome, string email, 
        string senhaHash, UserPerfil perfil)
    {
        
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));

        Id = Guid.NewGuid();
        Nome = nome;
        Email = new Email(email);
        SenhaHash = new Senha(senhaHash);
        Perfil = perfil;
        CriadoEm = DateTime.UtcNow;
        IsAtivo = true;

        var refreshToken = new RefreshToken(Guid.NewGuid()
            .ToString(), DateTime.UtcNow.AddDays(7),
            Id);

        AdicionarToken(refreshToken);
    }

    public void Desativar()
    {
        if(!IsAtivo)
            throw new InvalidOperationException("Usuário já está desativado.");
            
        foreach(var token in _tokens)
        {
            token.Revogar();
        }

        IsAtivo = false;
    }
    public void AlterarUsuario(string nome, string email, UserPerfil perfil)
    {
        if (!IsAtivo)
            throw new InvalidOperationException("Usuário desativado não pode ser alterado.");

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email é obrigatório.", nameof(email));
       
        Nome = nome;
        Email = new Email(email);
        Perfil = perfil;
    }
    
    public void AlterarSenha(Senha novaSenha)
    {
         if(IsAtivo == false)
            throw new ArgumentException("Usuário desativado não pode alterar senha.", nameof(novaSenha));

        if (SenhaHash.Equals(novaSenha))
            throw new ArgumentException("A nova senha deve ser diferente da senha atual.", nameof(novaSenha));

        SenhaHash = novaSenha;
    }

    public void AdicionarToken(RefreshToken token)
    {
        _tokens.Add(token);
    }

    public void RevogarToken(string token)
    {
        var refreshToken = _tokens.FirstOrDefault(t => t.Token == token);
        if (refreshToken == null)
            throw new ArgumentException("Token não encontrado.", nameof(token));
        
        refreshToken.Revogar();
    }

}
