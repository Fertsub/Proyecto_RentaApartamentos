using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using System.Linq;

    public class AtencionAlCliente : Empleado
    {
        /// <summary>
        /// Constructor de la clase AtencionAlCliente.
        /// </summary>
        public AtencionAlCliente() { }

        /// <summary>
        /// Obtiene o establece la extensión telefónica del empleado de atención al cliente.
        /// </summary>
        public string extTelefonica { get; set; }

        /// <summary>
        /// Obtiene o establece la cadena de idiomas del empleado de atención al cliente, almacenada en la base de datos.
        /// </summary>
        public string IdiomasDB { get; set; }

        /// <summary>
        /// Calcula el sueldo del empleado de atención al cliente, incluyendo un incremento del 20%.
        /// </summary>
        /// <returns>El sueldo calculado.</returns>
        public override float CalculoSueldo()
        {
            return sueldo * 1.2f;
        }

        /// <summary>
        /// Obtiene o establece la lista de idiomas del empleado de atención al cliente.
        /// Esta propiedad no se mapea a la base de datos y se utiliza para manipular los idiomas.
        /// </summary>
        [NotMapped]
        public List<string> Idiomas
        {
            get => string.IsNullOrEmpty(IdiomasDB) ? new List<string>() : IdiomasDB.Split(',').ToList();
            set => IdiomasDB = string.Join(",", value);
        }
    }
}