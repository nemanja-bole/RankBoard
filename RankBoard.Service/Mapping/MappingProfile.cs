using AutoMapper;
using RankBoard.Data.Models.Identity;
using RankBoard.Dto.Identity;
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
            CreateMap<Role, RoleDto>();

            CreateMap<RoleClaimDto, RoleClaim>();
            CreateMap<RoleClaim, RoleClaimDto>();

            CreateMap<ApplicationUserDto, User>();
            CreateMap<User, ApplicationUserDto>();

            CreateMap<UserRoleDto, UserRole>();
            CreateMap<UserRole, UserRoleDto>();

            CreateMap<UserLogin, UserLoginDto>();
            CreateMap<UserLoginDto, UserLogin>();

            CreateMap<UserClaim, UserClaimDto>();
            CreateMap<UserClaimDto, UserClaim>();

            CreateMap<UserToken, UserTokenDto>();
            CreateMap<UserTokenDto, UserToken>();
        }
    }
}
