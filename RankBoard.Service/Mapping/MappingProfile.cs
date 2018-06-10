using AutoMapper;
using RankBoard.Data.Models.Identity;
using RankBoard.Dto;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace RankBoard.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoleDto, Role>();
            CreateMap<RoleClaimDto, RoleClaim>();
        }
    }
}
