using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EO = FirstDemo.Infrastructure.Entities;
using BO = FirstDemo.Infrastructure.BusinessObjects;


namespace FirstDemo.Infrastructure.Profiles
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {

            CreateMap<EO.Course, BO.Course>()
                .ForMember(dest=>dest.Name , Src=>Src.MapFrom(x=>x.Title))
                .ReverseMap();


            CreateMap<EO.Student, BO.Student>()
                .ReverseMap();


            CreateMap<EO.Topic, BO.Topic>()
                .ReverseMap();


        }
    }
}
