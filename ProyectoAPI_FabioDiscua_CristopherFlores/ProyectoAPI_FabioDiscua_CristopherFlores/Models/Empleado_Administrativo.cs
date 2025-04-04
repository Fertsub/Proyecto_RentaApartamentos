using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class Empleado_Administrativo : Empleado
    {
        /// <summary>
        /// Constructor de la clase Empleado_Administrativo.
        /// </summary>
        public Empleado_Administrativo() { }

        /// <summary>
        /// Obtiene o establece la oficina del empleado administrativo.
        /// </summary>
        public string oficina { get; set; }

        /// <summary>
        /// Obtiene o establece el puesto del empleado administrativo.
        /// </summary>
        public string puesto { get; set; }

        /// <summary>
        /// Calcula el sueldo del empleado administrativo, incluyendo un incremento del 40%.
        /// </summary>
        /// <returns>El sueldo calculado.</returns>
        public override float CalculoSueldo()
        {
            return sueldo * 1.4f;
        }
    }
}