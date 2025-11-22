using System;
using System.Collections.Generic;
using CapaEntidadPiscina;
using CapaDatosPiscina;

namespace CapaNegocioPiscina
{
    public class CN_Promociones
    {
        private CD_Promociones objCD = new CD_Promociones();

        public bool RegistrarPromocion(Promocion promo,
                               PromocionCondicion cond,
                               PromocionLimite lim,
                               PromocionVigencia vig,
                               out string mensaje)
        {
            return objCD.RegistrarPromocion(promo, cond, lim, vig, out mensaje);
        }


        public List<PromocionListado> ListarPromociones2x1()
        {
            return objCD.ListarPromociones2x1();
        }


        public bool EliminarPromocion(int idPromocion, out string mensaje)
        {
            return objCD.EliminarPromocion(idPromocion, out mensaje);
        }

        public PromocionConfiguracion ObtenerPromocionActiva()
        {
            return objCD.ObtenerPromocionActiva();
        }

    }
}