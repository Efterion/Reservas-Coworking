using Coworking.Reservas.Domain.Enums;

namespace Coworking.Reservas.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; } = RolUsuario.Usuario;
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;


        public ICollection<Reserva>? Reservas { get; set; }
        }
    }

