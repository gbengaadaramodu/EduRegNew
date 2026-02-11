namespace EduReg.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequireInstitutionShortNameAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SkipRequireInstitutionShortNameAttribute : Attribute
    {
    }
}
