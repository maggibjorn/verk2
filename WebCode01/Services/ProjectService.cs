using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCode01.Models;
using WebCode01.ViewModels;

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
                                join t in db.types on p.projectTypeId equals t.id
                                where u.Id == id 
                                select new ProjectListViewModel
                                {
                                    name = p.name,
                                    type = t.name
                                    
                                }).ToList();
                       
            List<ProjectListViewModel> list = new List<ProjectListViewModel>();
            list = userProjects;
            return list;
        }

        public List<ProjectListViewModel> FilterProjects(string id, bool isAuthor)
        {
            var authorProjects = (from p in db.projects
                                join m in db.members on p.id equals m.projectId
                                join u in db.Users on m.userId equals u.Id
                                join t in db.types on p.projectTypeId equals t.id
                                where u.Id == id && m.isAuthor == isAuthor
                                select new ProjectListViewModel
                                {
                                    name = p.name,
                                    type = t.name

                                }).ToList();
            List<ProjectListViewModel> list = new List<ProjectListViewModel>();
            list = authorProjects;
            return list;
        }
    }
}