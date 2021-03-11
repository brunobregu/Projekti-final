using Academy.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.RepositoryInterface
{
    public interface IUserRepository
    {
        bool CreateUser(ApplicationUser u, string role);



        ApplicationUser GetUserById(string id);



        bool UpdateUser(ApplicationUser u);


        void DisableUser(string id);



        void EnableUser(string id);




        List<ApplicationUser> GetAllEnableUsers(string id);




        List<ApplicationUser> GetAllDisableUsers(string id);
    }
}
