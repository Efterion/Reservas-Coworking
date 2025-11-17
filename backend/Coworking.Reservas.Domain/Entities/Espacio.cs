using Coworking.Reservas.Domain.Enums;

namespace Coworking.Reservas.Domain.Entities
{
    public class Espacio
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public TipoEspacio Tipo { get; set; }
        public int Capacidad { get; set; }
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;


        public ICollection<Reserva>? Reservas { get; set; }
    }
}
