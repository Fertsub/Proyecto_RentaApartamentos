using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoAPI_FabioDiscua_CristopherFlores.Models;

namespace ProyectoAPI_FabioDiscua_CristopherFlores.Controllers
{
    public class ATCController : ApiController
    {
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Retorna la lista de atenciones al cliente
        /// </summary>
        /// <returns>Una lista de atenciones al cliente.</returns>
        public IEnumerable<AtencionAlCliente> Get()
        {
            return db.AtencionAlCliente;
        }

        /// <summary>
        /// Retorna una atención al cliente por id
        /// </summary>
        /// <param name="id">ID de la atención al cliente a retornar.</param>
        /// <returns>Una atención al cliente específica.</returns>
        /// <response code="200">Si la atención al cliente es encontrada.</response>
        /// <response code="404">Si la atención al cliente no es encontrada.</response>
        public IHttpActionResult Get(int id)
        {
            AtencionAlCliente atencionAlCliente = db.AtencionAlCliente.Find(id);
            if (atencionAlCliente == null)
            {
                return NotFound();
            }

            return Ok(atencionAlCliente);
        }

        /// <summary>
        /// Crea una nueva atención al cliente
        /// </summary>
        /// <param name="atencionAlCliente">La atención al cliente a crear.</param>
        /// <returns>La atención al cliente creada.</returns>
        public IHttpActionResult Post(AtencionAlCliente atencionAlCliente)
        {
            db.AtencionAlCliente.Add(atencionAlCliente);
            db.SaveChanges();
            return Ok(atencionAlCliente);
        }

        /// <summary>
        /// Actualiza una atención al cliente existente
        /// </summary>
        /// <param name="atencionAlClienteModificado">La atención al cliente con los datos actualizados.</param>
        /// /// <param name="id">ID de la atención al cliente a editar.</param>
        /// <returns>La atención al cliente actualizada.</returns>
        /// <response code="200">Si la atención al cliente es actualizada correctamente.</response>
        /// <response code="404">Si la atención al cliente no es encontrada.</response>
        public IHttpActionResult Put(int id, AtencionAlCliente atencionAlClienteModificado)
        {
            if (atencionAlClienteModificado == null)
            {
                return NotFound();
            }

            db.Entry(atencionAlClienteModificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(atencionAlClienteModificado);
        }

        /// <summary>
        /// Elimina una atención al cliente por id
        /// </summary>
        /// <param name="id">ID de la atención al cliente a eliminar.</param>
        /// <returns>La atención al cliente eliminada.</returns>
        /// <response code="200">Si la atención al cliente es eliminada correctamente.</response>
        /// <response code="404">Si la atención al cliente no es encontrada.</response>
        public IHttpActionResult Delete(int id)
        {
            AtencionAlCliente atencionAlCliente = db.AtencionAlCliente.Find(id);
            if (atencionAlCliente == null)
            {
                return NotFound();
            }

            db.AtencionAlCliente.Remove(atencionAlCliente);
            db.SaveChanges();
            return Ok(atencionAlCliente);
        }
    }
}