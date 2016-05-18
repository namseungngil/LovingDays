using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hellgate;

public class StudyDetailService : LovingService
{
    private StudyListData mStudy;

    public StudyDetailService (StudyListData study)
    {
        mStudy = study;
    }
        
    public void LoadScene ()
    {
        LovingSceneManager.Instance.Screen ("StudyDetail", mStudy);
    }
}
