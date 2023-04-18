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
            return RedirectToAction("Index")

            public List<Vehiculo> GetVehiculoFromDatabase()
            {
                return _context.Vehiculo.Where(w => w.Eliminado == false).ToList();
            }

            public IActionResult ExportToPdf()
            {
                var vehiculos = GetVehiculoFromDatabase(); // Obtener los estados de la base de datos

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


                    doc.Add(new Paragraph("Vehiculo", titleFont));
                    doc.Add(new Paragraph("\n")); // Agrega un salto de línea


                    var table = new PdfPTable(6);
                    table.WidthPercentage = 100;

                    BaseColor headerColor = new BaseColor(6, 7, 36);

                    var vehiculoIdHeader = new PdfPCell(new Phrase("Vehiculo Id", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                    vehiculoIdHeader.BackgroundColor = headerColor;
                    vehiculoIdHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(vehiculoIdHeader);

                    var modeloIdHeader = new PdfPCell(new Phrase("Modelo Id", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                    modeloIdHeader.BackgroundColor = headerColor;
                    modeloIdHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(modeloIdHeader);

                    var colorIdHeader = new PdfPCell(new Phrase("Color Id", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                    colorIdHeader.BackgroundColor = headerColor;
                    colorIdHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(colorIdHeader);

                    var placaHeader = new PdfPCell(new Phrase("Placa", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                    placaHeader.BackgroundColor = headerColor;
                    placaHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(placaHeader);

                    var userIdHeader = new PdfPCell(new Phrase("Id de usuario", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                    userIdHeader.BackgroundColor = headerColor;
                    userIdHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(userIdHeader);

                    foreach (var vehiculo in vehiculos)
                    {
                        var row = new PdfPRow(new PdfPCell[]
                        {
        new PdfPCell(new Phrase(vehiculo.VehiculoId.ToString())),
        new PdfPCell(new Phrase(vehiculo.ModeloId.ToString())),
        new PdfPCell(new Phrase(vehiculo.ColorId.ToString())),
        new PdfPCell(new Phrase(vehiculo.Placa)),
        new PdfPCell(new Phrase(vehiculo.UsuarioId.ToString()))
                        });
                        table.Rows.Add(row);
                    }

                    doc.Add(table);
                    doc.Close();

                    return File(ms.ToArray(), "application/pdf", "vehiculo.pdf");
                }
            }




            public IActionResult ExportToExcel()
            {
                var vehiculos = GetVehiculoFromDatabase(); // Obtener los estados de la base de datos

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("VehiculoMecanico");

                    // Configurar el estilo para el título
                    var titleStyle = worksheet.Cells["A1:B1"].Style;
                    titleStyle.Font.Bold = true;
                    titleStyle.Font.Size = 18;

                    worksheet.Cells["A1:D1"].Merge = true;
                    var titleCell = worksheet.Cells["A1"];
                    titleCell.Value = "Taller Mecánico Ensigna";
                    titleCell.Style.Font.Bold = true;
                    titleCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                    worksheet.Cells["A2"].Value = "Vehiculo";

                    worksheet.Cells["A3"].Value = "VehiculoId";
                    worksheet.Cells["B3"].Value = "Modelo";
                    worksheet.Cells["C3"].Value = "Color";
                    worksheet.Cells["D3"].Value = "Placa";
                    worksheet.Cells["E3"].Value = "Usuario";

                    int row = 4;
                    foreach (var vehiculo in vehiculos)
                    {
                        worksheet.Cells[$"A{row}"].Value = vehiculo.VehiculoId;
                        worksheet.Cells[$"B{row}"].Value = vehiculo.ModeloId;
                        worksheet.Cells[$"C{row}"].Value = vehiculo.ColorId;
                        worksheet.Cells[$"D{row}"].Value = vehiculo.Placa;
                        worksheet.Cells[$"F{row}"].Value = vehiculo.UsuarioId;

                        row++;
                    }

                    worksheet.Cells.AutoFitColumns();

                    // Establecer bordes
                    var allCells = worksheet.Cells[worksheet.Dimension.Address];
                    var border = allCells.Style.Border;
                    border.Top.Style = ExcelBorderStyle.Thin;
                    border.Bottom.Style = ExcelBorderStyle.Thin;
                    border.Left.Style = ExcelBorderStyle.Thin;
                    border.Right.Style = ExcelBorderStyle.Thin;


                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Vehiculo.xlsx");
                }
            }
        }
    }
}
