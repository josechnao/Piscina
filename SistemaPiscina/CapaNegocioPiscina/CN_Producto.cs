using System;
using System.Collections.Generic;
using CapaEntidadPiscina;
using CapaDatosPiscina;

namespace CapaNegocioPiscina
{
    public class CN_Producto
    {
        private CD_Producto cDatos = new CD_Producto();

        public List<Producto> Listar()
        {
            return cDatos.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // VALIDACIONES
            if (string.IsNullOrWhiteSpace(obj.Codigo))
            {
                Mensaje = "El campo 'Código' no puede estar vacío.";
                return 0;
            }

            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El campo 'Nombre' no puede estar vacío.";
                return 0;
            }

            if (obj.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoría.";
                return 0;
            }

            // Si todo está bien → enviamos a capa datos
            return cDatos.Registrar(obj, out Mensaje);
        }

        public int Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // VALIDACIONES
            if (obj.IdProducto == 0)
            {
                Mensaje = "Debe seleccionar un producto.";
                return 0;
            }

            if (string.IsNullOrWhiteSpace(obj.Codigo))
            {
                Mensaje = "El campo 'Código' no puede estar vacío.";
                return 0;
            }

            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El campo 'Nombre' no puede estar vacío.";
                return 0;
            }

            if (obj.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoría.";
                return 0;
            }

            // Pasamos a capa datos
            return cDatos.Editar(obj, out Mensaje);
        }

        public int CambiarEstado(int idProducto, bool nuevoEstado, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (idProducto == 0)
            {
                Mensaje = "Debe seleccionar un producto.";
                return 0;
            }

            return cDatos.CambiarEstado(idProducto, nuevoEstado, out Mensaje);
        }
    }
}
