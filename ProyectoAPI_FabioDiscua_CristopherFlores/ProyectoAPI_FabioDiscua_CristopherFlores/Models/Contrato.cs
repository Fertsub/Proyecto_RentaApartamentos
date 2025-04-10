using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class Contrato
    {
        /// <summary>
        /// Constructor de la clase Contrato.
        /// </summary>
        public Contrato() { }

        /// <summary>
        /// Obtiene o establece el ID del contrato.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del apartamento asociado al contrato.
        /// </summary>
        public int IdApartamento { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Apartamento asociado al contrato.
        /// </summary>
        public virtual Apartamento apartamento { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del arrendador asociado al contrato.
        /// </summary>
        public int IdArrendador { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Arrendador asociado al contrato.
        /// </summary>
        public virtual Arrendador arrendador { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del arrendatario asociado al contrato.
        /// </summary>
        public int IdArrendatario { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Arrendatario asociado al contrato.
        /// </summary>
        public virtual Arrendatario arrendatario { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de inicio del contrato.
        /// </summary>
        public DateTime fechaInicio { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de fin del contrato.
        /// </summary>
        public DateTime fechaFin { get; set; }

        /// <summary>
        /// Obtiene o establece la cuota del contrato.
        /// </summary>
        public float Alquiler
        {
            get => apartamento?.PrecioAlquiler ?? 0;
            set
            {
                if (apartamento != null)
                {
                    apartamento.PrecioAlquiler = value;
                }
            }
        }

        /// <summary>
        /// Obtiene o establece el estado del contrato (activo o inactivo).
        /// </summary>
        public bool estado { get; set; }
    }
}