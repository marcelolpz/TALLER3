using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using TallerMecanico.Models.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Examen.Models.ViewModels;

namespace TallerMecanico.Models.ViewModels
{
    public class VehiculoVm
    {
        public Guid VehiculoId { get; set; }
        public Modelo Modelo { get; set; }
        public Guid ModeloId { get; set; }
        public Color Color { get; set; }
        public Guid ColorId { get; set; }
        public string Placa { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public List<SelectListItem> Usuarios { get; set; }
        public List<SelectListItem> Colores { get; set; }
        public List<SelectListItem> Modelos { get; set; }
        public DateTime CreatedDate { get; set; }


        public AppResultVm Validar()
        {
            AppResultVm app = new AppResultVm();
            app.Mensaje = "";
            if ((this.ModeloId == null || this.ModeloId == Guid.Empty))
            {
                app.Mensaje = "El modelo no puede ir vacio. ";
            }
            if ((this.ColorId == null || this.ColorId == Guid.Empty))
            {
                app.Mensaje += "El color no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Placa))
            {
                app.Mensaje = "La placa no puede ir vacia. ";
            }
            if (this.UsuarioId == null || this.UsuarioId == Guid.Empty)
            {
                app.Mensaje += "El UsuarioId no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Vehiculo insertado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }

        public AppResultVm ValidarUpdate()
        {
            AppResultVm app = new AppResultVm();
            app.Mensaje = "";
            if ((this.ModeloId == null || this.ModeloId == Guid.Empty))
            {
                app.Mensaje = "El modelo no puede ir vacio. ";
            }
            if ((this.ColorId == null || this.ColorId == Guid.Empty))
            {
                app.Mensaje += "El color no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Placa))
            {
                app.Mensaje = "La placa no puede ir vacia. ";
            }
            if (this.UsuarioId == null || this.UsuarioId == Guid.Empty)
            {
                app.Mensaje += "El UsuarioId no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Vehiculo insertado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }


        public AppResultVm ValidarEliminar()
        {
            AppResultVm app = new AppResultVm();
            app.Mensaje = "";
            if (this.VehiculoId == null || this.VehiculoId == Guid.Empty)
            {
                app.Mensaje += "El Id del vehiculo no puede ir vacio. ";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Vehiculo Eliminado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }

    }
}
