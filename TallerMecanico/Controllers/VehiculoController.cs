using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using TallerMecanico.Models.Domain;
using TallerMecanico.Models.ViewModels;
using TallerMecanico.Models.Domain.Entities;
using TallerMecanico.Filters;
using iTextSharp.text.pdf;
using iTextSharp.text;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.IO;

namespace TallerMecanico.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly ILogger<VehiculoController> _logger;
        private TallerMecanicoDBContext _context;

        public VehiculoController(ILogger<VehiculoController> logger, TallerMecanicoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [ClaimRequirement("Vehiculo")]
        public IActionResult Index()
        {
            var listavehiculo = _context.Vehiculo.Where(w => w.Eliminado == false).ProjectToType<VehiculoVm>().ToList();
            return View(listavehiculo);
        }

        [HttpGet]
        [ClaimRequirement("Vehiculo")]
        public IActionResult Insertar()
        {
            var newVehiculo = new VehiculoVm();

            var modelos = _context.Modelo.Where(w => w.Eliminado == false).ProjectToType<ModeloVm>().ToList();
            List<SelectListItem> itemsmodelos = modelos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.ModeloId.ToString(),
                    Selected = false
                };
            });

            var colores = _context.Color.Where(w => w.Eliminado == false).ProjectToType<ColorVm>().ToList();
            List<SelectListItem> itemscolores = colores.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.ColorId.ToString(),
                    Selected = false
                };
            });

            var usuarios = _context.Usuario.Where(w => w.Eliminado == false).ProjectToType<UsuarioVm>().ToList();
            List<SelectListItem> itemsusuarios = usuarios.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.UsuarioId.ToString(),
                    Selected = false
                };
            });


            newVehiculo.Modelos = itemsmodelos;
            newVehiculo.Colores = itemscolores;
            newVehiculo.Usuarios = itemsusuarios;
            newVehiculo.CreatedDate = DateTime.Today;
            return View(newVehiculo);
        }

        [HttpPost]
        [ClaimRequirement("Vehiculo")]
        public IActionResult Insertar(VehiculoVm newVehiculo)
        {
         
            var modelos = _context.Modelo.Where(w => w.Eliminado == false).ProjectToType<ModeloVm>().ToList();
            List<SelectListItem> itemsmodelos = modelos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.ModeloId.ToString(),
                    Selected = false
                };
            });

            var colores = _context.Color.Where(w => w.Eliminado == false).ProjectToType<ColorVm>().ToList();
            List<SelectListItem> itemscolores = colores.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.ColorId.ToString(),
                    Selected = false
                };
            });

            var usuarios = _context.Usuario.Where(w => w.Eliminado == false).ProjectToType<UsuarioVm>().ToList();
            List<SelectListItem> itemsusuarios = usuarios.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.UsuarioId.ToString(),
                    Selected = false
                };
            });


            newVehiculo.Modelos = itemsmodelos;
            newVehiculo.Colores = itemscolores;
            newVehiculo.Usuarios = itemsusuarios;
            var validacion = newVehiculo.Validar();

            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newVehiculo);
            }

            var newentidadvehiculo = Vehiculo.Create(newVehiculo.ModeloId,newVehiculo.ColorId,newVehiculo.Placa,newVehiculo.UsuarioId);
            _context.Vehiculo.Add(newentidadvehiculo);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Vehiculo")]
        public IActionResult Editar(Guid VehiculoId)
        {
            var vehiculo = _context.Vehiculo.Where(w => w.VehiculoId == VehiculoId && w.Eliminado == false).ProjectToType<VehiculoVm>().FirstOrDefault();

            var modelos = _context.Modelo.Where(w => w.Eliminado == false).ProjectToType<ModeloVm>().ToList();
            List<SelectListItem> itemsmodelos = modelos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.ModeloId.ToString(),
                    Selected = false
                };
            });

            var colores = _context.Color.Where(w => w.Eliminado == false).ProjectToType<ColorVm>().ToList();
            List<SelectListItem> itemscolores = colores.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.ColorId.ToString(),
                    Selected = false
                };
            });

            var usuarios = _context.Usuario.Where(w => w.Eliminado == false).ProjectToType<UsuarioVm>().ToList();
            List<SelectListItem> itemsusuarios = usuarios.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.UsuarioId.ToString(),
                    Selected = false
                };
            });


            vehiculo.Modelos = itemsmodelos;
            vehiculo.Colores = itemscolores;
            vehiculo.Usuarios = itemsusuarios;
            return View(vehiculo);
        }

        [HttpPost]
        [ClaimRequirement("Vehiculo")]
        public IActionResult Editar(VehiculoVm newVehiculo)
        {

            var modelos = _context.Modelo.Where(w => w.Eliminado == false).ProjectToType<ModeloVm>().ToList();
            List<SelectListItem> itemsmodelos = modelos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.ModeloId.ToString(),
                    Selected = false
                };
            });

            var colores = _context.Color.Where(w => w.Eliminado == false).ProjectToType<ColorVm>().ToList();
            List<SelectListItem> itemscolores = colores.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.ColorId.ToString(),
                    Selected = false
                };
            });

            var usuarios = _context.Usuario.Where(w => w.Eliminado == false).ProjectToType<UsuarioVm>().ToList();
            List<SelectListItem> itemsusuarios = usuarios.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.UsuarioId.ToString(),
                    Selected = false
                };
            });


            newVehiculo.Modelos = itemsmodelos;
            newVehiculo.Colores = itemscolores;
            newVehiculo.Usuarios = itemsusuarios;

            var validacion = newVehiculo.ValidarUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newVehiculo);
            }

            var vehiculo = _context.Vehiculo.FirstOrDefault(w => w.VehiculoId == newVehiculo.VehiculoId);
            vehiculo.Update(newVehiculo.ModeloId, newVehiculo.ColorId, newVehiculo.Placa, newVehiculo.UsuarioId);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Vehiculo")]
        public IActionResult Eliminar(Guid VehiculoId)
        {
            var vehiculo = _context.Vehiculo.Where(w => w.VehiculoId == VehiculoId && w.Eliminado == false).ProjectToType<VehiculoVm>().FirstOrDefault();
            return View(vehiculo);

        }


        [HttpPost]
        [ClaimRequirement("Vehiculo")]
        public IActionResult Eliminar(VehiculoVm newVehiculo)
        {
            var validacion = newVehiculo.ValidarEliminar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newVehiculo);
            }
            var vehiculoactual = _context.Vehiculo.FirstOrDefault(w => w.VehiculoId == newVehiculo.VehiculoId);
            vehiculoactual.Delete();
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
