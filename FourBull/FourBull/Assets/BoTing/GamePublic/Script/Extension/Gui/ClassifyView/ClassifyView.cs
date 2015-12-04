using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace BoTing.GamePublic
{

public class ClassifyView : MonoBehaviour
{
	/// <summary>
	/// 挂载tab的根节点
	/// </summary>
	public Transform  rootNode;



	public GridLayoutGroup gridGroup;


	/// <summary>
	/// 切换用按钮的引用,会动态在此处绑定事件
	/// </summary>
	public List<Button> tabButtons;


	/// <summary>
	/// 按钮元素
	/// </summary>
	public GameObject classifyObj;

	
	//所有的元素表
	private List<GameObject> itemList
		= new List<GameObject>();


	/// <summary>
	/// 每个元素占据的大小
	/// </summary>
	public Vector2  gridSize;


	/// <summary>
	/// 元素个数
	/// </summary>
	protected  int   itemMax;



	/// <summary>
	/// 当前界面名称
	/// </summary>
	public int  currenViewIndex = 0;

	public void OnInitialView()
	{
		int icounter = tabButtons.Count;
		for (int i = 0; i < icounter; i++) {
			Button go = tabButtons [i];
            go.onClick.RemoveAllListeners();
			go.onClick.AddListener (delegate () {
				SwitchView (go);
			});
		}

		SwitchView(tabButtons[currenViewIndex]);
	}


	public void SwitchView(Button go)
	{
		int index = -1;
		for (int i=0; i<tabButtons.Count; i++) 
		{
			if(go == tabButtons[i])
			{
				index = i;
				break;
			}
		}

		OnCanceled (currenViewIndex);
		currenViewIndex = index;
		OnUpdateView (index);
		OnSelected (currenViewIndex);
	}





	/// <summary>
	/// 更新视图
	/// </summary>
	public void OnUpdateView(int index)
	{
		OnResetView ( index );

		gridGroup.cellSize = gridSize;

		//全部删除,还是隐藏，目前策略是隐藏
		for (int j = itemMax; j< itemList.Count; j++) 
		{
			itemList[j].gameObject.SetActive(false);
		}

		//需要补齐的元素
		int iaddcouter = itemMax - itemList.Count;
		for (int k = 0; k < iaddcouter; k++) 
		{
			//此处资源加载函数可能要重写
			GameObject obj = Instantiate( classifyObj) as GameObject;
			obj.transform.SetParent(rootNode);
			obj.transform.localScale = Vector3.one;
			obj.transform.localPosition = Vector3.zero;
			itemList.Add(obj);
		}

		//更新剩下的元素，不够的来补
		for (int i = 0; i < itemMax; i++) 
		{
			//更新每个元素
			itemList[i].gameObject.SetActive(true);
			OnUpdateItem(itemList[i],i);
		}
	}



	void Start()
	{
        OnInitialView();
	}



	public virtual void OnCanceled(int index)
	{
	}


	public virtual void OnSelected(int index)
	{

	}

    public virtual void OnResetView(int index)
    {

    }


	public virtual void OnUpdateItem(GameObject item,int index)
	{

	}
}

}