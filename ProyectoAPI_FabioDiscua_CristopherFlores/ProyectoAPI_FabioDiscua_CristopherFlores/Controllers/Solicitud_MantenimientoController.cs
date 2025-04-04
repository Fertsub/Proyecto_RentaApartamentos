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
    public class Solicitud_MantenimientoController : ApiController
    {
        // Instancia del DbContext
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Retorna la lista de solicitudes de mantenimiento
        /// </summary>
        /// <returns>Una lista de solicitudes de mantenimiento.</returns>
        public IHttpActionResult Get()
        {
            var solM = db.SolicitudMantenimiento
                .Include(a => a.Apartamento)
                .Include(a => a.Arrendatario)
                .Include(a => a.Mantenimiento)
                .ToList();

            return Ok(solM);
        }

        /// <summary>
        /// Retorna una solicitud de mantenimiento por id
        /// </summary>
        /// <param name="id">ID de la solicitud de mantenimiento a retornar.</param>
        /// <returns>Una solicitud de mantenimiento específica.</returns>
        /// <response code="200">Si la solicitud de mantenimiento es encontrada.</response>
        /// <response code="404">Si la solicitud de mantenimiento no es encontrada.</response>
        public IHttpActionResult Get(int id)
        {
            Solicitud_Mantenimiento solicitud = db.SolicitudMantenimiento.Find(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            return Ok(solicitud);
        }

        /// <summary>
        /// Crea una nueva solicitud de mantenimiento
        /// </summary>
        /// <param name="solicitud">La solicitud de mantenimiento a crear.</param>
        /// <returns>La solicitud de mantenimiento creada.</returns>
        public IHttpActionResult Post(Solicitud_Mantenimiento solicitud)
        {
            if (solicitud == null)
            {
                return BadRequest("La solicitud no puede ser nula.");
            }

            Apartamento apartamentoExistente = db.Apartamento.Find(solicitud.IdApartamento);
            Arrendatario arrendatarioExistente = db.Arrendatario.Find(solicitud.IdArrendatario);
            Empleado_Mantenimiento empleadoExistente = db.EmpleadoMantenimiento.Find(solicitud.IdEmpleado);

            if (apartamentoExistente == null || arrendatarioExistente == null || empleadoExistente == null)
            {
                return BadRequest("Apartamento, Arrendatario o Empleado no encontrado.");
            }

            solicitud.Apartamento = apartamentoExistente;
            solicitud.Arrendatario = arrendatarioExistente;
            solicitud.Mantenimiento = empleadoExistente;

            db.SolicitudMantenimiento.Add(solicitud);
            db.SaveChanges();

            return Ok(solicitud);
        }

        /// <summary>
        /// Actualiza una solicitud de mantenimiento existente
        /// </summary>
        /// <param name="solicitudModificada">La solicitud de mantenimiento con los datos actualizados.</param>
        /// <param name="id">ID de la solicitud de mantenimiento a editar.</param>
        /// <returns>La solicitud de mantenimiento actualizada.</returns>
        /// <response code="200">Si la solicitud de mantenimiento es actualizada correctamente.</response>
        /// <response code="404">Si la solicitud de mantenimiento no es encontrada.</response>
        public IHttpActionResult Put(int id, Solicitud_Mantenimiento solicitudModificada)
        {
            if (solicitudModificada == null)
            {
                return BadRequest("La solicitud de mantenimiento no puede ser nula.");
            }

            var solicitudExistente = db.SolicitudMantenimiento.Find(id);
            if (solicitudExistente == null)
            {
                return NotFound();
            }

            var apartamentoExistente = db.Apartamento.Find(solicitudModificada.IdApartamento);
            var arrendatarioExistente = db.Arrendatario.Find(solicitudModificada.IdArrendatario);
            var empleadoExistente = db.EmpleadoMantenimiento.Find(solicitudModificada.IdEmpleado);

            if (apartamentoExistente == null || arrendatarioExistente == null || empleadoExistente == null)
            {
                return BadRequest("Apartamento, Arrendatario o Empleado no encontrado.");
            }

            solicitudExistente.IdApartamento = solicitudModificada.IdApartamento;
            solicitudExistente.IdArrendatario = solicitudModificada.IdArrendatario;
            solicitudExistente.IdEmpleado = solicitudModificada.IdEmpleado;
            solicitudExistente.Apartamento = apartamentoExistente;
            solicitudExistente.Arrendatario = arrendatarioExistente;
            solicitudExistente.Mantenimiento = empleadoExistente;
            solicitudExistente.Descripcion = solicitudModificada.Descripcion;
            solicitudExistente.Costo = solicitudModificada.Costo;
            solicitudExistente.FechaRealizacion = solicitudModificada.FechaRealizacion;
            solicitudExistente.Estado = solicitudModificada.Estado;

            db.Entry(solicitudExistente).State = EntityState.Modified;
            db.SaveChanges();

            db.Entry(solicitudExistente).Reference(s => s.Apartamento).Load();
            db.Entry(solicitudExistente).Reference(s => s.Arrendatario).Load();
            db.Entry(solicitudExistente).Reference(s => s.Mantenimiento).Load();

            return Ok(solicitudExistente);
        }


        /// <summary>
        /// Elimina una solicitud de mantenimiento por id
        /// </summary>
        /// <param name="id">ID de la solicitud de mantenimiento a eliminar.</param>
        /// <returns>La solicitud de mantenimiento eliminada.</returns>
        /// <response code="200">Si la solicitud de mantenimiento es eliminada correctamente.</response>
        /// <response code="404">Si la solicitud de mantenimiento no es encontrada.</response>
        public IHttpActionResult Delete(int id)
        {
            Solicitud_Mantenimiento solicitud = db.SolicitudMantenimiento.Find(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            db.SolicitudMantenimiento.Remove(solicitud);
            db.SaveChanges();
            return Ok(solicitud);
        }
    }
}
