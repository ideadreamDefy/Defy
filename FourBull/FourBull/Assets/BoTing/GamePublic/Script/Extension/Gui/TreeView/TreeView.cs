using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BoTing.GamePublic
{
    public class TreeView : UIComponentBase
    {
        /// <summary>
        /// TreeView的内容容器，一个独立的面板，可以放在ScrollView的content的节点下或其他地方。
        /// This is the panel used to contain all the tree items.
        /// You can put this panel as a child to ScrollView's content node.
        /// </summary>
        public GameObject contentPanel;

        /// <summary>
        /// 树状界面的信息索引
        /// The index information for TreeView.
        /// </summary>
        protected List<TreeNode> treeNodes = new List<TreeNode>();
        public List<TreeNode> TreeNodes
        {
            get { return treeNodes; }
            set
            {
                treeNodes = value;
                InitailizeView();
            }
        }

        private float duration = 0.04f;
        private float treeHeight = 0;

        protected override void OnStartView()
        {
            base.OnStartView();
            if (contentPanel)
            {
                // Set Anchor to LeftTop.
                // Set Pivot to (0, 1).
                var parent = contentPanel.transform.parent as RectTransform;
                if (parent)
                {
                    parent.pivot = new Vector2(0f, 1f);
                    parent.anchoredPosition3D = new Vector3(0f, 0f, 0f);
                    parent.anchorMin = new Vector2(0f, 1f);
                    parent.anchorMin = new Vector2(0f, 1f);
                    parent.localPosition = new Vector3(0f, 0f, 0f);
                }

                // Anchor should be LeftTop.
                // Set Pivot to (0, 1).
                var rectTransform = contentPanel.transform as RectTransform;
                rectTransform.pivot = new Vector2(0f, 1f);
                rectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
                rectTransform.anchorMin = new Vector2(0f, 1f);
                rectTransform.anchorMin = new Vector2(0f, 1f);
                rectTransform.localPosition = new Vector3(0f, 0f, 0f);

                if (contentPanel.GetComponent<VerticalLayoutGroup>() == null)
                {
                    var verticalLayout = contentPanel.AddComponent<VerticalLayoutGroup>();
                    verticalLayout.childForceExpandWidth = true;
                    verticalLayout.childForceExpandHeight = false;
                }
            }
        }

        /// <summary>
        /// 根据每个treeNode获取数据信息，并创建UI对象。
        /// Method CreateTreeNode is called for every TreeNode is TreeNodes data source to create the Node UI GameObject.
        /// The created GameObject will be added to the tree as Branch or Leaf.
        /// </summary>
        /// <param name="treeNode">The index and information for this node</param>
        /// <returns></returns>
        protected virtual GameObject CreateTreeNode(TreeNode treeNode)
        {
            return new GameObject();
        }

        /// <summary>
        /// The last GameObject has been created for TreeView.
        /// It is used to calculate the height for TreeView.
        /// </summary>
        private GameObject theLastObject;


        private RectTransform ParentRectTransform
        {
            get { return gameObject.transform.parent as RectTransform; }
        }

        /// <summary>
        /// 重写InitailizeData来初始化数据源。
        /// Override this method to initialize the data source.
        /// </summary>
        protected virtual void InitailizeData()
        {
            
        }

        /// <summary>
        /// Call this method to initialize the tree view.
        /// </summary>
        protected virtual void InitailizeView()
        {
            if (contentPanel == null || TreeNodes == null)
            {
                Debug.LogError("ContentPanel or Data Source has not been set for TreeView.");
                return;
            }

            UIUtil.RemoveAllChildren(contentPanel);

            treeHeight = 0;

            foreach (var node in TreeNodes)
            {
                CreateTree(contentPanel, node);
            }

            UpdateContentPanelSize();
        }

        protected void CreateTree(GameObject parentObject, TreeNode treeNode)
        {
            do
            {
                var nodeObject = CreateTreeNode(treeNode);
                if (!nodeObject)
                {
                    break;
                }

                treeHeight += GetNodeHeight(nodeObject);
                UpdateContentPanelSize();

                // Add node object to tree.
                nodeObject.transform.SetParent(parentObject.transform, false);
                nodeObject.transform.SetAsLastSibling();
                nodeObject.transform.localScale = Vector3.one;

                theLastObject = nodeObject;

                // For branch node.
                if (treeNode.Type != TreeNode.NodeType.Branch)
                {
                    break;
                }

                var nodes = (treeNode as TreeBranch).Children;
                if (nodes == null || nodes.Count == 0)
                {
                    break;
                }

                float oldHeight = treeHeight;
                float addedHeight = 0;

                // Set Padding here if you want.
                GameObject grid = new GameObject();
                grid.name = "Grid" + treeNode.Index;

                grid.AddComponent<RectTransform>();
                var verticalLayoutGroup = grid.AddComponent<VerticalLayoutGroup>();
                verticalLayoutGroup.childForceExpandWidth = false;
                verticalLayoutGroup.childForceExpandHeight = false;

                grid.transform.SetParent(parentObject.transform, false);
                grid.transform.SetAsLastSibling();
                foreach (var node in nodes)
                {
                    CreateTree(grid, node);
                }

                var button = nodeObject.GetComponent<Button>();
                if (!button)
                {
                    Debug.LogError("Tree Branch should have a Button component.");
                    break;
                }

                addedHeight = treeHeight - oldHeight;
                button.onClick.AddListener(() =>
                {
                    bool shown = true;
                    bool hidden = true;
                    for (int i = 0; i < grid.transform.childCount; ++i)
                    {
                        GameObject node = grid.transform.GetChild(i).gameObject;
                        if (node.activeSelf)
                        {
                            hidden = false;
                        }
                        else
                        {
                            shown = false;
                        }
                    }

                    if (!shown && !hidden)
                    {
                        return;
                    }

                    if (shown)
                    {
                        for (int i = 0; i < grid.transform.childCount; ++i)
                        {
                            // Instantiate Avatar
                            GameObject node = grid.transform.GetChild(i).gameObject;

                            LeanTween.delayedCall(node, (i + 1) * duration, () =>
                            {
                                node.SetActive(false);
                            });
                        }
                        LeanTween.delayedCall(grid, grid.transform.childCount * duration, () =>
                        {
                            treeHeight -= addedHeight;
                            UpdateContentPanelSize();
                        });
                    }

                    if (hidden)
                    {
                        for (int i = grid.transform.childCount - 1; i >= 0; --i)
                        {
                            // Instantiate Avatar
                            GameObject node = grid.transform.GetChild(i).gameObject;

                            LeanTween.delayedCall(node, (i + 1) * duration, () =>
                            {
                                node.SetActive(true);
                            });
                        }

                        treeHeight += addedHeight;
                        UpdateContentPanelSize();
                    }
                });
            } while (false);
        }

        protected void UpdateContentPanelSize()
        {
            var contentRectTransform = contentPanel.transform as RectTransform;
            contentRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, treeHeight);
            UpdateParentContentSize();
        }

        protected void UpdateParentContentSize()
        {
            ParentRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (contentPanel.transform as RectTransform).rect.height);
        }

        private float GetNodeHeight(GameObject nodeObject)
        {
            float height = 0;
            height = (nodeObject.transform as RectTransform).rect.height;
            if (height == 0)
            {
                var layoutElement = nodeObject.GetComponent<LayoutElement>();
                if (layoutElement)
                {
                    height = layoutElement.preferredHeight;
                }
            }
            return height;
        }
    }
}