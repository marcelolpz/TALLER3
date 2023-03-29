using System;
using System.Collections.Generic;

namespace TallerMecanico.Models.Domain.Entities
{
    public class Marca
    {
        public Guid MarcaId { get; set; }
        public string Nombre { get; set; }
        public bool Eliminado { get; set; }
        public ICollection<Modelo> Modelos { get; set; }

        public Marca()
        {
             Modelos = new HashSet<Modelo>();
        }
    }
}
