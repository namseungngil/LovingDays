using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using Hellgate;

public class StudyService : LovingService
{
    private string mKey;

    public StudyService (string key)
    {
        mKey = key;
    }

    public void LoadScene ()
    {
        List<AssetBundleData> assetBundles = new List<AssetBundleData> ();

        AssetBundleData assetBundle = new AssetBundleData ("master");
        assetBundle.type = typeof(TextAsset);
        assetBundle.objName = "study";
        assetBundles.Add (assetBundle);

        LoadingJobData jobData = new LoadingJobData ();
        jobData.assetBundles = assetBundles;

        StudyData[] study = null;
        jobData.finishedDelegate = delegate(List<object> obj, LoadingJobController job) {
            if (study == null) {
                TextAsset text = obj.GetListObject<TextAsset> ();

                study = Reflection.Convert<StudyData> ((IList)Json.Deserialize (text.text));
                for (int i = 0; i < study.Length; i++) {
                    assetBundle = new AssetBundleData ("master");
                    assetBundle.type = typeof(TextAsset);
                    assetBundle.objName = study [i].Sheet;
                    assetBundles.Add (assetBundle);
                }

                job.LoadAssetBundle (assetBundles);
            } else {
                List<TextAsset> listText = obj.GetListObjects<TextAsset> ();
                Dictionary<string, StudyListData[]> dic = new Dictionary<string, StudyListData[]> ();

                for (int i = 0; i < study.Length; i++) {
                    TextAsset text = listText.Find (study [i].Sheet);
                    if (text != null) {
                        dic.Add (study [i].Sheet, Reflection.Convert<StudyListData> ((IList)Json.Deserialize (text.text)));
                    }
                }

                job.PutExtra ("key", mKey);
                job.PutExtra ("studys", study);
                job.PutExtra ("studyLists", dic);
                job.GoNextScene ("Study");
            }
        };

        LovingSceneManager.Instance.LoadingJob (jobData);
    }
}
