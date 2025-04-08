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
    public class PagoController : ApiController
    {
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Obtener la lista de Pagos pendientes ordenados por antiguedad
        /// </summary>
        /// <returns>JSON Pagos</returns>
        /// <response code="200">Devuelve el pago encontrado</response>
        /// <response code="404">Si el pago no es encontrado</response>
        [HttpGet]
        [SwaggerOperation("GetPagosPendientes")]
        [Route("api/GetPagosPendientes")]
        public IHttpActionResult GetPagosPendientes()
        {
            var query = from pago in db.Pago
                        join contrato in db.Contrato on pago.contrato.id equals contrato.id
                        join arrendatario in db.Arrendatario on contrato.arrendatario.id equals arrendatario.id
                        where pago.estado == false
                        orderby pago.fechaPago ascending
                        select new
                        {
                            ID_Pago = pago.id,
                            Fecha_Pago = pago.fechaPago,
                            Monto_Pago = pago.Monto,
                            Estado_Pago = "Pendiente",
                            ID_Contrato = contrato.id,
                            Fecha_Inicio_Contrato = contrato.fechaInicio,
                            Fecha_Fin_Contrato = contrato.fechaFin,
                            ID_Arrendatario = arrendatario.id,
                            Nombre_Arrendatario = arrendatario.nombres + " " + arrendatario.apellidos
                        };

            return Ok(query);
        }

        /// <summary>
        /// Retorna la lista de pagos
        /// </summary>
        /// <returns>Una lista de pagos.</returns>
        public IHttpActionResult Get()
        {
            var pagos = db.Pago
                .Include(a => a.contrato)
                .ToList();

            return Ok(pagos);
        }

        /// <summary>
        /// Retorna un pago por id
        /// </summary>
        /// <param name="id">ID del pago a retornar.</param>
        /// <returns>Un pago específico.</returns>
        /// <response code="200">Si el pago es encontrado.</response>
        /// <response code="404">Si el pago no es encontrado.</response>
        public IHttpActionResult Get(int id)
        {
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return NotFound();
            }

            return Ok(pago);
        }

        /// <summary>
        /// Crea un nuevo pago
        /// </summary>
        /// <param name="pago">El pago a crear.</param>
        /// <returns>El pago creado.</returns>
        public IHttpActionResult Post(Pago pago)
        {
            if (pago == null)
            {
                return BadRequest("El pago no puede ser nulo.");
            }

            Contrato contratoExistente = db.Contrato.Find(pago.IdContrato);
            if (contratoExistente == null)
            {
                return BadRequest("Contrato no encontrado.");
            }

            Factura factura = db.Factura.FirstOrDefault(f => f.IdContrato == pago.IdContrato);
            if (factura == null)
            {
                return BadRequest("No hay factura registrada para este contrato.");
            }

            pago.Factura = factura;
            pago.CalcularMonto();
            pago.contrato = contratoExistente;

            db.Pago.Add(pago);
            db.SaveChanges();

            return Ok(pago);
        }

        /// <summary>
        /// Actualiza un pago existente
        /// </summary>
        /// <param name="pagoModificado">El pago con los datos actualizados.</param>
        /// <param name="id">ID del pago a editar.</param>
        /// <returns>El pago actualizado.</returns>
        /// <response code="200">Si el pago es actualizado correctamente.</response>
        /// <response code="404">Si el pago no es encontrado.</response>
        public IHttpActionResult Put(int id, Pago pagoModificado)
        {
            Pago pagoExistente = db.Pago.Find(id);
            if (pagoExistente == null)
            {
                return NotFound();
            }

            Contrato contratoExistente = db.Contrato.Find(pagoModificado.IdContrato);
            if (contratoExistente == null)
            {
                return BadRequest("Contrato no encontrado.");
            }

            Factura factura = db.Factura.FirstOrDefault(f => f.IdContrato == pagoModificado.IdContrato);
            if (factura == null)
            {
                return BadRequest("No hay factura registrada para este contrato.");
            }

            pagoExistente.Factura = factura;
            pagoExistente.CalcularMonto(); 
            pagoExistente.fechaPago = pagoModificado.fechaPago;
            pagoExistente.estado = pagoModificado.estado;
            pagoExistente.IdContrato = pagoModificado.IdContrato;
            pagoExistente.contrato = contratoExistente;

            db.Entry(pagoExistente).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(pagoExistente);
        }

        /// <summary>
        /// Elimina un pago por id
        /// </summary>
        /// <param name="id">ID del pago a eliminar.</param>
        /// <returns>El pago eliminado.</returns>
        /// <response code="200">Si el pago es eliminado correctamente.</response>
        /// <response code="404">Si el pago no es encontrado.</response>
        public IHttpActionResult Delete(int id)
        {
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return NotFound();
            }

            db.Pago.Remove(pago);
            db.SaveChanges();
            return Ok(pago);
        }
    }

}