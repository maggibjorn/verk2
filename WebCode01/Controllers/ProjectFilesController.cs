using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCode01.ViewModels;
using WebCode01.Services;
using Microsoft.AspNet.Identity;

namespace WebCode01.Controllers
{
    public class ProjectFilesController : Controller
    {
        public ProjectFilesService service = new ProjectFilesService();
        // GET: ProjectFiles
        [Authorize]
        public ActionResult Index(int projectId)
        {            
            List<ProjectFileListViewModel> model = service.getProjectFilesById(projectId);
            if (model.Count > 0)
            {
                ViewBag.projectId = model[0].projectId; // Fetch project id if file list isn't empty, use this in editor view to save file to database
            }
           
            IEnumerable<ProjectFileListViewModel> modelList = model;
            return View(modelList);
        }

        // Filter the user projects by type of projects, java, c++, etc
        [Authorize]
        public ActionResult FilesByType(int projectId, string fileType)
        {
            ViewBag.projectId = Request.Url.ToString().Split('=')[1][0];
            List<ProjectFileListViewModel> model = service.GetFilesByType(projectId, fileType);
            IEnumerable<ProjectFileListViewModel> modelList = model;
            return View("Index", modelList);
        }

        [Authorize]
        public ActionResult Editor(int fileId)
        {
            ProjectFileViewModel model = service.getFileById(fileId);
            ViewBag.id = fileId;
            ViewBag.name = model.name;
            ViewBag.Code = model.fileContent;
            return View();
        }

        [Authorize]
        public ActionResult AddMember(int projectId)
        {
            ViewBag.projectId = projectId;
            return View();
        }
        
        [HttpPost]
        public ActionResult AddMember(AddMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            service.AddMember(model);

            return RedirectToAction("Index", new { projectId = model.projectId });
        }

        [Authorize]
        public ActionResult AddFile(int projectId)
        {
            ViewBag.projectId = projectId;
            return View();
        }

        [HttpPost]
        public ActionResult AddFile(AddFileViewModel model)
        {
            if(model.file == null)
            {
            return RedirectToAction("Index", new { projectId = model.projectId });
            }
            service.saveFileToDb(model);
            return RedirectToAction("Index", new { projectId = model.projectId });
        }


        [Authorize]
        public ActionResult CreateBlankFile(int projectId)
        {
            ViewBag.projectId = projectId;
            return View();
        }

        [HttpPost]
        public ActionResult CreateBlankFile(FormCollection coll)
        {
            string fName = coll["fileName"];
            string type = coll["type"];
            string projectId = coll["projectId"];
            int numId;
            bool test = Int32.TryParse(projectId, out numId);
            if (!test)
            {
                // Error
            }
            CreateBlankFileViewModel model = new CreateBlankFileViewModel
            {
                fileName = fName, // Add file exstension to name
                projectId = numId,
                fileType = type
            };

            if (model.fileName == null)
            {
                return RedirectToAction("Index", new { projectId = model.projectId });
            }
            service.SaveBlankFileToDb(model);
            return RedirectToAction("Index", new { projectId = numId});
        }

        // List up all project members in project
        public ActionResult ProjectMembers(int projectId)
        {
            ViewBag.projectId = Request.Url.ToString().Split('=')[1]; // Use this to make "back to project" button
            List<ProjectMemberViewModel> model = service.FindProjectMembers(projectId);
            IEnumerable<ProjectMemberViewModel> modelList = model;
            return View(modelList);
        }

       


    }
}