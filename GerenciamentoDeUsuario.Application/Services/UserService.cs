using GerenciamentoDeUsuario.Application.DTOs;
using GerenciamentoDeUsuario.Application.Interfaces;
using GerenciamentoDeUsuario.Domain.Entities;
using GerenciamentoDeUsuario.Domain.Enums;
using GerenciamentoDeUsuario.Domain.Interfaces;
using GerenciamentoDeUsuario.Domain.ValueObjects;

namespace GerenciamentoDeUsuario.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public async Task ChangePasswordAsync(ChangePasswordDto dto)
    {
        var user = await _repository.GetByIdAsync(dto.UserId);

        if (user == null)
            throw new Exception("Usuário não encontrado");

        user.AlterarSenha(new Senha(dto.NovaSenha));
        await _repository.UpdateAsync(user);
    }

    public async Task DeactivateAsync(Guid userId)
    {
        var user = await _repository.GetByIdAsync(userId);

        if(user == null)
            throw new Exception("Usuário não encontrado");
        user.Desativar();
        await _repository.UpdateAsync(user);
    }

    public async Task<UserResponseDto> GetByIdAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            throw new Exception("Usuário não encontrado");

        return new UserResponseDto
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email.Endereco
        };
    }

    public async Task RegisterAsync(RegisterUserDto dto)
    {
        var user = new User(
            dto.Nome,
            dto.Email,
            dto.Senha,
            UserPerfil.Usuario
            );

        await _repository.AddAsync(user);
    }

    public async Task UpdateUserAsync(UpdateUserDto dto)
    {
        var user = await _repository.GetByIdAsync(dto.Id);

        if (user == null)
            throw new Exception("Usuario não encontrado");

        user.AlterarUsuario(
            dto.Nome,
            dto.Email,
            UserPerfil.Usuario
        );
        await _repository.UpdateAsync(user);

    }
}
