using System;

namespace CapaEntidadPiscina
{
    public class PromocionConfiguracion
    {
        // --- Base de la promo ---
        public int IdPromocion { get; set; }
        public string TipoPromo { get; set; }
        public int IdEntradaTipo { get; set; }
        public decimal Porcentaje { get; set; }
        public bool Estado { get; set; }

        // NUEVO:
        public string Categoria { get; set; }
        // --- Categoría (Adulto, Niño, Todos...) ---
        public string NombreCategoria { get; set; }

        // --- Condición ---
        public string TipoCondicion { get; set; }
        public int CantidadCondicion { get; set; }

        // --- Límite ---
        public string TipoLimite { get; set; }
        public int CantidadLimite { get; set; }
        public int CantidadUsada { get; set; }

        // --- Vigencia ---
        public string TipoVigencia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaDia { get; set; }
    }
}
