using Coworking.Reservas.Domain.Entities;
using Coworking.Reservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace  Coworking.Reservas.Api.Services;

public class UsuarioService
{
    private readonly ReservasDbContext _context;

    public UsuarioService(ReservasDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAll()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> Create(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }
}