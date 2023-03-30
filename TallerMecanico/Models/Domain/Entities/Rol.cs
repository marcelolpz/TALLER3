using System.Collections.Generic;
using System;

namespace TallerMecanico.Models.Domain.Entities
{
    public class Rol
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion2 { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<ModulosRoles> ModulosRoles { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }

        public Rol()
        {
            ModulosRoles = new HashSet<ModulosRoles>();
            Usuarios = new HashSet<Usuario>();
        }
        public static Rol Create(string Descripcion, string Descripcion2)
        {
            return new Rol
            {
                Id = Guid.NewGuid(),
                Descripcion = Descripcion,
                Descripcion2 = Descripcion2,
                CreatedDate = DateTime.Now,
            };
        }
        public void Update(string Descripcion, string Descripcion2)
        {
            {
                this.Descripcion = Descripcion;
                this.Descripcion2 = Descripcion2;
            };
        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}
