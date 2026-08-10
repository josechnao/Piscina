using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace CapaPresentacionPiscina.Helpers
{
    public static class PdfGenerator
    {
        private static readonly SynchronizedConverter _converter =
            new SynchronizedConverter(new PdfTools());

        public static void GenerarPdf(string htmlContent, string rutaSalida)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
                throw new System.ArgumentException(
                    "El contenido HTML está vacío.",
                    nameof(htmlContent)
                );

            if (string.IsNullOrWhiteSpace(rutaSalida))
                throw new System.ArgumentException(
                    "La ruta de salida no es válida.",
                    nameof(rutaSalida)
                );

            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,

                    // Dejamos más espacio para tickets largos
                    PaperSize = new PechkinPaperSize("80mm", "200mm"),

                    Margins = new MarginSettings
                    {
                        Top = 5,
                        Bottom = 5,
                        Left = 2,
                        Right = 2
                    }
                },

                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = htmlContent,

                        WebSettings =
                        {
                            DefaultEncoding = "utf-8"
                        }
                    }
                }
            };

            byte[] pdf = _converter.Convert(doc);

            if (pdf == null || pdf.Length == 0)
                throw new IOException(
                    "DinkToPdf no generó contenido para el PDF."
                );

            File.WriteAllBytes(rutaSalida, pdf);
        }
    }
}