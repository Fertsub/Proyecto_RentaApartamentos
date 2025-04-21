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
            Empleado_Mantenimiento empleadoExistente = db.EmpleadoMantenimiento.Find(solicitud.IdEmpleado);

            if (apartamentoExistente == null || empleadoExistente == null)
            {
                return BadRequest("Apartamento o Empleado no encontrado.");
            }

            solicitud.Apartamento = apartamentoExistente;
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
            Solicitud_Mantenimiento solicitudExistente = db.SolicitudMantenimiento
                .FirstOrDefault(s => s.id == id);
            if (solicitudExistente == null)
                return NotFound();

            bool Pendiente = solicitudExistente.Estado == false;
            bool Realizado = solicitudModificada.Estado == true;

            solicitudExistente.Descripcion = solicitudModificada.Descripcion;
            solicitudExistente.Costo = solicitudModificada.Costo;
            solicitudExistente.FechaRealizacion = solicitudModificada.FechaRealizacion;
            solicitudExistente.Estado = solicitudModificada.Estado;
            solicitudExistente.IdEmpleado = solicitudModificada.IdEmpleado;
            solicitudExistente.Mantenimiento = db.EmpleadoMantenimiento.Find(solicitudModificada.IdEmpleado);

            db.Entry(solicitudExistente).State = EntityState.Modified;
            db.SaveChanges();

            if (Pendiente && Realizado)
            {
                var historico = new Historial_Mantenimiento
                {
                    IdSol = solicitudExistente.id,
                    IdEmpleado = solicitudExistente.IdEmpleado,
                    FechaRealizacion = DateTime.Now
                };
                db.Historial.Add(historico);
                db.SaveChanges();
            }

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
