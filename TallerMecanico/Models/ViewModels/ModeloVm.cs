using System;
using TallerMecanico.Models.Domain.Entities;

namespace TallerMecanico.Models.ViewModels
{
    public class ModeloVm
    {
        public Guid ModeloId { get; set; }
        public string Nombre { get; set; }
        public Marca Marca { get; set; }
        public Guid MarcaId { get; set; }
    }
}
