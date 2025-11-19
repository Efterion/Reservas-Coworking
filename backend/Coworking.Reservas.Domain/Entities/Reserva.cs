using Coworking.Reservas.Domain.Enums;

namespace Coworking.Reservas.Domain.Entities;

public class Reserva
{
    public Guid Id { get; set; }

    public string UsuarioDNI { get; set; } = string.Empty; // FK
    public Guid EspacioId { get; set; }

    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public EstadoReserva Estado { get; set; } = EstadoReserva.Pendiente;
    public string? Comentarios { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaActualizacion { get; set; }

    public Usuario? Usuario { get; set; }
    public Espacio? Espacio { get; set; }
}