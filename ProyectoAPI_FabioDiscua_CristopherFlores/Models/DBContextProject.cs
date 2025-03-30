using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Models
{
    public class DBContextProject : DbContext
    {
        /// <summary>
        /// Constructor de la clase DBContextProject. Inicializa la conexión a la base de datos utilizando la cadena de conexión "MyDbConnectionString".
        /// </summary>
        public DBContextProject() : base("MyDbConnectionString")
        { }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Arrendador.
        /// </summary>
        public DbSet<Arrendador> Arrendador { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Arrendatario.
        /// </summary>
        public DbSet<Arrendatario> Arrendatario { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Contrato.
        /// </summary>
        public DbSet<Contrato> Contrato { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Empleado.
        /// </summary>
        public DbSet<Empleado> Empleado { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Empleado_Administrativo.
        /// </summary>
        public DbSet<Empleado_Administrativo> EmpleadoAdministrativo { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Empleado_Seguridad.
        /// </summary>
        public DbSet<Empleado_Seguridad> EmpleadoSeguridad { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Empleado_Mantenimiento.
        /// </summary>
        public DbSet<Empleado_Mantenimiento> EmpleadoMantenimiento { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad AtencionAlCliente.
        /// </summary>
        public DbSet<AtencionAlCliente> AtencionAlCliente { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Factura.
        /// </summary>
        public DbSet<Factura> Factura { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Garante.
        /// </summary>
        public DbSet<Garante> Garante { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Pago.
        /// </summary>
        public DbSet<Pago> Pago { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Persona.
        /// </summary>
        public DbSet<Persona> Persona { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Apartamento.
        /// </summary>
        public DbSet<Apartamento> Apartamento { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Edificio.
        /// </summary>
        public DbSet<Edificio> Edificio { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Evaluacion.
        /// </summary>
        public DbSet<Evaluacion> Evaluacion { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Historial_Mantenimiento.
        /// </summary>
        public DbSet<Historial_Mantenimiento> Historial { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Queja_Solicitud.
        /// </summary>
        public DbSet<Queja_Solicitud> QuejaSolicitud { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Solicitud_Mantenimiento.
        /// </summary>
        public DbSet<Solicitud_Mantenimiento> SolicitudMantenimiento { get; set; }
    }
}