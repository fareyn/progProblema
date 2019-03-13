using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProblemaProject.Models;

namespace ProblemaProject.Controllers
{
    public class peliculasController : Controller
    {
        private SisteEntities db = new SisteEntities();

        // GET: peliculas
        public ActionResult Index()
        {
            var peliculas = db.peliculas.Include(p => p.genero1);
            return View(peliculas.ToList());
        }

        // GET: peliculas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/index");
            }
            pelicula pelicula = db.peliculas.Find(id);
            if (pelicula == null)
            {
                Response.Redirect("/index");
            }
            if(pelicula.disponible == true)
            {
                return RedirectToAction("pesdisp", new {id});
                
                // return Edit(id);
               
            }//esta disponible
            else
            {
                return RedirectToAction("pesren", new{id});
            }// no esta disponible
            //return View(pelicula);
        }//Details

        /* 
                 public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = db.peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            if(pelicula.disponible == true)
            {
                Response.Redirect("/Edit/"+id);
                
                // return Edit(id);
               
            }//esta disponible
            else
            {
                return Index();
            }// no esta disponible
            //return View(pelicula);
        }//Details
             */


        // GET: peliculas/Create
        public ActionResult Create()
        {
            ViewBag.genero = new SelectList(db.generoes, "id", "id");
            return View();
        }

        // POST: peliculas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,genero,sinopsis,falta,disponible,fretorno")] pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                pelicula.falta = DateTime.Now.Date;
                pelicula.fretorno = DateTime.Now.Date;
                pelicula.disponible = true;
                db.peliculas.Add(pelicula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.genero = new SelectList(db.generoes, "id", "id", pelicula.genero);
            return View(pelicula);
        }

        // GET: peliculas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = db.peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.genero = new SelectList(db.generoes, "id", "id", pelicula.genero);
            return View(pelicula);
        }

        // POST: peliculas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //"id,nombre,genero,sinopsis,falta,disponible,fretorno"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,genero,sinopsis,falta,disponible,fretorno")] pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pelicula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.genero = new SelectList(db.generoes, "id", "id", pelicula.genero);
            return View(pelicula);
        }

        public pelicula sDates(pelicula o)
        {
            if (o.falta == null)
            {
                o.falta = db.peliculas.Find(o.id).falta;
            }
            if (o.fretorno == null)
            {
                o.fretorno = db.peliculas.Find(o.id).fretorno;
            }
            return o;
        }//sDates

        // GET: peliculas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = db.peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // POST: peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pelicula pelicula = db.peliculas.Find(id);
            if(pelicula.disponible==true)
            {
                db.peliculas.Remove(pelicula);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

        // GET: peliculas/Edit/5
        [ActionName("pesdisp")]
        public ActionResult Pesdisp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = db.peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            //ViewBag.pesdisp = new SelectList(db.generoes, "id", "id", pelicula.genero);
            pDisp_Result x = db.Database.SqlQuery<pDisp_Result>("select * from pDisp(@d)", new SqlParameter("@d", id)).FirstOrDefault();
            return View(x);
        }

        // POST: peliculas/Delete/5
        [HttpPost, ActionName("pesdisp")]
        [ValidateAntiForgeryToken]
        public ActionResult PesdispRent(int id)
        {
            pelicula pelicula = db.peliculas.Find(id);
            if (pelicula.disponible == true)
            {
                db.Rentar(id);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [ActionName("pesren")]
        public ActionResult Pesren(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = db.peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            //ViewBag.pesdisp = new SelectList(db.generoes, "id", "id", pelicula.genero);
            pRen_Result x = db.Database.SqlQuery<pRen_Result>("select * from pRen(@d)", new SqlParameter("@d", id)).FirstOrDefault();
            return View(x);
        }

        // POST: peliculas/Delete/5
        [HttpPost, ActionName("pesRen")]
        [ValidateAntiForgeryToken]
        public ActionResult PesRenLib(int id)
        {
            pelicula pelicula = db.peliculas.Find(id);
            if (pelicula.disponible == false)
            {
                db.finRenta(id);
                db.SaveChanges();
            }

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
