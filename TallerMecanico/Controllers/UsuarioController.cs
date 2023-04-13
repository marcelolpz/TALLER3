using iTextSharp.text.pdf;
using iTextSharp.text;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TallerMecanico.Filters;
using TallerMecanico.Models.Domain;
using TallerMecanico.Models.Domain.Entities;
using TallerMecanico.Models.ViewModels;

namespace TallerMecanico.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private TallerMecanicoDBContext _context;

        public UsuarioController(ILogger<UsuarioController> logger, TallerMecanicoDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ClaimRequirement("Usuario")]
        public IActionResult Index()
        {
            var ListaUsuarios = _context.Usuario.Where(w => w.Eliminado == false).ProjectToType<UsuarioVm>().ToList();
            return View(ListaUsuarios);
        }

        [HttpGet]
        [ClaimRequirement("Usuario")]
        public IActionResult Insertar()
        {
            var registro = new UsuarioVm();
            var listaRoles= _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsTaxis = listaRoles.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.Id.ToString(),
                    Selected = false
                };
            });
            registro.Roles = itemsTaxis;

            return View(registro);
        }

        [HttpPost]
        [ClaimRequirement("Usuario")]
        public IActionResult Insertar(UsuarioVm registro)
        {
            var listaRoles = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsTaxis = listaRoles.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.Id.ToString(),
                    Selected = false
                };
            });
            registro.Roles = itemsTaxis;

            var validacion = registro.Validar();

            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(registro);
            }

            var newEntidadRegistro = Usuario.Create(
                registro.Nombre,
                registro.Apellido,
                registro.Correo,
                Utilidades.Utilidades.GetMD5(registro.Password),
                registro.RolId
            );

            _context.Usuario.Add(newEntidadRegistro);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Usuario")]
        public IActionResult Editar(Guid Id)
        {
            var registro = _context.Usuario.Where(w => w.UsuarioId == Id && w.Eliminado == false).ProjectToType<UsuarioVm>().FirstOrDefault();

            var listaRoles = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsTaxis = listaRoles.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.Id.ToString(),
                    Selected = false
                };
            });
            registro.Roles = itemsTaxis;

            return View(registro);
        }

        [HttpPost]
        [ClaimRequirement("Usuario")]
        public IActionResult Editar(UsuarioVm registro)
        {
            var listaRoles = _context.Rol.Where(w => w.Eliminado == false).ProjectToType<RolVm>().ToList();
            List<SelectListItem> itemsTaxis = listaRoles.ConvertAll(t => {
                return new SelectListItem()
                {
                    Text = t.Descripcion.ToString(),
                    Value = t.Id.ToString(),
                    Selected = false
                };
            });
            registro.Roles = itemsTaxis;

            var validacion = registro.ValidarEnUpdate();

            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(registro);
            }

            var registroActual = _context.Usuario.FirstOrDefault(w => w.UsuarioId == registro.UsuarioId);
            registroActual.Update(
                registro.Nombre,
                registro.Apellido,
                registro.Correo,
                registro.Password,
                registro.RolId
            );
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement("Usuario")]
        public IActionResult Eliminar(Guid Id)
        {
            var registro = _context.Usuario.Where(w => w.UsuarioId == Id && w.Eliminado == false).ProjectToType<UsuarioVm>().FirstOrDefault();

            return View(registro);
        }

        [HttpPost]
        [ClaimRequirement("Usuario")]
        public IActionResult Eliminar(UsuarioVm registro)
        {
            var validacion = registro.ValidarEnDelete();
            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(registro);
            }

            var registroActual = _context.Usuario.FirstOrDefault(w => w.UsuarioId == registro.UsuarioId);
            registroActual.Delete();
            _context.SaveChanges();

            return RedirectToAction("Index");

            public List<Usuario> GetUsuarioFromDatabase()
            {
                return _context.Usuario.Where(w => w.Eliminado == false).ToList();
            }

            public IActionResult ExportToPdf()
            {
                var usuarios = GetUsuarioFromDatabase(); // Obtener los usuarios de la base de datos

                using (var ms = new MemoryStream())
                {
                    var doc = new Document(PageSize.A4, 30, 30, 30, 30);
                    var writer = PdfWriter.GetInstance(doc, ms);

                    doc.Open();
                    var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, BaseColor.BLACK);
                    var title = new Paragraph("Taller Mecánico Ensigna", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 20;
                    doc.Add(title);


                    doc.Add(new Paragraph("Uusario", titleFont));
                    doc.Add(new Paragraph("\n")); // Agrega un salto de línea

                    var table = new PdfPTable(2);
                    table.WidthPercentage = 100;
                    // Crear objeto BaseColor para representar el color #060724
                    BaseColor headerColor = new BaseColor(6, 7, 36);

                    // Crear encabezados de tabla y establecer el color de fondo y texto
                    var idHeader = new PdfPCell(new Phrase("Id de usuario", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                    idHeader.BackgroundColor = headerColor;
                    idHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(idHeader);

                    var nameHeader = new PdfPCell(new Phrase("Nombre", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                    nameHeader.BackgroundColor = headerColor;
                    nameHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(nameHeader);

                    var apellidoHeader = new PdfPCell(new Phrase("Apellido", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                    apellidoHeader.BackgroundColor = headerColor;
                    apellidoHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(apellidoHeader);

                    var correoHeader = new PdfPCell(new Phrase("Correo", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                    correoHeader.BackgroundColor = headerColor;
                    correoHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(correoHeader);

                    var passwordHeader = new PdfPCell(new Phrase("Password", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                    passwordHeader.BackgroundColor = headerColor;
                    passwordHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(passwordHeader);

                    var rolIdHeader = new PdfPCell(new Phrase("Rol Id", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                    rolIdHeader.BackgroundColor = headerColor;
                    rolIdHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(passwordHeader);




                    foreach (var usuario in usuarios)
                    {
                        table.AddCell(usuario.UsuarioId.ToString());
                        table.AddCell(usuario.Nombre);
                        table.AddCell(usuario.Apellido);
                        table.AddCell(usuario.Correo);
                        table.AddCell(usuario.Password);
                        table.AddCell(usuario.RolId.ToString());
                    }

                    doc.Add(table);
                    doc.Close();

                    return File(ms.ToArray(), "application/pdf", "usuario.pdf");
                }
            }



            public IActionResult ExportToExcel()
            {
                var usuarios = GetUsuarioFromDatabase(); // Obtener los usuarios de la base de datos

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Usuario");

                    // Configurar el estilo para el título
                    var titleStyle = worksheet.Cells["A1:B1"].Style;
                    titleStyle.Font.Bold = true;
                    titleStyle.Font.Size = 18;

                    worksheet.Cells["A1:D1"].Merge = true;
                    var titleCell = worksheet.Cells["A1"];
                    titleCell.Value = "Taller Mecánico Ensigna";
                    titleCell.Style.Font.Bold = true;
                    titleCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                    worksheet.Cells["A2"].Value = "Usuario";

                    worksheet.Cells["A3"].Value = "Id de usuario";
                    worksheet.Cells["B3"].Value = "Nombre";
                    worksheet.Cells["C3"].Value = "Apellido";
                    worksheet.Cells["D3"].Value = "Correo";
                    worksheet.Cells["E3"].Value = "Password";
                    worksheet.Cells["F3"].Value = "Rol";

                    int row = 4;
                    foreach (var Usuario in usuarios)
                    {
                        worksheet.Cells[$"A{row}"].Value = Usuario.UsuarioId;
                        worksheet.Cells[$"B{row}"].Value = Usuario.Nombre;
                        worksheet.Cells[$"C{row}"].Value = Usuario.Apellido;
                        worksheet.Cells[$"D{row}"].Value = Usuario.Correo;
                        worksheet.Cells[$"E{row}"].Value = Usuario.Password;
                        worksheet.Cells[$"F{row}"].Value = Usuario.RolId;


                        row++;
                    }

                    worksheet.Cells.AutoFitColumns();

                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Usuario.xlsx");
                }
            }
        }
    }
}
