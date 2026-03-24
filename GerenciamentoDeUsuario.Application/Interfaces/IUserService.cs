using GerenciamentoDeUsuario.Application.DTOs;

namespace GerenciamentoDeUsuario.Application.Interfaces;

public interface IUserService
{
    Task RegisterAsync(RegisterUserDto dto);
    Task UpdateUserAsync(UpdateUserDto dto);
    Task ChangePasswordAsync(ChangePasswordDto dto);
    Task DeactivateAsync(Guid userId);
    Task<UserResponseDto> GetByIdAsync(Guid id);
    Task<string> LoginAsync(LoginDto dto);
}
