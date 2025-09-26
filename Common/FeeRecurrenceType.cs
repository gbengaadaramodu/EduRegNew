namespace EduReg.Common
{

    [Flags]
    public enum FeeRecurrenceType
    {
        OneTime = 1,       // Paid once in a lifetime
        PerSession = 2,    // Once every academic session
        PerSemester = 3,   // Once every semester
        PerCourse = 4      // Per scheduled course
    }

}
