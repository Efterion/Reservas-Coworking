using Coworking.Reservas.Domain.Enums;

namespace Coworking.Reservas.Api.DTOs;

public class EspacioDto
{
    public string Nombre { get; set; } = string.Empty;
    public TipoEspacio Tipo { get; set; }
    public int Capacidad { get; set; }
    public string? Descripcion { get; set; }
}