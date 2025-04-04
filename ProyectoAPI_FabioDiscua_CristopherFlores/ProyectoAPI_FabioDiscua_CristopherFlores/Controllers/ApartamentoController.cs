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
    public class ApartamentoController : ApiController
    {
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Obtener la lista de Apartamentos con contratos activos ordenados por antiguedad
        /// </summary>
        /// <returns>JSON Apartamentos</returns>
        /// <response code="200">Devuelve el apartamento encontrado</response>
        /// <response code="404">Si el apartamento no es encontrado</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetApartamentosContratos")]
        [Route("api/GetApartamentosContratos")]
        public IHttpActionResult GetApartamentosContratos()
        {
            var query = from apartamento in db.Apartamento
                        join contrato in db.Contrato
                        on apartamento.id equals contrato.apartamento.id
                        join arrendatario in db.Arrendatario
                        on contrato.arrendatario.id equals arrendatario.id
                        join edificio in db.Edificio
                        on apartamento.IdEdificio equals edificio.id
                        where contrato.estado == true
                        orderby contrato.fechaInicio descending
                        select new
                        {
                            idApartamento = apartamento.id,
                            direccion = apartamento.Direccion,
                            precioAlquiler = apartamento.PrecioAlquiler,
                            arrendatario = arrendatario.nombres + " " + arrendatario.apellidos,
                            edificio = edificio.Nombre,
                            fechaInicioContrato = contrato.fechaInicio,
                            fechaFinContrato = contrato.fechaFin
                        };

            return Ok(query);
        }

        /// <summary>
        /// Retorna la lista de apartamentos
        /// </summary>
        /// <returns>Una lista de apartamentos.</returns>
        public IHttpActionResult Get()
        {
            var apartamentos = db.Apartamento
                .Include(a => a.Arrendador)
                .Include(a => a.Edificio)
                .ToList();

            return Ok(apartamentos);
        }

        /// <summary>
        /// Retorna un apartamento por id
        /// </summary>
        /// <param name="id">ID del apartamento a retornar.</param>
        /// <returns>Un apartamento específico.</returns>
        /// <response code="200">Si el apartamento es encontrado.</response>
        /// <response code="404">Si el apartamento no es encontrado.</response>
        public IHttpActionResult Get(int id)
        {
            Apartamento apartamento = db.Apartamento.Find(id);
            if (apartamento == null)
            {
                return NotFound();
            }
            return Ok(apartamento);
        }

        /// <summary>
        /// Crea un nuevo apartamento
        /// </summary>
        /// <param name="apartamento">El apartamento a crear.</param>
        /// <returns>El apartamento creado.</returns>
        public IHttpActionResult Post(Apartamento apartamento)
        {
            Arrendador arrendador = db.Arrendador.Find(apartamento.IdArrendador);
            if (arrendador == null)
            {
                return NotFound();
            }

            Edificio edificio = db.Edificio.Include("Apartamentos").FirstOrDefault(e => e.id == apartamento.IdEdificio);
            if (edificio == null)
            {
                return NotFound(); ;
            }

            if (edificio.Apartamentos.Count >= edificio.Capacidad)
            {
                return BadRequest("No se pueden añadir más apartamentos, se ha alcanzado la capacidad máxima del edificio.");
            }

            apartamento.Arrendador = arrendador;
            apartamento.Edificio = edificio;

            db.Apartamento.Add(apartamento);
            db.SaveChanges();
            return Ok(apartamento);
        }

        /// <summary>
        /// Actualiza un apartamento existente
        /// </summary>
        /// <param name="apartamentoModificado">El apartamento con los datos actualizados.</param>
        /// <param name="id">ID del apartamento a editar.</param>
        /// <returns>El apartamento actualizado.</returns>
        /// <response code="200">Si el apartamento es actualizado correctamente.</response>
        /// <response code="404">Si el apartamento no es encontrado.</response>
        public IHttpActionResult Put(int id, Apartamento apartamentoModificado)
        {
            Apartamento apartamentoExistente = db.Apartamento.Find(id);
            if (apartamentoExistente == null)
            {
                return NotFound();
            }

            var arrendadorExistente = db.Arrendador.Find(apartamentoModificado.IdArrendador);
            var edificioExistente = db.Edificio.Find(apartamentoModificado.IdEdificio);

            if (arrendadorExistente == null || edificioExistente == null)
            {
                return NotFound();
            }

            apartamentoExistente.Direccion = apartamentoModificado.Direccion;
            apartamentoExistente.NumHabitaciones = apartamentoModificado.NumHabitaciones;
            apartamentoExistente.PrecioAlquiler = apartamentoModificado.PrecioAlquiler;
            apartamentoExistente.Disponible = apartamentoModificado.Disponible;
            apartamentoExistente.Arrendador = arrendadorExistente;
            apartamentoExistente.Edificio = edificioExistente;

            db.Entry(apartamentoExistente).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(apartamentoExistente);
        }

        /// <summary>
        /// Elimina un apartamento por id
        /// </summary>
        /// <param name="id">ID del apartamento a eliminar.</param>
        /// <returns>El apartamento eliminado.</returns>
        /// <response code="200">Si el apartamento es eliminado correctamente.</response>
        /// <response code="404">Si el apartamento no es encontrado.</response>
        public IHttpActionResult Delete(int id)
        {
            Apartamento apartamento = db.Apartamento.Find(id);
            if (apartamento == null)
            {
                return NotFound();
            }

            db.Apartamento.Remove(apartamento);
            db.SaveChanges();
            return Ok(apartamento);
        }
    }
}
