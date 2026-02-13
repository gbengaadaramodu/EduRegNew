using AutoMapper;
using EduReg.Models.Dto;
using EduReg.Models.Entities;

namespace EduReg.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { // Add your mappings here // Example: // CreateMap<SourceModel, DestinationModel>(); }

            CreateMap<Faculties, FacultiesDto>().ReverseMap();

            CreateMap<AdmissionBatches, AdmissionBatchesDto>().ReverseMap(); 
            CreateMap<AcademicLevel, AcademicLevelsDto>().ReverseMap();
            CreateMap<Departments, DepartmentsDto>().ReverseMap();
            CreateMap<CommonBase, CommonBaseDto>().ReverseMap();
            CreateMap<FeeRule, FeeRuleDto>().ReverseMap();
            CreateMap<FeeType, FeeTypeDto>().ReverseMap();

        }
    }
}
