using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebCode01.Services;
using WebCode01.ViewModels;

namespace WebCode01.Controllers
{
    public class ProjectController : Controller
    {
        public ProjectService service = new ProjectService(null);
        /// <summary>
        /// Action returns list of all user projects.
        /// </summary>
        [Authorize]
        public ActionResult Index()
        { 
            string id = User.Identity.GetUserId(); // Get current user id
            
            List<ProjectListViewModel> model = service.GetUserProjectList(id); // The list can be empty (no user projects)
            IEnumerable<ProjectListViewModel> modelList = model;
            return View(modelList);
        }

        /// <summary>
        /// isAuthor=true returns user author projects, 
        /// isAuthor=false returns user member projects.
        /// </summary>
        [Authorize]
        public ActionResult FilterProjects(bool isAuthor)
        {
            string id = User.Identity.GetUserId(); // Get current user id
            List<ProjectListViewModel> model = service.FilterProjects(id, isAuthor); // The list can be empty 
            IEnumerable<ProjectListViewModel> modelList = model;
            return View("Index", modelList);
        }

        /// <summary>
        /// Get action for creating new user project.
        /// </summary>
        [Authorize]
        public ActionResult CreateProject()
        {
            var model = new CreateProjectViewModel();
            return View(model);
        }

        /// <summary>
        /// Post action for creating new user project.
        /// </summary>
        [HttpPost]
        public ActionResult CreateProject(CreateProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string id = User.Identity.GetUserId(); // Get current user id
            service.AddProjectToDb(model, id);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action that returns list of searched user projects.
        /// searchValue variable is the keyword.
        /// </summary>
        [Authorize]
        public ActionResult ProjectSearch(FormCollection coll)
        {
            string id = User.Identity.GetUserId(); // Get current user id
            string searchValue = coll["search"]; // Retrieve the search value
            
            List<ProjectListViewModel> model = service.ProjectSearch(id, searchValue);
            IEnumerable<ProjectListViewModel> modelList = model;
            return View("Index", modelList);
        }

        /// <summary>
        /// Action that returns list of all author projects.
        /// The user can choose to delete his author projects.
        /// The deletion takes place in ProjectFilesController.
        /// Because there are already service classes in ProjectFilesController
        /// that can handle the deletion.
        /// </summary>
        [Authorize]
        public ActionResult DeleteProject()
        {
            string userId = User.Identity.GetUserId();
            List<ProjectListViewModel> myProjects = service.FilterProjects(userId, true);
            IEnumerable<ProjectListViewModel> modelList = myProjects;
            return View("DeleteProject", modelList);
        }

        /// <summary>
        /// Action that returns list of all author projects.
        /// The user can choose to kick members from his author projects.
        /// The kicking takes place in ProjectFilesController.
        /// Because there are already service classes in ProjectFilesController
        /// that can handle the kicking.
        /// </summary>
        public ActionResult KickMember()
        {
            string userId = User.Identity.GetUserId();
            List<KickMemberViewModel> myProjects = service.GetMyProjectsAndMembers(userId, true);
            IEnumerable<KickMemberViewModel> modelList = myProjects;
            return View("KickMember", modelList);
        }
    }
}