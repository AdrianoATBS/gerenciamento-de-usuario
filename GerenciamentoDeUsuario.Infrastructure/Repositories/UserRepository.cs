using GerenciamentoDeUsuario.Application.Interfaces;
using GerenciamentoDeUsuario.Domain.Entities;
using GerenciamentoDeUsuario.Domain.Interfaces;
using GerenciamentoDeUsuario.Infrastructure.Persistencie;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeUsuario.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
      
    }
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
       
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
      
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.Tokens)
            .FirstOrDefaultAsync(u => u.Email.Endereco == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
       return await _context.Users
            .AsNoTracking()
            .Include(u => u.Tokens)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        
    }
}
