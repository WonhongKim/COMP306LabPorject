using AutoMapper;
using JobInfoAPI.Models;
using JobLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobInfoAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Job, JobDto>();
            CreateMap<Job, JobForUpdateDto>();
            CreateMap<Job, JobForCreateDto>();
            CreateMap<JobRank, JobRankDto>();
            CreateMap<JobForCreateDto, Job>();
            CreateMap<JobRankForUpdateDto, JobRank>();
            
        }
    }
}
