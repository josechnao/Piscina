using System.Text;

public static class EscPosCommands
{
    // ================================
    // RESET (NO usar en cada ticket)
    // ================================
    public static byte[] Init()
        => new byte[] { 0x1B, 0x40 };

    // ================================
    // ESPACIADO DE LINEA (mínimo)
    // ================================
    public static byte[] LineSpacingDefault()
        => new byte[] { 0x1B, 0x32 }; // ESC 2

    // ================================
    // CODE PAGE (para ñ, tildes, ¿¡)
    // ================================
    public static byte[] CodePage850()
        => new byte[] { 0x1B, 0x74, 0x02 }; // ESC t 2 (CP850)

    // ================================
    // ALINEACION
    // ================================
    public static byte[] AlignCenter()
        => new byte[] { 0x1B, 0x61, 0x01 };

    public static byte[] AlignLeft()
        => new byte[] { 0x1B, 0x61, 0x00 };

    // ================================
    // NEGRITA
    // ================================
    public static byte[] BoldOn()
        => new byte[] { 0x1B, 0x45, 0x01 };

    public static byte[] BoldOff()
        => new byte[] { 0x1B, 0x45, 0x00 };

    // ================================
    // FUENTE
    // ================================
    public static byte[] FontA()
        => new byte[] { 0x1B, 0x4D, 0x00 };

    // ================================
    // TEXTO (CP850)
    // ================================
    public static byte[] Line(string text)
    {
        return Encoding.GetEncoding(850).GetBytes(text + "\n");
    }

    // ================================
    // CORTE DE PAPEL
    // ================================
    // Avanza papel N líneas
    public static byte[] Feed(int lineas)
    {
        return new byte[] { 0x1B, 0x64, (byte)lineas }; // ESC d n
    }

    // Corte parcial (más seguro)
    public static byte[] CutPartial()
    {
        return new byte[] { 0x1D, 0x56, 0x01 };
    }


}
