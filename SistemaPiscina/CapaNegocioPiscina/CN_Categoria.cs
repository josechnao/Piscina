using System.Collections.Generic;
using CapaDatosPiscina;
using CapaEntidadPiscina;

namespace CapaNegocioPiscina
{
    public class CN_Categoria
    {
        private CD_Categoria objCapaDatos = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return objCapaDatos.Listar();
        }
        public List<Categoria> ListarActivas()
        {
            return objCapaDatos.ListarActivas();   // ESTE ERA EL ERROR
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // Validación sencilla (como en tus otros módulos)
            if (string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción no puede estar vacía.";
                return 0;
            }

            return objCapaDatos.Registrar(obj, out Mensaje);
        }

        public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción no puede estar vacía.";
                return false;
            }

            return objCapaDatos.Editar(obj, out Mensaje);
        }

        public bool CambiarEstado(int idCategoria, bool nuevoEstado, out string Mensaje)
        {
            return objCapaDatos.CambiarEstado(idCategoria, nuevoEstado, out Mensaje);
        }
        

    }
}
