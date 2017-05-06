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
                             fileName = f.name,

                         }).ToList();
            return files;
        }

        //public List<ProjectFileViewModel>

        /*public void addFileToDb(ProjectFileViewModel model)
        {
            byte[] uploadedFile = new byte[model.file.InputStream.Length];
            model.file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
            File newFile = new File
            {
                name = model.fileName,
                fileBinary = uploadedFile,
                projectId = 1
            };
            db.files.Add(newFile);
            db.SaveChanges();

        }*/
    }
}