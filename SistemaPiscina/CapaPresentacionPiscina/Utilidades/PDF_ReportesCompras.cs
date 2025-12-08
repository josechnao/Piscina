using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CapaPresentacionPiscina.Utilidades
{
    public class PDF_ReportesCompras
    {
        public static void ExportarReporteCompras(
            string rutaArchivo,
            byte[] logoNegocio,
            string nombreNegocio,
            string direccion,
            string ciudad,
            DataGridView dgv,
            string totalGeneral
        )
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

            doc.Add(new Paragraph(" "));

            // ======================
            // NEGOCIO
            // ======================
            var tituloNegocio = new Paragraph(
                nombreNegocio,
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)
            );
            tituloNegocio.Alignment = Element.ALIGN_CENTER;
            doc.Add(tituloNegocio);

            var info = new Paragraph(
                $"{direccion} - {ciudad}",
                FontFactory.GetFont(FontFactory.HELVETICA, 10)
            );
            info.Alignment = Element.ALIGN_CENTER;
            doc.Add(info);

            doc.Add(new Paragraph("\n"));

            // ======================
            // TITULO REPORTE
            // ======================
            var tituloReporte = new Paragraph(
                "REPORTE DE COMPRAS",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)
            );
            tituloReporte.Alignment = Element.ALIGN_LEFT;
            doc.Add(tituloReporte);

            doc.Add(new Paragraph("\n"));

            // ======================
            // TABLA
            // ======================
            // Contar solo columnas visibles excepto btnVerDetalle
            int columnasVisibles = 0;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible && col.Name != "btnVerDetalle")
                    columnasVisibles++;
            }

            PdfPTable tabla = new PdfPTable(columnasVisibles);
            tabla.WidthPercentage = 100;

            // Encabezados
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible && col.Name != "btnVerDetalle")
                {
                    PdfPCell cell = new PdfPCell(
                        new Phrase(col.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9))
                    );
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
                        if (dgv.Columns[cel.ColumnIndex].Visible &&
                            dgv.Columns[cel.ColumnIndex].Name != "btnVerDetalle")
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
            var total = new Paragraph(
                $"\nTOTAL GENERAL: {totalGeneral}",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)
            );
            total.Alignment = Element.ALIGN_RIGHT;
            doc.Add(total);

            doc.Close();
        }
    }
}
