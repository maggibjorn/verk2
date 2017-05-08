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
            /*var userProjects = (from p in db.projects
                                join m in db.members on p.id equals m.projectId
                                join u in db.Users on m.userId equals u.Id
                                join t in db.types on p.projectTypeId equals t.id
                                where u.Id == id 
                                select new ProjectListViewModel
                                {
                                    id = p.id,
                                    name = p.name,
                                    type = t.name
                                    
                                }).ToList();
                       
            List<ProjectListViewModel> list = new List<ProjectListViewModel>();
            list = userProjects;*/
            return null;
        }

        public List<ProjectListViewModel> FilterProjects(string id, bool isAuthor)
        {
            /*var authorProjects = (from p in db.projects
                                join m in db.members on p.id equals m.projectId
                                join u in db.Users on m.userId equals u.Id
                                join t in db.types on p.projectTypeId equals t.id
                                where u.Id == id && m.isAuthor == isAuthor
                                select new ProjectListViewModel
                                {
                                    name = p.name,
                                    type = t.name

                                }).ToList();*/
            List<ProjectListViewModel> list = new List<ProjectListViewModel>();
            //list = authorProjects;
            return null;
        }

        public List<ProjectListViewModel> GetUserProjectsByType(string id, string type)
        {
            var userProjectsList = getUserProjectList(id); // Get user projects list
            var projectListByType = (from p in userProjectsList
                                     where p.type == type
                                     select p).ToList();
            return projectListByType;
        }
        public void AddProjectToDb(CreateProjectViewModel model, string id)
        {
            Project UsersProject = new Project
            {
                name = model.name,
                //projectTypeId = model.type
            };
            db.projects.Add(UsersProject);
            db.SaveChanges();
            Member newConnection = new Member
            {
                projectId = UsersProject.id,
                userId = id,
                isAuthor = true
            };
            db.members.Add(newConnection);
            db.SaveChanges();
            File firstFile = new File
            {
                name = "Index.txt",
                fileContent = " ",
                projectId = UsersProject.id
            };
            db.files.Add(firstFile);
            db.SaveChanges();
        }
    }
}