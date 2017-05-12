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
        
        /// <summary>
        /// Function that returns all projects of certain user.
        /// </summary>
        public List<ProjectListViewModel> GetUserProjectList(string id)
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

        /// <summary>
        /// Function that filter user projects by author/member projects.
        /// </summary>
        public List<ProjectListViewModel> FilterProjects(string id, bool isAuthor)
        {
            var filter = (from p in db.projects
                                join m in db.members on p.id equals m.projectId
                                join u in db.Users on m.userId equals u.Id                               
                                where u.Id == id && m.isAuthor == isAuthor
                                select new ProjectListViewModel
                                {
                                    name = p.name,
                                    id = p.id                                    
                                }).ToList();   
                     
            return filter;
        }

        /// <summary>
        /// Function that adds new project to database.
        /// The project is owned be specific user.
        /// </summary>
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

        /// <summary>
        /// Function that searches specific user projects.
        /// User types in search value and function returns all projects containing the search value.
        /// </summary>
        public List<ProjectListViewModel> ProjectSearch(string id, string searchValue)
        {
            var userProjects = GetUserProjectList(id); // Get user project list

            var result = (from u in userProjects
                          where u.name.ToLower().Contains(searchValue.ToLower())
                          select u).ToList();

            return result;
        }

        /// <summary>
        /// Function that returns all author projects of user
        /// and all members in each project.
        /// </summary>
        public List<KickMemberViewModel> GetMyProjectsAndMembers(string userId, bool isAuthor)
        {
            var authorProjects = FilterProjects(userId, true);

            List<KickMemberViewModel> members = new List<KickMemberViewModel>();
            foreach(var item in authorProjects)
            {
                var getMembers = (from m in db.members
                                  join u in db.Users on m.userId equals u.Id
                                  where m.projectId == item.id && m.userId != userId
                                  select new AddMemberViewModel
                                  {
                                      projectId = m.projectId,
                                      userEmail = u.Email
                                  }).ToList();
                KickMemberViewModel info = new KickMemberViewModel();
                info.name = item.name;
                info.projectId = item.id;
                info.members = getMembers;
                members.Add(info);
            }
            return members;
        }
    }
}