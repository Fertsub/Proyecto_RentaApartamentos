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
    public class ContratoController : ApiController
    {
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Obtener la lista de Contratos Activos
        /// </summary>
        /// <returns>JSON Contratos</returns>
        /// <response code="200">Devuelve el contrato encontrado</response>
        /// <response code="404">Si el contrato no es encontrado</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetContratosActivos")]
        [Route("api/GetContratosActivos")]
        public IHttpActionResult GetContratosActivos()
        {
            var query = from contrato in db.Contrato
                        join apartamento in db.Apartamento
                        on contrato.apartamento.id equals apartamento.id
                        join arrendador in db.Arrendador
                        on contrato.arrendador.id equals arrendador.id
                        join arrendatario in db.Arrendatario
                        on contrato.arrendatario.id equals arrendatario.id
                        where contrato.estado == true
                        orderby contrato.fechaInicio descending
                        select new
                        {
                            contratoId = contrato.id,
                            contrato.fechaInicio,
                            contrato.fechaFin,
                            Alquiler = apartamento.PrecioAlquiler,
                            apartamento = apartamento.Direccion,
                            arrendador = arrendador.nombres + " " + arrendador.apellidos,
                            arrendatario = arrendatario.nombres + " " + arrendatario.apellidos
                        };

            return Ok(query);
        }

        /// <summary>
        /// Retorna la lista de contratos
        /// </summary>
        /// <returns>Una lista de contratos.</returns>
        public IHttpActionResult Get()
        {
            var contratos = db.Contrato
                .Include(a => a.apartamento)
                .Include(a => a.arrendador)
                .Include(a => a.arrendatario)
                .ToList();

            return Ok(contratos);
        }

        /// <summary>
        /// Retorna un contrato por id
        /// </summary>
        /// <param name="id">ID del contrato a retornar.</param>
        /// <returns>Un contrato específico.</returns>
        /// <response code="200">Si el contrato es encontrado.</response>
        /// <response code="404">Si el contrato no es encontrado.</response>
        public IHttpActionResult Get(int id)
        {
            Contrato contrato = db.Contrato.Find(id);
            if (contrato == null)
            {
                return NotFound();
            }

            return Ok(contrato);
        }

        /// <summary>
        /// Crea un nuevo contrato
        /// </summary>
        /// <param name="contrato">El contrato a crear.</param>
        /// <returns>El contrato creado.</returns>
        public IHttpActionResult Post(Contrato contrato)
        {
            if (contrato == null)
            {
                return BadRequest("El contrato no puede ser nulo.");
            }

            Apartamento apartamentoExistente = db.Apartamento.Find(contrato.IdApartamento);
            Arrendador arrendadorExistente = db.Arrendador.Find(contrato.IdArrendador);
            Arrendatario arrendatarioExistente = db.Arrendatario.Find(contrato.IdArrendatario);

            if (apartamentoExistente == null || arrendadorExistente == null || arrendatarioExistente == null)
            {
                return BadRequest("Apartamento, Arrendador o Arrendatario no encontrado.");
            }

            contrato.apartamento = apartamentoExistente;
            contrato.arrendador = arrendadorExistente;
            contrato.arrendatario = arrendatarioExistente;

            db.Contrato.Add(contrato);
            db.SaveChanges();

            return Ok(contrato);
        }

        /// <summary>
        /// Actualiza un contrato existente
        /// </summary>
        /// <param name="contratoModificado">El contrato con los datos actualizados.</param>
        /// <param name="id">ID del contrato a editar.</param>
        /// <returns>El contrato actualizado.</returns>
        /// <response code="200">Si el contrato es actualizado correctamente.</response>
        /// <response code="404">Si el contrato no es encontrado.</response>
        public IHttpActionResult Put(int id, Contrato contratoModificado)
        {
            Contrato contratoExistente = db.Contrato.Find(id);

            if (contratoExistente == null)
            {
                return NotFound();
            }

            Apartamento apartamentoExistente = db.Apartamento.Find(contratoModificado.IdApartamento);
            Arrendador arrendadorExistente = db.Arrendador.Find(contratoModificado.IdArrendador);
            Arrendatario arrendatarioExistente = db.Arrendatario.Find(contratoModificado.IdArrendatario);

            if (apartamentoExistente == null || arrendadorExistente == null || arrendatarioExistente == null)
            {
                return NotFound();
            }

            contratoExistente.fechaInicio = contratoModificado.fechaInicio;
            contratoExistente.fechaFin = contratoModificado.fechaFin;
            contratoExistente.estado = contratoModificado.estado;
            contratoExistente.IdApartamento = contratoModificado.IdApartamento;
            contratoExistente.IdArrendador = contratoModificado.IdArrendador;
            contratoExistente.IdArrendatario = contratoModificado.IdArrendatario;
            contratoExistente.apartamento = apartamentoExistente;
            contratoExistente.arrendador = arrendadorExistente;
            contratoExistente.arrendatario = arrendatarioExistente;
            contratoExistente.Alquiler = apartamentoExistente.PrecioAlquiler;

            db.Entry(contratoExistente).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(contratoExistente);
        }

        /// <summary>
        /// Elimina un contrato por id
        /// </summary>
        /// <param name="id">ID del contrato a eliminar.</param>
        /// <returns>El contrato eliminado.</returns>
        /// <response code="200">Si el contrato es eliminado correctamente.</response>
        /// <response code="404">Si el contrato no es encontrado.</response>
        public IHttpActionResult Delete(int id)
        {
            Contrato contrato = db.Contrato.Find(id);
            if (contrato == null)
            {
                return NotFound();
            }

            db.Contrato.Remove(contrato);
            db.SaveChanges();
            return Ok(contrato);
        }

    }
}