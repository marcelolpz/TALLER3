using System.Collections.Generic;
using System;
using TallerMecanico.Models.ViewModels;

namespace TallerMecanico.Models.Domain.Entities
{
    public class Modulo
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Metodo { get; set; }
        public string Controller { get; set; }
        public bool Eliminado { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public AgrupadoModulos AgrupadoModulos { get; set; }
        public Guid AgrupadoModulosId { get; set; }
        public ICollection<ModulosRoles> ModulosRoles { get; set; }

        public Modulo()
        {
            ModulosRoles = new HashSet<ModulosRoles>();
        }

        public static Modulo Create(string Nombre, string Metodo, string Controller, Guid AgrupadoModulosId, DateTime CreatedDate, Guid CreatedBy)
        {
            return new Modulo
            {
                Id = Guid.NewGuid(),
                Nombre = Nombre,
                Metodo = Metodo,
                Controller = Controller,
                AgrupadoModulosId = AgrupadoModulosId,
                CreatedDate = CreatedDate,
                CreatedBy = CreatedBy,
            };
        }
        public void Update(string Nombre, string Metodo, string Controller, Guid AgrupadoModulosId, DateTime CreatedDate, Guid CreatedBy)
        {
            {
                this.Nombre = Nombre;
                this.Metodo = Metodo;
                this.Controller = Controller;
                this.AgrupadoModulosId = AgrupadoModulosId;
                this.CreatedDate = CreatedDate;
                this.CreatedBy = CreatedBy;
            };
        }
        public void Delete()
        {
            this.Eliminado = true;
        }
    }
}