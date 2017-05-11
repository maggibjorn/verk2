using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCode01.Entities;
using WebCode01.Models;
using WebCode01.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCode01.ViewModels;

namespace WebCode01.Tests.Services
{
    /// <summary>
    /// Unit testing for ProjectFilesService class
    /// </summary>
    [TestClass]
    public class ProjectFilesServiceTest
    {
        private ProjectFilesService service;

        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDataContext();

            //----Create users----//
            var u1 = new ApplicationUser
            {
                Id = "a",
                UserName = "John@John.com"
            };
            mockDb.Users.Add(u1);
            var u2 = new ApplicationUser
            {
                Id = "b",
                UserName = "Lue@Lue.com"
            };
            mockDb.Users.Add(u2);
            var u3 = new ApplicationUser
            {
                Id = "c",
                UserName = "Sarah@Sarah.com"
            };
            mockDb.Users.Add(u3);
            //----Create projects----//
            var p1 = new Project
            {
                id = 1,
                name = "MyCProject"
            };
            mockDb.projects.Add(p1);
            var p2 = new Project
            {
                id = 2,
                name = "testing101"
            };
            mockDb.projects.Add(p2);
            var p3 = new Project
            {
                id = 3,
                name = "code"
            };
            mockDb.projects.Add(p3);


            //----Create members table----//
            var m1 = new Member
            {
                id = 1,
                projectId = 1,
                userId = "a",
                isAuthor = true

            };
            mockDb.members.Add(m1);
            var m2 = new Member
            {
                id = 2,
                projectId = 2,
                userId = "a",
                isAuthor = true
            };
            mockDb.members.Add(m2);
            var m3 = new Member
            {
                id = 3,
                projectId = 3,
                userId = "a",
                isAuthor = false

            };
            mockDb.members.Add(m3);
            var m4 = new Member
            {
                id = 3,
                projectId = 3,
                userId = "b",
                isAuthor = true
            };
            mockDb.members.Add(m4);
            var m5 = new Member
            {
                id = 4,
                projectId = 1,
                userId = "b",
                isAuthor = false
            };
            mockDb.members.Add(m5);

            //----Create FileTypes table----//
            var t1 = new FileType
            {
                id = 1,
                name = "C#"
            };
            mockDb.types.Add(t1);
            var t2 = new FileType
            {
                id = 2,
                name = "C++"
            };
            mockDb.types.Add(t2);
            var t3 = new FileType
            {
                id = 3,
                name = "Javascript"
            };
            mockDb.types.Add(t3);
            var t4 = new FileType
            {
                id = 4,
                name = "HTML"
            };
            mockDb.types.Add(t4);
            var t5 = new FileType
            {
                id = 5,
                name = "CSS"
            };
            mockDb.types.Add(t5);
            var t6 = new FileType
            {
                id = 6,
                name = "Dat"
            };
            mockDb.types.Add(t6);


            //----Create Files table----//
            var f1 = new File
            {
                id = 1,
                name = "index.html",
                projectId = 1,
                fileContent = "<div>Something clever</div>",
                fileTypeId = 4
            };
            mockDb.files.Add(f1);
            var f2 = new File
            {
                id = 2,
                name = "cstuff.cpp",
                projectId = 1,
                fileContent = "for (int i = 0; i < 10; i++ {}",
                fileTypeId = 2
            };
            mockDb.files.Add(f2);
            var f3 = new File
            {
                id = 3,
                name = "script.js",
                projectId = 1,
                fileContent = "$('.h1').submit...",
                fileTypeId = 3
            };
            mockDb.files.Add(f3);
            var f4 = new File
            {
                id = 4,
                name = "style.CSS",
                projectId = 2,
                fileContent = "div > a {}",
                fileTypeId = 5
            };
            mockDb.files.Add(f4);
            var f5 = new File
            {
                id = 5,
                name = "test.css",
                projectId = 3,
                fileContent = "div > a {color: blue;}",
                fileTypeId = 5
            };
            mockDb.files.Add(f5);
            var f6 = new File
            {
                id = 6,
                name = "sharp.cs",
                projectId = 3,
                fileContent = "var item = 56;",
                fileTypeId = 4
            };
            mockDb.files.Add(f6);
            var f7 = new File
            {
                id = 7,
                name = "testing101.cpp",
                projectId = 3,
                fileContent = "for (int i = 0; i < 5; i++) {// Do Something}",
                fileTypeId = 2
            };
            mockDb.files.Add(f7);
            var f8 = new File
            {
                id = 8,
                name = "stylesheet.CSS",
                projectId = 3,
                fileContent = "<div>Something clever</div>",
                fileTypeId = 5
            };
            mockDb.files.Add(f8);


            service = new ProjectFilesService(mockDb);

        }

