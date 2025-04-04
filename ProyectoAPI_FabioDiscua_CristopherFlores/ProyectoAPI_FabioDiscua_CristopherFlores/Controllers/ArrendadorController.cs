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
    public class ArrendadorController : ApiController
    {
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Obtener la lista de Arrendadores ordenados según el contrato mas antiguo
        /// </summary>
        /// <returns>JSON Arrendadores</returns>
        /// <response code="200">Devuelve el arrendador encontrado</response>
        /// <response code="404">Si el arrendador no es encontrado</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetContratoReciente")]
        [Route("api/GetContratoReciente")]
        public IHttpActionResult GetContratoReciente()
        {
            var query = from arrendador in db.Arrendador
                        join contrato in db.Contrato
                        on arrendador.id equals contrato.arrendador.id
                        join apartamento in db.Apartamento
                        on contrato.apartamento.id equals apartamento.id
                        join arrendatario in db.Arrendatario
                        on contrato.arrendatario.id equals arrendatario.id
                        orderby contrato.fechaInicio ascending 
                        select new
                        {
                            idArrendador = arrendador.id,
                            nombreArrendador = arrendador.nombres + " " + arrendador.apellidos,
                            arrendador.telefono,
                            numeroApartamento = apartamento.NumHabitaciones,
                            nombreArrendatario = arrendatario.nombres + " " + arrendatario.apellidos,
                            fechaInicioContrato = contrato.fechaInicio,
                            fechaFinContrato = contrato.fechaFin
                        };

            return Ok(query);
        }

        /// <summary>
        /// Obtener la lista de Arrendadores ordenados según los pagos mas recientes
        /// </summary>
        /// <returns>JSON Arrendadores</returns>
        /// <response code="200">Devuelve el arrendador encontrado</response>
        /// <response code="404">Si el arrendador no es encontrado</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetPagosRecientes")]
        [Route("api/GetPagosRecientes")]
        public IHttpActionResult GetPagosRecientes()
        {
            var query = from arrendador in db.Arrendador
                        join apartamento in db.Apartamento
                        on arrendador.id equals apartamento.Arrendador.id
                        join contrato in db.Contrato
                        on apartamento.id equals contrato.apartamento.id
                        join pago in db.Pago
                        on contrato.id equals pago.contrato.id
                        join arrendatario in db.Arrendatario
                        on contrato.arrendatario.id equals arrendatario.id
                        orderby pago.fechaPago descending
                        select new
                        {
                            idArrendador = arrendador.id,
                            nombreArrendador = arrendador.nombres + " " + arrendador.apellidos,
                            apartamento = apartamento.NumHabitaciones,
                            pago.fechaPago,
                            monto = pago.Monto,
                            pagadoPor = arrendatario.nombres + " " + arrendatario.apellidos
                        };

            return Ok(query);
        }

        /// <summary>
        /// Retorna la lista de arrendadores
        /// </summary>
        /// <returns>Una lista de arrendadores.</returns>
        public IEnumerable<Arrendador> Get()
        {
            return db.Arrendador;
        }

        /// <summary>
        /// Obtener un arrendador por ID
        /// </summary>
        /// <param name="id">ID del arrendador</param>
        /// <returns>JSON con el arrendador encontrado</returns>
        /// <response code="200">Devuelve el arrendador encontrado</response>
        /// <response code="404">Si el arrendador no es encontrado</response>
        public IHttpActionResult Get(int id)
        {
            Arrendador arrendador = db.Arrendador.Find(id);
            if (arrendador == null)
            {
                return NotFound();
            }

            return Ok(arrendador);
        }

        /// <summary>
        /// Insertar un nuevo arrendador
        /// </summary>
        /// <param name="arrendador">Objeto arrendador</param>
        /// <returns>JSON con el arrendador registrado</returns>
        /// /// <response code="200">Devuelve el arrendador encontrado</response>
        /// <response code="404">Si el arrendador no es encontrado</response>
        public IHttpActionResult Post(Arrendador arrendador)
        {
            db.Arrendador.Add(arrendador);
            db.SaveChanges();
            return Ok(arrendador);
        }

        /// <summary>
        /// Actualizar un arrendador
        /// </summary>
        /// <param name="arrendadorModificado">Objeto arrendador modificado</param>
        /// <param name="id">Id para buscar el arrendador</param>
        /// <returns>JSON con el arrendador actualizado</returns>
        /// /// <response code="200">Devuelve el arrendador encontrado</response>
        /// <response code="404">Si el arrendador no es encontrado</response>
        public IHttpActionResult Put(int id, Arrendador arrendadorModificado)
        {
            db.Entry(arrendadorModificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(arrendadorModificado);
        }

        /// <summary>
        /// Eliminar un arrendador por ID
        /// </summary>
        /// <param name="id">ID del arrendador</param>
        /// <returns>JSON con el arrendador eliminado</returns>
        /// <response code="200">Devuelve el arrendador encontrado</response>
        /// <response code="404">Si el arrendador no es encontrado</response>
        public IHttpActionResult Delete(int id)
        {
            Arrendador arrendador = db.Arrendador.Find(id);
            if (arrendador == null)
            {
                return NotFound();
            }

            db.Arrendador.Remove(arrendador);
            db.SaveChanges();
            return Ok(arrendador);
        }
    }
}
