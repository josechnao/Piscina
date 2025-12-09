using System;
using CapaEntidadPiscina;
using CapaDatosPiscina;

namespace CapaNegocioPiscina
{
    public class CN_ReportesFinancieros
    {
        private CD_ReportesFinancieros objDatos = new CD_ReportesFinancieros();

        public EResumenFinanciero ObtenerResumenFinanciero(DateTime desde, DateTime hasta)
        {
            return objDatos.ObtenerResumenFinanciero(desde, hasta);
        }
    }
}
