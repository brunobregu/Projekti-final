using Academy.DataLayer;
using Academy.DomainModels;
using Academy.RepositoryInterface;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Repository
{
    public class UserRepository : IUserRepository
    {
        ProjectDbContext db;
        ApplicationUserStore userStore;
        ApplicationUserManager userManager;
        public UserRepository()
        {
            this.db = new ProjectDbContext();
            this.userStore = new ApplicationUserStore(db);
            this.userManager = new ApplicationUserManager(userStore);
        }


        public bool CreateUser(ApplicationUser u, string role)
        {
            //kontrollojme nqs ka usera qe kane te njetin email ose username si ai i userit qe po shtohet
            List<ApplicationUser> searchUsers = db.Users.Where(m => m.UserName == u.UserName || m.Email == u.Email).ToList();
            if (searchUsers.Count > 0)
                return false;

            IdentityResult result = userManager.Create(u);
            userManager.AddToRole(u.Id, role);
            return result.Succeeded;
        }



        public ApplicationUser GetUserById(string id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }


        public bool UpdateUser(ApplicationUser u)
        {
            // kontrollojme nqs ka usera qe kane te njetin email ose username si ai i userit qe po shtohet
            List<ApplicationUser> searchUsers = db.Users.Where(m => m.UserName == u.UserName && m.Email == u.Email && m.FirstName == u.FirstName && m.LastName == u.LastName).ToList();
            if (searchUsers.Count > 0)
                return false;
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == u.Id);
            currentUser.FirstName = u.FirstName;
            currentUser.LastName = u.LastName;
            currentUser.Email = u.Email;
            currentUser.UserName = u.UserName;
            db.SaveChanges();
            return true;
        }



        public void DisableUser(string id)
        {
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == id);
            currentUser.Enable = false;
            db.SaveChanges();
        }



        public void EnableUser(string id)
        {
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == id);
            currentUser.Enable = true;
            db.SaveChanges();
        }


        public List<ApplicationUser> GetAllEnableUsers(string id)
        {
            return db.Users.Where(x => x.Id != id && x.Enable == true).ToList();
        }


        public List<ApplicationUser> GetAllDisableUsers(string id)
        {
            return db.Users.Where(x => x.Id != id && x.Enable == false).ToList();
        }
    }
}
