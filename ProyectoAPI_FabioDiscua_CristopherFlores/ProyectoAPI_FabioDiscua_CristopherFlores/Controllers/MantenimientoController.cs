using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoAPI_FabioDiscua_CristopherFlores.Models;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Controllers
{
    public class MantenimientoController : ApiController
    {

        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Retorna la lista de empleados de mantenimiento
        /// </summary>
        /// <returns>Una lista de empleados de mantenimiento.</returns>
        [HttpGet]
        public IEnumerable<Empleado_Mantenimiento> Get()
        {
            return db.EmpleadoMantenimiento;
        }

        /// <summary>
        /// Retorna un empleado de mantenimiento por id
        /// </summary>
        /// <param name="id">ID del empleado de mantenimiento a retornar.</param>
        /// <returns>Un empleado de mantenimiento específico.</returns>
        /// <response code="200">Si el empleado de mantenimiento es encontrado.</response>
        /// <response code="404">Si el empleado de mantenimiento no es encontrado.</response>
        public IHttpActionResult Get(int id)
        {
            Empleado_Mantenimiento empleadoMantenimiento = db.EmpleadoMantenimiento.Find(id);
            if (empleadoMantenimiento == null)
            {
                return NotFound();
            }

            return Ok(empleadoMantenimiento);
        }

        /// <summary>
        /// Crea un nuevo empleado de mantenimiento
        /// </summary>
        /// <param name="empleadoMantenimiento">El empleado de mantenimiento a crear.</param>
        /// <returns>El empleado de mantenimiento creado.</returns>
        public IHttpActionResult Post(Empleado_Mantenimiento empleadoMantenimiento)
        {
            if (empleadoMantenimiento == null)
            {
                return BadRequest("El empleado no puede ser nulo");
            }

            db.EmpleadoMantenimiento.Add(empleadoMantenimiento);
            db.SaveChanges();
            return Ok(empleadoMantenimiento);
        }

        /// <summary>
        /// Actualiza un empleado de mantenimiento existente
        /// </summary>
        /// <param name="mantModificado">El empleado de mantenimiento con los datos actualizados.</param>
        /// <param name="id">ID del empleado de mantenimiento a editar.</param>
        /// <returns>El empleado de mantenimiento actualizado.</returns>
        /// <response code="200">Si el empleado de mantenimiento es actualizado correctamente.</response>
        /// <response code="404">Si el empleado de mantenimiento no es encontrado.</response>
        public IHttpActionResult Put(int id, Empleado_Mantenimiento mantModificado)
        {
            if (mantModificado == null)
            {
                return NotFound();
            }

            db.Entry(mantModificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(mantModificado);
        }

        /// <summary>
        /// Elimina un empleado de mantenimiento por id
        /// </summary>
        /// <param name="id">ID del empleado de mantenimiento a eliminar.</param>
        /// <returns>El empleado de mantenimiento eliminado.</returns>
        /// <response code="200">Si el empleado de mantenimiento es eliminado correctamente.</response>
        /// <response code="404">Si el empleado de mantenimiento no es encontrado.</response>
        public IHttpActionResult Delete(int id)
        {
            Empleado_Mantenimiento empleadoMantenimiento = db.EmpleadoMantenimiento.Find(id);
            if (empleadoMantenimiento == null)
            {
                return NotFound();
            }

            db.EmpleadoMantenimiento.Remove(empleadoMantenimiento);
            db.SaveChanges();
            return Ok(empleadoMantenimiento);
        }
    }
}