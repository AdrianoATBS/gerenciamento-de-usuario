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
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository repository
        ,IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    

    public async Task ChangePasswordAsync(ChangePasswordDto dto)
    {
        var user = await GetUserOrThrow(dto.UserId);

        user.AlterarSenha(new Senha(dto.NovaSenha));

        await _repository.UpdateAsync(user);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task DeactivateAsync(Guid userId)
    {
        var user = await GetUserOrThrow(userId);

        user.Desativar();

        await _repository.UpdateAsync(user);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task<UserResponseDto> GetByIdAsync(Guid id)
    {
        var user = await GetUserOrThrow(id);

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
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task UpdateUserAsync(UpdateUserDto dto)
    {
        var user = await GetUserOrThrow(dto.Id);

     
        user.AlterarUsuario(
            dto.Nome,
            dto.Email,
            UserPerfil.Usuario
        );
        await _repository.UpdateAsync(user);
        await _unitOfWork.SaveChangeAsync();

    }

    private async Task<User> GetUserOrThrow(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        if(user == null)
            throw new Exception("Usuário não encontrado");

        return user;
    }
}
