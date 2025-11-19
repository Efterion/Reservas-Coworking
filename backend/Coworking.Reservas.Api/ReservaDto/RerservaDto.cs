public class ReservaDto
{
    public string UsuarioDNI { get; set; } = string.Empty;
    public Guid EspacioId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string? Comentarios { get; set; }
}