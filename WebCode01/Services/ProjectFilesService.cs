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

        /// <summary>
        /// Function that returns all files in certain user project.
        /// </summary>
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

        /// <summary>
        /// Function that returns file when given file id as parameter.
        /// </summary>
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

        /// <summary>
        /// Function that returns user given user id as parameter.
        /// </summary>
        public string GetUserNameById(string userId)
        {
            var userName = (from u in db.Users
                            where u.Id == userId
                            select u.UserName).FirstOrDefault();

            return userName;
        }

        /// <summary>
        /// Function that filters files in project by type.
        /// Javascript, c++ etc.
        /// </summary>
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

        /// <summary>
        /// Function that saves code in file to database.
        /// Calles each time the editor of file is changed so no need to
        /// push button to save file.
        /// </summary>
        public void SaveCodeToDb(ProjectFileViewModel model)
        {
            var file = (from f in db.files
                        where f.id == model.id
                        select f).FirstOrDefault();
            file.fileContent = model.fileContent;
            db.SaveChanges();

        }

        /// <summary>
        /// Function that saves uploaded user file to database.
        /// The user can upload file from his computer and save it on the website.
        /// </summary>
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

        /// <summary>
        /// Function that adds new member to project.
        /// </summary>
        public void AddMember(AddMemberViewModel model)
        {
            var userId = (from u in db.Users
                       where model.userEmail == u.Email
                       select u.Id).FirstOrDefault();

            Member newMember = new Member()
            {
                projectId = model.projectId,
                userId = userId,
                isAuthor = false
            };
            db.members.Add(newMember);
            db.SaveChanges();
        }

        /// <summary>
        /// Function that saves blank file created by the user on the 
        /// website to database.
        /// </summary>
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

        /// <summary>
        /// Function that returns true if filename is aldready in project, else false.
        /// </summary>
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

        /// <summary>
        /// Function that finds all members of certain project.
        /// </summary>
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

        /// <summary>
        /// Function that searches for member to add to project.
        /// Used to prevent adding member that doesn't exist on website.
        /// </summary>
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

        /// <summary>
        /// Function that fetches all members in project with project id passed as parameter.
        /// </summary>
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

        /// <summary>
        /// Returns true if member is already in project else false.
        /// Used so user cannot add the same member many times to the same project.
        /// </summary>
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

        /// <summary>
        /// Function that takes care of deleteing project.
        /// Deletes all information corresponding to the project.
        /// </summary>
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

        /// <summary>
        /// Function that takes care of removing member from certain project.
        /// </summary>
        public void DeleteMemberFromProject(string email, int projectId)
        {
            var userId = (from u in db.Users
                          where u.Email == email
                          select u.Id).FirstOrDefault();

            var project = (from m in db.members
                           where m.userId == userId && m.projectId == projectId
                           select m).FirstOrDefault();

            db.members.Remove(project);
            db.SaveChanges();
        }

        //-----Helper functions-----//

        /// <summary>
        /// Returns id of file type given extension name.
        /// </summary>
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

        /// <summary>
        /// Returns file extension string given the programming language name.
        /// </summary>
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


    }
}