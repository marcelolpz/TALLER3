using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TallerMecanico.Filters;
using TallerMecanico.Models.Domain;
using TallerMecanico.Models.Domain.Entities;
using TallerMecanico.Models.ViewModels;

namespace TallerMecanico.Controllers
{
    public class AgrupadoModulosController : Controller
    {
        private readonly ILogger<AgrupadoModulosController> _logger;
        private TallerMecanicoDBContext _context;


        public AgrupadoModulosController(ILogger<AgrupadoModulosController> logger, TallerMecanicoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("AgrupadoModulos")]
        public IActionResult Index()
        {
            var listaAgrupadoModulos = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<AgrupadoVm>().ToList();
            return View(listaAgrupadoModulos);

        }
        [HttpGet]
        [ClaimRequirement("AgrupadoModulos")]
        public IActionResult Insertar()
        {
            var newAgrupadoModulos = new AgrupadoVm();
            return View(newAgrupadoModulos);
        }
        [HttpPost]
        [ClaimRequirement("AgrupadoModulos")]
        public IActionResult Insertar(AgrupadoVm newAgrupadoModulos)
        {
            var newentidadAgrupadoModulos = AgrupadoModulos.Create(newAgrupadoModulos.Descripcion);

            _context.AgrupadoModulos.Add(newentidadAgrupadoModulos);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("AgrupadoModulos")]

        public IActionResult Editar(string Id)
        {
            var AgrupadoModulos = _context.AgrupadoModulos.Where(w => w.Id == new Guid(Id) && w.Eliminado == false).ProjectToType<AgrupadoVm>().FirstOrDefault();
            return View(AgrupadoModulos);

        }
        [HttpPost]
        [ClaimRequirement("AgrupadoModulos")]
        public IActionResult Editar(AgrupadoVm newAgrupadoModulos)
        {

            var validacion = newAgrupadoModulos.ValidarEnUpdate();
            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newAgrupadoModulos);
            }
            var AgrupadoModuloactual = _context.AgrupadoModulos.FirstOrDefault(w => w.Id == newAgrupadoModulos.Id);
            AgrupadoModuloactual.Update(newAgrupadoModulos.Descripcion);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ClaimRequirement("AgrupadoModulos")]
        public IActionResult Eliminar(Guid Id)
        {
            var AgrupadoModulos = _context.AgrupadoModulos.Where(w => w.Id == Id && w.Eliminado == false).ProjectToType<AgrupadoVm>().FirstOrDefault();
            return View(AgrupadoModulos);
        }
        [HttpPost]
        [ClaimRequirement("AgrupadoModulos")]
        public IActionResult Eliminar(AgrupadoVm newAgrupadoModulos)
        {

            var validacion = newAgrupadoModulos.ValidarEnDelete();

            TempData["mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(newAgrupadoModulos);
            }
            var AgrupadoModuloactual = _context.AgrupadoModulos.FirstOrDefault(w => w.Id == newAgrupadoModulos.Id);
            AgrupadoModuloactual.Delete();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

