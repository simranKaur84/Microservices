using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeighborhoodHelpers.UserMicroservice.Entities.RequestDto;
using NeighborhoodHelpers.UserMicroservice.Entities.ResponseDto;
using NeighborhoodHelpers.UserMicroservice.Services.UserServices;

namespace NeighborhoodHelpers.UserMicroservice.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class LoginController : ControllerBase
    {
        #region Injectors

        private readonly IUserService _userService;
        public LoginController(
            IUserService userService
            )
        {
            _userService = userService;
        }

        #endregion

        [HttpPost]
        [Route("CreateAnAccount")]
        [ProducesResponseType(200, Type = typeof(LoginResponseDto))]
        public IActionResult CreateAnAccount(LoginRequestDto loginRequestDto)
        {
            var data = _userService.CreateAnAccount(loginRequestDto);
            return Ok(data);
        }

        [HttpPost]
        [Route("UserLogin")]
        [ProducesResponseType(200, Type = typeof(LoginResponseDto))]
        public IActionResult UserLogin(LoginRequestDto loginRequestDto)
        {
            var data = _userService.UserLogin(loginRequestDto);
            return Ok(data);
        } 
    }
}
