using AutoMapper;
using NeighborhoodHelpers.UserMicroservice.Entities.Constants;
using NeighborhoodHelpers.UserMicroservice.Entities.DatabaseContext;
using NeighborhoodHelpers.UserMicroservice.Entities.Models;
using NeighborhoodHelpers.UserMicroservice.Entities.RequestDto;
using NeighborhoodHelpers.UserMicroservice.Entities.ResponseDto;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NeighborhoodHelpers.UserMicroservice.DataAccessProvider.UserDataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        #region Injectors

        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        public UserDataAccess(
                UserDbContext context,
                IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public LoginResponseDto CreateAnAccount(LoginRequestDto loginRequestDto)
        {
            var userInfo = _mapper.Map<Users>(loginRequestDto);
            userInfo.Id = Guid.NewGuid();
            userInfo.Password = GetMd5Hash(userInfo.Password);
            var createTable = _context.CreateTable("Users", "Id");
            // also, check if email exists already
            createTable.Wait();
            _context.InsertItem<Users>(userInfo);
            var insertedUserDetails = _context.GetItemById<Users>(userInfo.Id);
            insertedUserDetails.Wait();
            return _mapper.Map<LoginResponseDto>(insertedUserDetails.Result);
        }

        public LoginResponseDto UserLogin(LoginRequestDto loginRequestDto)
        {
            var userInfo = _context.GetItemByString<Users>(Messages.Email ,loginRequestDto.Email);
            userInfo.Wait();
            return _mapper.Map<LoginResponseDto>(userInfo.Result[0]);
        }

        public string GetMd5Hash(string input)
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        #endregion
    }
}
