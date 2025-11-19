using CapaDatosPiscina;

namespace CapaNegocioPiscina
{
    public class CN_CorrelativoCompra
    {
        private CD_CorrelativoCompra cd_correlativo = new CD_CorrelativoCompra();

        public int ObtenerCorrelativo()
        {
            return cd_correlativo.ObtenerCorrelativo();
        }

        public bool ActualizarCorrelativo()
        {
            return cd_correlativo.ActualizarCorrelativo();
        }
    }

}
