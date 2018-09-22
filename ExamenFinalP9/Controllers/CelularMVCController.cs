using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ExamenFinalP9.Models;

namespace ExamenFinalP9.Controllers
{
    public class CelularMVCController : Controller
    {
        private TelefonosEntities db = new TelefonosEntities();

        // GET: CelularMVC
        public ActionResult Index()
        {
            IEnumerable<Celular> alumno = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58924/api/");
                //GET ALUMNOS
                //obtiene asincronamente y espera hasta obetener la data
                var responseTask = client.GetAsync("Celular");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var leer = result.Content.ReadAsAsync<IList<Celular>>();
                    leer.Wait();
                    alumno = leer.Result;
                }
                else
                {
                    alumno = Enumerable.Empty<Celular>();
                    ModelState.AddModelError(string.Empty, "Error .... Try Again");
                }
            }
            return View(alumno.ToList());
        }

        // GET: CelularMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celular celular = db.Celular.Find(id);
            if (celular == null)
            {
                return HttpNotFound();
            }
            return View(celular);
        }

        // GET: CelularMVC/Create
        public ActionResult Create()
        {
            ViewBag.id_color = new SelectList(db.Color, "id_color", "color1");
            ViewBag.id_ensamble = new SelectList(db.Ensamble, "id_ensamble", "pais");
            ViewBag.id_gama = new SelectList(db.Gama, "id_gama", "gama1");
            return View();
        }

        // POST: CelularMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_telefono,id_gama,id_ensamble,id_color,fecha,hora")] Celular celular)
        {
            if (ModelState.IsValid)
            {
                db.Celular.Add(celular);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_color = new SelectList(db.Color, "id_color", "color1", celular.id_color);
            ViewBag.id_ensamble = new SelectList(db.Ensamble, "id_ensamble", "pais", celular.id_ensamble);
            ViewBag.id_gama = new SelectList(db.Gama, "id_gama", "gama1", celular.id_gama);
            return View(celular);
        }

        // GET: CelularMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celular celular = db.Celular.Find(id);
            if (celular == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_color = new SelectList(db.Color, "id_color", "color1", celular.id_color);
            ViewBag.id_ensamble = new SelectList(db.Ensamble, "id_ensamble", "pais", celular.id_ensamble);
            ViewBag.id_gama = new SelectList(db.Gama, "id_gama", "gama1", celular.id_gama);
            return View(celular);
        }

        // POST: CelularMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_telefono,id_gama,id_ensamble,id_color,fecha,hora")] Celular celular)
        {
            if (ModelState.IsValid)
            {
                db.Entry(celular).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_color = new SelectList(db.Color, "id_color", "color1", celular.id_color);
            ViewBag.id_ensamble = new SelectList(db.Ensamble, "id_ensamble", "pais", celular.id_ensamble);
            ViewBag.id_gama = new SelectList(db.Gama, "id_gama", "gama1", celular.id_gama);
            return View(celular);
        }

        // GET: CelularMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celular celular = db.Celular.Find(id);
            if (celular == null)
            {
                return HttpNotFound();
            }
            return View(celular);
        }

        // POST: CelularMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Celular celular = db.Celular.Find(id);
            db.Celular.Remove(celular);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
