using UnityEngine;
using System.Collections;

namespace BoTing.GamePublic
{

public class ClassifyItem : MonoBehaviour 
{
	/// <summary>
	/// 元素的索引 
	/// </summary>
	private int  itemIndex = 0;
	public int ItemIndex
	{
		get { return itemIndex;}
		set { itemIndex = value;}
	}

	//更新元素
	public virtual void UpdateItem(int index)
	{

	}
}

}