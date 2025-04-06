using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoF_FabioCalix_CristopherFlores.Models
{
    public class Empleado_Mantenimiento : Empleado
    {
        /// <summary>
        /// Constructor de la clase Empleado_Mantenimiento.
        /// </summary>
        public Empleado_Mantenimiento() { }

        /// <summary>
        /// Obtiene o establece la cadena de especialidades del empleado de mantenimiento, almacenada en la base de datos.
        /// </summary>
        public string EspecialidadesDB { get; set; }

        /// <summary>
        /// Calcula el sueldo del empleado de mantenimiento, incluyendo un incremento del 25%.
        /// </summary>
        /// <returns>El sueldo calculado.</returns>
        public override float CalculoSueldo()
        {
            return sueldo * 1.25f;
        }

        /// <summary>
        /// Obtiene o establece la lista de especialidades del empleado de mantenimiento.
        /// Esta propiedad se utiliza para manipular las especialidades.
        /// </summary>
        public List<string> Especialidades
        {
            get => string.IsNullOrEmpty(EspecialidadesDB) ? new List<string>() : EspecialidadesDB.Split(',').ToList();
            set => EspecialidadesDB = string.Join(",", value);
        }
    }
}