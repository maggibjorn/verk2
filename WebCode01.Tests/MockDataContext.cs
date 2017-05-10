using System.Data.Entity;
using WebCode01.Models;
using WebCode01.Entities;
using FakeDbSet;



namespace WebCode01.Tests
{
    class MockDataContext : IAppDataContext
    {
        /// <summary>
        /// Sets up the fake database.
        /// </summary>
        public MockDataContext()
        {
            // We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
            
            members = new InMemoryDbSet<Member>();
            projects = new InMemoryDbSet<Project>();
            types = new InMemoryDbSet<FileType>();
            files = new InMemoryDbSet<File>();
            comments = new InMemoryDbSet<Comment>();
            Users = new InMemoryDbSet<ApplicationUser>();

        }

        public IDbSet<Member> members { get; set; }
        public IDbSet<Project> projects { get; set; }
        public IDbSet<FileType> types { get; set; }
        public IDbSet<File> files { get; set; }
        public IDbSet<Comment> comments { get; set; }
        public IDbSet<ApplicationUser> Users { get; set; }



        public int SaveChanges()
        {
            // Pretend that each entity gets a database id when we hit save.
            int changes = 0;

            return changes;
        }

        public void Dispose()
        {
            // Do nothing!
        }
    }
}
