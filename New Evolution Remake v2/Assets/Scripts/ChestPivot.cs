using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestPivot : MonoBehaviour
{
    public List<Pivot> pivots;


    [System.Serializable]
    public class Pivot
    {
         public string name;
        public Transform pivot;
    }


    public void Awake()
    {
        GetPivotsInChildren(this.transform);
    }

    void GetPivotsInChildren(Transform parent)
    {
        Pivot _pivot = new Pivot();
        foreach (Transform pivot in parent)
        {
            CheckPivot("Head", pivot);
            CheckPivot("LeftLeg", pivot);
            CheckPivot("RightLeg", pivot);
            CheckPivot("LeftArm", pivot);
            CheckPivot("RightArm", pivot);

            if (pivot.childCount > 0)
            {
                GetPivotsInChildren(pivot);
            }
        }
    }

    void CheckPivot(string bodyPart, Transform pivot)
    {
        Pivot _pivot = new Pivot();
        if (pivot.name == bodyPart)
        {
            _pivot.name = bodyPart;
            _pivot.pivot = pivot;
            pivots.Add(_pivot);
        }
    }


    /*
        pivots[0].pivot = this.transform.Find("Head");
        pivots[0].pivot.name = "Head";

        pivots[1].pivot = this.transform.Find("LeftArm");
        pivots[1].pivot.name = "LeftArm";

        pivots[2].pivot = this.transform.Find("RightArm");
        pivots[2].pivot.name = "RightArm";

        pivots[3].pivot = this.transform.Find("LeftLeg");
        pivots[3].pivot.name = "LeftLeg";

        pivots[4].pivot = this.transform.Find("RightLeg");
        pivots[4].pivot.name = "RightLeg";

        foreach(Pivot pivot in pivots)
            { Debug.Log(pivot.pivot.name); };
    }*/



}
