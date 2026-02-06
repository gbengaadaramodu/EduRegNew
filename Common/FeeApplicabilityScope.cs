using System.ComponentModel;

namespace EduReg.Common
{

    [Flags]     
    public enum FeeApplicabilityScope
    {
        //InstitutionWide = 1,
        //PerFaculty = 2,
        //PerDepartment = 4,
        //PerProgramme = 8,
        //PerLevel = 16,
        //PerStudent = 32,
        //Other = 64
        [Description("Institution-wide")]
        InstitutionWide = 1,

        [Description("Per Faculty")]
        PerFaculty = 2,

        [Description("Per Department")]
        PerDepartment = 4,

        [Description("Per Programme")]
        PerProgramme = 8,

        [Description("Per Level")]
        PerLevel = 16,

        [Description("Per Student")]
        PerStudent = 32,

        [Description("Other")]
        Other = 64
    }

}


 