using UnityEngine;
using System.Collections;
using Hellgate;

public class StudyListData
{
    private int idx = 0;
    private string jp = "";
    private string en = "";
    private string kr;
    private string description = "";
    private string sound = "";
    public string key;

    public int Idx {
        get {
            return idx;
        }
    }

    public string JP {
        get {
            return jp;
        }
    }

    public string EN {
        get {
            return en;
        }
    }

    public string Description {
        get {
            return description;
        }
    }

    public string Sound {
        get {
            return sound;
        }
    }

    public string Mean {
        get {
            return en;
        }
    }
}
