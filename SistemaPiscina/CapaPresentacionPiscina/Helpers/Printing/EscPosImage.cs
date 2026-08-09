using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace CapaPresentacionPiscina.Helpers.Printing
{
    internal static class EscPosImage
    {
        // 80 mm normalmente trabaja con hasta 576 puntos.
        // Nuestro ticket usa 48 caracteres, así que encaja bien con 576 dots.
        private const int ANCHO_MAXIMO = 576;

        public static byte[] ConvertirLogo(byte[] imagenBytes)
        {
            if (imagenBytes == null || imagenBytes.Length == 0)
                return Array.Empty<byte>();

            using (MemoryStream ms = new MemoryStream(imagenBytes))
            using (Image imagenOriginal = Image.FromStream(ms))
            {
                // ==========================================
                // 1. REDIMENSIONAR CONSERVANDO PROPORCIÓN
                // ==========================================
                int nuevoAncho = Math.Min(imagenOriginal.Width, ANCHO_MAXIMO);

                double proporcion =
                    (double)nuevoAncho / imagenOriginal.Width;

                int nuevoAlto =
                    (int)(imagenOriginal.Height * proporcion);

                using (Bitmap bitmap = new Bitmap(
                    nuevoAncho,
                    nuevoAlto))
                {
                    // Fondo blanco importante para PNG transparentes
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(Color.White);

                        g.InterpolationMode =
                            InterpolationMode.HighQualityBicubic;

                        g.SmoothingMode =
                            SmoothingMode.HighQuality;

                        g.DrawImage(
                            imagenOriginal,
                            0,
                            0,
                            nuevoAncho,
                            nuevoAlto
                        );
                    }

                    return ConvertirBitmapEscPos(bitmap);
                }
            }
        }


        private static byte[] ConvertirBitmapEscPos(Bitmap bitmap)
        {
            int ancho = bitmap.Width;
            int alto = bitmap.Height;

            // Cada byte representa 8 píxeles horizontales
            int bytesPorFila = (ancho + 7) / 8;

            List<byte> resultado = new List<byte>();

            // ==========================================
            // COMANDO ESC/POS:
            // GS v 0
            // ==========================================
            resultado.Add(0x1D);
            resultado.Add(0x76);
            resultado.Add(0x30);
            resultado.Add(0x00);

            // Ancho en bytes
            resultado.Add((byte)(bytesPorFila & 0xFF));
            resultado.Add((byte)((bytesPorFila >> 8) & 0xFF));

            // Alto en píxeles
            resultado.Add((byte)(alto & 0xFF));
            resultado.Add((byte)((alto >> 8) & 0xFF));

            // ==========================================
            // CONVERTIR IMAGEN A BLANCO Y NEGRO
            // ==========================================
            for (int y = 0; y < alto; y++)
            {
                for (int byteX = 0; byteX < bytesPorFila; byteX++)
                {
                    byte valorByte = 0;

                    for (int bit = 0; bit < 8; bit++)
                    {
                        int x = (byteX * 8) + bit;

                        if (x >= ancho)
                            continue;

                        Color pixel = bitmap.GetPixel(x, y);

                        // Convertir RGB a luminosidad
                        int luminosidad =
                            (pixel.R * 299 +
                             pixel.G * 587 +
                             pixel.B * 114) / 1000;

                        // Si es oscuro => punto negro
                        if (luminosidad < 160)
                        {
                            valorByte |= (byte)(0x80 >> bit);
                        }
                    }

                    resultado.Add(valorByte);
                }
            }

            // Espacio después de la imagen
            resultado.Add(0x0A);

            return resultado.ToArray();
        }
    }
}