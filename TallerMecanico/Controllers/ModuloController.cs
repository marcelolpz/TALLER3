using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TallerMecanico.Filters;
using TallerMecanico.Models.Domain;
using TallerMecanico.Models.Domain.Entities;
using TallerMecanico.Models.ViewModels;

namespace TallerMecanico.Controllers
{
    public class ModuloController : Controller
    {
        private readonly ILogger<ModuloController> _logger;
        private TallerMecanicoDBContext _context;

        public ModuloController(ILogger<ModuloController> logger, TallerMecanicoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("Modulo")]
        public IActionResult Index()
        {
            var ListaModulo = _context.Modulo.Where(w => w.Eliminado == false).ProjectToType<ModuloVm>().ToList();
            return View(ListaModulo);
        }
        [HttpGet]
        [ClaimRequirement("modulo")]
        public IActionResult Insertar()
        {
            var newmodulo = new ModuloVm();
            var AgrupadoModulo = _context.AgrupadoModulos.Where(w => w.Eliminado == false).ProjectToType<AgrupadoVm>().ToList();
            List<SelectListItem> itemsAgrupadoModulo = AgrupadoModulo.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newmodulo.AgrupadoModulo = itemsAgrupadoModulo;
            return View(newmodulo);
        }
        [HttpPost]
        [ClaimRequirement("Modulo")]
        public IActionResult Insertar(ModuloVm newmodulo)
        {
            var AgrupadoModulo = _context.AgrupadoModulos.Where(w => w.Eliminado == false).ProjectToType<AgrupadoVm>().ToList();
            List<SelectListItem> itemsAgrupadoModulo = AgrupadoModulo.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newmodulo.AgrupadoModulo = itemsAgrupadoModulo;
            var validacion = newmodulo.Validar();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newmodulo);
            }
            var newentidadmodulo = Modulo.Create(newmodulo.Nombre, newmodulo.Metodo, newmodulo.Controller, newmodulo.AgrupadoModulosId, newmodulo.CreatedDate, newmodulo.CreatedBy);
            _context.Modulo.Add(newentidadmodulo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Modulo")]
        public IActionResult Editar(Guid Id)
        {
            var Modulo = _context.Modulo.Where(w => w.Id == Id && w.Eliminado == false).ProjectToType<ModuloVm>().FirstOrDefault();
            var AgrupadoModulo = _context.AgrupadoModulos.Where(w => w.Eliminado == false).ProjectToType<AgrupadoVm>().ToList();
            List<SelectListItem> itemsAgrupadoModulo = AgrupadoModulo.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            Modulo.AgrupadoModulo = itemsAgrupadoModulo;
            return View(Modulo);
        }
        [HttpPost]
        [ClaimRequirement("Modulo")]
        public IActionResult Editar(ModuloVm newmodulo)
        {
            var AgrupadoModulo = _context.AgrupadoModulos.Where(w => w.Eliminado == false).ProjectToType<AgrupadoVm>().ToList();
            List<SelectListItem> itemsAgrupadoModulo = AgrupadoModulo.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Descripcion,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            newmodulo.AgrupadoModulo = itemsAgrupadoModulo;
            var validacion = newmodulo.ValidarEnUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newmodulo);
            }
            var moduloactual = _context.Modulo.FirstOrDefault(w => w.Id == newmodulo.Id);
            moduloactual.Update(newmodulo.Nombre, newmodulo.Metodo, newmodulo.Controller, newmodulo.AgrupadoModulosId, newmodulo.CreatedDate, newmodulo.CreatedBy);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("Modulo")]
        public IActionResult Eliminar(Guid Id)
        {
            var Modulo = _context.Modulo.Where(w => w.Id == Id && w.Eliminado == false).ProjectToType<ModuloVm>().FirstOrDefault();

            return View(Modulo);
        }

        [HttpPost]
        [ClaimRequirement("Modulo")]
        public IActionResult Eliminar(ModuloVm Modulo)
        {
            var validacion = Modulo.ValidarEnDelete();
            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(Modulo);
            }

            var moduloactual = _context.Modulo.FirstOrDefault(w => w.Id == Modulo.Id);
            moduloactual.Delete();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
