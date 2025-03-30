using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class Edificio
    {
        /// <summary>
        /// Constructor de la clase Edificio.
        /// </summary>
        public Edificio() { }

        /// <summary>
        /// Obtiene o establece el ID del edificio.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del edificio.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección del edificio.
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Obtiene o establece el número de pisos del edificio.
        /// </summary>
        public int NumPisos { get; set; }
    }
}

