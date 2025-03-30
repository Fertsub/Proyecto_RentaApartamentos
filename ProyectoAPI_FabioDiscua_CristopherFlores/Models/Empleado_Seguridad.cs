using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class Empleado_Seguridad : Empleado
    {
        /// <summary>
        /// Constructor de la clase Empleado_Seguridad.
        /// </summary>
        public Empleado_Seguridad() { }

        /// <summary>
        /// Obtiene o establece el turno del empleado de seguridad.
        /// </summary>
        public string turno { get; set; }

        /// <summary>
        /// Obtiene o establece si el empleado de seguridad está armado.
        /// </summary>
        public bool armado { get; set; }

        /// <summary>
        /// Calcula el sueldo del empleado de seguridad, incluyendo un incremento del 25%.
        /// </summary>
        /// <returns>El sueldo calculado.</returns>
        public override float CalculoSueldo()
        {
            return sueldo * 1.25f;
        }
    }
}