using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCode01.Models;
using WebCode01.ViewModels;
using WebCode01.Entities;

namespace WebCode01.Services
{
    public class ProjectService
    {
        private ApplicationDbContext db;
        public ProjectService()
        {
            db = new ApplicationDbContext();
        }
        
        public List<ProjectListViewModel> getUserProjectList(string id)
        {
            
            var userProjects = (from p in db.projects
                                join m in db.members on p.id equals m.projectId
                                join u in db.Users on m.userId equals u.Id
                                where u.Id == id 
                                select new ProjectListViewModel
                                {
                                    id = p.id,
                                    name = p.name,
                                    
                                    
                                }).ToList();
                     
            return userProjects;
        }

        public List<ProjectListViewModel> FilterProjects(string id, bool isAuthor)
        {
            var filter = (from p in db.projects
                                join m in db.members on p.id equals m.projectId
                                join u in db.Users on m.userId equals u.Id                               
                                where u.Id == id && m.isAuthor == isAuthor
                                select new ProjectListViewModel
                                {
                                    name = p.name,                                    

                                }).ToList();
            
            return filter;
        }

        public void AddProjectToDb(CreateProjectViewModel model, string id)
        {
            Project usersProject = new Project
            {
                name = model.name
                //projectTypeId = model.type
            };
            db.projects.Add(usersProject);
            db.SaveChanges();
            Member newConnection = new Member
            {
                projectId = usersProject.id,
                userId = id,
                isAuthor = true
            };
            db.members.Add(newConnection);
            db.SaveChanges();
            File firstFile = new File
            {
                name = "index.html",
                fileContent = "",
                projectId = usersProject.id,
                fileTypeId = 4 // The HTML type Id
            };
            db.files.Add(firstFile);
            db.SaveChanges();
        }
    }
}