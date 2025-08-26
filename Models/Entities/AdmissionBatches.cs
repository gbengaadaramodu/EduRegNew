using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduReg.Models.Entities
{
    public class AdmissionBatches: CommonBase
    {
         
        

        //[ForeignKey("ShortName")]
        //public Institutions? Institutions { get; set; }
        // The FK from Institution  
        public string? InstitutionShortName { get; set; }
        public  string? BatchName { get; set; }
        public string? BatchShortName { get; set; } //PK
        public  string?  Description { get; set; }
        


    }
}
