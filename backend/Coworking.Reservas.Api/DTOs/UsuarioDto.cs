namespace Coworking.Reservas.Api.DTOs;

public class UsuarioDto
{
    public string DNI { get; set; } = string.Empty;  
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}