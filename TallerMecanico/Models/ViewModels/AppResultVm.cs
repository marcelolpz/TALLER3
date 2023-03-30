using System;

namespace Examen.Models.ViewModels
{
    public class AppResultVm
    {
        public Boolean IsValid { get; set; }
        public string Mensaje { get; set; }
        public object Data { get; set; }
        public static AppResultVm NoSuccess(string mensaje)
        {
            return new AppResultVm { IsValid = false, Mensaje = mensaje };
        }
        public static AppResultVm Success(string mensaje, object data)
        {
            return new AppResultVm { IsValid = true, Mensaje = mensaje, Data = data };
        }
    }
}
