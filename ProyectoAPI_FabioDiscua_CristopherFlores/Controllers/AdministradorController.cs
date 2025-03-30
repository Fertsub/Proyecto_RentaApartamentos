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
    public class AdministradorController : ApiController
    {
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Retorna la lista de empleados administrativos
        /// </summary>
        /// <returns>Una lista de empleados administrativos.</returns>
        public IEnumerable<Empleado_Administrativo> Get()
        {
            return db.EmpleadoAdministrativo;
        }

        /// <summary>
        /// Retorna un empleado administrativo por id
        /// </summary>
        /// <param name="id">ID del empleado administrativo a retornar.</param>
        /// <returns>Un empleado administrativo específico.</returns>
        /// <response code="200">Si el empleado administrativo es encontrado.</response>
        /// <response code="404">Si el empleado administrativo no es encontrado.</response>
        public IHttpActionResult Get(int id)
        {
            Empleado_Administrativo empleadoAdministrativo = db.EmpleadoAdministrativo.Find(id);
            if (empleadoAdministrativo == null)
            {
                return NotFound();
            }

            return Ok(empleadoAdministrativo);
        }

        /// <summary>
        /// Crea un nuevo empleado administrativo
        /// </summary>
        /// <param name="empleadoAdministrativo">El empleado administrativo a crear.</param>
        /// <returns>El empleado administrativo creado.</returns>
        public IHttpActionResult Post(Empleado_Administrativo empleadoAdministrativo)
        {
            db.EmpleadoAdministrativo.Add(empleadoAdministrativo);
            db.SaveChanges();
            return Ok(empleadoAdministrativo);
        }

        /// <summary>
        /// Actualiza un empleado administrativo existente
        /// </summary>
        /// <param name="empleadoAdministrativoModificado">El empleado administrativo con los datos actualizados.</param>
        /// <param name="id">ID del empleado administrativo a editar.</param>
        /// <returns>El empleado administrativo actualizado.</returns>
        /// <response code="200">Si el empleado administrativo es actualizado correctamente.</response>
        /// <response code="404">Si el empleado administrativo no es encontrado.</response>
        public IHttpActionResult Put(int id, Empleado_Administrativo empleadoAdministrativoModificado)
        {
            if (empleadoAdministrativoModificado == null)
            {
                return NotFound();
            }

            db.Entry(empleadoAdministrativoModificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(empleadoAdministrativoModificado);
        }

        /// <summary>
        /// Elimina un empleado administrativo por id
        /// </summary>
        /// <param name="id">ID del empleado administrativo a eliminar.</param>
        /// <returns>El empleado administrativo eliminado.</returns>
        /// <response code="200">Si el empleado administrativo es eliminado correctamente.</response>
        /// <response code="404">Si el empleado administrativo no es encontrado.</response>
        public IHttpActionResult Delete(int id)
        {
            Empleado_Administrativo empleadoAdministrativo = db.EmpleadoAdministrativo.Find(id);
            if (empleadoAdministrativo == null)
            {
                return NotFound();
            }

            db.EmpleadoAdministrativo.Remove(empleadoAdministrativo);
            db.SaveChanges();
            return Ok(empleadoAdministrativo);
        }
    }
}