﻿using AutoMapper;
using RankBoard.Data.Models.Identity;
using RankBoard.Dto;
using RankBoard.Repositories;
using RankBoard.Service.Interface;

namespace RankBoard.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkIdentity _unitOfWork;
        private IMapper _mapper;

        public UserService(IUnitOfWorkIdentity unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddRole(RoleDto roleDto)
        {
            _unitOfWork.RoleRepository.Add(_mapper.Map<RoleDto, Role>(roleDto));
        }

        public void RemoveRole(string id)
        {
            var roleToRemove = _unitOfWork.RoleRepository.FindById(id);

            _unitOfWork.RoleRepository.Remove(roleToRemove);
        }
    }
}
