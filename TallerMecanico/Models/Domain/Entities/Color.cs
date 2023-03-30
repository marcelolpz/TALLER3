using System;
using System.Collections.Generic;

namespace TallerMecanico.Models.Domain.Entities
{
    public class Color
    {
        public Guid ColorId { get; set; }
        public string Nombre { get; set; }
        public bool Eliminado { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }

        public Color()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }
    }
}
