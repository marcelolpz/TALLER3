using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using TallerMecanico.Filters;
using TallerMecanico.Models.Domain;
using TallerMecanico.Models.Domain.Entities;
using TallerMecanico.Models.ViewModels;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;

using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;

namespace TallerMecanico.Controllers
{
    public class EstadoController : Controller
    {
        private readonly ILogger<EstadoController> _logger;
        private TallerMecanicoDBContext _context;
        public EstadoController(ILogger<EstadoController> logger, TallerMecanicoDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]

        public IActionResult Index()
        {
            var ListaEstados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            return View(ListaEstados);
        }

        [HttpGet]

        public IActionResult Insertar()
        {
            var registro = new EstadoVM();
            var ListaEstados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsTaxis = ListaEstados.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.idEstado.ToString(),
                    Selected = false
                };
            });
            registro.Estados = itemsTaxis;

            return View(registro);
        }

        [HttpPost]

        public IActionResult Insertar(EstadoVM registro)
        {
            var ListaEstados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsTaxis = ListaEstados.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.idEstado.ToString(),
                    Selected = false
                };
            });
            registro.Estados = itemsTaxis;

            var validacion = registro.Validar();

            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(registro);
            }

            var newEntidadRegistro = Estado.Create(
                registro.Nombre


            );

            _context.Estados.Add(newEntidadRegistro);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Editar(Guid Id)
        {

            var registro = _context.Estados.Where(w => w.idEstado == Id && w.Eliminado == false).ProjectToType<EstadoVM>().FirstOrDefault();

            var ListaEstados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsTaxis = ListaEstados.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.idEstado.ToString(),
                    Selected = false
                };
            });
            registro.Estados = itemsTaxis;

            return View(registro);
        }

        [HttpPost]

        public IActionResult Editar(EstadoVM registro)
        {
            var ListaEstados = _context.Estados.Where(w => w.Eliminado == false).ProjectToType<EstadoVM>().ToList();
            List<SelectListItem> itemsTaxis = ListaEstados.ConvertAll(t =>
            {
                return new SelectListItem()
                {
                    Text = t.Nombre.ToString(),
                    Value = t.idEstado.ToString(),
                    Selected = false
                };
            });
            registro.Estados = itemsTaxis;

            var validacion = registro.ValidarEnUpdate();

            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(registro);
            }

            var registroActual = _context.Estados.FirstOrDefault(w => w.idEstado == registro.idEstado);
            registroActual.Update(
                registro.Nombre);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Eliminar(Guid Id)
        {
            var registro = _context.Estados.Where(w => w.idEstado == Id && w.Eliminado == false).ProjectToType<EstadoVM>().FirstOrDefault();

            return View(registro);
        }

        [HttpPost]

        public IActionResult Eliminar(EstadoVM registro)
        {
            var validacion = registro.ValidarEnDelete();
            TempData["Mensaje"] = validacion.Mensaje;
            if (!validacion.IsValid)
            {
                return View(registro);
            }

            var registroActual = _context.Estados.FirstOrDefault(w => w.idEstado == registro.idEstado);
            registroActual.Delete();
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public List<Estado> GetEstadosFromDatabase()
        {
            return _context.Estados.Where(w => w.Eliminado == false).ToList();
        }

        public IActionResult ExportToPdf()
        {
            var estados = GetEstadosFromDatabase(); // Obtener los estados de la base de datos

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


                doc.Add(new Paragraph("Estados",titleFont));
                doc.Add(new Paragraph("\n")); // Agrega un salto de línea

                var table = new PdfPTable(2);
                table.WidthPercentage = 100;
                // Crear objeto BaseColor para representar el color #060724
                BaseColor headerColor = new BaseColor(6, 7, 36);

                // Crear encabezados de tabla y establecer el color de fondo y texto
                var idHeader = new PdfPCell(new Phrase("ID", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                idHeader.BackgroundColor = headerColor;
                idHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(idHeader);

                var nameHeader = new PdfPCell(new Phrase("Nombre", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                nameHeader.BackgroundColor = headerColor;
                nameHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(nameHeader);

                foreach (var estado in estados)
                {
                    table.AddCell(estado.idEstado.ToString());
                    table.AddCell(estado.Nombre);
                }

                doc.Add(table);
                doc.Close();

                return File(ms.ToArray(), "application/pdf", "estados.pdf");
            }
        }



        public IActionResult ExportToExcel()
        {
            var estados = GetEstadosFromDatabase(); // Obtener los estados de la base de datos

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Estados");

                // Configurar el estilo para el título
                var titleStyle = worksheet.Cells["A1:B1"].Style;
                titleStyle.Font.Bold = true;
                titleStyle.Font.Size = 18;

                worksheet.Cells["A1:D1"].Merge = true;
                var titleCell = worksheet.Cells["A1"];
                titleCell.Value = "Taller Mecánico Ensigna";
                titleCell.Style.Font.Bold = true;
                titleCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                worksheet.Cells["A2"].Value = "Estados";

                worksheet.Cells["A3"].Value = "ID";
                worksheet.Cells["B3"].Value = "Nombre";

                int row = 4;
                foreach (var estado in estados)
                {
                    worksheet.Cells[$"A{row}"].Value = estado.idEstado;
                    worksheet.Cells[$"B{row}"].Value = estado.Nombre;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Estados.xlsx");
            }
        }







    }
}

