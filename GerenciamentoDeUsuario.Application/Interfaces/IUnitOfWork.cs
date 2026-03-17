namespace GerenciamentoDeUsuario.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangeAsync(CancellationToken cancellationTolen = default);
}
