using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTree
{
    public class Node
    {
        public String data;
        public Object key;
        public Node left, right;

        public Node(Object key, String data)
        {
            this.data = data;
            this.key = key;
            left = null;
            right = null;
        }
    }

    public class BinarySearchTree
    {
        private Node root;
        private int count = 0;

        public BinarySearchTree()
        {
            root = null;
            count = 0;
        }

        public int Count
        {
            get
            {
                return this.count;
            }

            set
            {
                this.count = value;
            }
        }

        public Node Root
        {
            get { return this.root; }
        }


        private void CreateNode(Node node, ref Node tree)
        {
            if (tree == null)
                tree = node;
            else
            {
                int result = String.Compare(node.key.ToString(), tree.key.ToString());

                if (result == 0)
                    throw new Exception("Duplicate key...");
                else if (result < 0)
                    CreateNode(node, ref tree.left);
                else
                    CreateNode(node, ref tree.right);

            }
        }


        public Node Insert(Object key, String data)
        {
            Node node = new Node(key, data);
            try
            {
                if (root == null)
                    root = node;
                else
                    CreateNode(node, ref root);
                this.Count++;
                return node;
            }
            catch { return null; }
        }



        public Node Search(Node node, Object key)
        {
            if (node == null)
                return null;
            else
            {
                int result = String.Compare(key.ToString(), node.key.ToString());

                if (result < 0)
                    return Search(node.left, key);
                else if (result > 0)
                    return Search(node.right, key);

                else
                    return node;

            }
        }
    }
}
