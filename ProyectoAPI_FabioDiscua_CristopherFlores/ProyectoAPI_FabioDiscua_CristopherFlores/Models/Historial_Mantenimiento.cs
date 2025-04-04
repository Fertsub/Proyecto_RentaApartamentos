using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class Historial_Mantenimiento
    {
        /// <summary>
        /// Constructor de la clase Historial_Mantenimiento.
        /// </summary>
        public Historial_Mantenimiento() { }

        /// <summary>
        /// Obtiene o establece el ID del historial de mantenimiento.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del empleado de mantenimiento asociado.
        /// </summary>
        public int IdEmpleado { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Empleado_Mantenimiento asociado al historial.
        /// </summary>
        public virtual Empleado_Mantenimiento EmpleadoMantenimiento { get; set; }

        /// <summary>
        /// Obtiene o establece el ID de la solicitud de mantenimiento asociada.
        /// </summary>
        public int IdSol { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Solicitud_Mantenimiento asociado al historial.
        /// </summary>
        public virtual Solicitud_Mantenimiento SolicitudMantenimiento { get; set; }
    }
}

