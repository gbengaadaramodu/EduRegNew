namespace EduReg.Common
{
    [Flags]
    public enum FeeApplicabilityScope
    {
        InstitutionWide = 1,   // Applies to all students       
        PerDepartment = 2,     // Specific department
        PerProgramme = 4,      // Specific programme
        PerLevel = 8,          // Specific level
        PerClass = 16,          // Specific class (e.g., for specific cohort or curriculum)
        PerStudent= 32        // Specific individual student
    }
}


 