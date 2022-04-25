using Domain.Models;
using Domain.Services;

namespace RestApi.Services
{
    public class UserService : IUserService
    {
        public bool IsValidUserInformation(LoginModel model)
        {
            if (model.UserName.Equals("admin") && model.Password.Equals("1111")) return true;
            return false;
        }
        
        public LoginModel GetUserDetails()
        {
            return new LoginModel(){UserName = "admin", Password = "1111"};
        }
    }
}