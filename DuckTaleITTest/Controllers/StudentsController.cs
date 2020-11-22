using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DuckTaleITTest.Models;

namespace DuckTaleITTest.Controllers
{
    public class StudentsController : Controller
    {
        private DB db = new DB();

        // GET: Students
        [HttpGet]
        public ActionResult Index()
        {
            //return View(db.Students.ToList());

            return View(db.Students.ToList());
        }

        [HttpPost]
        public ActionResult Index(string CatSearch, string NameSearch)
        {
            //return View(db.Students.ToList());

            if(CatSearch == "Class" && NameSearch != null)
            {
                return View(db.Students.Where(x => x.Std_Class.Contains(NameSearch)));
            }
            else if (CatSearch == "Subject" && NameSearch != null)
            {
                return View(db.Students.Where(x => x.Subjects.Select(y => y.Name).Contains(NameSearch)));
            }
            else
            {
                return View(db.Students.Where(x => x.Std_FirstName.Contains(NameSearch) || x.Std_LastName.Contains(NameSearch) || NameSearch == null).ToList());
            }
            
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View(new Student()
            {
                Std_Subjects = new List<Subject>()
                {
                    new Subject()
                }
            });
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Std_Id,Std_FirstName,Std_LastName,Std_Class,Subjects,Marks")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return Json(true);
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Std_Id,Std_FirstName,Std_LastName,Std_Class,Subject,Marks")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
