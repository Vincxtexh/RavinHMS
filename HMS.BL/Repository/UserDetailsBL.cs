using HMS.Entity;
using HMS.Dal;
using HMS.Entity.Response;
using HMS.Entity.Request;

namespace HMS.BL.Repository
{
    public class UserDetailsBL : IUserDetails
    {
        public IUserDetailsDL UserDetailsDL { get; set; }
        private readonly IUserDetailsDL _userDetailsDL;

        public UserDetailsBL(IUserDetailsDL userDetailsDL)
        {
            _userDetailsDL = userDetailsDL;

        }

        public async Task<List<UserResponse>> GetUserDetails()
        {
            return await _userDetailsDL.GetUserDetails();

        }

        public async Task<UserResponse> GetUserDetailsById(int Id)
        {
            return await _userDetailsDL.GetUserDetailsByIdAsync(Id);

        }

        public async Task<bool> DeleteUserDetailsById(int Id)
        {
            return await _userDetailsDL.DeleteUserDetailsByIdAsync(Id);

        }

        public async Task<CreateUserResponse> CreateCRMSUser(CreateUserRequest createUserRequest)
        {
            return await _userDetailsDL.CreateCRMSUser(createUserRequest);

        }
    }
}
