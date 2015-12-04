using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace BoTing.GamePublic
{
    public abstract class TreeNode
    {
        public enum NodeType
        {
            Node = 0, //
            Branch,   // 大节点，一个大节点下 多个叶子节点。
            Leaf,     // 叶子节点
        }

        protected NodeType type = NodeType.Node;

        public NodeType Type
        {
            get { return type; }
        }

        public TreeNode Parent
        {
            get;
            set;
        }

        protected List<TreeNode> children = new List<TreeNode>();
        public List<TreeNode> Children
        {
            get { return children; }
            set { children = value; }
        }

        private int index = -1;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
    }

    public class TreeLeaf : TreeNode
    {
        public TreeLeaf()
        {
            type = NodeType.Leaf;
        }
    }

    public class TreeBranch : TreeNode
    {
        public TreeBranch()
        {
            type = NodeType.Branch;
        }
    }
}