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
                             projectId = p.id

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
        public void saveFileToDb(AddFileViewModel model)
        {   
            List<string> file = new List<string>();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(model.file.InputStream))
            {
                while (!reader.EndOfStream)
                {
                    file.Add(reader.ReadLine());
                }
            }
            string content = string.Join(Environment.NewLine, file.ToArray());
            File newFile = new File
            {
                name = model.file.FileName,
                projectId = model.projectId,
                fileContent = content
            };
            db.files.Add(newFile);
            db.SaveChanges();

        }
        public void AddMember(AddMemberViewModel model)
        {
            var userId = (from u in db.Users
                       where model.userEmail == u.Email
                       select u.Id).FirstOrDefault();
            Member newMember = new Member()
            {
                projectId = model.projectId,
                userId = userId
            };
            db.members.Add(newMember);
            db.SaveChanges();
        }

    }
}