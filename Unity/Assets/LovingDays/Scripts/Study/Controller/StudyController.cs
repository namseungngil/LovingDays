using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Hellgate;

public class StudyController : LovingSceneController
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject menuTemp;
    [SerializeField]
    private GameObject list;
    [SerializeField]
    private GameObject listTemp;
    private StudyData[] mStudys;
    private Dictionary<string, StudyListData[]> mDic;
    private List<StudyListData> mListStudy;
    private List<GameObject> mListGObj;
    private string mKey;

    public static void Main (string key = "")
    {
        StudyService service = new StudyService (key);
        service.LoadScene ();
    }

    public override void OnSet (object data)
    {
        base.OnSet (data);

        List<object> listObj = data as List<object>;
        Dictionary<string, object> intent = listObj.GetListObject<Dictionary<string, object>> ();

        mStudys = (StudyData[])intent ["studys"];
        mDic = (Dictionary<string, StudyListData[]>)intent ["studyLists"];
        mKey = (string)intent ["key"];

        mListGObj = new List<GameObject> ();
        SetView ();
    }

    private void SetView ()
    {
        Button[] buttons = list.GetComponentsInChildren<Button> ();
        for (int i = 0; i < buttons.Length; i++) {
            Destroy (buttons [i].gameObject);
        }

        if (mKey == "") {
            mKey = mStudys [0].Sheet;
        }
        StudyListData[] studyList = mDic [mKey];

        if (mListGObj.Count == 0) {
            for (int i = 0; i < mStudys.Length; i++) {
                GameObject gObj = Instantiate (menuTemp) as GameObject;
                gObj.transform.SetParent (menu.transform);
                gObj.transform.localScale = new Vector3 (1f, 1f, 1f);
                gObj.name = mStudys [i].Sheet.ToString ();
                gObj.FindChildObject<Text> ("Text").text = mStudys [i].Name;

                if (mKey == mStudys [i].Sheet) {
                    gObj.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.5f);
                }

                gObj.SetActive (true);
                mListGObj.Add (gObj);
            }
        }

        mListStudy = new List<StudyListData> (studyList);
        for (int i = 0; i < mListStudy.Count; i++) {
            GameObject gObj = Instantiate (listTemp) as GameObject;
            gObj.transform.SetParent (list.transform);
            gObj.transform.localScale = new Vector3 (1f, 1f, 1f);
            gObj.name = studyList [i].Idx.ToString ();
            gObj.FindChildObject<Text> ("Word").text = studyList [i].JP;
            gObj.FindChildObject<Text> ("Mean").text = studyList [i].Mean;

            gObj.SetActive (true);
        }
    }

    public void OnClickMenu (GameObject gObj)
    {
        mKey = gObj.name;
        SetView ();
    }

    public void OnClick (GameObject gObj)
    {
        StudyListData study = mListStudy.Find (x => x.Idx == int.Parse(gObj.name));
        study.key = mKey;

        StudyDetailController.Main (study);
    }
}
