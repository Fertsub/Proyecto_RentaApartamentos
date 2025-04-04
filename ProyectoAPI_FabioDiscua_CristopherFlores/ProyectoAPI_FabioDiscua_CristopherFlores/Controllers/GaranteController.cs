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
    public class GaranteController : ApiController
    {
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Obtener la lista de Garantes ordenados según el contrato mas reciente
        /// </summary>
        /// <returns>JSON Garantes</returns>
        /// <response code="200">Devuelve el garante encontrado</response>
        /// <response code="404">Si el garante no es encontrado</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetGarantesDetallados")]
        [Route("api/GetGarantesDetallados")]
        public IHttpActionResult GetGarantesDetallados()
        {
            var query = from garante in db.Garante
                        join arrendatario in db.Arrendatario on garante.IdArrendatario equals arrendatario.id
                        join contrato in db.Contrato on arrendatario.id equals contrato.arrendatario.id
                        join apartamento in db.Apartamento on contrato.IdApartamento equals apartamento.id
                        orderby garante.emisionDoc descending
                        select new
                        {
                            ID_Garante = garante.id,
                            Nombre_Garante = garante.nombres + " " + garante.apellidos,
                            Documento_Aval = garante.docAval,
                            Fecha_Emision_Aval = garante.emisionDoc,
                            ID_Arrendatario = arrendatario.id,
                            Nombre_Arrendatario = arrendatario.nombres + " " + arrendatario.apellidos,
                            ID_Contrato = contrato.id,
                            Fecha_Inicio_Contrato = contrato.fechaInicio,
                            Fecha_Fin_Contrato = contrato.fechaFin,
                            ID_Apartamento = apartamento.id,
                            Direccion_Apartamento = apartamento.Direccion
                        };

            return Ok(query);
        }

        /// <summary>
        /// Retorna la lista de garantes
        /// </summary>
        /// <returns>Una lista de garantes.</returns>
        public IHttpActionResult Get()
        {
            var garantes = db.Garante
                .Include(a => a.arrendatario)
                .ToList();

            return Ok(garantes);
        }

        /// <summary>
        /// Retorna un garante por id
        /// </summary>
        /// <param name="id">ID del garante a retornar.</param>
        /// <returns>Un garante específico.</returns>
        /// <response code="200">Si el garante es encontrado.</response>
        /// <response code="404">Si el garante no es encontrado.</response>
        public IHttpActionResult Get(int id)
        {
            Garante garante = db.Garante.Find(id);
            if (garante == null)
            {
                return NotFound();
            }

            return Ok(garante);
        }

        /// <summary>
        /// Crea un nuevo garante
        /// </summary>
        /// <param name="garante">El garante a crear.</param>
        /// <returns>El garante creado.</returns>
        public IHttpActionResult Post(Garante garante)
        {
            if (garante == null)
            {
                return BadRequest("El garante no puede ser nulo.");
            }

            Arrendatario arrendatarioExistente = db.Arrendatario.Find(garante.IdArrendatario);

            if (arrendatarioExistente == null)
            {
                return NotFound();
            }

            garante.arrendatario = arrendatarioExistente;

            db.Garante.Add(garante);
            db.SaveChanges();

            return Ok(garante);
        }

        /// <summary>
        /// Actualiza un garante existente
        /// </summary>
        /// <param name="garanteModificado">El garante con los datos actualizados.</param>
        /// <param name="id">ID del garante a editar.</param>
        /// <returns>El garante actualizado.</returns>
        /// <response code="200">Si el garante es actualizado correctamente.</response>
        /// <response code="404">Si el garante no es encontrado.</response>
        public IHttpActionResult Put(int id, Garante garanteModificado)
        {
            Garante garanteExistente = db.Garante.Find(id);

            if (garanteExistente == null)
            {
                return NotFound();
            }

            Arrendatario arrendatarioExistente = db.Arrendatario.Find(garanteModificado.IdArrendatario);

            if (arrendatarioExistente == null)
            {
                return NotFound();
            }

            garanteExistente.docAval = garanteModificado.docAval;
            garanteExistente.emisionDoc = garanteModificado.emisionDoc;
            garanteExistente.IdArrendatario = garanteModificado.IdArrendatario;
            garanteExistente.arrendatario = arrendatarioExistente;

            db.SaveChanges();
            return Ok(garanteExistente);
        }

        /// <summary>
        /// Elimina un garante por id
        /// </summary>
        /// <param name="id">ID del garante a eliminar.</param>
        /// <returns>El garante eliminado.</returns>
        /// <response code="200">Si el garante es eliminado correctamente.</response>
        /// <response code="404">Si el garante no es encontrado.</response>
        public IHttpActionResult Delete(int id)
        {
            Garante garante = db.Garante.Find(id);
            if (garante == null)
            {
                return NotFound();
            }

            db.Garante.Remove(garante);
            db.SaveChanges();
            return Ok(garante);
        }
    }
}