using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoAPI_FabioDiscua_CristopherFlores.Models;
using Swashbuckle.Swagger.Annotations;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Controllers
{
    public class SeguridadController : ApiController
    {
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Retorna la lista de empleados de seguridad
        /// </summary>
        /// <returns>Una lista de empleados de seguridad.</returns>
        public IEnumerable<Empleado_Seguridad> Get()
        {
            return db.EmpleadoSeguridad;
        }

        /// <summary>
        /// Retorna un empleado de seguridad por id
        /// </summary>
        /// <param name="id">ID del empleado de seguridad a retornar.</param>
        /// <returns>Un empleado de seguridad específico.</returns>
        /// <response code="200">Si el empleado de seguridad es encontrado.</response>
        /// <response code="404">Si el empleado de seguridad no es encontrado.</response>
        public IHttpActionResult Get(int id)
        {
            Empleado_Seguridad empleadoSeguridad = db.EmpleadoSeguridad.Find(id);
            if (empleadoSeguridad == null)
            {
                return NotFound();
            }

            return Ok(empleadoSeguridad);
        }

        /// <summary>
        /// Crea un nuevo empleado de seguridad
        /// </summary>
        /// <param name="empleadoSeguridad">El empleado de seguridad a crear.</param>
        /// <returns>El empleado de seguridad creado.</returns>
        public IHttpActionResult Post(Empleado_Seguridad empleadoSeguridad)
        {
            db.EmpleadoSeguridad.Add(empleadoSeguridad);
            db.SaveChanges();
            return Ok(empleadoSeguridad);
        }

        /// <summary>
        /// Actualiza un empleado de seguridad existente
        /// </summary>
        /// <param name="empleadoSeguridadModificado">El empleado de seguridad con los datos actualizados.</param>
        /// <param name="id">ID del empleado de seguridad a editar.</param>
        /// <returns>El empleado de seguridad actualizado.</returns>
        /// <response code="200">Si el empleado de seguridad es actualizado correctamente.</response>
        /// <response code="404">Si el empleado de seguridad no es encontrado.</response>
        public IHttpActionResult Put(int id, Empleado_Seguridad empleadoSeguridadModificado)
        {
            if (empleadoSeguridadModificado == null)
            {
                return NotFound();
            }

            db.Entry(empleadoSeguridadModificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(empleadoSeguridadModificado);
        }

        /// <summary>
        /// Elimina un empleado de seguridad por id
        /// </summary>
        /// <param name="id">ID del empleado de seguridad a eliminar.</param>
        /// <returns>El empleado de seguridad eliminado.</returns>
        /// <response code="200">Si el empleado de seguridad es eliminado correctamente.</response>
        /// <response code="404">Si el empleado de seguridad no es encontrado.</response>
        public IHttpActionResult Delete(int id)
        {
            Empleado_Seguridad empleadoSeguridad = db.EmpleadoSeguridad.Find(id);
            if (empleadoSeguridad == null)
            {
                return NotFound();
            }

            db.EmpleadoSeguridad.Remove(empleadoSeguridad);
            db.SaveChanges();
            return Ok(empleadoSeguridad);
        }
    }
}