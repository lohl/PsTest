using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls.Expressions;
using CodingTest;
using CodingTest.Models;
using CodingTest.Repositories;

namespace CodingTest.Controllers
{
    public class ReadingsController : Controller
    {

        private readonly DataContext _dc;
        private readonly IRepositoryFactory _repositoryFactory;

        public ReadingsController(DataContext dc, IRepositoryFactory repositoryFactory)
        {
            _dc = dc;
            _repositoryFactory = repositoryFactory;
        }

        // GET: Readings
        public ActionResult Index(string message)
        {
            ViewBag.Confirmation = message;
            return View();
        }

        // GET: Readings/Details/5
        public async Task<ActionResult> Details(int? id, string readingType)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (readingType == "Reading")
            {
                Reading reading = await _repositoryFactory.GetRepository<Reading>().Get(id);
                if (reading == null)
                {
                    return HttpNotFound();
                }
                return View("DetailsMag", reading);

            }
            else
            {
                ReadingGrav readingGrav = await _repositoryFactory.GetRepository<ReadingGrav>().Get(id);
                if (readingGrav == null)
                {
                    return HttpNotFound();
                }
                return View("DetailsGrav", readingGrav);

            }
        }

        // GET: Readings/Create
        public ActionResult Create(string readingType)
        {
            return View(readingType == "Reading" ? "CreateMag" : "CreateGrav");
        }

        // POST: Readings/CreateMag
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateMag()
        {
            if (ModelState.IsValid)
            {
                string conf = string.Empty;
                conf = await Create<Reading>();
                return RedirectToAction("Index", new { message = conf});
            }

            return View();
        }

        private async Task<string> Create<T>() where T: class, new()
        {
            T a = new T();
            TryUpdateModel(a);
            int retValue = await _repositoryFactory.GetRepository<T>().Add(a);
            var conf = (retValue == 1) ? "Record Added." : string.Empty;
            return conf;
        }

        // POST: Readings/CreateMag
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateGrav()
        {
            if (ModelState.IsValid)
            {
                var conf = await Create<ReadingGrav>();
                return RedirectToAction("Index", new { message = conf });
            }

            return View();
        }

        // GET: Readings/Edit/5
        public async Task<ActionResult> Edit(int? id, string readingType)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (readingType == "Reading")
            {
                Reading reading = await _repositoryFactory.GetRepository<Reading>().Get(id);
                if (reading == null)
                {
                    return HttpNotFound();
                }
                return View("EditMag", reading);

            }
            else
            {
                ReadingGrav reading = await _repositoryFactory.GetRepository<ReadingGrav>().Get(id);
                if (reading == null)
                {
                    return HttpNotFound();
                }
                return View("EditGrav", reading);

            }
        }

        // POST: Readings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMag()
        {

            if (ModelState.IsValid)
            {
                await Edit<Reading>();
                return RedirectToAction("Index");
            }

            return View();
        }

        // POST: Readings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditGrav()
        {

            if (ModelState.IsValid)
            {
                await Edit<ReadingGrav>();
                return RedirectToAction("Index");
            }

            return View();
        }

        private async Task Edit<T>() where T: class, new()
        {
            T a = new T();
            TryUpdateModel(a);
            await _repositoryFactory.GetRepository<T>().Update(a);
        }


        // GET: Readings/Delete/5
        public async Task<ActionResult> Delete(int? id, string readingType)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (readingType == "Reading")
            {
                Reading reading = await _repositoryFactory.GetRepository<Reading>().Get(id);
                if (reading == null)
                {
                    return HttpNotFound();
                }
                return View("DeleteMag", reading);

            }
            else
            {
                ReadingGrav reading = await _repositoryFactory.GetRepository<ReadingGrav>().Get(id);
                if (reading == null)
                {
                    return HttpNotFound();
                }
                return View("DeleteGrav", reading);

            }
        }

        // POST: Readings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string readingType)
        {
            if (readingType == "Reading")
            {
                var repo = _repositoryFactory.GetRepository<Reading>();
                await repo.Delete(await repo.Get(id));
            }
            else
            {
                var repo = _repositoryFactory.GetRepository<ReadingGrav>();
                await repo.Delete(await repo.Get(id));
            }

            //            _dc.Readings.Remove(reading);
            //            await _dc.SaveChangesAsync();
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

        public ActionResult IndexMag()
        {
            var model = _repositoryFactory.GetRepository<Reading>().GetAll();
            model.ConfigureAwait(false);
            return PartialView("_IndexMag", model.Result.OrderBy(m => m.Depth));

        }
        public ActionResult IndexGrav()
        {
            var model = _repositoryFactory.GetRepository<ReadingGrav>().GetAll();
            model.ConfigureAwait(false);
            return PartialView("_IndexGrav", model.Result.OrderBy(m => m.Depth));

        }
    }
}
