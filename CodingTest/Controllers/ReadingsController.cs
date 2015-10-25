using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodingTest;
using CodingTest.Models;

namespace CodingTest.Controllers
{
    public class ReadingsController : Controller
    {

        private readonly DataContext _dc;
        public ReadingsController(DataContext dc)
        {
            _dc = dc;
        }

        // GET: Readings
        public async Task<ActionResult> Index()
        {
            return View(await _dc.Readings.ToListAsync());
        }

        // GET: Readings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reading reading = await _dc.Readings.FindAsync(id);
            if (reading == null)
            {
                return HttpNotFound();
            }
            return View(reading);
        }

        // GET: Readings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Readings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Depth,MagX,MagY,MaxZ")] Reading reading)
        {
            if (ModelState.IsValid)
            {
                _dc.Readings.Add(reading);
                await _dc.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(reading);
        }

        // GET: Readings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reading reading = await _dc.Readings.FindAsync(id);
            if (reading == null)
            {
                return HttpNotFound();
            }
            return View(reading);
        }

        // POST: Readings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Depth,MagX,MagY,MaxZ")] Reading reading)
        {
            if (ModelState.IsValid)
            {
                _dc.Entry(reading).State = EntityState.Modified;
                await _dc.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reading);
        }

        // GET: Readings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reading reading = await _dc.Readings.FindAsync(id);
            if (reading == null)
            {
                return HttpNotFound();
            }
            return View(reading);
        }

        // POST: Readings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reading reading = await _dc.Readings.FindAsync(id);
            _dc.Readings.Remove(reading);
            await _dc.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dc.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
