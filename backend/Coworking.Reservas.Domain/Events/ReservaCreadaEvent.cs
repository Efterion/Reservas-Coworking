namespace Coworking.Reservas.Domain.Events;

public class ReservaCreadaEvent
{
    public Guid ReservaId { get; set; }
    public string UsuarioDNI { get; set; } = string.Empty;
    public Guid EspacioId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public DateTime FechaCreacion { get; set; }
}