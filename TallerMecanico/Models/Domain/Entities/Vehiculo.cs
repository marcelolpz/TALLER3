using System;
using System.Collections.Generic;

namespace TallerMecanico.Models.Domain.Entities
{
    public class Vehiculo
    {
        public Guid VehiculoId { get; set; }
        public Modelo Modelo { get; set; }
        public Guid ModeloId { get; set; }
        public Color Color { get; set; }
        public Guid ColorId { get; set; }
        public string Placa { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Eliminado { get; set; }
    }
}
