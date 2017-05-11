using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCode01.ViewModels;
using WebCode01.Services;
using Microsoft.AspNet.Identity;
using WebCode01.Entities;

namespace WebCode01.Controllers
{
    public class ProjectFilesController : Controller
    {
        public ProjectFilesService service = new ProjectFilesService(null);
        // GET: ProjectFiles
        [Authorize]
        public ActionResult Index(int projectId)
        {            
            List<ProjectFileListViewModel> model = service.GetProjectFilesById(projectId);
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
            ProjectFileViewModel model = service.GetFileById(fileId);
            ViewBag.id = fileId;
            ViewBag.name = model.name;
            ViewBag.Code = model.fileContent;
            return View();
        }

        /*[Authorize]
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
        }*/

        [Authorize]
        public ActionResult AddFile(int projectId)
        {
            ViewBag.projectId = projectId;
            return View();
        }

        [HttpPost]
        public ActionResult AddFile(AddFileViewModel model)
        {
            bool nameTaken = service.CheckFileName(model.file.FileName, model.projectId);

            if(model.file == null || nameTaken == true)
            {
            return RedirectToAction("Index", new { projectId = model.projectId });
            }
            service.SaveFileToDb(model);
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
            string name = model.fileName + "." + model.fileType;
            bool nameTaken = service.CheckFileName(name, model.projectId);//check if name is taken
            if (model.fileName == null || nameTaken == true)
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

        [Authorize]
        public ActionResult AddMember(int projectId)
        {
            string u = Request.Url.ToString().Split('=')[1];
            int theId;
            Int32.TryParse(u, out theId);
            ViewBag.projectId = theId;
            List<AddMemberViewModel> members = service.GetMembers(projectId);
            IEnumerable<AddMemberViewModel> modelList = members;
            return View(Tuple.Create(projectId, modelList));
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddMember(FormCollection coll, int Id)
        {
            string email = coll["email"];
            int id = Id;

            AddMemberViewModel model = new AddMemberViewModel();
            model.projectId = id;
            model.userEmail = email;

            bool checkIfMemberExist = service.IsInProject(email, id);
            if(checkIfMemberExist==true)
            {
                service.AddMember(model, id);
            }

            return RedirectToAction("Index", new { projectId = model.projectId });
        }


        [Authorize]
        public ActionResult SearchMember(FormCollection coll)
        {
            string searchValue = coll["search"];
            string Id = coll["projectId"];
            int id;
            string u = Request.Url.ToString();
            Int32.TryParse(Id, out id);
            List<AddMemberViewModel> emails = service.MemberSearch(searchValue, id);
            IEnumerable<AddMemberViewModel> modelList = emails;
            return View("AddMember",Tuple.Create(id, modelList));
        }
        [Authorize]
        public ActionResult ComfirmDelete(int projectId)
        {
            List<ProjectFileListViewModel> projectFiles = service.GetProjectFilesById(projectId);
            IEnumerable<ProjectFileListViewModel> files = projectFiles;
            List<ProjectMemberViewModel> projectMembers = service.FindProjectMembers(projectId);
            IEnumerable<ProjectMemberViewModel> members = projectMembers;
            return View("ComfirmDelete", Tuple.Create(members,files,projectId));
        }

        [Authorize]
        [HttpPost]
        public ActionResult ConfirmDelete(int projectId)
        {
            string userId = User.Identity.GetUserId();
            service.DeleteProject(projectId);
            return RedirectToAction("Index", "Project");
        }
    }
}