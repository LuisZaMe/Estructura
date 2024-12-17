using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Enums
{
    public enum CandidateStatus
    {
        NONE = 0,
        NOT_RATED = 1,
        IN_PROGRESS = 2,
        RECOMMENDABLE = 3,
        NOT_RECOMMENDABLE = 4,
        PARTIALY_RECOMMENDABLE = 5
    }

    public enum StudyStatus
    {
        NONE = 0,
        NOT_STARTED = 1,
        REJECTED = 2,
        ACCEPTED = 3,
        IN_PROGRESS = 4
    }

    public enum StudyProgressStatus
    {
        NONE = 0,
        UNDER_INTERVIEWER = 1,
        UNDER_ADMON = 2,
        UNDER_ANALYST = 3,
        UNDER_CLIENT = 4,
    }
}
