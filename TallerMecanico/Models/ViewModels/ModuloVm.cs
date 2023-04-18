using Examen.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using TallerMecanico.Models.Domain.Entities;

namespace TallerMecanico.Models.ViewModels
{
    public class ModuloVm
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Metodo { get; set; }
        public string Controller { get; set; }
        public AgrupadoModulos AgrupadoModulos { get; set; }
        public Guid AgrupadoModulosId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public List<SelectListItem> AgrupadoModulo { set; get; }

        public AppResultVm Validar()
        {
            AppResultVm app = new AppResultVm();

            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El nombre no puede esta vacío \n";

            }
            if (string.IsNullOrEmpty(this.Metodo))
            {
                app.Mensaje += "El apellido no puede esta vacío \n";

            }
            if (string.IsNullOrEmpty(this.Controller))
            {
                app.Mensaje += "El correo no puede esta vacío \n";

            }
            if (this.AgrupadoModulosId == Guid.Empty)
            {
                app.Mensaje += "Debe seleccionar un modulo agrupado \n";

            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Registro finalizado con éxito.";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
        public AppResultVm ValidarEnUpdate()
        {
            AppResultVm app = new AppResultVm();

            if (this.Id == Guid.Empty)
            {
                app.Mensaje += "El ID no debe estar vacio \n";
            }
            if (string.IsNullOrEmpty(this.Nombre))
            {
                app.Mensaje += "El nombre no puede esta vacío \n";

            }
            if (string.IsNullOrEmpty(this.Metodo))
            {
                app.Mensaje += "El apellido no puede esta vacío \n";

            }
            if (string.IsNullOrEmpty(this.Controller))
            {
                app.Mensaje += "El correo no puede esta vacío \n";

            }
            if (this.AgrupadoModulosId == Guid.Empty)
            {
                app.Mensaje += "Debe seleccionar un modulo agrupado \n";

            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Registro finalizado con éxito.";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
        public AppResultVm ValidarEnDelete()
        {
            AppResultVm app = new AppResultVm();

            if (this.Id == Guid.Empty)
            {
                app.Mensaje += "El ID no debe estar vacio \n";
            }
            if (string.IsNullOrEmpty(app.Mensaje))
            {
                app.IsValid = true;
                app.Mensaje = "Registro eliminado con éxito.";
            }
            else
            {
                app.IsValid = false;
            }
            return app;
        }
    }
}