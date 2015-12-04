using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace BoTing.GamePublic
{

public class ScrollListener 
    : MonoBehaviour, IScrollHandler,IDragHandler
{

    //内部模块可以拖动赋值
    public ListView listView; 


    //鼠标滑轮
    public void OnScroll(UnityEngine.EventSystems.PointerEventData evt)
    {
        if (listView != null)
        {
           // listView.OnScroll(evt);
        }
    }

   
    //拖动
    public void OnDrag(PointerEventData evt)
    {
            if (listView != null)
        {
           // listView.OnScroll(evt);
        }
    }
}

}
