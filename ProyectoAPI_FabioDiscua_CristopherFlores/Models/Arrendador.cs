using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class Arrendador : Persona
    {
        /// <summary>
        /// Constructor de la clase Arrendador.
        /// </summary>
        public Arrendador() { }

        /// <summary>
        /// Obtiene o establece el número de registro del arrendador.
        /// </summary>
        public string numRegistro { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección de residencia del arrendador.
        /// </summary>
        public string direccionResidencia { get; set; }
    }
}