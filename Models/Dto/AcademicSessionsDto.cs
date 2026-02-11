// File: AcademicSessionDto.cs
using EduReg.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Dto
{
    public class CreateAcademicSessionDto : CommonBaseDto
    {
        [Required(ErrorMessage = "SessionName is required")]
        [StringLength(100)]
        public string? SessionName { get; set; }

        [Required(ErrorMessage = "BatchShortName is required")]
        [StringLength(50)]
        public string? BatchShortName { get; set; } // FK to Admission Batches

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }

    public class UpdateAcademicSessionDto : CommonBaseDto
    {
        [Required(ErrorMessage = "SessionId is required")]
        public int SessionId { get; set; } // Identifies the record to update

        [Required(ErrorMessage = "SessionName is required")]
        [StringLength(100)]
        public string? SessionName { get; set; }

        [Required(ErrorMessage = "BatchShortName is required")]
        [StringLength(50)]
        public string? BatchShortName { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
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
