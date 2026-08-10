using CapaDatosPiscina;
using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapaNegocioPiscina
{
    public class CN_Usuario
    {
        private CD_Usuario objCapaDatos = new CD_Usuario();

        public async Task<Usuario> LoginAsync(string documento, string clave)
        {
            if (string.IsNullOrWhiteSpace(documento) || string.IsNullOrWhiteSpace(clave))
            {
                return new Usuario();
            }

            return await objCapaDatos.LoginAsync(documento, clave);
        }

        public List<Usuario> Listar()
        {
            return objCapaDatos.Listar();
        }

        public int Guardar(Usuario u)
        {
            return objCapaDatos.Guardar(u);
        }

        public bool Eliminar(int idUsuario)
        {
            return objCapaDatos.Eliminar(idUsuario);
        }
    }
}