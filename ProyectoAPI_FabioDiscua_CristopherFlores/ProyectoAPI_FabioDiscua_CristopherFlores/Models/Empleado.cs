using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public abstract class Empleado : Persona
    {
        /// <summary>
        /// Constructor de la clase abstracta Empleado.
        /// </summary>
        public Empleado() { }

        /// <summary>
        /// Obtiene o establece el sueldo del empleado.
        /// </summary>
        public float sueldo { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de contratación del empleado.
        /// </summary>
        public DateTime fechaContratacion { get; set; }

        /// <summary>
        /// Obtiene o establece el estado del empleado (activo o inactivo).
        /// </summary>
        public bool estado { get; set; }

        /// <summary>
        /// Calcula el sueldo del empleado. Puede ser sobreescrito en clases derivadas para aplicar cálculos específicos.
        /// </summary>
        /// <returns>El sueldo calculado.</returns>
        public virtual float CalculoSueldo()
        {
            return sueldo;
        }
    }
}