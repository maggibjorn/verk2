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
        private readonly IAppDataContext db;

        public ProjectFilesService(IAppDataContext context)
        {
            db = context ?? new ApplicationDbContext();
        }

        public List<ProjectFileListViewModel> GetProjectFilesById(int projectId)
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

        public ProjectFileViewModel GetFileById(int fileId)
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

        public void SaveCodeToDb(ProjectFileViewModel model)
        {
            var file = (from f in db.files
                        where f.id == model.id
                        select f).FirstOrDefault();
            file.fileContent = model.fileContent;
            db.SaveChanges();

        }
        public void SaveFileToDb(AddFileViewModel model)
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
            int typeId = checkFileType(fileType);
            
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
        public void AddMember(AddMemberViewModel model, int? Id)
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

            string ext = GetFileExstension(model.fileType.ToLower());
            File file = new File
            {
                name = model.fileName + "." + ext,
                fileContent = "",
                fileTypeId = typeId,
                projectId = model.projectId
            };
            db.files.Add(file);
            db.SaveChanges();
        }
        public bool CheckFileName(string fileName, int projectId)
        {
            var name = (from u in db.files
                        where u.projectId == projectId && u.name == fileName
                        select u).ToList();

            if(name.Count() == 0)
            {
                return false;
            }

            return true;
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

        public string GetFileExstension(string fileType)
        {
            string ext;
            if (fileType == "c#")
            {
                ext = "cs";
            }
            else if (fileType == "c++")
            {
                ext = "cpp";
            }
            else if (fileType == "javascript")
            {
                ext = "js";
            }
            else if (fileType == "html")
            {
                ext = "html";
            }
            else if (fileType == "css")
            {
                ext = "css";
            }
            else
            {
                ext = "cs"; // Assign file as dat file if fileType in unknown
            }

            return ext;
        }

        public List<AddMemberViewModel> MemberSearch(string searchValue, int projectId)
        {
            var result = (from u in db.Users
                          where u.Email.ToLower().Contains(searchValue.ToLower())
                          select new AddMemberViewModel
                          {
                              userEmail = u.Email,
                              projectId = projectId
                          }).ToList();
            
            return result;
        }

        public List<AddMemberViewModel> GetMembers(int projectId)
        {
            var members = (from m in db.members
                           join u in db.Users on m.userId equals u.Id
                           where m.id == projectId
                           select new AddMemberViewModel
                           {
                               projectId = m.id,
                               userEmail = u.Email
                           }).ToList();

            return members;
        }

        public bool IsInProject(string email, int projectId)
        {
            var userId = (from u in db.Users
                          where u.UserName == email
                          select u.Id).FirstOrDefault();

            var test = (from m in db.members
                        where m.userId == userId && m.projectId == projectId
                        select m).ToList();

            if (test.Count == 0)
            {
                return true;
            }
            return false;
        }

        public void DeleteProject(int projectId)
        {
            var deleteConnection = (from m in db.members
                                    where m.projectId == projectId
                                    select m).ToList();
            foreach(var item in deleteConnection)
            {
                db.members.Remove(item);
                db.SaveChanges();
            }
            var deleteFiles = (from f in db.files
                                    where f.projectId == projectId
                                    select f).ToList();
            foreach (var item in deleteFiles)
            {
                db.files.Remove(item);
                db.SaveChanges();
            }
            var deleteProject = (from p in db.projects
                                 where p.id == projectId
                                 select p).FirstOrDefault();
            db.projects.Remove(deleteProject);
            db.SaveChanges();
        }

    }
}