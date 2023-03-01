using HMS.Entity;
using HMS.Entity.Request;
using HMS.Entity.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Dal
{
    public interface IUserDetailsDL
    {
        public Task<List<UserResponse>> GetUserDetails();
        public Task<UserResponse> GetUserDetailsByIdAsync(int Id);

        public Task<bool> DeleteUserDetailsByIdAsync(int Id);

        public Task<CreateUserResponse> CreateCRMSUser(CreateUserRequest createUserRequest);
    }
}
