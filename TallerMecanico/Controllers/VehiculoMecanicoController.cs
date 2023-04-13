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
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using OfficeOpenXml.Style;
using OfficeOpenXml;

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


        [HttpGet]
        [ClaimRequirement("Mis Vehiculos")]
        public IActionResult BuscarClienteVehiculos()
        {
            var sesionBase64 = HttpContext.Session.GetString("usuarioObjeto");
            var base64EncodedBytes = System.Convert.FromBase64String(sesionBase64);
            var sesion = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            UsuarioLoginVm usuarioObjeto = JsonConvert.DeserializeObject<UsuarioLoginVm>(sesion);

            var vehiculos = _context.Vehiculo.Where(w => w.UsuarioId == usuarioObjeto.UsuarioId).ProjectToType<VehiculoVm>().ToList();
            //VehiculoMecanicoVm[] registros = new VehiculoMecanicoVm[0];
            List<VehiculoMecanicoVm> registros = new List<VehiculoMecanicoVm>();

            foreach ( var v in vehiculos )
            {
                var registrosActuales = _context.VehiculoMecanico.Where(w => w.VehiculoId == v.VehiculoId).ProjectToType<VehiculoMecanicoVm>().ToList();

                foreach (var vM in  registrosActuales )
                {
                    registros.Add(vM);
                }
            }

            return View(registros);

        }

        [HttpGet]
        [ClaimRequirement("Mis Reparaciones")]
        public IActionResult BuscarReparaciones()
        {
            var sesionBase64 = HttpContext.Session.GetString("usuarioObjeto");
            var base64EncodedBytes = System.Convert.FromBase64String(sesionBase64);
            var sesion = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            UsuarioLoginVm usuarioObjeto = JsonConvert.DeserializeObject<UsuarioLoginVm>(sesion);

            var registrosActuales = _context.VehiculoMecanico.Where(w => w.UsuarioId == usuarioObjeto.UsuarioId).ProjectToType<VehiculoMecanicoVm>().ToList();

            return View(registrosActuales);

        }


        public List<VehiculoMecanico> GetVehiculoMecanicoFromDatabase()
        {
            return _context.VehiculoMecanico.Where(w => w.Eliminado == false).ToList();
        }

        public IActionResult ExportToPdf()
        {
            var vehiculoMecanicos = GetVehiculoMecanicoFromDatabase(); // Obtener los estados de la base de datos

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


                doc.Add(new Paragraph("Vehiculo Mecánico", titleFont));
                doc.Add(new Paragraph("\n")); // Agrega un salto de línea


                var table = new PdfPTable(6);
                table.WidthPercentage = 100;

                BaseColor headerColor = new BaseColor(6, 7, 36);

                var idHeader = new PdfPCell(new Phrase("Vehiculo Mecánico Id", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                idHeader.BackgroundColor = headerColor;
                idHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(idHeader);

                var userIdHeader = new PdfPCell(new Phrase("Id de usuario", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                userIdHeader.BackgroundColor = headerColor;
                userIdHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(userIdHeader);

                var vehiculoIdHeader = new PdfPCell(new Phrase("Vehiculo Id", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                vehiculoIdHeader.BackgroundColor = headerColor;
                vehiculoIdHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(vehiculoIdHeader);

                var diagnosticoHeader = new PdfPCell(new Phrase("Diagnostico", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                diagnosticoHeader.BackgroundColor = headerColor;
                diagnosticoHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(diagnosticoHeader);

                var comentarioHeader = new PdfPCell(new Phrase("Comentario", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                comentarioHeader.BackgroundColor = headerColor;
                comentarioHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(comentarioHeader);

                var estadoIdHeader = new PdfPCell(new Phrase("Id estado", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE)));
                estadoIdHeader.BackgroundColor = headerColor;
                estadoIdHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(estadoIdHeader);

                foreach (var vehiculoMecanico in vehiculoMecanicos)
                {
                    var row = new PdfPRow(new PdfPCell[]
                    {
        new PdfPCell(new Phrase(vehiculoMecanico.VehiculoMecanicoId.ToString())),
        new PdfPCell(new Phrase(vehiculoMecanico.UsuarioId.ToString())),
        new PdfPCell(new Phrase(vehiculoMecanico.VehiculoId.ToString())),
        new PdfPCell(new Phrase(vehiculoMecanico.Diagnostico)),
        new PdfPCell(new Phrase(vehiculoMecanico.Comentario)),
        new PdfPCell(new Phrase(vehiculoMecanico.EstadoId.ToString()))
                    });
                    table.Rows.Add(row);
                }

                doc.Add(table);
                doc.Close();

                return File(ms.ToArray(), "application/pdf", "vehiculoMecanico.pdf");
            }
        }




        public IActionResult ExportToExcel()
        {
            var vehiculoMecanicos = GetVehiculoMecanicoFromDatabase(); // Obtener los estados de la base de datos

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


                worksheet.Cells["A2"].Value = "Vehiculo Mecanico";

                worksheet.Cells["A3"].Value = "Id";
                worksheet.Cells["B3"].Value = "Usuario"; 
                worksheet.Cells["C3"].Value = "VehiculoId";
                worksheet.Cells["D3"].Value = "Diagnostico";
                worksheet.Cells["E3"].Value = "Comentario";
                worksheet.Cells["F3"].Value = "Estado";

                int row = 4;
                foreach (var vehiculoMecanico in vehiculoMecanicos)
                {
                    worksheet.Cells[$"A{row}"].Value = vehiculoMecanico.VehiculoMecanicoId;
                    worksheet.Cells[$"B{row}"].Value = vehiculoMecanico.UsuarioId;
                    worksheet.Cells[$"C{row}"].Value = vehiculoMecanico.VehiculoId;
                    worksheet.Cells[$"D{row}"].Value = vehiculoMecanico.Diagnostico;
                    worksheet.Cells[$"E{row}"].Value = vehiculoMecanico.Comentario;
                    worksheet.Cells[$"F{row}"].Value = vehiculoMecanico.EstadoId;

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

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VehiculoMecanico.xlsx");
            }
        }

    }
}
