using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Entity;
using HMS.Entity.Request;
using HMS.Entity.Response;

namespace HMS.BL
{
    public interface IUserDetails
    {
        public Task<List<UserResponse>> GetUserDetails();
        public Task<UserResponse> GetUserDetailsById(int Id);
        public Task<bool> DeleteUserDetailsById(int Id);
        public Task<CreateUserResponse> CreateCRMSUser(CreateUserRequest createUserRequest);
    }
}
