using System;

namespace TallerMecanico.Models.Domain.Entities
{
    public class VehiculoMecanico
    {
        public Guid VehiculoMecanicoId { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid VehiculoId { get; set; }
        public string Diagnostico { get; set; }
        public string Comentario { get; set; }
        public Estado Estado { get; set; }
        public Guid EstadoId { get; set; }
        public bool Eliminado { get; set; }
    }
}
