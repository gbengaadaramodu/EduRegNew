// File: AcademicSessionDto.cs
using EduReg.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class CreateAcademicSessionDto : CommonBaseDto
    {
        [Required]
        [StringLength(100)]
        public string? SessionName { get; set; }

        [Required]
        [StringLength(50)]
        public string? BatchShortName { get; set; } // FK to Admission Batches

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }

    public class UpdateAcademicSessionDto : CommonBaseDto
    {
        [Required]
        public int SessionId { get; set; } // Identifies the record to update

        [StringLength(100)]
        public string? SessionName { get; set; }

        [StringLength(50)]
        public string? BatchShortName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public bool IsDeleted { get; internal set; }
    }

    
    public class AcademicSessionResponseDto
    {
        public int SessionId { get; set; }

        public string? SessionName { get; set; }

        public string? SemesterName { get; set; }

        public string? BatchShortName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
