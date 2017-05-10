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
                                   name = f.name,
                                   fileContent = f.fileContent
                               }).FirstOrDefault();

            return fileDetails;

        }

        public List<ProjectFileListViewModel> GetFilesByType(int projectId, string type)
        {
            var filesByType = (from f in db.files
                               join p in db.projects on f.projectId equals p.id
                               join t in db.types on f.fileTypeId equals t.id
                               where t.name == type && p.id == projectId
                               select new ProjectFileListViewModel
                               {
                                   id = f.id,
                                   fileName = f.name,
                                   projectId = p.id

                               }).ToList();
            return filesByType;
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
            // Check file type
            string fileType = model.file.FileName.Split('.')[1].ToLower();
            int typeId;
            if (fileType == "cs")
            {
                typeId = 1;
            }
            else if (fileType == "cpp")
            {
                typeId = 2;
            }
            else if (fileType == "js")
            {
                typeId = 3;
            }
            else if (fileType == "html")
            {
                typeId = 4;
            }
            else if (fileType == "css")
            {
                typeId = 5;
            }
            else
            {
                typeId = 6; // Assign file as dat file if exstension in unknown
            }
            string content = string.Join(Environment.NewLine, file.ToArray());
            File newFile = new File
            {
                name = model.file.FileName,
                projectId = model.projectId,
                fileContent = content,
                fileTypeId = typeId
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

        public void SaveBlankFileToDb(CreateBlankFileViewModel model)
        {
            var typeId = (from t in db.types
                          where t.name.ToLower() == model.fileType.ToLower()
                          select t.id).FirstOrDefault();
            File file = new File
            {
                name = model.fileName,
                fileContent = "",
                fileTypeId = typeId,
                projectId = model.projectId
            };

            db.files.Add(file);
            db.SaveChanges();
        }

        public List<ProjectMemberViewModel> FindProjectMembers(int projectId)
        {
            var projectMembers = (from m in db.members
                                  join p in db.projects on m.projectId equals p.id
                                  join u in db.Users on m.userId equals u.Id
                                  where m.projectId == projectId
                                  select new ProjectMemberViewModel
                                  {
                                      name = u.UserName
                                  }).ToList();
            return projectMembers;
        }

        //-----Helper functions-----//

        public int checkFileType(string ext)
        {
            int id;
            if (ext == "cs")
            {
                id = 1;
            }
            else if (ext == "cpp")
            {
                id = 2;
            }
            else if (ext == "js")
            {
                id = 3;
            }
            else if (ext == "html")
            {
                id = 4;
            }
            else if (ext == "css")
            {
                id = 5;
            }
            else
            {
                id = 6; // Assign file as dat file if exstension in unknown
            }

            return id;
        }




    }
}