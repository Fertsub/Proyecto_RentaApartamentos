using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using static ProyectoAPI_FabioDiscua_CristopherFlores.Models.Factura;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class Factura
    {
        /// <summary>
        /// Constructor de la clase Factura. Inicializa la lista de servicios.
        /// </summary>
        public Factura()
        {
            Servicios = new List<Servicio>();
        }

        /// <summary>
        /// Obtiene o establece el ID de la factura.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del contrato asociado a la factura.
        /// </summary>
        public int IdContrato { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto Contrato asociado a la factura.
        /// </summary>
        public virtual Contrato contrato { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de emisión de la factura.
        /// </summary>
        public DateTime emision { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de servicios incluidos en la factura.
        /// El valor predeterminado es Servicio.Cable_Internet.
        /// </summary>
        [DefaultValue(Servicio.Cable_Internet)]
        public List<Servicio> Servicios { get; set; }

        /// <summary>
        /// Obtiene o establece el plan de servicios de la factura.
        /// El valor predeterminado es Planes.Basico.
        /// </summary>
        [DefaultValue(Planes.Basico)]
        public Planes? plan { get; set; }

        /// <summary>
        /// Obtiene el monto total de la factura. Se calcula utilizando el método CalcularMontoTotal().
        /// </summary>
        public float montoTotal { get; private set; }

        /// <summary>
        /// Enumeración que define los tipos de servicios disponibles.
        /// </summary>
        public enum Servicio
        {
            Cable_Internet,
            Limpieza,
            Seguridad
        }

        /// <summary>
        /// Enumeración que define los planes de servicios disponibles.
        /// </summary>
        public enum Planes
        {
            Basico,
            Avanzado,
            Premium
        }

        /// <summary>
        /// Calcula el monto total de la factura sumando los precios de los servicios y el plan seleccionado.
        /// </summary>
        public void CalcularMontoTotal()
        {
            montoTotal = 0;

            foreach (Servicio servicio in Servicios)
            {
                switch (servicio)
                {
                    case Servicio.Cable_Internet:
                        montoTotal += GetPrecioPlan(plan);
                        break;
                    case Servicio.Limpieza:
                        montoTotal += 200;
                        break;
                    case Servicio.Seguridad:
                        montoTotal += 500;
                        break;
                }
            }
        }

        /// <summary>
        /// Obtiene el precio del plan de servicios seleccionado.
        /// </summary>
        /// <param name="plan">El plan de servicios.</param>
        /// <returns>El precio del plan.</returns>
        private float GetPrecioPlan(Planes? plan)
        {
            if (plan == null)
            {
                return 0;
            }

            switch (plan)
            {
                case Planes.Basico:
                    return 600;
                case Planes.Avanzado:
                    return 900;
                case Planes.Premium:
                    return 1300;
                default:
                    return 0;
            }
        }
    }
}