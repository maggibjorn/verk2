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
        private readonly IAppDataContext db;
        
        public ProjectService(IAppDataContext context)
        {
            db = context ?? new ApplicationDbContext();
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
                fileTypeId = 4 // The HTML type Id, first file of project is always html
            };
            db.files.Add(firstFile);
            db.SaveChanges();
        }

        // Search project list of user
        public List<ProjectListViewModel> ProjectSearch(string id, string searchValue)
        {
            var userProjects = getUserProjectList(id); // Get user project list

            var result = (from u in userProjects
                          where u.name.ToLower().Contains(searchValue.ToLower())
                          select u).ToList();

            return result;
        }
    }
}