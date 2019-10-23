using AutoMapper;
using CompanyName.ProjectName.API.DTOs;
using CompanyName.ProjectName.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.API.Mappings
{
    public class DTOToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DTOToDomainMappings"; }
        }

        public DTOToDomainMappingProfile()
        {
            CreateMap<UserRegisterDTO, User>();
        }
    }
}
