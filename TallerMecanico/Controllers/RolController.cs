using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TallerMecanico.Filters;
using TallerMecanico.Models.Domain.Entities;
using TallerMecanico.Models.Domain;
using TallerMecanico.Models.ViewModels;
using System.Linq;
using Mapster;

namespace TallerMecanico.Controllers
{
    public class RolController : Controller
    {
        private readonly ILogger<RolController> _logger;
        private TallerMecanicoDBContext _context;


        public RolController(ILogger<RolController> logger, TallerMecanicoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("Rol")]
        public IActionResult Index()
        {
            var listaroles = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            return View(listaroles);

        }
        [HttpGet]
        [ClaimRequirement("Rol")]
        public IActionResult Insertar()
        {
            var newrol = new RolVm();
            return View(newrol);
        }

        [HttpPost]
        [ClaimRequirement("Rol")]
        public IActionResult Insertar(RolVm newrol)
        {
            var newentidadrol = Rol.Create(newrol.Descripcion, newrol.Descripcion2);

            _context.Rol.Add(newentidadrol);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Rol")]

        public IActionResult Editar(string Id)
        {
            var rol = _context.Rol.Where(w => w.Id == new Guid(Id) && w.Eliminado == false).ProjectToType<RolVm>().FirstOrDefault();
            return View(rol);

        }
        [HttpPost]
        [ClaimRequirement("Rol")]
        public IActionResult Editar(RolVm newrol)
        {

            var validacion = newrol.ValidarEnUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newrol);
            }
            var rolactual = _context.Rol.FirstOrDefault(w => w.Id == newrol.Id);
            rolactual.Update(newrol.Descripcion, newrol.Descripcion2);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("Rol")]
        public IActionResult Eliminar(Guid Id)
        {
            var rol = _context.Rol.Where(w => w.Id == Id && w.Eliminado == false).ProjectToType<RolVm>().FirstOrDefault();
            return View(rol);
        }

        [HttpPost]
        [ClaimRequirement("Rol")]
        public IActionResult Eliminar(RolVm newrol)
        {

            var validacion = newrol.ValidarEnDelete();

            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newrol);
            }
            var rolactual = _context.Rol.FirstOrDefault(w => w.Id == newrol.Id);
            rolactual.Delete();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
