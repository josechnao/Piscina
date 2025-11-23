using CapaDatosPiscina;
using CapaEntidadPiscina;
using System;

namespace CapaNegocioPiscina
{
    public class CN_Promocion
    {
        private CD_Promocion objCdPromo = new CD_Promocion();


        public EPromocion Obtener()
        {
            return objCdPromo.Obtener();
        }


        public bool Actualizar(EPromocion obj, out string mensaje)
        {
            mensaje = string.Empty;

            // Validación simple
            if (string.IsNullOrEmpty(obj.Categoria))
            {
                mensaje = "Debe seleccionar una categoría válida.";
                return false;
            }

            return objCdPromo.Actualizar(obj, out mensaje);
        }
    }
}
