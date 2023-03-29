using System;
using System.Collections.Generic;

namespace TallerMecanico.Models.Domain.Entities
{
    public class Modelo
    {
        public Guid ModeloId { get; set; }
        public string Nombre { get; set; }
        public Marca Marca { get; set; }
        public Guid MarcaId { get; set; }
        public bool Eliminado { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }

        public Modelo()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }
    }
}
