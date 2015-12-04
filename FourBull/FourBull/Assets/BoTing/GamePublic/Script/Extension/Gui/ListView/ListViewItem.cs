
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BoTing.GamePublic
{

public class ListViewItem : MonoBehaviour
{
    private int itemIndex = 0;
    public int ItemIndex
    {
        get { return itemIndex;  }
        set { itemIndex = value; }
    }

	private ListViewItem nextItem;
	public ListViewItem NextItem
	{
		get { return nextItem;  }
		set { nextItem = value; }
	}

	private ListViewItem preferItem;
	public ListViewItem PreferItem
	{
		get { return preferItem;  }
		set { preferItem = value; }
	}
	
	private Vector2 itemSize;
	public Vector2 ItemSize
	{
		get{ return RectTransform.sizeDelta;}
	}

	private Vector3 lastPosition;
	public Vector3 LastPosition
	{
		get { return lastPosition;  }
		set { lastPosition = value; }
	}

	public Vector3 CurrentPosition
	{
		set { transform.localPosition = value; }
		get { return transform.localPosition;}
	}

	private RectTransform rectTransform;
	public RectTransform RectTransform
	{
		get
		{
			if(rectTransform == null)
			{
				rectTransform = (RectTransform)transform;
			}
			return rectTransform;
		}
	}
}

}