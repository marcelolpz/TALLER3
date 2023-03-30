using Examen.Models.ViewModels;
using System;

namespace TallerMecanico.Models.ViewModels
{
    public class RolVm
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion2 { get; set; }
        public AppResultVm Validar()
        {
            AppResultVm app = new AppResultVm();

            if (string.IsNullOrEmpty(this.Descripcion))
            {
                app.Mensaje += "La descripcion no puede esta vacío \n";

            }
            if (string.IsNullOrEmpty(this.Descripcion2))
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
            if (string.IsNullOrEmpty(this.Descripcion2))
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
