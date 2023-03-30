using System.Collections.Generic;
using System;
using Examen.Models.ViewModels;

namespace TallerMecanico.Models.ViewModels
{
    public class AgrupadoVm
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public List<ModuloVm> Modulos { get; set; }
        public AppResultVm Validar()
        {
            AppResultVm app = new AppResultVm();

            if (string.IsNullOrEmpty(this.Descripcion))
            {
                app.Mensaje += "La descripcion no puede esta vacío \n";

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
            if (string.IsNullOrEmpty(this.Descripcion))
            {
                app.Mensaje += "La descripcion no puede esta vacío \n";

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

