using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebCode01.Entities;


namespace WebCode01.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {       
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    /// <summary>
    /// Virtual class to be able to create mock database 
    /// </summary>
    public interface IAppDataContext
    {
        IDbSet<Member> members { get; set; }
        IDbSet<Project> projects { get; set; }
        IDbSet<FileType> types { get; set; }
        IDbSet<File> files { get; set; }
        IDbSet<Comment> comments { get; set; }
        IDbSet<ApplicationUser> Users { get; set; } // This variable has to be in Pascal casing 
        int SaveChanges();
        
    }

    /// <summary>
    /// Takes care of all database access in the system.
    /// Used in service classes. 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDataContext
    {
        public IDbSet<Member> members { get; set; }
        public IDbSet<Project> projects { get; set; }
        public IDbSet<FileType> types { get; set; }
        public IDbSet<File> files { get; set; }
        public IDbSet<Comment> comments { get; set; }
      

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}