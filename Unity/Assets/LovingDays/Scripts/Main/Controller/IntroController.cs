using UnityEngine;
using System.Collections;
using Hellgate;

public class IntroController : LovingSceneController
{
    public override void OnSet (object data)
    {
        base.OnSet (data);

        LovingSceneManager.Instance.Wait (1, delegate() {
            LovingSceneManager.Instance.Screen ("Login");
        });
    }
}
