using UnityEngine;
using System.Collections;
using Hellgate;

public class LoginController : LovingSceneController
{
    private AssetBundleInitialDownloader downloader;
    private AssetBundleInitalStatus status;

    public override void OnSet (object data)
    {
        base.OnSet (data);

        downloader = new AssetBundleInitialDownloader ("http://namseungngil.cafe24.com/lovingdays/v1/resource.json",
                                                       "http://namseungngil.cafe24.com/lovingdays/v1/" + Util.GetDevice ());
        downloader.aEvent = CallbackDownloader;
        downloader.Download ();
    }

    private void CallbackDownloader (AssetBundleInitalStatus initStatus)
    {
        if (initStatus == AssetBundleInitalStatus.HttpError) {
            LovingSceneManager.Instance.PopUp ("Error !!", PopUpType.Ok, delegate(PopUpYNType type) {
                LovingSceneManager.Instance.Screen ("Login");
            });
        } else if (initStatus == AssetBundleInitalStatus.HttpTimeover) {
            LovingSceneManager.Instance.PopUp ("Time Over !!", PopUpType.Ok, delegate(PopUpYNType type) {
                LovingSceneManager.Instance.Screen ("Login");
            });
        } else if (initStatus == AssetBundleInitalStatus.Start) {

        } else {
            LoadingJobData jobData = new LoadingJobData ("Main");
            LovingSceneManager.Instance.LoadingJob (jobData);
        }
    }
}
