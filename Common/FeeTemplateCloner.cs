using EduReg.Models.Entities;

namespace EduReg.Common
{
    public class FeeTemplateCloner
    {
         
            public static List<FeeRule> CloneRulesToNewSession(IEnumerable<FeeRule> sourceRules, string newSessionId)
            {
                return sourceRules.Select(r => new FeeRule
                {
                    Id = r.Id,
                    InstitutionShortName = r.InstitutionShortName,
                    ProgrammeCode = r.ProgrammeCode,
                    DepartmentCode = r.DepartmentCode,
                    LevelName = r.LevelName,
                    ClassCode = r.ClassCode,
                    SessionId = newSessionId,
                    SemesterId = r.SemesterId,
                    Amount = r.Amount,
                    IsRecurring = r.IsRecurring,
                    RecurrenceType = r.RecurrenceType,
                    ApplicabilityScope = r.ApplicabilityScope,
                    EffectiveFrom = r.EffectiveFrom,
                    EffectiveTo = r.EffectiveTo
                }).ToList();
            }
        


    }
}
