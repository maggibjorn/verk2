using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCode01.ViewModels;
using WebCode01.Services;

namespace WebCode01.Controllers
{
    public class ProjectFilesController : Controller
    {
        public ProjectFilesService service = new ProjectFilesService();
        // GET: ProjectFiles
        public ActionResult Index()
        {
            // TODO: Birta lista af skrám í verkefni
            return View();
        }

        public ActionResult AddFile()
        {
            var model = new ProjectFileViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Addfile(ProjectFileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            service.addFileToDb(model);
            

            return RedirectToAction("Index");
        }

       


    }
}