namespace GerenciamentoDeUsuario.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationTolen = default);
}
