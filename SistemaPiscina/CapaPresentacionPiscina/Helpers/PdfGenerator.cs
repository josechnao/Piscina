using System;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace CapaPresentacionPiscina.Helpers
{
    public class PdfGenerator
    {
        private static SynchronizedConverter _converter =
            new SynchronizedConverter(new PdfTools());

        public static void GenerarPdf(string htmlContent, string rutaSalida)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,

                    PaperSize = new PechkinPaperSize("80mm", "200mm"),
                    Margins = new MarginSettings { Top = 5, Bottom = 5 }
                },

                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            var pdf = _converter.Convert(doc);

            File.WriteAllBytes(rutaSalida, pdf);
        }
    }
}
