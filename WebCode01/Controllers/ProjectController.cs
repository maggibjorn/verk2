﻿using System;
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
        public ProjectService service = new ProjectService();
        // GET: Project
        public ActionResult Index()
        { 
            string id = User.Identity.GetUserId(); // Get current user id
            List<ProjectListViewModel> model = service.getUserProjectList(id);
            IEnumerable<ProjectListViewModel> modelList = model;
            return View(modelList);
        }

        // Filter the user project list by member projects and user projects
        public ActionResult FilterProjects(bool isAuthor)
        {
            string id = User.Identity.GetUserId(); // Get current user id
            List<ProjectListViewModel> model = service.FilterProjects(id, isAuthor);
            IEnumerable<ProjectListViewModel> modelList = model;
            return View("Index", modelList);
        }

        public ActionResult CreateProject()
        {
            var model = new CreateProjectViewModel();
            return View(model);
        }

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

    }
}