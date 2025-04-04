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
    public class EdificioController : ApiController
    {
        
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Obtener la lista de edificios con la cantidad de apartamentos y contratos activos
        /// </summary>
        /// <returns>JSON con los edificios y sus detalles</returns>
        /// <response code="200">Devuelve los edificios encontrados</response>
        /// <response code="404">Si no se encuentran edificios</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetEdificiosContratos")]
        [Route("api/GetEdificiosContratos")]
        public IHttpActionResult GetEdificiosContratos()
        {
            var query = from edificio in db.Edificio
                        join apartamento in db.Apartamento
                        on edificio.id equals apartamento.Edificio.id
                        join contrato in db.Contrato
                        on apartamento.id equals contrato.apartamento.id
                        where contrato.estado == true
                        group contrato by new
                        {
                            edificio.id,
                            edificio.Nombre,
                            edificio.Direccion,
                            edificio.Capacidad
                        } into g
                        orderby g.Count() descending
                        select new
                        {
                            idEdificio = g.Key.id,
                            nombre = g.Key.Nombre,
                            direccion = g.Key.Direccion,
                            numPisos = g.Key.Capacidad,
                            cantidadContratosActivos = g.Count()
                        };

            return Ok(query);
        }

        /// <summary>
        /// Obtener la lista de edificios con arrendadores y arrendatarios
        /// </summary>
        /// <returns>JSON con los edificios, arrendadores y arrendatarios</returns>
        /// <response code="200">Devuelve los edificios encontrados</response>
        /// <response code="404">Si no se encuentran edificios</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetEdificiosPersonas")]
        [Route("api/GetEdificiosConPersonas")]
        public IHttpActionResult GetEdificiosPersonas()
        {
            var query = from edificio in db.Edificio
                        join apartamento in db.Apartamento
                        on edificio.id equals apartamento.Edificio.id
                        join contrato in db.Contrato
                        on apartamento.id equals contrato.apartamento.id
                        join arrendador in db.Arrendador
                        on contrato.arrendador.id equals arrendador.id
                        join arrendatario in db.Arrendatario
                        on contrato.arrendatario.id equals arrendatario.id
                        orderby edificio.Nombre, arrendatario.nombres, arrendatario.apellidos
                        select new
                        {
                            edificioId = edificio.id,
                            nombre = edificio.Nombre,
                            direccion = edificio.Direccion,
                            arrendador = arrendador.nombres + " " + arrendador.apellidos,
                            arrendatario = arrendatario.nombres + " " + arrendatario.apellidos
                        };

            return Ok(query);
        }

        /// <summary>
        /// Retorna la lista de Edificios
        /// </summary>
        /// <returns>Una lista de Edificios.</returns>
        public IEnumerable<Edificio> Get()
        {
            return db.Edificio;
        }

        /// <summary>
        /// Retorna un edificio por id
        /// </summary>
        /// <param name="id">ID del edificio a retornar.</param>
        /// <returns>Un edificio específico.</returns>
        /// <response code="200">Si el edificio es encontrado.</response>
        /// <response code="404">Si el edificio no es encontrado.</response>
        public IHttpActionResult Get(int id)
        {
            Edificio edificio = db.Edificio.Find(id);
            if (edificio == null)
            {
                return NotFound();
            }
            return Ok(edificio);
        }

        /// <summary>
        /// Crea un nuevo edificio
        /// </summary>
        /// <param name="edificio">El edificio a crear.</param>
        /// <returns>El edificio creado.</returns>
        /// <response code="200">Si el edificio es creado correctamente.</response>
        public IHttpActionResult Post(Edificio edificio)
        {
            if (edificio == null)
            {
                return BadRequest("El edificio no puede ser nulo.");
            }

            db.Edificio.Add(edificio);
            db.SaveChanges();

            return Ok(edificio);
        }

        /// <summary>
        /// Actualiza un edificio existente
        /// </summary>
        /// <param name="edificioModificado">El edificio con los datos actualizados.</param>
        /// <param name="id">ID del edificio a editar.</param>
        /// <returns>El edificio actualizado.</returns>
        /// <response code="200">Si el edificio es actualizado correctamente.</response>
        /// <response code="404">Si el edificio no es encontrado.</response>
        public IHttpActionResult Put(int id, Edificio edificioModificado)
        {
            Edificio edificioExistente = db.Edificio.Find(id);

            if (edificioExistente == null)
            {
                return NotFound();
            }

            edificioExistente.Nombre = edificioModificado.Nombre;
            edificioExistente.Direccion = edificioModificado.Direccion;
            edificioExistente.Capacidad = edificioModificado.Capacidad;

            db.SaveChanges();
            return Ok(edificioExistente);
        }

        /// <summary>
        /// Elimina un edificio por id
        /// </summary>
        /// <param name="id">ID del edificio a eliminar.</param>
        /// <returns>El edificio eliminado.</returns>
        /// <response code="200">Si el edificio es eliminado correctamente.</response>
        /// <response code="404">Si el edificio no es encontrado.</response>
        public IHttpActionResult Delete(int id)
        {
            Edificio edificio = db.Edificio.Find(id);
            if (edificio == null)
            {
                return NotFound();
            }

            db.Edificio.Remove(edificio);
            db.SaveChanges();
            return Ok(edificio);
        }
    }
}

