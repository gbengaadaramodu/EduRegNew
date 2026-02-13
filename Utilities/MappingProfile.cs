using AutoMapper;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Repositories;

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
            CreateMap<SessionSemester, SessionSemesterDto>().ReverseMap();
            CreateMap<AcademicSession, CreateAcademicSessionDto>().ReverseMap();
            CreateMap<AcademicSession, UpdateAcademicSessionDto>().ReverseMap();
            CreateMap<CourseMaxMin, CourseMaxMinDto>().ReverseMap();
            CreateMap<CourseRegistration, CourseRegistrationViewDto>().ReverseMap();
            CreateMap<CourseSchedule, CourseScheduleDto>().ReverseMap();
            CreateMap<CourseRegistration, CreateCourseRegistrationDto>().ReverseMap();
            CreateMap<CourseType, CourseTypeDto>().ReverseMap();
            CreateMap<DepartmentCourses, DepartmentCoursesDto>().ReverseMap();
            CreateMap<ELibrary, ELibraryDto>().ReverseMap();
            CreateMap<Faculties, FacultiesDto>().ReverseMap();
            CreateMap<FeeItem, FeeItemDto>().ReverseMap();







            
            CreateMap<FeeRule, FeeRuleDto>().ReverseMap();
            CreateMap<FeeType, FeeTypeDto>().ReverseMap();
            CreateMap<Institutions, InstitutionsDto>().ReverseMap();
            CreateMap<ProgramCourses, ProgramCoursesDto>().ReverseMap();
            CreateMap<ProgrammeFeeSchedule, ProgrammeFeeScheduleDto>().ReverseMap();
            CreateMap<Programmes, ProgrammesDto>().ReverseMap();

        }
    }
}
