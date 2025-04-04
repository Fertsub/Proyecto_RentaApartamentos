using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class Evaluacion
    {
        /// <summary>
        /// Constructor de la clase Evaluacion.
        /// </summary>
        public Evaluacion() { }

        /// <summary>
        /// Obtiene o establece el ID de la evaluación.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Obtiene o establece la calificación del apartamento.
        /// </summary>
        public int calificacionApartamento { get; set; }

        /// <summary>
        /// Obtiene o establece la calificación del arrendador.
        /// </summary>
        public int calificacionArrendador { get; set; }

        /// <summary>
        /// Obtiene o establece la opinión del usuario.
        /// </summary>
        public string opinion { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de la evaluación.
        /// </summary>
        public DateTime fecha { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del apartamento evaluado.
        /// </summary>
        public int IdApartamento { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Apartamento asociado a la evaluación.
        /// </summary>
        public virtual Apartamento Apartamento { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del arrendador evaluado.
        /// </summary>
        public int IdArrendador { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Arrendador asociado a la evaluación.
        /// </summary>
        public virtual Arrendador arrendador { get; set; }
    }
}
