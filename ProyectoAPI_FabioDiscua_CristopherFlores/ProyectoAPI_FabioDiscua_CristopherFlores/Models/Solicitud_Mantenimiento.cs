using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class Solicitud_Mantenimiento
    {
        /// <summary>
        /// Constructor de la clase Solicitud_Mantenimiento.
        /// </summary>
        public Solicitud_Mantenimiento() { }

        /// <summary>
        /// Obtiene o establece el ID de la solicitud de mantenimiento.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del apartamento asociado a la solicitud.
        /// </summary>
        public int IdApartamento { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Apartamento asociado a la solicitud.
        /// </summary>
        public virtual Apartamento Apartamento { get; set; }

        /// <summary>
        /// Obtiene o establece la descripción de la solicitud de mantenimiento.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del empleado de mantenimiento asignado a la solicitud.
        /// </summary>
        public int IdEmpleado { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Empleado_Mantenimiento asociado a la solicitud.
        /// </summary>
        public virtual Empleado_Mantenimiento Mantenimiento { get; set; }

        /// <summary>
        /// Obtiene o establece el costo del mantenimiento.
        /// </summary>
        public float Costo { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de realización del mantenimiento.
        /// </summary>
        public DateTime FechaRealizacion { get; set; }

        /// <summary>
        /// Obtiene o establece el estado de la solicitud de mantenimiento (resuelta o pendiente).
        /// </summary>
        public bool Estado { get; set; }
    }
}
