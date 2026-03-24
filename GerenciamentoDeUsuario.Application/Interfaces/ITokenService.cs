using GerenciamentoDeUsuario.Domain.Entities;

namespace GerenciamentoDeUsuario.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(User user);

}
