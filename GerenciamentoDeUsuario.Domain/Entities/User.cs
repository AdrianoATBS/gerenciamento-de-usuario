using GerenciamentoDeUsuario.Domain.Enums;

namespace GerenciamentoDeUsuario.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } 
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string SenhaHash { get; private set; }
    public UserPerfil Perfil { get; private set; }
    public DateTime CriadoEm { get; private set; } 
    public bool IsAtivo { get; private set; } 

    public User(string nome, string email, 
        string senhaHash, UserPerfil perfil)
    {
        
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email é obrigatório.", nameof(email));
        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new ArgumentException("Senha é obrigatória.", nameof(senhaHash));


        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Perfil = perfil;
        CriadoEm = DateTime.UtcNow;
        IsAtivo = true;
    }

    public void Desativar()
    {
        if(!IsAtivo)
            throw new InvalidOperationException("Usuário já está desativado.");

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
        Email = email;
        Perfil = perfil;
    }
    
    public void AlterarSenha(string novaSenhaHash)
    {
        if(string.IsNullOrWhiteSpace(novaSenhaHash))
            throw new ArgumentException("Nova senha é obrigatória.", nameof(novaSenhaHash));

        if (SenhaHash == novaSenhaHash)
            throw new ArgumentException("A nova senha deve ser diferente da senha atual.", nameof(novaSenhaHash));

        if (string.IsNullOrWhiteSpace(novaSenhaHash))
            throw new ArgumentException("Nova senha é obrigatória.", nameof(novaSenhaHash));

        SenhaHash = novaSenhaHash;
    }

}
