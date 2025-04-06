using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoF_FabioCalix_CristopherFlores.Models
{
    public class Apartamento
    {
        /// <summary>
        /// Constructor de la clase Apartamento.
        /// </summary>
        public Apartamento() { }

        /// <summary>
        /// Obtiene o establece el ID del apartamento.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección del apartamento.
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Obtiene o establece el número de habitaciones del apartamento.
        /// </summary>
        public int NumHabitaciones { get; set; }

        /// <summary>
        /// Obtiene o establece el precio de alquiler del apartamento.
        /// </summary>
        public float PrecioAlquiler { get; set; }

        /// <summary>
        /// Obtiene o establece la disponibilidad del apartamento.
        /// </summary>
        public bool Disponible { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del arrendador del apartamento.
        /// </summary>
        public int IdArrendador { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Arrendador asociado al apartamento.
        /// </summary>
        public virtual Arrendador Arrendador { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del edificio al que pertenece el apartamento.
        /// </summary>
        public int IdEdificio { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Edificio asociado al apartamento.
        /// </summary>
        public virtual Edificio Edificio { get; set; }
    }
}