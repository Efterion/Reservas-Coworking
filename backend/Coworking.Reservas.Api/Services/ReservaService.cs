using Coworking.Reservas.Domain.Entities;
using Coworking.Reservas.Domain.Enums;
using Coworking.Reservas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Reservas.Api.Services;

public class ReservaService
{
    private readonly ReservasDbContext _context;

    public ReservaService(ReservasDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reserva>> GetAll()
    {
        return await _context.Reservas
            .Include(r => r.Usuario)
            .Include(r => r.Espacio)
            .ToListAsync();
    }

    public async Task<bool> TieneSolapamiento(Guid espacioId, DateTime inicio, DateTime fin)
    {
        return await _context.Reservas.AnyAsync(r =>
                r.EspacioId == espacioId &&
                r.Estado != EstadoReserva.Cancelada &&
                inicio < r.FechaFin && 
                fin > r.FechaInicio
        );
    }

    public async Task<Reserva> Create(Reserva reserva)
    {
        // Validar solapamiento
        if (await TieneSolapamiento(reserva.EspacioId, reserva.FechaInicio, reserva.FechaFin))
        {
            throw new Exception("El espacio ya está reservado en ese horario.");
        }

        reserva.Id = Guid.NewGuid();
        reserva.FechaCreacion = DateTime.UtcNow;

        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();
        return reserva;
    }

    public async Task<Reserva> ConfirmarReservaAsync(Guid id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null)
        
            throw new Exception("La reserva no existe.");

        if (reserva.Estado != EstadoReserva.Pendiente)
        
            throw new Exception("Solo se pueden confirmar reservas pendientes.");
        
        reserva.Estado = EstadoReserva.Confirmada;
        reserva.FechaActualizacion = DateTime.UtcNow;

        await _context.SaveChangesAsync(); 
        return reserva;

    }

    public async Task<IEnumerable<Reserva>> GetByUsuarioAsync(string dni)
    {
        return await _context.Reservas
            .Where(r => r.UsuarioDNI == dni)
            .Include(r => r.Espacio)
            .ToListAsync();
    }

    public async Task<Reserva> FinalizarReservaAsync(Guid id)
    {
        var reserva = await _context.Reservas.FindAsync(id);

        if (reserva == null)
            throw new Exception("La reserva no existe.");

        if (reserva.Estado == EstadoReserva.Cancelada)
            throw new Exception("No se puede finalizar una reserva cancelada.");

        reserva.Estado = EstadoReserva.Finalizada;
        reserva.FechaActualizacion = DateTime.UtcNow;

        await _context.SaveChangesAsync(); 
        return reserva;
    }
}