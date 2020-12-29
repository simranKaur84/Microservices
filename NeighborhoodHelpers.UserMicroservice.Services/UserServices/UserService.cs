using NeighborhoodHelpers.UserMicroservice.DataAccessProvider.UserDataAccess;
using NeighborhoodHelpers.UserMicroservice.Entities.RequestDto;
using NeighborhoodHelpers.UserMicroservice.Entities.ResponseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeighborhoodHelpers.UserMicroservice.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserDataAccess _userDataAccess;
        public UserService(
            IUserDataAccess userDataAccess
            )
        {
            _userDataAccess = userDataAccess;
        }

        public LoginResponseDto CreateAnAccount(LoginRequestDto loginRequestDto)
        {
            return _userDataAccess.CreateAnAccount(loginRequestDto);
        }

        public LoginResponseDto UserLogin(LoginRequestDto loginRequestDto) 
        {
            return _userDataAccess.UserLogin(loginRequestDto);
        }
    }
}
