using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class Garante : Persona
    {
        /// <summary>
        /// Constructor de la clase Garante.
        /// </summary>
        public Garante() { }

        /// <summary>
        /// Obtiene o establece el documento de aval del garante.
        /// </summary>
        public string docAval { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de emisión del documento de aval.
        /// </summary>
        public DateTime emisionDoc { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del arrendatario garantizado.
        /// </summary>
        public int IdArrendatario { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Arrendatario asociado al garante.
        /// </summary>
        public virtual Arrendatario arrendatario { get; set; }
    }
}