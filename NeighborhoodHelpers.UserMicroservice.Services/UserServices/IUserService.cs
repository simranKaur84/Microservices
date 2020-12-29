using NeighborhoodHelpers.UserMicroservice.Entities.RequestDto;
using NeighborhoodHelpers.UserMicroservice.Entities.ResponseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeighborhoodHelpers.UserMicroservice.Services.UserServices
{
    public interface IUserService
    {
        LoginResponseDto CreateAnAccount(LoginRequestDto loginRequestDto);

        LoginResponseDto UserLogin(LoginRequestDto loginRequestDto);
    }
}
