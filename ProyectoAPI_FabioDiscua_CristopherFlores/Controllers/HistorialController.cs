using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using ProyectoAPI_FabioDiscua_CristopherFlores.Models;
using Swashbuckle.Swagger.Annotations;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Controllers
{
    public class HistorialController : ApiController
    {
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Obtener la lista del historial de mantenimiento con detalles del empleado, solicitud, apartamento y edificio
        /// </summary>
        /// <returns>JSON con el historial de mantenimiento</returns>
        /// <response code="200">Devuelve la lista del historial encontrado</response>
        /// <response code="404">Si no se encuentra historial</response>
        [HttpGet]
        [SwaggerOperation("GetHistorialMantenimientoCompleto")]
        [Route("api/GetHistorialMantenimientoCompleto")]
        public IHttpActionResult GetHistorialMantenimientoCompleto()
        {
            var query = from historial in db.Historial
                        join empleado in db.EmpleadoMantenimiento
                        on historial.IdEmpleado equals empleado.id
                        join solicitud in db.SolicitudMantenimiento
                        on historial.IdSol equals solicitud.id
                        join apartamento in db.Apartamento
                        on solicitud.IdApartamento equals apartamento.id 
                        join edificio in db.Edificio
                        on apartamento.IdEdificio equals edificio.id 
                        orderby historial.id descending
                        select new
                        {
                            idHistorial = historial.id,
                            empleadoNombre = empleado.nombres + " " + empleado.apellidos,
                            solicitudDescripcion = solicitud.Descripcion,
                            solicitudFecha = solicitud.FechaRealizacion,
                            solicitudEstado = solicitud.Estado,
                            apartamentoDireccion = apartamento.Direccion,
                            edificioNombre = edificio.Nombre
                        };

            return Ok(query);
        }

        /// <summary>
        /// Retorna la lista de historial de mantenimiento
        /// </summary>
        /// <returns>Una lista de historial de mantenimiento.</returns>
        public IHttpActionResult Get()
        {
            var historial = db.Historial
                .Include(a => a.SolicitudMantenimiento)
                .Include(a => a.EmpleadoMantenimiento)
                .ToList();

            return Ok(historial);
        }

        /// <summary>
        /// Retorna un historial de mantenimiento por id
        /// </summary>
        /// <param name="id">ID del historial de mantenimiento a retornar.</param>
        /// <returns>Un historial de mantenimiento específico.</returns>
        /// <response code="200">Si el historial de mantenimiento es encontrado.</response>
        /// <response code="404">Si el historial de mantenimiento no es encontrado.</response>
        public IHttpActionResult Get(int id)
        {
            Historial_Mantenimiento historial = db.Historial.Find(id);
            if (historial == null)
            {
                return NotFound();
            }
            return Ok(historial);
        }

        /// <summary>
        /// Crea un nuevo historial de mantenimiento
        /// </summary>
        /// <param name="historial">El historial de mantenimiento a crear.</param>
        /// <returns>El historial de mantenimiento creado.</returns>
        public IHttpActionResult Post(Historial_Mantenimiento historial)
        {
            if (historial == null)
            {
                return BadRequest("El historial no puede ser nulo.");
            }

            Empleado_Mantenimiento empleadoExistente = db.EmpleadoMantenimiento.Find(historial.IdEmpleado);
            Solicitud_Mantenimiento solicitudExistente = db.SolicitudMantenimiento.Find(historial.IdSol);

            if (empleadoExistente == null || solicitudExistente == null)
            {
                return BadRequest("Empleado o Solicitud no encontrado.");
            }

            historial.EmpleadoMantenimiento = empleadoExistente;
            historial.SolicitudMantenimiento = solicitudExistente;

            db.Historial.Add(historial);
            db.SaveChanges();

            return Ok(historial);
        }

        /// <summary>
        /// Actualiza un historial de mantenimiento existente
        /// </summary>
        /// <param name="historialModificado">El historial de mantenimiento con los datos actualizados.</param>
        /// <param name="id">ID del historial de mantenimiento a eliminar.</param>
        /// <returns>El historial de mantenimiento actualizado.</returns>
        /// <response code="200">Si el historial de mantenimiento es actualizado correctamente.</response>
        /// <response code="404">Si el historial de mantenimiento no es encontrado.</response>
        public IHttpActionResult Put(int id, Historial_Mantenimiento historialModificado)
        {
            if (historialModificado == null)
            {
                return NotFound();
            }

            Historial_Mantenimiento historialExistente = db.Historial.Find(id);

            if (historialExistente == null)
            {
                return NotFound();
            }

            Empleado_Mantenimiento empleadoExistente = db.EmpleadoMantenimiento.Find(historialModificado.IdEmpleado);
            Solicitud_Mantenimiento solicitudExistente = db.SolicitudMantenimiento.Find(historialModificado.IdSol);

            if (empleadoExistente == null || solicitudExistente == null)
            {
                return BadRequest("Empleado o Solicitud no encontrado.");
            }

            historialExistente.IdEmpleado = historialModificado.IdEmpleado;
            historialExistente.IdSol = historialModificado.IdSol;

            db.Entry(historialExistente).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(historialExistente);
        }


        /// <summary>
        /// Elimina un historial de mantenimiento por id
        /// </summary>
        /// <param name="id">ID del historial de mantenimiento a eliminar.</param>
        /// <returns>El historial de mantenimiento eliminado.</returns>
        /// <response code="200">Si el historial de mantenimiento es eliminado correctamente.</response>
        /// <response code="404">Si el historial de mantenimiento no es encontrado.</response>
        public IHttpActionResult Delete(int id)
        {
            Historial_Mantenimiento historial = db.Historial.Find(id);
            if (historial == null)
            {
                return NotFound();
            }

            db.Historial.Remove(historial);
            db.SaveChanges();
            return Ok(historial);
        }
    }
}

