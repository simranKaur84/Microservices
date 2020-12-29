using NeighborhoodHelpers.UserMicroservice.Entities.RequestDto;
using NeighborhoodHelpers.UserMicroservice.Entities.ResponseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeighborhoodHelpers.UserMicroservice.DataAccessProvider.UserDataAccess
{
    public interface IUserDataAccess
    {
        LoginResponseDto CreateAnAccount(LoginRequestDto loginRequestDto);

        LoginResponseDto UserLogin(LoginRequestDto loginRequestDto);
    }
}
