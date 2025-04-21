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
    public class ArrendatarioController : ApiController
    {
        private DBContextProject db = new DBContextProject();

        /// <summary>
        /// Obtener la lista de Arrendatarios ordenados según el contrato mas reciente
        /// </summary>
        /// <returns>JSON Arrendatarios</returns>
        /// <response code="200">Devuelve el arrendatario encontrado</response>
        /// <response code="404">Si el arrendatario no es encontrado</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetContratosRecientes")]
        [Route("api/GetContratosRecientes")]
        public IHttpActionResult GetContratoReciente()
        {
            var query = from arrendatario in db.Arrendatario
                        join contrato in db.Contrato
                        on arrendatario.id equals contrato.arrendatario.id
                        join apartamento in db.Apartamento
                        on contrato.apartamento.id equals apartamento.id
                        join edificio in db.Edificio
                        on apartamento.Edificio.id equals edificio.id
                        orderby contrato.fechaInicio descending  
                        select new
                        {
                            idArrendatario = arrendatario.id,
                            nombreArrendatario = arrendatario.nombres + " " + arrendatario.apellidos,
                            arrendatario.telefono,
                            arrendatario.email,
                            numeroApartamento = apartamento.NumHabitaciones,
                            nombreEdificio = edificio.Nombre,
                            fechaInicioContrato = contrato.fechaInicio,
                            fechaFinContrato = contrato.fechaFin
                        };

            return Ok(query);
        }

        /// <summary>
        /// Obtener la lista de Arrendatarios el mantenimiento realizado en su apartamento
        /// </summary>
        /// <returns>JSON Arrendatarios</returns>
        /// <response code="200">Devuelve el arrendatario encontrado</response>
        /// <response code="404">Si el arrendatario no es encontrado</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetMatenimientos")]
        [Route("api/GetMantenimientos")]
        public IHttpActionResult GetMantenimientos()
        {
            var query = from arrendatario in db.Arrendatario
                        join solicitud in db.SolicitudMantenimiento
                        on arrendatario.id equals solicitud.IdApartamento
                        join apartamento in db.Apartamento
                        on solicitud.IdApartamento equals apartamento.id
                        join historial in db.Historial
                        on solicitud.id equals historial.IdSol
                        join empleado in db.Empleado
                        on historial.IdEmpleado equals empleado.id
                        orderby historial.SolicitudMantenimiento.FechaRealizacion descending
                        select new
                        {
                            idArrendatario = arrendatario.id,
                            nombreArrendatario = arrendatario.nombres + " " + arrendatario.apellidos,
                            arrendatario.telefono,
                            numeroApartamento = apartamento.NumHabitaciones,
                            fechaMantenimiento = historial.SolicitudMantenimiento.FechaRealizacion,
                            descripcion = solicitud.Descripcion,
                            realizadoPor = empleado.nombres + " " + empleado.apellidos
                        };

            return Ok(query);
        }



        /// <summary>
        /// Obtener la lista de Arrendatarios ordenados por Edad
        /// </summary>
        /// <returns>JSON Arrendatarios</returns>
        /// <response code="200">Devuelve el arrendatario encontrado</response>
        /// <response code="404">Si el arrendatario no es encontrado</response>
        /// 
        [HttpGet]
        [SwaggerOperation("GetEdad")]
        [Route("api/GetEdad")]
        public IHttpActionResult GetEdad()
        {
            var query = from arrendatario in db.Arrendatario
                        orderby arrendatario.fechaNacimiento ascending
                        select new
                        {
                            idArrendatario = arrendatario.id,
                            nombreArrendatario = arrendatario.nombres + " " + arrendatario.apellidos,
                            arrendatario.telefono,
                            arrendatario.email,
                            arrendatario.fechaNacimiento,
                            edad = DateTime.Now.Year - arrendatario.fechaNacimiento.Year
                        };

            return Ok(query);
        }

        /// <summary>
        /// Retorna la lista de arrendatarios
        /// </summary>
        /// <returns>Una lista de arrendatarios.</returns>
        public IEnumerable<Arrendatario> Get()
        {
            return db.Arrendatario;
        }

        /// <summary>
        /// Retorna un arrendatario por id
        /// </summary>
        /// <param name="id">ID del arrendatario a retornar.</param>
        /// <returns>Un arrendatario específico.</returns>
        /// <response code="200">Si el arrendatario es encontrado.</response>
        /// <response code="404">Si el arrendatario no es encontrado.</response>
        public IHttpActionResult Get(int id)
        {
            Arrendatario arrendatario = db.Arrendatario.Find(id);
            if (arrendatario == null)
            {
                return NotFound();
            }

            return Ok(arrendatario);
        }

        /// <summary>
        /// Crea un nuevo arrendatario
        /// </summary>
        /// <param name="arrendatario">El arrendatario a crear.</param>
        /// <returns>El arrendatario creado.</returns>
        public IHttpActionResult Post(Arrendatario arrendatario)
        {
            db.Arrendatario.Add(arrendatario);
            db.SaveChanges();
            return Ok(arrendatario);
        }

        /// <summary>
        /// Actualiza un arrendatario existente
        /// </summary>
        /// <param name="arrendatarioModificado">El arrendatario con los datos actualizados.</param>
        /// /// <param name="id">ID del arrendatario a editar.</param>
        /// <returns>El arrendatario actualizado.</returns>
        /// <response code="200">Si el arrendatario es actualizado correctamente.</response>
        /// <response code="404">Si el arrendatario no es encontrado.</response>
        public IHttpActionResult Put(int id, Arrendatario arrendatarioModificado)
        {
            if (arrendatarioModificado == null)
            {
                return NotFound();
            }

            db.Entry(arrendatarioModificado).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(arrendatarioModificado);
        }

        /// <summary>
        /// Elimina un arrendatario por id
        /// </summary>
        /// <param name="id">ID del arrendatario a eliminar.</param>
        /// <returns>El arrendatario eliminado.</returns>
        /// <response code="200">Si el arrendatario es eliminado correctamente.</response>
        /// <response code="404">Si el arrendatario no es encontrado.</response>
        public IHttpActionResult Delete(int id)
        {
            Arrendatario arrendatario = db.Arrendatario.Find(id);
            if (arrendatario == null)
            {
                return NotFound();
            }

            db.Arrendatario.Remove(arrendatario);
            db.SaveChanges();
            return Ok(arrendatario);
        }
    }
}