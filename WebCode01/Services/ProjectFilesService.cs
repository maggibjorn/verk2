using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCode01.Models;
using WebCode01.ViewModels;
using WebCode01.Entities;

namespace WebCode01.Services
{
    public class ProjectFilesService
    {
        private ApplicationDbContext db;
        public ProjectFilesService()
        {
            db = new ApplicationDbContext();
        }

        public List<ProjectFileListViewModel> getProjectFilesById(int projectId)
        {
            var files = (from f in db.files
                         join p in db.projects on f.projectId equals p.id
                         where p.id == projectId
                         select new ProjectFileListViewModel
                         {
                             id = f.id,
                             fileName = f.name,

                         }).ToList();
            return files;
        }
        
        public ProjectFileViewModel getFileById(int fileId)
        {
            var fileDetails = (from f in db.files
                              where f.id == fileId
                              select new ProjectFileViewModel
                              {
                                  id = f.id,
                                  fileContent = f.fileContent
                              }).FirstOrDefault();

            return fileDetails;

        }

        public void saveCodeToDb(ProjectFileViewModel model)
        {
            var file = (from f in db.files
                        where f.id == model.id
                        select f).FirstOrDefault();
            file.fileContent = model.fileContent;
            db.SaveChanges();

        }

        

        
    }
}