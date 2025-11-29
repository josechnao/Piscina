using CapaDatosPiscina;
using CapaEntidadPiscina;
using System.Collections.Generic;

namespace CapaNegocioPiscina
{
    public class CN_CategoriaGasto
    {
        private CD_CategoriaGasto objCdCategoria = new CD_CategoriaGasto();

        public List<ECategoriaGasto> Listar()
        {
            return objCdCategoria.Listar();
        }
    }
}
