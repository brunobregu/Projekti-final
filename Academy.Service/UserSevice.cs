using Academy.DomainModels;
using Academy.RepositoryInterface;
using Academy.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Service
{
    public class UserSevice : IUserService
    {
        IUserRepository repository;
        public UserSevice(IUserRepository rep)
        {
            this.repository = rep;
        }


        public bool CreateUser(ApplicationUser u, string role)
        {
            return repository.CreateUser(u, role);
        }



        public ApplicationUser GetUserById(string id)
        {
            return repository.GetUserById(id);
        }


        public bool UpdateUser(ApplicationUser u)
        {
            return repository.UpdateUser(u);
        }


        public void DisableUser(string id)
        {
            repository.DisableUser(id);
        }



        public void EnableUser(string id)
        {
            repository.EnableUser(id);
        }


        public List<ApplicationUser> GetAllEnableUsers(string id)
        {
            return repository.GetAllEnableUsers(id);
        }


        public List<ApplicationUser> GetAllDisableUsers(string id)
        {
            return repository.GetAllDisableUsers(id);
        }
    }
}
