using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FinalProgra.Models;

namespace FinalProgra.Controllers
{
    public class NumerosController : ApiController
    {
        [HttpGet]
        public string funcion(int numero)
        {
            string texto = "";
            if (numero > 0)
            {
                texto = "Usted ingresó el número  " + numero;
            }
            if (numero < 0)
            {
                texto = "Error";
            }
            if (numero == 0)
            {
                texto = "Realizado por Enrique";
            }
            return texto;
        }

        private DataContext db = new DataContext();

        // GET: api/Numeros
        public IQueryable<Numero> GetNumeros()
        {
            return db.Numeros;
        }

        // GET: api/Numeros/5
        [ResponseType(typeof(Numero))]
        public IHttpActionResult GetNumero(int id)
        {
            Numero numero = db.Numeros.Find(id);
            if (numero == null)
            {
                return NotFound();
            }

            return Ok(numero);
        }

        // PUT: api/Numeros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNumero(int id, Numero numero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != numero.numero)
            {
                return BadRequest();
            }

            db.Entry(numero).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NumeroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Numeros
        [ResponseType(typeof(Numero))]
        public IHttpActionResult PostNumero(Numero numero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Numeros.Add(numero);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = numero.numero }, numero);
        }

        // DELETE: api/Numeros/5
        [ResponseType(typeof(Numero))]
        public IHttpActionResult DeleteNumero(int id)
        {
            Numero numero = db.Numeros.Find(id);
            if (numero == null)
            {
                return NotFound();
            }

            db.Numeros.Remove(numero);
            db.SaveChanges();

            return Ok(numero);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NumeroExists(int id)
        {
            return db.Numeros.Count(e => e.numero == id) > 0;
        }
    }
}