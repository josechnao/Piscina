using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CapaPresentacionPiscina.Utilidades
{
    public class PDF_Reportes
    {
        public static void ExportarReporteVentas(
            string rutaArchivo,
            byte[] logoNegocio,
            string nombreNegocio,
            string direccion,
            string ciudad,
            DataGridView dgv,
            string totalGeneral)
        {
            Document doc = new Document(PageSize.LETTER, 40, 40, 40, 40);
            PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
            doc.Open();

            // ======================
            // LOGO
            // ======================
            if (logoNegocio != null)
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoNegocio);
                logo.ScaleToFit(60f, 60f);
                logo.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
                doc.Add(logo);
            }

            // ESPACIO
            doc.Add(new iTextSharp.text.Paragraph(" "));

            // ======================
            // NOMBRE NEGOCIO
            // ======================
            var tituloNegocio = new iTextSharp.text.Paragraph(
                nombreNegocio,
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)
            );
            tituloNegocio.Alignment = Element.ALIGN_CENTER;
            doc.Add(tituloNegocio);

            // Dirección
            var info = new iTextSharp.text.Paragraph(
                $"{direccion} - {ciudad}",
                FontFactory.GetFont(FontFactory.HELVETICA, 10)
            );
            info.Alignment = Element.ALIGN_CENTER;
            doc.Add(info);

            doc.Add(new iTextSharp.text.Paragraph("\n"));

            // ======================
            // TÍTULO DEL REPORTE
            // ======================
            var tituloReporte = new iTextSharp.text.Paragraph(
                "REPORTE DE VENTAS",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)
            );
            tituloReporte.Alignment = Element.ALIGN_LEFT;
            doc.Add(tituloReporte);

            doc.Add(new iTextSharp.text.Paragraph(" "));

            // ======================
            // TABLA
            // ======================
            PdfPTable tabla = new PdfPTable(6);
            tabla.WidthPercentage = 100;

            // Encabezados
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Name != "btnVerDetalle")
                {
                    PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)));
                    cell.BackgroundColor = new BaseColor(230, 230, 230);
                    tabla.AddCell(cell);
                }
            }

            // Filas
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cel in row.Cells)
                    {
                        if (dgv.Columns[cel.ColumnIndex].Name != "btnVerDetalle")
                        {
                            tabla.AddCell(cel.Value?.ToString() ?? "");
                        }
                    }
                }
            }

            doc.Add(tabla);

            // ======================
            // TOTAL GENERAL
            // ======================
            var total = new iTextSharp.text.Paragraph(
                $"\nTOTAL GENERAL: {totalGeneral}",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)
            );
            total.Alignment = Element.ALIGN_RIGHT;
            doc.Add(total);

            doc.Close();
        }
    }
}
