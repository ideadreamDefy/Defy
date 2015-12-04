using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BoTing.GamePublic
{ 

public class RichEdit : UIComponentBase
{
    public float BlockSpacing = 4;
    public float LineSpacing = 4;

    private Text textSample;
    private RectTransform contentPanel;
    
    private VerticalLayoutGroup layoutGroup;
    public VerticalLayoutGroup LayoutGroup
    {
        get { return layoutGroup; }
    }

    private List<RichEditBlock> blockList = new List<RichEditBlock>();
    private RichEditBlock currentBlock;

    private float previousHeight = 0;

    private RectTransform rectTransform
    {
        get { return transform as RectTransform; }
    }
    
    protected override void OnAwakeView()
    {
        Transform _Text_TextSample0 = transform.FindChild("TextSample");
        textSample = _Text_TextSample0.GetComponent<Text>();
        Transform ScrollView1 = transform.FindChild("ScrollView");
        Transform Viewport10 = ScrollView1.FindChild("Viewport");
        Transform _Panel_Content100 = Viewport10.FindChild("Content");
        contentPanel = _Panel_Content100.GetComponent<RectTransform>();

        layoutGroup = contentPanel.GetComponent<VerticalLayoutGroup>();
        if(layoutGroup == null)
        {
            layoutGroup = contentPanel.gameObject.AddComponent<VerticalLayoutGroup>();
        }

        UpdateWidth();
    }

	// Use this for initialization
    protected override void OnStartView() 
    {
        layoutGroup.spacing = BlockSpacing;
	}

    public void AddBlock()
    {
        RichEditBlock block = new RichEditBlock(contentPanel, rectTransform.rect.width, LineSpacing);
        blockList.Add(block);
        block.LayoutGroup.spacing = LineSpacing;

        UpdateHeightForNewBlock(block);

        currentBlock = block;
    }

    public void Add(string text)
    {
        currentBlock.Add(text, textSample);
        UpdateHeight();
    }

    public void Add(RectTransform child)
    {
        currentBlock.Add(child);
        UpdateHeight();
    }

    public void Clear()
    {
        transform.DetachChildren();

        foreach(var block in blockList)
        {
            Destroy(block.GameObject);
        }
        blockList.Clear();

        previousHeight = 0;
        currentBlock = null;
    }

    private void UpdateHeightForNewBlock(RichEditBlock block)
    {
        if (currentBlock != null)
        {
            previousHeight += currentBlock.LayoutElement.preferredHeight + BlockSpacing;
        }        
        contentPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, previousHeight + block.LayoutElement.preferredHeight);
    }

    private void UpdateWidth()
    {
        contentPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.rect.width);
    }

    private void UpdateHeight()
    {
        contentPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, previousHeight + currentBlock.LayoutElement.preferredHeight);
    }
}

} // BoTing.GamePublic