using CapaDatosPiscina;
using CapaEntidadPiscina;
using System;

namespace CapaNegocioPiscina
{
    public class CN_Usuario
    {
        private CD_Usuario objCapaDatos = new CD_Usuario();

        public Usuario Login(string documento, string clave)
        {
            if (string.IsNullOrWhiteSpace(documento) || string.IsNullOrWhiteSpace(clave))
            {
                return new Usuario(); // devuelve vacío si faltan datos
            }

            return objCapaDatos.Login(documento, clave);
        }
    }
}
