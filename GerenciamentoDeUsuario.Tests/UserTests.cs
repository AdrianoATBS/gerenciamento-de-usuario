using System;
using GerenciamentoDeUsuario.Domain.Entities;
using GerenciamentoDeUsuario.Domain.Enums;
using GerenciamentoDeUsuario.Domain.ValueObjects;

namespace GerenciamentoDeUsuario.Tests;

public class UserTests
{
    [Fact]
    public void CriarUsuario()
    {
        string nome = "João Silva";
        string email = "Joao@gmail.com";
        string senhaHash = "12345";
        UserPerfil perfil = UserPerfil.Usuario;

        var user = new User(nome, email, senhaHash, perfil);

        Assert.NotEmpty(user.Tokens);
    }

    [Fact]
    public void CriarUsuario_DeveFalharSeNomeVazio()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new User("", "email@gmai.com", "12345", UserPerfil.Usuario));

        Assert.StartsWith("Nome é obrigatório.", exception.Message);
    }
    public void CriarUsuario_DeveFalharSeEmailInvalido()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new User("João Silva", "emailgmai.com", "12345", UserPerfil.Usuario));
        Assert.StartsWith("Email inválido.", exception.Message);
    }

    [Fact]
    public void UsuarioDesativado_NaoPodeAlterarDados()
    {
        var user = new User("João", "email@email.com", "12345", UserPerfil.Usuario);

        user.Desativar();

        Assert.Throws<InvalidOperationException>(() =>
            user.AlterarUsuario("Maria", "email2@gmail.com", UserPerfil.Usuario));
    }

    public void UsuarioDesativado_NãoPodeAlterarSenha()
    {
        var user = new User("João", "email@email.com", "12345", UserPerfil.Usuario);

        user.Desativar();

        Assert.Throws<InvalidOperationException>(() =>
        user.AlterarSenha(new Senha("54321")));

    }

    [Fact]
    public void DesativarUsuario_DeveRevogarTokens()
    {
        string nome = "João Silva";
        string email = "joao@gmail.com";
        string senhaHash = "12345";
        UserPerfil perfil = UserPerfil.Usuario;

        var user = new User(nome, email, senhaHash, perfil);

        user.Desativar();

        Assert.True(user.Tokens.All(t => t.Revogado));
    }
    
    public void RevogarToken_DeveLancarErroSeTokenNaoExistir()
    {
         var user = new User("João", "email@gmail.com", "12345", UserPerfil.Usuario);

        var exception = Assert.Throws<ArgumentException>(() =>
            user.RevogarToken("token-inexistente"));

        Assert.StartsWith("Token não encontrado", exception.Message);
    }

    [Fact]
    public void AlterarUsuario_DeveAlterarNomeEmailPerfil()
    {
        string nome = "João Silva";
        string email = "joao@gmail.com";
        string senhaHash = "12345";
        UserPerfil perfil = UserPerfil.Usuario;
        var user = new User(nome, email, senhaHash, perfil);

        user.AlterarUsuario("Maria Silva", "maria@gmail.com", 
            UserPerfil.Administrador);

        Assert.Equal("Maria Silva", user.Nome);
        Assert.Equal("maria@gmail.com", user.Email.Endereco);
        Assert.Equal(UserPerfil.Administrador, user.Perfil);

    }

    [Fact]
    public void AlterarSenha_DeveLancarErroSeSenhaForIgual()
    {
        string nome = "João Silva";
        string email = "joao@gmail.com";
        string senhaHash = "12345";
        UserPerfil perfil = UserPerfil.Usuario;
        var user = new User(nome, email, senhaHash, perfil);

        var novaSenha = new Senha("12345");

        var exception = 
            Assert.Throws<ArgumentException>(() => 
            user.AlterarSenha(novaSenha));
        
        Assert.StartsWith("A nova senha deve ser diferente da senha atual.", exception.Message);

    }

    [Fact]
    public void Email_DeveLancarErroSeEmailInvalido()
    {
        string nome = "João Silva";
        string email = "Joaogmai.com";  
        string senhaHash = "12345";
        UserPerfil perfil = UserPerfil.Usuario;

        var exception =
            Assert.Throws<ArgumentException>(() => new User(nome, email, senhaHash, perfil));
        Assert.StartsWith("Email inválido.", exception.Message);
    }
}
