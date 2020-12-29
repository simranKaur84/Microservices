using AutoMapper;
using NeighborhoodHelpers.UserMicroservice.Entities.Models;
using NeighborhoodHelpers.UserMicroservice.Entities.RequestDto;
using NeighborhoodHelpers.UserMicroservice.Entities.ResponseDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeighborhoodHelpers.UserMicroservice.Entities.Utilities.AutoMapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<LoginRequestDto, Users>();
            CreateMap<Users, LoginResponseDto>()
                .ForMember(x => x.Address, y => y.MapFrom(z => JsonConvert.DeserializeObject<UserAddress>(z.Address)));
        }
    }
}
