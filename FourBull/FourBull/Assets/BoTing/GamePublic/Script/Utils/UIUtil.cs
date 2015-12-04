using UnityEngine;
using System.Collections;

namespace BoTing.GamePublic
{

public class UIUtil
{
    public static void RemoveAllChildren(GameObject gameObject, bool immediate = true)
    {
        int childCount = gameObject.transform.childCount;
        for(int i = childCount - 1; i >= 0; --i)
        {
            Transform child = gameObject.transform.GetChild(i);
            if (immediate)
            {
                GameObject.DestroyImmediate(child.gameObject);
            }
            else
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}

}