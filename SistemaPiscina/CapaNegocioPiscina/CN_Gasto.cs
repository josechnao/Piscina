using CapaDatosPiscina;
using CapaEntidadPiscina;
using System;
using System.Collections.Generic;

namespace CapaNegocioPiscina
{
    public class CN_Gasto
    {
        private CD_Gasto objCdGasto = new CD_Gasto();

        // ======================================================
        // 1. REGISTRAR
        // ======================================================
        public int Registrar(EGasto obj, out string mensaje)
        {
            mensaje = string.Empty;

            // VALIDACIONES
            if (obj.IdCategoriaGasto == 0)
            {
                mensaje = "Debe seleccionar una categoría.";
                return 0;
            }

            if (obj.Monto <= 0)
            {
                mensaje = "El monto debe ser mayor a 0.";
                return 0;
            }

            if (string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                mensaje = "La descripción no puede estar vacía.";
                return 0;
            }

            if (obj.IdUsuario == 0)
            {
                mensaje = "No se identificó al usuario.";
                return 0;
            }

            // LLAMAR A CAPA DATOS
            return objCdGasto.Registrar(obj, out mensaje);
        }


        // ======================================================
        // 2. EDITAR
        // ======================================================
        public bool Editar(EGasto obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (obj.IdGasto == 0)
            {
                mensaje = "Debe seleccionar un gasto válido.";
                return false;
            }

            if (obj.IdCategoriaGasto == 0)
            {
                mensaje = "Debe seleccionar una categoría.";
                return false;
            }

            if (obj.Monto <= 0)
            {
                mensaje = "El monto debe ser mayor a 0.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                mensaje = "La descripción no puede estar vacía.";
                return false;
            }

            return objCdGasto.Editar(obj, out mensaje);
        }


        // ======================================================
        // 3. CAMBIAR ESTADO
        // ======================================================
        public bool CambiarEstado(int idGasto, bool estado, out string mensaje)
        {
            mensaje = string.Empty;

            if (idGasto == 0)
            {
                mensaje = "Debe seleccionar un gasto válido.";
                return false;
            }

            return objCdGasto.CambiarEstado(idGasto, estado, out mensaje);
        }


        // ======================================================
        // 4. LISTAR – ADMIN (TODOS)
        // ======================================================
        public List<EGasto> ListarAdmin()
        {
            return objCdGasto.ListarAdmin();
        }


        // ======================================================
        // 5. LISTAR – CAJERO (SOLO SU TURNO)
        // ======================================================
        public List<EGasto> ListarCajero(int idCajaTurno)
        {
            return objCdGasto.ListarCajero(idCajaTurno);
        }


        // ======================================================
        // 6. FILTRAR – ADMIN
        // ======================================================
        public List<EGasto> FiltrarAdmin(string descripcion, int idCategoria, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return objCdGasto.FiltrarAdmin(descripcion, idCategoria, fechaDesde, fechaHasta);
        }


        // ======================================================
        // 7. FILTRAR – CAJERO
        // ======================================================
        public List<EGasto> FiltrarCajero(int idCajaTurno, string descripcion, int idCategoria)
        {
            return objCdGasto.FiltrarCajero(idCajaTurno, descripcion, idCategoria);
        }
    }
}
