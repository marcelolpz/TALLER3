using System;
using System.Collections.Generic;
using System.Numerics;
using TallerMecanico.Models.ViewModels;

namespace TallerMecanico.Models.Domain.Entities
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public bool Eliminado { get; set; }
        public DateTime CreatedDate { get; set; }
        public Rol Rol { get; set; }
        public Guid RolId { get; set; }
        public string recovery { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }
        public ICollection<VehiculoMecanico> VehiculoMecanicos { get; set; }
        public Usuario()
        {
            Vehiculos = new HashSet<Vehiculo>();
            VehiculoMecanicos = new HashSet<VehiculoMecanico>();
        }

        public static Usuario Create(string nombre, string apellido, string correo, string password, Guid rolId)
        {
            return new Usuario
            {
                UsuarioId = Guid.NewGuid(),
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                Password = password,
                Eliminado = false,
                RolId = rolId
            };
        }
        public void Update(string nombre, string apellido, string correo, string password, Guid rolId)
        {
            {
                this.Nombre = nombre;
                this.Apellido = apellido;
                this.Correo = correo;
                this.Password = password;
                this.Eliminado = false;
                this.RolId = rolId;
            };
        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}
