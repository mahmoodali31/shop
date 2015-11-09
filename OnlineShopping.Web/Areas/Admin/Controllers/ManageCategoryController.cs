using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using OnlineShopping.Domain.Interfaces;

namespace OnlineShopping.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class ManageCategoryController : Controller
    {
       
       private readonly ICategory repository;

       public ManageCategoryController( ICategory categoryRepository)
        {

            repository = categoryRepository;
         
        }

        // GET: Admin/ManageCategory
        public ActionResult Index()
        {
            return View(repository.GetAllCategory().ToList());
        }

        // GET: Admin/ManageCategory/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = repository.GetById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/ManageCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ManageCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                repository.Create(category);
                
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/ManageCategory/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = repository.GetById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/ManageCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKCategoryId,CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                repository.Update(category);
                
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/ManageCategory/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = repository.GetById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/ManageCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = repository.GetById(id);
            repository.Delete(id);
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
