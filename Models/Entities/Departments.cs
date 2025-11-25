using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Entities
{
    [Index(nameof(InstitutionShortName))]
    public class Departments: CommonBase
    {
        public string? InstitutionShortName { get; set; }
        public string? FacultyCode { get; set; } //FK
        public string? DepartmentName { get; set; }
        public string? DepartmentCode { get; set; } //FK 201
        public string? Description { get; set; }          
        
        public int Duration { get; set; }
        public int NumberofSemesters { get; set; }   
        public int MaximumNumberofSemesters { get; set; }           
       

    }
}
