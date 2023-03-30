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


        public static Vehiculo Create(Guid modeloId, Guid colorId, string matricula,Guid usuarioId)
        {
            return new Vehiculo
            {
                ModeloId = modeloId,
                ColorId = colorId,
                Placa = matricula,
                UsuarioId = usuarioId,
                CreatedDate = DateTime.Today,
                VehiculoId = Guid.NewGuid(),
            };
        }

        public void Update(Guid modeloId, Guid colorId, string matricula, Guid usuarioId)
        {
            this.ModeloId = modeloId;
            this.ColorId = colorId;
            this.Placa = matricula;
            this.UsuarioId = usuarioId;
        }

        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}
