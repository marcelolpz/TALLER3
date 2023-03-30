using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System.Collections.Generic;
using System;
using System.Linq;
using TallerMecanico.Models.Domain;
using TallerMecanico.Models.ViewModels;
using TallerMecanico.Models.Domain.Entities;
using TallerMecanico.Filters;

namespace TallerMecanico.Controllers
{
    
   
    public class VehiculoMecanicoController : Controller
    {
        private readonly ILogger<VehiculoMecanicoController> _logger;
        private TallerMecanicoDBContext _context;

        public VehiculoMecanicoController(ILogger<VehiculoMecanicoController> logger, TallerMecanicoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [ClaimRequirement("VehiculoMecanico")]
        public IActionResult Index()
        {
            var listaregistros = _context.VehiculoMecanico.Where(w => w.Eliminado == false).ProjectToType<VehiculoMecanicoVm>().ToList();
            return View(listaregistros);
        }

        [HttpGet]
        [ClaimRequirement("VehiculoMecanico")]
        public IActionResult Insertar()
        {
            var newVehiculoMecanico = new VehiculoMecanicoVm();

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

            var vehiculos = _context.Vehiculo.Where(w => w.Eliminado == false).ProjectToType<VehiculoVm>().ToList();
            List<SelectListItem> itemsvehiculo = vehiculos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Placa,
                    Value = d.VehiculoId.ToString(),
                    Selected = false
                };
            });

            var estados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsestados = estados.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.idEstado.ToString(),
                    Selected = false
                };
            });

            newVehiculoMecanico.Usuarioss = itemsusuarios;
            newVehiculoMecanico.Vehiculoss = itemsvehiculo;
            newVehiculoMecanico.Estadoss = itemsestados;

            return View(newVehiculoMecanico);
        }

        [HttpPost]
        [ClaimRequirement("VehiculoMecanico")]
        public IActionResult Insertar(VehiculoMecanicoVm newVehiculoMecanico)
        {
          
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

            var vehiculos = _context.Vehiculo.Where(w => w.Eliminado == false).ProjectToType<VehiculoVm>().ToList();
            List<SelectListItem> itemsvehiculo = vehiculos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Placa,
                    Value = d.VehiculoId.ToString(),
                    Selected = false
                };
            });

            var estados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsestados = estados.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.idEstado.ToString(),
                    Selected = false
                };
            });

           
            newVehiculoMecanico.Usuarioss = itemsusuarios;
            newVehiculoMecanico.Vehiculoss = itemsvehiculo;
            newVehiculoMecanico.Estadoss = itemsestados;

            var validacion = newVehiculoMecanico.Validar();

            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newVehiculoMecanico);
            }

            var newEntidadVehiculoMecanico = VehiculoMecanico.Create(newVehiculoMecanico.UsuarioId,newVehiculoMecanico.VehiculoId
                ,newVehiculoMecanico.Diagnostico,newVehiculoMecanico.Comentario,newVehiculoMecanico.EstadoId);

            _context.VehiculoMecanico.Add(newEntidadVehiculoMecanico);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("VehiculoMecanico")]
        public IActionResult Editar(Guid VehiculoMecanicoId)
        {
            var vehiculoMecanico = _context.VehiculoMecanico.Where(w => w.VehiculoMecanicoId == VehiculoMecanicoId && w.Eliminado == false).ProjectToType<VehiculoMecanicoVm>().FirstOrDefault();

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

            var vehiculos = _context.Vehiculo.Where(w => w.Eliminado == false).ProjectToType<VehiculoVm>().ToList();
            List<SelectListItem> itemsvehiculo = vehiculos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Placa,
                    Value = d.VehiculoId.ToString(),
                    Selected = false
                };
            });

            var estados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsestados = estados.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.idEstado.ToString(),
                    Selected = false
                };
            });

            vehiculoMecanico.Usuarioss = itemsusuarios;
            vehiculoMecanico.Vehiculoss = itemsvehiculo;
            vehiculoMecanico.Estadoss = itemsestados;

            return View(vehiculoMecanico);

        }

        [HttpPost]
        [ClaimRequirement("VehiculoMecanico")]
        public IActionResult Editar(VehiculoMecanicoVm newVehiculoMecanico)
        {
            
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

            var vehiculos = _context.Vehiculo.Where(w => w.Eliminado == false).ProjectToType<VehiculoVm>().ToList();
            List<SelectListItem> itemsvehiculo = vehiculos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Placa,
                    Value = d.VehiculoId.ToString(),
                    Selected = false
                };
            });

            var estados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsestados = estados.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.idEstado.ToString(),
                    Selected = false
                };
            });

            newVehiculoMecanico.Usuarioss = itemsusuarios;
            newVehiculoMecanico.Vehiculoss = itemsvehiculo;
            newVehiculoMecanico.Estadoss = itemsestados;
            var validacion = newVehiculoMecanico.ValidarUpdate();

            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newVehiculoMecanico);
            }

            var vehiculoMecancioActual = _context.VehiculoMecanico.FirstOrDefault(w => w.VehiculoMecanicoId == newVehiculoMecanico.VehiculoMecanicoId);

            vehiculoMecancioActual.Update(newVehiculoMecanico.UsuarioId, newVehiculoMecanico.VehiculoId
                ,newVehiculoMecanico.Diagnostico, newVehiculoMecanico.Comentario, newVehiculoMecanico.EstadoId);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        [ClaimRequirement("VehiculoMecanico")]
        public IActionResult Eliminar(Guid VehiculoMecanicoId)
        {
            var vehiculoMecanico = _context.VehiculoMecanico.Where(w => w.VehiculoMecanicoId == VehiculoMecanicoId && w.Eliminado == false).ProjectToType<VehiculoMecanicoVm>().FirstOrDefault();
            return View(vehiculoMecanico);

        }

        [HttpPost]
        [ClaimRequirement("VehiculoMecanico")]
        public IActionResult Eliminar(VehiculoMecanicoVm newVehiculoMecanico)
        {
            var validacion = newVehiculoMecanico.ValidarEliminar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newVehiculoMecanico);
            }
            var usuarioactual = _context.VehiculoMecanico.FirstOrDefault(w => w.VehiculoMecanicoId == newVehiculoMecanico.VehiculoMecanicoId);
            usuarioactual.Delete();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
