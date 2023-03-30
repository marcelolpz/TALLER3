using Examen.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using TallerMecanico.Models.Domain.Entities;

namespace TallerMecanico.Models.ViewModels
{
    public class VehiculoMecanicoVm
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


        public List<SelectListItem> Vehiculoss { get; set; }
        public List<SelectListItem> Usuarioss { get; set; }
        public List<SelectListItem> Estadoss { get; set; }

        public AppResultVm Validar()
        {
            AppResultVm app = new AppResultVm();
            app.Mensaje = "";
            if ((this.UsuarioId == null || this.UsuarioId == Guid.Empty))
            {
                app.Mensaje = "El usuario no puede ir vacio. ";
            }
            if ((this.VehiculoId == null || this.VehiculoId == Guid.Empty))
            {
                app.Mensaje += "El vehiculo no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Diagnostico))
            {
                app.Mensaje = "El diagnostico no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Comentario))
            {
                app.Mensaje = "El comentario no puede ir vacio. ";
            }
            if (this.EstadoId == null || this.EstadoId == Guid.Empty)
            {
                app.Mensaje += "El estadoId no puede ir vacio. ";
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
            if ((this.UsuarioId == null || this.UsuarioId == Guid.Empty))
            {
                app.Mensaje = "El usuario no puede ir vacio. ";
            }
            if ((this.VehiculoId == null || this.VehiculoId == Guid.Empty))
            {
                app.Mensaje += "El vehiculo no puede ir vacio. ";
            }
            if ((this.VehiculoMecanicoId == null || this.VehiculoMecanicoId == Guid.Empty))
            {
                app.Mensaje += "El vehiculo mecanico no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Diagnostico))
            {
                app.Mensaje = "El diagnostico no puede ir vacio. ";
            }
            if (string.IsNullOrEmpty(this.Comentario))
            {
                app.Mensaje = "El comentario no puede ir vacio. ";
            }
            if (this.EstadoId == null || this.EstadoId == Guid.Empty)
            {
                app.Mensaje += "El estadoId no puede ir vacio. ";
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
            if (this.VehiculoMecanicoId == null || this.VehiculoMecanicoId == Guid.Empty)
            {
                app.Mensaje += "El VehiculoMecanicoId no puede ir vacio. ";
            }

            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "vehiculo mecanico eliminado correctamente";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}