        /// <summary>
        /// Test GetProjectFilesById for project with id=2, that project has 1 file with name style.CSS
        /// </summary>
        [TestMethod]
        public void TestGetProjectFilesById()
        {
            // Arrange:
            const int projectId = 2; // The id of the project

            // Act:
            var result = service.GetProjectFilesById(projectId);

            // Assert:
            Assert.IsTrue(result.Count == 1 && result[0].fileName == "style.CSS");            
        }

        /// <summary>
        /// Get file by calling GetFileById(int fileid), id=7 should return 1 file with name: testing101.cpp
        /// </summary>
        [TestMethod]
        public void TestGetFileById()
        {
            // Arrange:
            const int fileId = 7; // The id of the file

            // Act:
            var result = service.GetFileById(fileId);

            // Assert:
            Assert.IsTrue(result.name == "testing101.cpp");
        }
        /// <summary>
        /// Should return NULL so webpage can display corresponding error page
        /// </summary>
        [TestMethod]
        public void TestGetFileByIdDontExists()
        {
            // Arrange:
            const int fileId = 33; // No file has id = 33

            // Act:
            var result = service.GetFileById(fileId);

            // Assert:
            Assert.AreEqual(null, result);
        }

        /// <summary>
        /// Test if GetFilesByType is fetching files in project with id = 1 and of type css 
        /// File extesions both in upper and lower case, should return 2 files
        /// </summary>
        [TestMethod]
        public void TestGetFilesByType()
        {
            // Arrange:
            const int projectId = 3; 
            const string type = "CSS";

            // Act:
            var result = service.GetFilesByType(projectId, type);

            // Assert:
            Assert.AreEqual(2, result.Count);
            foreach (var item in result)
            {
                Assert.AreEqual("css", item.fileName.Split('.')[1].ToLower());
            }
        }

        /// <summary>
        /// Needs to return empty list if project has no file of certain type.
        /// Project with id=2 has no file of type C++
        /// </summary>
        [TestMethod]
        public void TestGetFilesByTypeNoFile()
        {
            // Arrange:
            const int projectId = 2; 
            const string type = "C++";

            // Act:
            var result = service.GetFilesByType(projectId, type);

            // Assert:
            Assert.AreEqual(0, result.Count);
        }

        /// <summary>
        /// If filename is in project return true
        /// </summary>
        [TestMethod]
        public void TestCheckFileNameExists()
        {
            // Arrange:
            const string fileName = "testing101.cpp";
            const int projectId = 3;

            // Act:
            var result = service.CheckFileName(fileName, projectId);

            // Assert:
            Assert.IsTrue(result);
        }

        /// <summary>
        /// If filename is not in project return false
        /// </summary>
        [TestMethod]
        public void TestCheckFileNameDontExist()
        {
            // Arrange:
            const string fileName = "john123.dat"; // this file is not in project with id=1
            const int projectId = 1;

            // Act:
            var result = service.CheckFileName(fileName, projectId);

            // Assert:
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Project with id = 1 has two members with usernames: John@John.com and Lue@Lue.com
        /// </summary>
        [TestMethod]
        public void TestFindProjectMembers()
        {
            // Arrange:
            const int projectId = 1;

            // Act:
            var result = service.FindProjectMembers(projectId);

            // Assert:
            Assert.IsTrue(result.Count == 2 && result[0].name == "John@John.com" && result[1].name == "Lue@Lue.com");
        }

        /// <summary>
        /// Every project in database has at least one member, should never return empty list
        /// </summary>
        [TestMethod]
        public void TestFindProjectMembersGreaterZero()
        {
            // Arrange:
            List<int> projectIds = new List<int> { 1, 2, 3 }; // All project id's in database

            // Act:
            List<int> result = new List<int>();
            foreach (var item in projectIds)
            {
                var test = service.FindProjectMembers(item).Count;
                result.Add(test);
            }
           
            // Assert:
            foreach (var item in result)
            {
                Assert.IsTrue(item > 0); // Always returns a number bigger than 0
            }
            
        }

        /// <summary>
        /// Test if user "John@John.com" is in project 1, should return false.
        /// </summary>
        [TestMethod]
        public void TestIsInProjectFalse()
        {
            // Arrange:
            const int projectId = 1;
            const string userName = "John@John.com";

            // Act:
            var result = service.IsInProject(userName, projectId);

            // Assert:
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test if user "Sarah@Sarah.com" is in project 2, should return false.
        /// </summary>
        [TestMethod]
        public void TestIsInProjectTrue()
        {
            // Arrange:
            const int projectId = 2;
            const string userName = "Sarah@Sarah.com";

            // Act:
            var result = service.IsInProject(userName, projectId);

            // Assert:
            Assert.IsTrue(result);
        }

    }
}
