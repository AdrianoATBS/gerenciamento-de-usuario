namespace GerenciamentoDeUsuario.Application.DTOs;

public class ChangePasswordDto
{
    public Guid UserId { get; set; } 
    public string NovaSenha { get; set; } = string.Empty;
}
