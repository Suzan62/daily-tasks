using Mission.Entities;
using Mission.Entities.Models;
using Mission.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories.IRepositories
{
    public interface ILoginRepository
    {
        public UserDetail GetUserProfileDetailById(int userId);
        LoginUserResponseModel LoginUser(LoginUserRequestModel model);
        Task<string> RegisterUser(RegisterUserRequestModel registerUserRequest);
        User GetUserDetailById(int id);
        string UpdateUser(UserDetails updatedUser);
        Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel);
    
    }
}
