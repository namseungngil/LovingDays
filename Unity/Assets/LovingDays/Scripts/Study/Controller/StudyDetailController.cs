using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Hellgate;

public class StudyDetailController : LovingSceneController
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;
    private StudyListData mStudy;

    public static void Main (StudyListData study)
    {
        StudyDetailService service = new StudyDetailService (study);
        service.LoadScene ();
    }

    public override void OnSet (object data)
    {
        base.OnSet (data);

        mStudy = data as StudyListData;

        title.text = mStudy.JP + "\n" + mStudy.Mean;
        description.text = mStudy.Description;
    }

    public void OnClick ()
    {
        StudyController.Main (mStudy.key);
    }
}
