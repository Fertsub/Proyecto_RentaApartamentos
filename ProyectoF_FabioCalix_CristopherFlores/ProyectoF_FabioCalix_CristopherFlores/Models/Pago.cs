using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoF_FabioCalix_CristopherFlores.Models
{
    public class Pago
    {
        /// <summary>
        /// Constructor de la clase Pago.
        /// </summary>
        public Pago() { }

        /// <summary>
        /// Obtiene o establece el ID del pago.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del contrato asociado al pago.
        /// </summary>
        public int IdContrato { get; set; }

        /// <summary>
        /// Obtiene o establece el contrato asociado al pago.
        /// </summary>
        public virtual Contrato contrato { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de pago.
        /// </summary>
        public DateTime fechaPago { get; set; }

        /// <summary>
        /// Obtiene o establece el monto del pago.
        /// </summary>
        public float Monto { get; set; }

        /// <summary>
        /// Obtiene o establece el costo mensual del pago.
        /// </summary>
        public float costoMensual { get; set; }

        /// <summary>
        /// Obtiene o establece el estado del pago (activo o inactivo).
        /// </summary>
        public bool estado { get; set; }
    }
}