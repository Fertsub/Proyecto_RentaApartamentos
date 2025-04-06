using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoF_FabioCalix_CristopherFlores.Models
{
    public class Persona
    {
        /// <summary>
        /// Constructor de la clase Persona.
        /// </summary>
        public Persona() { }

        /// <summary>
        /// Obtiene o establece el ID de la persona.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Obtiene o establece el número de documento de identidad (DNI) de la persona.
        /// </summary>
        public int dni { get; set; }

        /// <summary>
        /// Obtiene o establece los nombres de la persona.
        /// </summary>
        public string nombres { get; set; }

        /// <summary>
        /// Obtiene o establece los apellidos de la persona.
        /// </summary>
        public string apellidos { get; set; }

        /// <summary>
        /// Obtiene o establece el número de teléfono de la persona.
        /// </summary>
        public string telefono { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico de la persona.
        /// </summary>
        public string email { get; set; }
    }
}