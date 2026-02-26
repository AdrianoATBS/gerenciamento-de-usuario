using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciamentoDeUsuario.Domain.Entities;

namespace GerenciamentoDeUsuario.Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
}
