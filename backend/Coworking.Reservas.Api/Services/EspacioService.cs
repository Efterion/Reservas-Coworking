using Coworking.Reservas.Domain.Entities;
using Coworking.Reservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Reservas.Api.Services;

public class EspacioService
{
    private readonly ReservasDbContext _context;

    public EspacioService(ReservasDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Espacio>> GetAll()
    {
        return await _context.Espacios.ToListAsync();
    }

    public async Task<Espacio> Create(Espacio espacio)
    {
        espacio.Id = Guid.NewGuid();
        _context.Espacios.Add(espacio);
        await _context.SaveChangesAsync();
        return espacio;
    }
}