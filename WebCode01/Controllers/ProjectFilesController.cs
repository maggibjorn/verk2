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
        /// <summary>
        /// Action that returns all files in corresponding project.
        /// </summary>
        [Authorize]
        public ActionResult Index(int projectId)
        {            
            List<ProjectFileListViewModel> model = service.GetProjectFilesById(projectId);
            if (model.Count > 0)
            {
                //ViewBag.projectId = model[0].projectId; // Fetch project id if file list isn't empty, use this in editor view to save file to database
            }
            IEnumerable<ProjectFileListViewModel> modelList = model;
            return View(Tuple.Create(modelList,projectId));
        }

        /// <summary>
        /// Action that filters the file list by file type, c++ javascript etc.
        /// </summary>
        [Authorize]
        public ActionResult FilesByType(int projectId, string fileType)
        {
            List<ProjectFileListViewModel> model = service.GetFilesByType(projectId, fileType);
            IEnumerable<ProjectFileListViewModel> modelList = model;
            return View("Index", Tuple.Create(modelList, projectId));
        }
    
        /// <summary>
        /// Action that returns the editor. 
        /// Sends important variables to view to handle javascript code.
        /// </summary>
        [Authorize]
        public ActionResult Editor(int fileId)
        {
            ProjectFileViewModel model = service.GetFileById(fileId);
            ViewBag.id = fileId;
            ViewBag.name = model.name;
            ViewBag.Code = model.fileContent;
            @ViewBag.userId = User.Identity.GetUserId();
            string userId = User.Identity.GetUserId(); ;
            ViewBag.userName = service.GetUserNameById(userId);         
            return View();
        }

        /// <summary>
        /// Get action for adding file to project. 
        /// Used when user uploads file from his computer.
        /// </summary>
        [Authorize]
        public ActionResult AddFile(int projectId)
        {
            ViewBag.projectId = projectId;
            return View();
        }

        /// <summary>
        /// Post action for adding file to project. 
        /// Used when user uploads file from his computer.
        /// </summary>
        [HttpPost]
        public ActionResult AddFile(AddFileViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            bool nameTaken = service.CheckFileName(model.file.FileName, model.projectId);

            if(nameTaken == true)
            {
            return RedirectToAction("Index", new { projectId = model.projectId });
            }
            service.SaveFileToDb(model);
            return RedirectToAction("Index", new { projectId = model.projectId });
        }

        /// <summary>
        /// Get action for creating blank file on website of certain type, c++ javascript etc.
        /// </summary>
        [Authorize]
        public ActionResult CreateBlankFile(int projectId)
        {
            ViewBag.projectId = projectId;
            return View();
        }

        /// <summary>
        /// Post action for creating blank file on website of certain type, c++ javascript etc.
        /// </summary>
        [HttpPost]
        public ActionResult CreateBlankFile(FormCollection coll)
        {
            string fName = coll["fileName"];
            string type = coll["type"];
            string ext = service.GetFileExstension(type);
            string projectId = coll["projectId"];
            int numId;
            bool test = Int32.TryParse(projectId, out numId);
            if (!test)
            {
                return View("Error"); // Return error view if parse doesn't work
            }
            CreateBlankFileViewModel model = new CreateBlankFileViewModel
            {
                fileName = fName + "." + ext, // Add file exstension to name
                projectId = numId,
                fileType = type
            };
    
            bool nameTaken = service.CheckFileName(model.fileName, model.projectId); //check if name is taken
            if (string.IsNullOrEmpty(model.fileName)|| nameTaken == true)
            {
                return RedirectToAction("Index", new { projectId = model.projectId });
            }
            service.SaveBlankFileToDb(model);
            return RedirectToAction("Index", new { projectId = numId});
        }

        /// <summary>
        /// Action that returns all project members to view.
        /// </summary>
        [Authorize]
        public ActionResult ProjectMembers(int projectId)
        {
            ViewBag.projectId = Request.Url.ToString().Split('=')[1]; // Use this to make "back to project" button
            List<ProjectMemberViewModel> model = service.FindProjectMembers(projectId);
            IEnumerable<ProjectMemberViewModel> modelList = model;
            return View(modelList);
        }

        /// <summary>
        /// Action that returns list of all project members to view.
        /// The user can then decide which member to delete from project.
        /// </summary>
        [Authorize]
        public ActionResult AddMember(int projectId)
        {
            List<AddMemberViewModel> members = service.GetMembers(projectId);
            IEnumerable<AddMemberViewModel> modelList = members;
            return View(Tuple.Create(projectId, modelList));
        }

        /// <summary>
        /// Action that adds new member to project.
        /// </summary>
        [Authorize]
        public ActionResult AddThisMember(string email, int Id)
        {

            AddMemberViewModel model = new AddMemberViewModel();
            model.projectId = Id;
            model.userEmail = email;

            bool checkIfMemberExist = service.IsInProject(email, Id);
            if(checkIfMemberExist == true)
            {
                service.AddMember(model);
            }

            return RedirectToAction("ProjectMembers", new { projectId = Id });
        }

        /// <summary>
        /// Action that searches for member in certain project.
        /// Used to prevent user errors.
        /// User cannot add unexisting user to project.
        /// </summary>
        [Authorize]
        [HttpPost]
        public ActionResult SearchMember(FormCollection coll, int Id)
        {
            string searchValue = coll["search"];
            List<AddMemberViewModel> emails = service.MemberSearch(searchValue, Id);
            IEnumerable<AddMemberViewModel> modelList = emails;
            return View("AddMember",Tuple.Create(Id, modelList));
        }

        /// <summary>
        /// Action for confirming deletion of project.
        /// Grammar invalid, reveived errors when changing name of action and corresponding view.
        /// </summary>
        [Authorize]
        public ActionResult ComfirmDelete(int projectId)
        {
            List<ProjectFileListViewModel> projectFiles = service.GetProjectFilesById(projectId);
            IEnumerable<ProjectFileListViewModel> files = projectFiles;
            List<ProjectMemberViewModel> projectMembers = service.FindProjectMembers(projectId);
            IEnumerable<ProjectMemberViewModel> members = projectMembers;
            return View("ComfirmDelete", Tuple.Create(members,files,projectId));
        }

        /// <summary>
        /// Post action that takes care of deleting certain project.
        /// </summary>
        [Authorize]
        [HttpPost]
        public ActionResult ConfirmDelete(int projectId)
        {
            string userId = User.Identity.GetUserId();
            service.DeleteProject(projectId);
            return RedirectToAction("Index", "Project");
        }

        /// <summary>
        /// Action that returns view that lists the member being kicked.
        /// </summary>
        [Authorize]
        public ActionResult KickThisMember(int projectId, string email, string projectName)
        {
            ProjectMemberViewModel kickThisMember = new ProjectMemberViewModel();
            kickThisMember.name = email;
            return View("KickThisMember", Tuple.Create(kickThisMember,projectId,projectName));
        }

        /// <summary>
        /// Action that takes care of actually kicking the member from project.
        /// </summary>
        public ActionResult ConfirmKick(string email, int projectId)
        {
            service.DeleteMemberFromProject(email, projectId);
            return RedirectToAction("ProjectMembers", new { projectId = projectId });
        }
    }
}