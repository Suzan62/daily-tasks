using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Entity.Entities;
using Mission.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories
{
    public class LoginRepository(MissionDbContext cIDbContext) : ILoginRepository
    {
        private readonly MissionDbContext _cIDbContext = cIDbContext;

        public LoginUserResponseModel LoginUser(LoginUserRequestModel model)
        {
            var existingUser = _cIDbContext.User
                .FirstOrDefault(u => u.EmailAddress.ToLower() == model.EmailAddress.ToLower() && !u.IsDeleted);

            if (existingUser == null)
            {
                return new LoginUserResponseModel() { Message = "Email Address Not Found." };
            }
            if (existingUser.Password != model.Password)
            {
                return new LoginUserResponseModel() { Message = "Incorrect Password." };
            }

            return new LoginUserResponseModel
            {
                Id = existingUser.Id,
                FirstName = existingUser.FirstName ?? string.Empty,
                LastName = existingUser.LastName ?? string.Empty,
                PhoneNumber = existingUser.PhoneNumber,
                EmailAddress = existingUser.EmailAddress,
                UserType = existingUser.UserType,
                UserImage = existingUser.UserImage != null ? existingUser.UserImage : string.Empty,
                Message = "Login Successfully"
            };
        }


        public async Task<string> RegisterUser(RegisterUserRequestModel registerUserRequest)
        {
            var isEmailExist = await _cIDbContext.User.FirstOrDefaultAsync(u => u.EmailAddress.ToLower() == registerUserRequest.EmailAddress.ToLower());

            if (isEmailExist != null) throw new Exception("User already exist");

            User user = new User()
            {
                FirstName = registerUserRequest.FirstName,
                LastName = registerUserRequest.LastName,
                EmailAddress = registerUserRequest.EmailAddress,
                Password = registerUserRequest.Password,
                PhoneNumber = registerUserRequest.PhoneNumber,
                UserType = registerUserRequest.UserType,
                UserImage = "default.png",
                CreatedDate = DateTime.UtcNow,
            };

            await _cIDbContext.User.AddAsync(user);
            await _cIDbContext.SaveChangesAsync();
            return "User registered!";
        }
        public User GetUserDetailById(int id)
        {
            return _cIDbContext.User.FirstOrDefault(u => u.Id == id);
        }
        public string UpdateUser(UserDetails updatedUser)
        {
            var user = _cIDbContext.User.FirstOrDefault(x => x.Id == updatedUser.Id && !x.IsDeleted);

            if (user == null)
                throw new Exception("Account doesn't exist!");

            // Update only allowed fields
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.EmailAddress = updatedUser.EmailAddress;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.ModifiedDate = DateTime.Now;

            _cIDbContext.User.Update(user);
            _cIDbContext.SaveChanges();

            return "User updated successfully!";
        }



        public UserDetail GetUserProfileDetailById(int userId)
        {
            var userDetail = _cIDbContext.UserDetails.FirstOrDefault(u => u.UserId == userId);

            if (userDetail == null) return null; 

            userDetail.Availability ??= "Not specified";
            userDetail.LinkedInUrl ??= "No LinkedIn URL";

            return userDetail;
        }


        //public UserDetail GetUserProfileDetailById(int userId)
        //{
        //    return _cIDbContext.UserDetails.FirstOrDefault(u => u.UserId == userId);

        //}
     
        public async Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel)
        {
            try
            {
                var user = _cIDbContext.User.Where(x => x.Id == requestModel.UserId).FirstOrDefault();

                if (user == null) throw new Exception("Not Found!");

                var userDetails = _cIDbContext.UserDetails.Where(x => x.UserId == requestModel.UserId).FirstOrDefault();

                if (userDetails == null)
                {
                    // Add User Details
                    UserDetail userDetail = new UserDetail()
                    {
                        UserId = requestModel.UserId,
                        Availability = requestModel.Availability,
                        CityId = requestModel.CityId,
                        CountryId = requestModel.CountryId,
                        Department = requestModel.Department,
                        EmployeeId = requestModel.EmployeeId,
                        LinkedInUrl = requestModel.LinkedInUrl,
                        Manager = requestModel.Manager,
                        MyProfile = requestModel.MyProfile,
                        MySkills = requestModel.MySkills,
                        Surname = requestModel.Surname,
                        Name = requestModel.Name,
                        UserImage = requestModel.UserImage,
                        WhyIVolunteer = requestModel.WhyIVolunteer,
                        Status = requestModel.Status,
                        Title = requestModel.Title,

                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                    };

                    await _cIDbContext.UserDetails.AddAsync(userDetail);
                }
                else
                {
                    // Update User Details
                    userDetails.UserId = requestModel.UserId;
                    userDetails.Availability = requestModel.Availability;
                    userDetails.CityId = requestModel.CityId;
                    userDetails.CountryId = requestModel.CountryId;
                    userDetails.Department = requestModel.Department;
                    userDetails.EmployeeId = requestModel.EmployeeId;
                    userDetails.LinkedInUrl = requestModel.LinkedInUrl;
                    userDetails.Manager = requestModel.Manager;
                    userDetails.MyProfile = requestModel.MyProfile;
                    userDetails.MySkills = requestModel.MySkills;
                    userDetails.Surname = requestModel.Surname;
                    userDetails.Name = requestModel.Name;
                    userDetails.UserImage = requestModel.UserImage;
                    userDetails.WhyIVolunteer = requestModel.WhyIVolunteer;
                    userDetails.Status = requestModel.Status;
                    userDetails.Title = requestModel.Title;

                    userDetails.ModifiedDate = DateTime.Now;

                    _cIDbContext.UserDetails.Update(userDetails);
                }

                user.FirstName = requestModel.Name;
                user.LastName = requestModel.Surname;

                _cIDbContext.User.Update(user);
                await _cIDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public LoginUserResponseModel LoginUserDetailById(int id)
        //{
        //    var user = _cIDbContext.User.FirstOrDefault(u => u.Id == id);
        //    if (user == null) return null;

        //    return new LoginUserResponseModel
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName ?? string.Empty,
        //        LastName = user.LastName ?? string.Empty,
        //        PhoneNumber = user.PhoneNumber,
        //        EmailAddress = user.EmailAddress,
        //        UserType = user.UserType,
        //        UserImage = user.UserImage ?? string.Empty,
        //        Message = "User details retrieved successfully"
        //    };
        //}
    }
}
