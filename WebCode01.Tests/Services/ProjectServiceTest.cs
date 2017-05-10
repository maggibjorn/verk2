using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCode01.Services;
using WebCode01.Entities;
using System.Data.Entity;
using WebCode01.Models;
using Microsoft.AspNet.Identity.EntityFramework;





namespace WebCode01.Tests.Services
{
    /// <summary>
    /// Unit testing for ProjectService class
    /// </summary>
   
    [TestClass]
    public class ProjectServiceTest
    {
        private ProjectService service;
        

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

            service = new ProjectService(mockDb);

        }
       
        /// <summary>
        /// John has three projects, GetUserProjectList has to return 3 projects when John's id is passed as parameter
        /// </summary>
        [TestMethod]
        public void TestGetUserProjectListJohn()
        {
            // Arrange:
            const string userId = "a"; // The id of user John

            // Act:
            var result = service.getUserProjectList(userId);

            // Assert:
            Assert.AreEqual(3, result.Count); // John has three projects so GetUserProjectList needs to return 3
        }

        /// <summary>
        /// Lue has two projects so GetUserProjectList needs to return 2 when Lue's id is passed as parameter.
        /// John owns all the projects Lue has so it´s important to see if the function returns 2 items
        /// </summary>
        [TestMethod]
        public void TestGetUserProjectListLue()
        {
            // Arrange:
            const string userId = "b"; // The id of user Lue

            // Act:
            var result = service.getUserProjectList(userId);

            // Assert:
            Assert.AreEqual(2, result.Count); // John has two projects so GetUserProjectList needs to return 2
        }

        /// <summary>
        /// Sarah is a user, but she doesnt own any projects. GetUserProjectList needs to return zero elements
        /// </summary>
        [TestMethod]
        public void TestGetUserProjectListSarah()
        {
            // Arrange:
            const string userId = "c"; // The id of user Sarah

            // Act:
            var result = service.getUserProjectList(userId);

            // Assert:
            Assert.AreEqual(0, result.Count); // John has two projects so GetUserProjectList needs to return 2
        }

        /// <summary>
        /// John is author of two projects so FilterProjects(true) needs to return 2 projects
        /// </summary>
        [TestMethod]
        public void TestFilterProjectTrue()
        {
            // Arrange:
            const string userId = "a"; // The id of user John
            var state = true;

            // Act:
            var result = service.FilterProjects(userId, state);

            // Assert:
            Assert.AreEqual(2, result.Count);

        }

        /// <summary>
        /// John is member of one project (not author) so FilterProjects(false) needs to return 1 project.
        /// The project name is code
        /// </summary>
        [TestMethod]
        public void TestFilterProjectFalse()
        {
            // Arrange:
            const string userId = "a"; // The id of user John
            var state = false;

            // Act:
            var result = service.FilterProjects(userId, state);

            // Assert:
            Assert.IsTrue(result.Count == 1 && result[0].name == "code");

        }




    }

}
