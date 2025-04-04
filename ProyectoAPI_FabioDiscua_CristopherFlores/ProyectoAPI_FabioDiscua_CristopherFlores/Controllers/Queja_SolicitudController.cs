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
    public class Queja_SolicitudController : ApiController
    {
        // Instancia del DbContext
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Obtener la lista de quejas y solicitudes con detalles del apartamento y arrendatario
        /// </summary>
        /// <returns>JSON con quejas y solicitudes</returns>
        /// <response code="200">Devuelve la lista de quejas y solicitudes encontradas</response>
        /// <response code="404">Si no se encuentran registros</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetQuejasSolicitudes")]
        [Route("api/GetQuejasSolicitudes")]
        public IHttpActionResult GetQuejasSolicitudes()
        {
            var query = from queja in db.QuejaSolicitud
                        join apartamento in db.Apartamento
                        on queja.IdApartamento equals apartamento.id
                        join contrato in db.Contrato
                        on apartamento.id equals contrato.apartamento.id
                        join arrendatario in db.Arrendatario
                        on contrato.arrendatario.id equals arrendatario.id
                        join edificio in db.Edificio
                        on apartamento.Edificio.id equals edificio.id
                        orderby queja.Estado ascending, queja.id descending
                        select new
                        {
                            idQueja = queja.id,
                            descripcion = queja.Descripcion,
                            estado = queja.Estado ? "Resuelta" : "Pendiente",
                            apartamento = apartamento.id,
                            edificio = edificio.Nombre,
                            arrendatario = arrendatario.nombres + " " + arrendatario.apellidos
                        };

            return Ok(query);
        }

        /// <summary>
        /// Obtener la lista de quejas pendientes
        /// </summary>
        /// <returns>JSON con quejas pendientes</returns>
        /// <response code="200">Devuelve la lista de quejas pendientes</response>
        /// <response code="404">Si no se encuentran registros</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetQuejasPendientes")]
        [Route("api/GetQuejasPendientes")]
        public IHttpActionResult GetQuejasPendientes()
        {
            var query = from queja in db.QuejaSolicitud
                        join apartamento in db.Apartamento
                        on queja.IdApartamento equals apartamento.id
                        join contrato in db.Contrato
                        on apartamento.id equals contrato.apartamento.id
                        join arrendatario in db.Arrendatario
                        on contrato.arrendatario.id equals arrendatario.id
                        join edificio in db.Edificio
                        on apartamento.Edificio.id equals edificio.id
                        where queja.Estado == false
                        orderby queja.id descending
                        select new
                        {
                            idQueja = queja.id,
                            descripcion = queja.Descripcion,
                            apartamento = apartamento.id,
                            edificio = edificio.Nombre,
                            arrendatario = arrendatario.nombres + " " + arrendatario.apellidos
                        };

            return Ok(query);
        }

        /// <summary>
        /// Retorna la lista de quejas y solicitudes
        /// </summary>
        /// <returns>Una lista de quejas y solicitudes.</returns>
        public IHttpActionResult Get()
        {
            var QS = db.QuejaSolicitud
                .Include(a => a.Apartamento)
                .ToList();

            return Ok(QS);
        }

        /// <summary>
        /// Retorna una queja o solicitud por id
        /// </summary>
        /// <param name="id">ID de la queja o solicitud a retornar.</param>
        /// <returns>Una queja o solicitud específica.</returns>
        /// <response code="200">Si la queja o solicitud es encontrada.</response>
        /// <response code="404">Si la queja o solicitud no es encontrada.</response>
        public IHttpActionResult Get(int id)
        {
            Queja_Solicitud quejaSolicitud = db.QuejaSolicitud.Find(id);
            if (quejaSolicitud == null)
            {
                return NotFound();
            }
            return Ok(quejaSolicitud);
        }

        /// <summary>
        /// Crea una nueva queja o solicitud
        /// </summary>
        /// <param name="quejaSolicitud">La queja o solicitud a crear.</param>
        /// <returns>La queja o solicitud creada.</returns>
        public IHttpActionResult Post(Queja_Solicitud queja)
        {
            if (queja == null)
            {
                return BadRequest("La queja no puede ser nula.");
            }

            Apartamento apartamentoExistente = db.Apartamento.Find(queja.IdApartamento);

            if (apartamentoExistente == null)
            {
                return BadRequest("Apartamento no encontrado.");
            }

            queja.Apartamento = apartamentoExistente;

            db.QuejaSolicitud.Add(queja);
            db.SaveChanges();

            return Ok(queja);
        }

        /// <summary>
        /// Actualiza una queja o solicitud existente
        /// </summary>
        /// <param name="quejaModificada">La queja o solicitud con los datos actualizados.</param>
        /// <param name="id">ID de la queja o solicitud a editar.</param>
        /// <returns>La queja o solicitud actualizada.</returns>
        /// <response code="200">Si la queja o solicitud es actualizada correctamente.</response>
        /// <response code="404">Si la queja o solicitud no es encontrada.</response>
        public IHttpActionResult Put(int id, Queja_Solicitud quejaModificada)
        {
            if (quejaModificada == null)
            {
                return BadRequest("La queja o solicitud modificada no puede ser nula.");
            }

            Queja_Solicitud quejaExistente = db.QuejaSolicitud.Find(id);

            if (quejaExistente == null)
            {
                return NotFound();
            }

            Apartamento apartamentoExistente = db.Apartamento.Find(quejaModificada.IdApartamento);

            if (apartamentoExistente == null)
            {
                return BadRequest("Apartamento no encontrado.");
            }

            quejaExistente.IdApartamento = quejaModificada.IdApartamento;
            quejaExistente.Apartamento = apartamentoExistente;
            quejaExistente.Descripcion = quejaModificada.Descripcion;
            quejaExistente.Estado = quejaModificada.Estado;

            db.Entry(quejaExistente).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(quejaExistente);
        }


        /// <summary>
        /// Elimina una queja o solicitud por id
        /// </summary>
        /// <param name="id">ID de la queja o solicitud a eliminar.</param>
        /// <returns>La queja o solicitud eliminada.</returns>
        /// <response code="200">Si la queja o solicitud es eliminada correctamente.</response>
        /// <response code="404">Si la queja o solicitud no es encontrada.</response>
        public IHttpActionResult Delete(int id)
        {
            Queja_Solicitud quejaSolicitud = db.QuejaSolicitud.Find(id);
            if (quejaSolicitud == null)
            {
                return NotFound();
            }

            db.QuejaSolicitud.Remove(quejaSolicitud);
            db.SaveChanges();
            return Ok(quejaSolicitud);
        }
    }
}
