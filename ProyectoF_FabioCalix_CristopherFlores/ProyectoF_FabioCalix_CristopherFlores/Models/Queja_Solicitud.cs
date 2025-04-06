using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoF_FabioCalix_CristopherFlores.Models
{
    public class Queja_Solicitud
    {
        /// <summary>
        /// Constructor de la clase Queja_Solicitud.
        /// </summary>
        public Queja_Solicitud() { }

        /// <summary>
        /// Obtiene o establece el ID de la queja o solicitud.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del apartamento asociado a la queja o solicitud.
        /// </summary>
        public int IdApartamento { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Apartamento asociado a la queja o solicitud.
        /// </summary>
        public virtual Apartamento Apartamento { get; set; }

        /// <summary>
        /// Obtiene o establece la descripción de la queja o solicitud.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Obtiene o establece el estado de la queja o solicitud (resuelta o pendiente).
        /// </summary>
        public bool Estado { get; set; }
    }
}