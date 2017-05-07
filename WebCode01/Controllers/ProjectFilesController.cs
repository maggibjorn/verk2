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
        public ActionResult Index(int projectId)
        {
            // Birta lista af skrám í verkefni
            List<ProjectFileListViewModel> model = service.getProjectFilesById(projectId);
            IEnumerable<ProjectFileListViewModel> modelList = model;
            return View(modelList);
        }

        public ActionResult Editor(int fileId)
        {
            ProjectFileViewModel model = service.getFileById(fileId);
            ViewBag.Code = model.fileContent;
            ViewBag.id = fileId;
            return View();
        }

        [HttpPost]
        public ActionResult SaveCode(ProjectFileViewModel model)
        {
            service.saveCodeToDb(model);
            return View("Editor", model);
        }

        public ActionResult AddFile()
        {
            var model = new ProjectFileViewModel();
            return View(model);
        }

        

       


    }
}