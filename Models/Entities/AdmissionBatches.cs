﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduReg.Models.Entities
{
    public class AdmissionBatches: CommonBase
    {


 
        [Key]
        public string? BatchShortName { get; set; } //PK
        public string? InstitutionShortName { get; set; }
        public  string? BatchName { get; set; }
       
        public  string?  Description { get; set; }
        


    }
}
