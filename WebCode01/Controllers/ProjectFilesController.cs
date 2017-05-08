﻿using System;
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
            List<ProjectFileListViewModel> model = service.getProjectFilesById(projectId);
            if (model.Count > 0)
            {
                ViewBag.projectId = model[0].projectId; // Fetch project id if file list isn't empty, use this in editor view to save file to database
            }
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







        

        

       


    }
}