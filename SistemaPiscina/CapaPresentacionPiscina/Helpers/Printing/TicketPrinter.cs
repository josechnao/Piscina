using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using CapaPresentacionPiscina.Helpers;

public static class TicketPrinter
{
    // ⚠️ Debe coincidir EXACTO con el nombre en Windows
    private const string PRINTER_NAME = "POS-80C";

    // ================================
    // MÉTODO PRINCIPAL
    // ================================
    public static void Imprimir(
        string nombreNegocio,
        string fechaHora,
        string cajero,
        string numeroTicket,
        string cliente,
        string documento,
        string telefono,
        List<ItemTicket> items,
        string metodoPago,
        decimal total
    )
    {
        List<byte> buffer = new List<byte>();

        // ===== CONFIGURACIÓN BASE =====
        buffer.AddRange(EscPosCommands.LineSpacingDefault());
        buffer.AddRange(EscPosCommands.CodePage850());
        buffer.AddRange(EscPosCommands.FontA());
        buffer.AddRange(EscPosCommands.AlignLeft());

        // ===== CABECERA =====
        buffer.AddRange(EscPosCommands.BoldOn());
        buffer.AddRange(EscPosCommands.Line(
            Centrar(NormalizarTexto(nombreNegocio))
        ));
        buffer.AddRange(EscPosCommands.BoldOff());

        buffer.AddRange(EscPosCommands.Line(Linea()));

        // ===== DATOS GENERALES =====
        buffer.AddRange(EscPosCommands.Line(
            NormalizarTexto($"Fecha/Hora: {fechaHora}")
        ));
        buffer.AddRange(EscPosCommands.Line(
            NormalizarTexto($"Cajero: {cajero}")
        ));
        buffer.AddRange(EscPosCommands.Line(
            NormalizarTexto($"Ticket: {numeroTicket}")
        ));

        // ===== CLIENTE =====
        buffer.AddRange(EscPosCommands.Line(Linea()));
        buffer.AddRange(EscPosCommands.Line(
            NormalizarTexto($"Cliente: {cliente} | CI: {documento}")
        ));
        buffer.AddRange(EscPosCommands.Line(
            NormalizarTexto($"Telefono: {telefono}")
        ));

        // ===== TABLA =====
        buffer.AddRange(EscPosCommands.Line(Linea()));
        buffer.AddRange(EscPosCommands.Line(
            $"{Pad("Cant", 4)} {Pad("Item", 12)} {PadRight("P.Unit", 7)} {PadRight("SubT", 7)}"
        ));

        // ===== ITEMS =====
        foreach (var it in items)
        {
            buffer.AddRange(EscPosCommands.Line(
                $"{Pad(it.Cantidad.ToString(), 4)} " +
                $"{Pad(NormalizarTexto(it.Nombre), 12)} " +
                $"{PadRight(it.PrecioUnitario.ToString("0.00"), 7)} " +
                $"{PadRight(it.SubTotal.ToString("0.00"), 7)}"
            ));

            if (!string.IsNullOrWhiteSpace(it.Descripcion))
            {
                buffer.AddRange(EscPosCommands.Line(
                    $"    {NormalizarTexto(it.Descripcion)}"
                ));
            }
        }

        // ===== TOTALES =====
        buffer.AddRange(EscPosCommands.Line(Linea()));
        buffer.AddRange(EscPosCommands.Line(
            NormalizarTexto($"Pago: {metodoPago}")
        ));

        buffer.AddRange(EscPosCommands.BoldOn());
        buffer.AddRange(EscPosCommands.Line(
            NormalizarTexto($"TOTAL: {total:0.00} Bs")
        ));
        buffer.AddRange(EscPosCommands.BoldOff());

        // ===== PIE =====
        buffer.AddRange(EscPosCommands.Line(""));
        buffer.AddRange(EscPosCommands.Line(
            Centrar(NormalizarTexto("Gracias por elegir Aguavida!"))
        ));
        buffer.AddRange(EscPosCommands.Line(""));

        // ===== AVANCE Y CORTE =====
        buffer.AddRange(EscPosCommands.Feed(4));
        buffer.AddRange(EscPosCommands.CutPartial());

        // ===== ENVÍO A IMPRESORA =====
        RawPrinterHelper.SendBytesToPrinter(
            PRINTER_NAME,
            buffer.ToArray()
        );
    }

    // ================================
    // UTILIDADES
    // ================================

    private static string Linea()
        => new string('-', 48); // ancho real 80mm (72mm útil)

    private static string Centrar(string texto, int ancho = 48)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return "";

        if (texto.Length >= ancho)
            return texto;

        int espacios = (ancho - texto.Length) / 2;
        return new string(' ', espacios) + texto;
    }

    private static string Pad(string texto, int ancho)
    {
        if (texto.Length > ancho)
            return texto.Substring(0, ancho);

        return texto.PadRight(ancho);
    }

    private static string PadRight(string texto, int ancho)
    {
        if (texto.Length > ancho)
            return texto.Substring(0, ancho);

        return texto.PadLeft(ancho);
    }

    // ================================
    // NORMALIZACIÓN (CLAVE)
    // ================================
    private static string NormalizarTexto(string texto)
    {
        if (string.IsNullOrEmpty(texto))
            return string.Empty;

        return texto
            .Replace("á", "a").Replace("Á", "A")
            .Replace("é", "e").Replace("É", "E")
            .Replace("í", "i").Replace("Í", "I")
            .Replace("ó", "o").Replace("Ó", "O")
            .Replace("ú", "u").Replace("Ú", "U")
            .Replace("ñ", "n").Replace("Ñ", "N")
            .Replace("¿", "")
            .Replace("¡", "");
    }
}
