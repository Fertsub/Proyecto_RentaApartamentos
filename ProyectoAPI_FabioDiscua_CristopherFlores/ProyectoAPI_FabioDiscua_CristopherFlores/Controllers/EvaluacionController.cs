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
    public class EvaluacionesController : ApiController
    {
        // Instancia del DbContext
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Obtener la lista de evaluaciones con detalles del apartamento y edificio
        /// </summary>
        /// <returns>JSON con las evaluaciones y detalles de ubicación</returns>
        /// <response code="200">Devuelve las evaluaciones encontradas</response>
        /// <response code="404">Si no se encuentran evaluaciones</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetEvaluacionesUbicacion")]
        [Route("api/GetEvaluacionesUbicacion")]
        public IHttpActionResult GetEvaluacionesUbicacion()
        {
            var query = from evaluacion in db.Evaluacion
                        join apartamento in db.Apartamento
                        on evaluacion.IdApartamento equals apartamento.id
                        join edificio in db.Edificio
                        on apartamento.IdEdificio equals edificio.id
                        orderby evaluacion.fecha descending
                        select new
                        {
                            idEvaluacion = evaluacion.id,
                            evaluacion.calificacionApartamento,
                            evaluacion.calificacionArrendador,
                            evaluacion.opinion,
                            evaluacion.fecha,
                            apartamentoId = apartamento.id,
                            edificioNombre = edificio.Nombre,
                            edificioDireccion = edificio.Direccion
                        };

            return Ok(query);
        }

        /// <summary>
        /// Obtener la lista de evaluaciones con detalles del arrendador
        /// </summary>
        /// <returns>JSON con las evaluaciones y los arrendadores relacionados</returns>
        /// <response code="200">Devuelve las evaluaciones encontradas</response>
        /// <response code="404">Si no se encuentran evaluaciones</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetEvaluacionesArrendador")]
        [Route("api/GetEvaluacionesArrendador")]
        public IHttpActionResult GetEvaluacionesConArrendador()
        {
            var query = from evaluacion in db.Evaluacion
                        join apartamento in db.Apartamento
                        on evaluacion.IdApartamento equals apartamento.id
                        join arrendador in db.Arrendador
                        on apartamento.IdArrendador equals arrendador.id
                        orderby arrendador.nombres ascending, evaluacion.fecha descending
                        select new
                        {
                            idEvaluacion = evaluacion.id,
                            evaluacion.calificacionApartamento,
                            evaluacion.calificacionArrendador,
                            evaluacion.opinion,
                            evaluacion.fecha,
                            apartamentoId = apartamento.id,
                            arrendadorNombreCompleto = arrendador.nombres + " " + arrendador.apellidos,
                            arrendador.telefono,
                            arrendador.email
                        };

            return Ok(query);
        }

        /// <summary>
        /// Retorna la lista de evaluaciones
        /// </summary>
        /// <returns>Una lista de evaluaciones.</returns>
        public IHttpActionResult Get()
        {
            var evaluacion = db.Evaluacion
                .Include(a => a.Apartamento)
                .Include(a => a.arrendador)
                .ToList();

            return Ok(evaluacion);
        }

        /// Retorna una evaluación por id
        /// </summary>
        /// <param name="id">ID de la evaluación a retornar.</param>
        /// <returns>Una evaluación específica.</returns>
        /// <response code="200">Si la evaluación es encontrada.</response>
        /// <response code="404">Si la evaluación no es encontrada.</response>
        public IHttpActionResult Get(int id)
        {
            Evaluacion evaluacion = db.Evaluacion.Find(id);
            if (evaluacion == null)
            {
                return NotFound();
            }
            return Ok(evaluacion);
        }

        /// <summary>
        /// Crea una nueva evaluación
        /// </summary>
        /// <param name="evaluacion">La evaluación a crear.</param>
        /// <returns>La evaluación creada.</returns>
        /// <response code="200">Si la evaluación es creada correctamente.</response>
        public IHttpActionResult Post(Evaluacion evaluacion)
        {
            if (evaluacion == null)
            {
                return BadRequest("La evaluación no puede ser nula.");
            }

            Apartamento apartamentoExistente = db.Apartamento.Find(evaluacion.IdApartamento);
            Arrendador arrendadorExistente = db.Arrendador.Find(evaluacion.IdArrendador);

            if (apartamentoExistente == null || arrendadorExistente == null)
            {
                return BadRequest("Apartamento o Arrendador no encontrado.");
            }

            evaluacion.Apartamento = apartamentoExistente;
            evaluacion.arrendador = arrendadorExistente;

            db.Evaluacion.Add(evaluacion);
            db.SaveChanges();

            return Ok(evaluacion);
        }

        /// <summary>
        /// Actualiza una evaluación existente
        /// </summary>
        /// <param name="evaluacionModificada">La evaluación con los datos actualizados.</param>
        /// <param name="id">ID de la evaluación a editar.</param>
        /// <returns>La evaluación actualizada.</returns>
        /// <response code="200">Si la evaluación es actualizada correctamente.</response>
        /// <response code="404">Si la evaluación no es encontrada.</response>
        public IHttpActionResult Put(int id, Evaluacion evaluacionModificada)
        {
            if (evaluacionModificada == null)
            {
                return NotFound();
            }

            Evaluacion evaluacionExistente = db.Evaluacion.Find(id);

            if (evaluacionExistente == null)
            {
                return NotFound();
            }

            Apartamento apartamentoExistente = db.Apartamento.Find(evaluacionModificada.IdApartamento);
            Arrendador arrendadorExistente = db.Arrendador.Find(evaluacionModificada.IdArrendador);

            if (apartamentoExistente == null || arrendadorExistente == null)
            {
                return BadRequest("Apartamento o Arrendador no encontrado.");
            }

            evaluacionExistente.calificacionApartamento = evaluacionModificada.calificacionApartamento;
            evaluacionExistente.calificacionArrendador = evaluacionModificada.calificacionArrendador;
            evaluacionExistente.opinion = evaluacionModificada.opinion;
            evaluacionExistente.fecha = evaluacionModificada.fecha;
            evaluacionExistente.Apartamento = apartamentoExistente;
            evaluacionExistente.arrendador = arrendadorExistente;

            db.Entry(evaluacionExistente).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(evaluacionExistente);
        }


        /// <summary>
        /// Elimina una evaluación por id
        /// </summary>
        /// <param name="id">ID de la evaluación a eliminar.</param>
        /// <returns>La evaluación eliminada.</returns>
        /// <response code="200">Si la evaluación es eliminada correctamente.</response>
        /// <response code="404">Si la evaluación no es encontrada.</response>
        public IHttpActionResult Delete(int id)
        {
            Evaluacion evaluacion = db.Evaluacion.Find(id);
            if (evaluacion == null)
            {
                return NotFound();
            }

            db.Evaluacion.Remove(evaluacion);
            db.SaveChanges();
            return Ok(evaluacion);
        }
    }
}
