using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    

    public class AcademicsManager
    {
        private readonly IAdmissionBatches _batches;
        private readonly IAcademicSessions _sessions;
        private readonly ISemesters _semesters;
        public AcademicsManager(IAdmissionBatches batches, IAcademicSessions sessions, ISemesters semesters)
        {
            _batches = batches;
            _sessions = sessions;
            _semesters = semesters;
        }



    }
}
