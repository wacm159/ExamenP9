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
using ExamenFinalP9.Models;

namespace ExamenFinalP9.Controllers
{
    public class CelularController : ApiController
    {
        private TelefonosEntities db = new TelefonosEntities();

        // GET: api/Celular
        public IQueryable<Celular> GetCelular()
        {
            return db.Celular;
        }

        // GET: api/Celular/5
        [ResponseType(typeof(Celular))]
        public IHttpActionResult GetCelular(int id)
        {
            Celular celular = db.Celular.Find(id);
            if (celular == null)
            {
                return NotFound();
            }

            return Ok(celular);
        }

        // PUT: api/Celular/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCelular(int id, Celular celular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != celular.id_telefono)
            {
                return BadRequest();
            }

            db.Entry(celular).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CelularExists(id))
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

        // POST: api/Celular
        [ResponseType(typeof(Celular))]
        public IHttpActionResult PostCelular(Celular celular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Celular.Add(celular);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = celular.id_telefono }, celular);
        }

        // DELETE: api/Celular/5
        [ResponseType(typeof(Celular))]
        public IHttpActionResult DeleteCelular(int id)
        {
            Celular celular = db.Celular.Find(id);
            if (celular == null)
            {
                return NotFound();
            }

            db.Celular.Remove(celular);
            db.SaveChanges();

            return Ok(celular);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CelularExists(int id)
        {
            return db.Celular.Count(e => e.id_telefono == id) > 0;
        }
    }
}