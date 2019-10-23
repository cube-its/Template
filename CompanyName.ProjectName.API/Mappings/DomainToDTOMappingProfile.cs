using AutoMapper;
using CompanyName.ProjectName.API.DTOs;
using CompanyName.ProjectName.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.API.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToDTOMappings"; }
        }

        public DomainToDTOMappingProfile()
        {
            CreateMap<User, UserLoginResultDTO>();
            CreateMap<User, UserRegisterResultDTO>();
        }
    }
}
