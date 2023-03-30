using System;

namespace TallerMecanico.Models.Domain.Entities
{
    public class VehiculoMecanico
    {
        public Guid VehiculoMecanicoId { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public Guid VehiculoId { get; set; }
        public string Diagnostico { get; set; }
        public string Comentario { get; set; }
        public Estado Estado { get; set; }
        public Guid EstadoId { get; set; }

        public bool Eliminado { get; set; }

        public static VehiculoMecanico Create(Guid usuarioId, Guid vehiculoId, string diagnostico, string comentario, Guid estadoId){

            return new VehiculoMecanico
            {
                UsuarioId = usuarioId,
                VehiculoId = vehiculoId,
                Diagnostico = diagnostico,
                Comentario = comentario,
                EstadoId = estadoId,
                VehiculoMecanicoId = Guid.NewGuid(),
            };
        }

        public void Update(Guid usuarioId, Guid vehiculoId, string diagnostico, string comentario, Guid estadoId)
        {
            this.UsuarioId = usuarioId;
            this.VehiculoId = vehiculoId;
            this.Diagnostico = diagnostico;
            this.Comentario = comentario;
            this.EstadoId = estadoId;
        }

        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}
