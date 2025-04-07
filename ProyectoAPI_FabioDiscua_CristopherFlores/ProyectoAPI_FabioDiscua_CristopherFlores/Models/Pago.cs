using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing.Constraints;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
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
        /// Obtiene o establece el ID de la factura asociada al pago.
        /// </summary>
        public virtual Factura Factura { get; set; }

        /// <summary>
        /// Obtiene el monto total del pago.
        /// </summary>
        public float Monto
        {
            get
            {
                return Factura?.montoTotal ?? 0;
            }
            set{}
        }

        /// <summary>
        /// Obtiene o establece el estado del pago (activo o inactivo).
        /// </summary>
        public bool estado { get; set; }
    }
}