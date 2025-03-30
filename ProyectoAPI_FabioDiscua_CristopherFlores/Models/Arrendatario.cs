using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class Arrendatario : Persona
    {
        /// <summary>
        /// Constructor de la clase Arrendatario.
        /// </summary>
        public Arrendatario() { }

        /// <summary>
        /// Obtiene o establece la fecha de nacimiento del arrendatario.
        /// </summary>
        public DateTime fechaNacimiento { get; set; }
    }
}