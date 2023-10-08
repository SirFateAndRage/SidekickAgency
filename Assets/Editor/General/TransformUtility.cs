using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformUtility
{
    public static void DeleteAllChildrenRecursively(this Transform tranform)
    {
        if (tranform.childCount <= 0)
            return;

        if(tranform.childCount == 1)
        {
            MonoBehaviour.DestroyImmediate(tranform.GetChild(0).gameObject);
            return;
        }

        MonoBehaviour.DestroyImmediate(tranform.GetChild(0).gameObject);
        DeleteAllChildrenRecursively(tranform);
    }

    public static IEnumerable GetAllChildren(this Transform transform)
    {
        int i = 0;
        while(transform.childCount > 0 && i < transform.childCount)
        {
            yield return transform.GetChild(i);
            i++;
        }
    }
}
