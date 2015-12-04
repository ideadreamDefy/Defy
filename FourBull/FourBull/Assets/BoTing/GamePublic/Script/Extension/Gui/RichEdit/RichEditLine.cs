using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BoTing.GamePublic
{

public class RichEditLine
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

    private HorizontalLayoutGroup layoutGroup;
    public HorizontalLayoutGroup LayoutGroup
    {
        get { return layoutGroup; }
    }

    public float CurrentWidth = 0;
    public float AvailableWidth
    {
        get { return layoutElement.preferredWidth - CurrentWidth; }
    }

    public bool IsFull
    {
        get { return AvailableWidth < 0.01; }
    }

    public bool IsEmpty
    {
        get { return CurrentWidth < 0.01; }
    }

    public RichEditLine(Transform parent, float width)
    {
        gameObject = new GameObject("RichEditLine");
        transform = gameObject.AddComponent<RectTransform>();
        transform.SetParent(parent, false);
        transform.SetAsLastSibling();

        layoutElement = gameObject.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = width;
        layoutElement.preferredHeight = 0;

        layoutGroup = gameObject.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandWidth = false;
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childAlignment = TextAnchor.LowerLeft;
    }

    public bool CanAdd(RectTransform child)
    {
        return IsFull ? false : child.rect.width <= AvailableWidth;
    }

    public bool CanAddText(string text, Text textSample)
    {
        textSample.text = text;
        return IsFull ? false : textSample.preferredWidth <= AvailableWidth;
    }

    public bool Add(RectTransform child, bool forceAdd = false)
    {
        bool canAdd = child.rect.width <= AvailableWidth;

        if (!canAdd && !forceAdd)
        {
            return false;
        }

        UpdateHeightForChild(child);

        if (canAdd)
        {
            child.SetParent(transform, false);
            child.SetAsLastSibling();

            CurrentWidth += child.rect.width;
        }
        else
        {
            GameObject containerObject = new GameObject("LineForceContainer");
            RectTransform containerTransformRect = containerObject.AddComponent<RectTransform>();
            containerTransformRect.SetParent(transform, false);
            containerTransformRect.SetAsLastSibling();

            LayoutElement containerLayoutElement = containerObject.AddComponent<LayoutElement>();
            containerLayoutElement.preferredWidth = AvailableWidth;
            containerLayoutElement.preferredHeight = child.rect.height;

            CurrentWidth += containerLayoutElement.preferredWidth;
        }
        return true;
    }

    private void UpdateHeightForChild(RectTransform child)
    {
        if (layoutElement.preferredHeight < child.rect.height)
        {
            layoutElement.preferredHeight = child.rect.height;
        }
    }
}

} //BoTing.GamePublic