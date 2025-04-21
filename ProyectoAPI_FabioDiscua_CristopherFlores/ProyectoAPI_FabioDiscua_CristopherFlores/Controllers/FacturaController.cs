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
    public class FacturaController : ApiController
    {
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Retorna la lista de facturas
        /// </summary>
        /// <returns>Una lista de facturas.</returns>
        public IHttpActionResult Get()
        {
            var facturas = db.Factura
                .Include(a => a.contrato)
                .ToList();

            return Ok(facturas);
        }

        /// <summary>
        /// Retorna una factura por id
        /// </summary>
        /// <param name="id">ID de la factura a retornar.</param>
        /// <returns>Una factura específica.</returns>
        /// <response code="200">Si la factura es encontrada.</response>
        /// <response code="404">Si la factura no es encontrada.</response>
        public IHttpActionResult Get(int id)
        {
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        /// <summary>
        /// Crea una nueva factura
        /// </summary>
        /// <param name="factura">La factura a crear.</param>
        /// <returns>La factura creada.</returns>
        /// <response code="200">Si la factura es creada correctamente.</response>
        /// <response code="400">Si el contrato no es encontrado.</response>
        public IHttpActionResult Post(Factura factura)
        {
            if (factura == null)
            {
                return BadRequest("La factura no puede ser nula.");
            }

            Contrato contratoExistente = db.Contrato
                .Include(c => c.apartamento)
                .FirstOrDefault(c => c.id == factura.IdContrato);

            if (contratoExistente == null)
            {
                return BadRequest("Contrato no encontrado.");
            }

            factura.contrato = contratoExistente;
            factura.CalcularMontoTotal();

            db.Factura.Add(factura);
            db.SaveChanges();

            return Ok(factura);
        }

        /// <summary>
        /// Actualiza una factura existente
        /// </summary>
        /// <param name="facturaModificada">La factura con los datos actualizados.</param>
        /// /// <param name="id">ID de la factura a editar.</param>
        /// <returns>La factura actualizada.</returns>
        /// <response code="200">Si la factura es actualizada correctamente.</response>
        /// <response code="404">Si la factura no es encontrada.</response>
        public IHttpActionResult Put(int id, Factura facturaModificada)
        {
            Factura facturaExistente = db.Factura.Find(id);
            if (facturaExistente == null)
            {
                return NotFound();
            }
            Contrato contratoExistente = db.Contrato
                .Include(c => c.apartamento)
                .FirstOrDefault(c => c.id == facturaModificada.IdContrato);

            if (contratoExistente == null)
            {
                return BadRequest("Contrato no encontrado.");
            }

            facturaExistente.IdContrato = facturaModificada.IdContrato;
            facturaExistente.emision = facturaModificada.emision;
            facturaExistente.ServiciosStr = facturaModificada.ServiciosStr;
            facturaExistente.contrato = contratoExistente; 
            facturaExistente.CalcularMontoTotal();

            db.Entry(facturaExistente).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(facturaExistente);
        }

        /// <summary>
        /// Elimina una factura por id
        /// </summary>
        /// <param name="id">ID de la factura a eliminar.</param>
        /// <returns>La factura eliminada.</returns>
        /// <response code="200">Si la factura es eliminada correctamente.</response>
        /// <response code="404">Si la factura no es encontrada.</response>
        public IHttpActionResult Delete(int id)
        {
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return NotFound();
            }

            db.Factura.Remove(factura);
            db.SaveChanges();
            return Ok(factura);
        }
    }
}