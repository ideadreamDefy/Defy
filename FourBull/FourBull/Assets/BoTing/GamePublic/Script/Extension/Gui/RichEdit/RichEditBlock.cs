using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace BoTing.GamePublic
{

public class RichEditBlock
{
    private GameObject gameObject;
    public GameObject GameObject
    {
        get { return gameObject; }
    }

    private RectTransform transform;
    public RectTransform Transform
    {
        get { return transform; }
    }

    private LayoutElement layoutElement;
    public LayoutElement LayoutElement
    {
        get { return layoutElement; }
    }

    private VerticalLayoutGroup layoutGroup;
    public VerticalLayoutGroup LayoutGroup
    {
        get { return layoutGroup; }
    }

    private List<RichEditLine> lineList = new List<RichEditLine>();
    private RichEditLine currentLine;

    private float previousHeight = 0;

    private float LineSpacing
    {
        get { return layoutGroup.spacing; }
    }

    public RichEditBlock(Transform parent, float width, float lineSpacing)
    {
        gameObject = new GameObject("RichEditBlock");
        transform = gameObject.AddComponent<RectTransform>();
        transform.SetParent(parent, false);
        transform.SetAsLastSibling();

        layoutElement = gameObject.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = width;
        layoutElement.preferredHeight = 0;

        layoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childForceExpandWidth = false;
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childAlignment = TextAnchor.LowerLeft;
        layoutGroup.spacing = lineSpacing;

        currentLine = AddNewLine();
    }

    public void Add(string text, Text textSample)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        textSample.gameObject.SetActive(true);

        string remain = text;
        string addableText = "";
        while (!string.IsNullOrEmpty(remain))
        {
            bool canAdd = false;
            bool forceAdd = false;

            addableText = GetAddableText(remain, textSample);
            canAdd = !string.IsNullOrEmpty(addableText);

            if (!canAdd && currentLine.IsEmpty)
            {
                canAdd = true;
                forceAdd = true;
                addableText = remain.Substring(0, 1);
            }

            if (!canAdd)
            {
                currentLine = AddNewLine();
                continue;
            }

            remain = remain.Substring(addableText.Length, remain.Length - addableText.Length);

            var textObject = GameObject.Instantiate(textSample.gameObject);
            var newText = textObject.GetComponent<Text>();
            newText.text = addableText;
            var textRectTransform = textObject.transform as RectTransform;
            textRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newText.preferredWidth);
            textRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newText.preferredHeight);

            currentLine.Add(textObject.transform as RectTransform, forceAdd);
            UpdateHeight();
        }

        textSample.gameObject.SetActive(true);
    }

    public void Add(RectTransform child)
    {
        bool canAdd = currentLine.CanAdd(child);
        if (!canAdd && !currentLine.IsEmpty)
        {
            currentLine = AddNewLine();
        }

        currentLine.Add(child, true);
        UpdateHeight();
    }

    private string GetAddableText(string text, Text textSample)
    {
        if (currentLine.IsFull)
        {
            return "";
        }

        string addableText = text;
        while (!currentLine.CanAddText(addableText, textSample))
        {
            addableText = addableText.Substring(0, addableText.Length - 1);
        }
        return addableText;
    }

    private RichEditLine AddNewLine()
    {
        RichEditLine line = new RichEditLine(transform, layoutElement.preferredWidth);
        lineList.Add(line);

        UpdateHeightForNewLine(line);

        return line;
    }

    private void UpdateHeightForNewLine(RichEditLine line)
    {
        if (currentLine != null)
        {
            previousHeight += currentLine.LayoutElement.preferredHeight + LineSpacing;
        }
        layoutElement.preferredHeight = previousHeight + line.LayoutElement.preferredHeight;
    }

    private void UpdateHeight()
    {
        layoutElement.preferredHeight = previousHeight + currentLine.LayoutElement.preferredHeight;
    }
}

} //BoTing.GamePublic