using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hades
{



    class RedBlackTree
    {
        protected Node mRoot = null;

        protected int mNodeCount = 0;

        #region Properties
        public Node Root
        {
            get { return mRoot; }
        }
        public int NodeCount
        {
            get { return mNodeCount; }
        }
        #endregion

        


        public RedBlackTree()
        {
        }
        #region Public Methods
        /// <summary>
        /// Insertion entry method
        /// </summary>
        /// <param name="p">New person to insert</param>
        public void Insert(Person p)
        {
            if (mRoot == null)
            {
                mRoot = new Node(p);
                mRoot.Parent = mRoot;
               
            }
            else
                Insert(mRoot, p);
        }

        /// <summary>
        /// Deletion entry method
        /// </summary>
        ///<param name="p">Person to delete</param>
        public void Delete(Person p)
        {
            if (mRoot != null)
                Delete(mRoot, p);
        }




        public bool Search(Person p)
        {
            int depth = -1;
            return Search(mRoot, p, ref depth);
        }

        public Node.Colors getColor(Person p)
        {
            int depth = -1;
            return getColor(mRoot, p, ref depth);
        }


        #endregion

        #region Protected Methods
        /// <summary>
        /// Code adapted from python code found at
        /// http://en.wikipedia.org/wiki/Binary_search_tree#Deletion
        /// <summary>
        protected virtual Node Delete(Node subroot, Person p)
        {
            //All these cases are when we are still searching
            if (p < subroot.Key)
                return Delete(subroot.LeftChild, p);
            else if (p > subroot.Key)
                return Delete(subroot.RightChild, p);
            //All these cases are when the node is found
            else if (subroot.LeftChild != null && subroot.RightChild != null)
            {
                Node successor = LocalMin(subroot.RightChild);
                subroot.Key = successor.Key;
                successor.ReplaceInParent(successor.RightChild);
                mNodeCount--;
            }
            else if (subroot.LeftChild != null && subroot.RightChild == null)
            {
                subroot.ReplaceInParent(subroot.LeftChild);
                if (subroot == mRoot)
                    mRoot = mRoot.LeftChild;
                mNodeCount--;
            }
            else if (subroot.LeftChild == null && subroot.RightChild != null)
            {
                subroot.ReplaceInParent(subroot.RightChild);
                if (subroot == mRoot)
                    mRoot = mRoot.RightChild;
                mNodeCount--;
            }
            else
            {
                subroot.ReplaceInParent(null);
                if (subroot == mRoot)
                    mRoot = null;
                mNodeCount--;
            }

            if (subroot == null)
                return null;

            return subroot;
        }

        protected Node LocalMin(Node subroot)
        {
            Node current = subroot;
            while (current.LeftChild != null)
                current = current.LeftChild;
            return current;
        }
      

        /// <summary>
        /// Code adapted from python code found at
        /// http://en.wikipedia.org/wiki/Binary_search_tree#Searching
        /// </summary>
        protected bool Search(Node subroot, Person p, ref int depth)
        {
            depth++;
            if (subroot == null)
                return false;
            if (p < subroot.Key)
                return Search(subroot.LeftChild, p, ref depth);
            else if (p > subroot.Key)
                return Search(subroot.RightChild, p, ref depth);
            else
                return true;
        }

        protected Node.Colors getColor(Node subroot, Person p, ref int depth)
        {

            if (p == subroot.Key)
                return subroot.Color;
            else if (p > subroot.Key)
                return getColor(subroot.RightChild, p, ref depth);
            else if (p < subroot.Key)
                return getColor(subroot.LeftChild, p, ref depth);
            else
                return Node.Colors.bad;
        }
        

        /// <summary>
        /// Code adapted from java code found at
        /// http://en.wikipedia.org/wiki/Binary_search_tree#Insertion
        /// </summary>
        protected Node Insert(Node subroot, Person p)
        {
            if (subroot == null)
            {
                subroot = new Node(p);
                return subroot;
            }
            else
            {
                if (p != null && subroot != null)
                {
                    if (p < subroot.Key)
                    {
                        subroot.LeftChild = Insert(subroot.LeftChild, p);
                        subroot.LeftChild.Parent = subroot;
                        if (subroot.LeftChild.Color == Node.Colors.Red &&
                        subroot.RightChild.Color == Node.Colors.Red)
                            ReArrange(p,subroot);
                    }
                    else
                    {
                        subroot.RightChild = Insert(subroot.RightChild, p);
                        subroot.RightChild.Parent = subroot;

                        if (subroot.RightChild.Color == Node.Colors.Red &&
                        subroot.LeftChild.Color == Node.Colors.Red)
                        ReArrange(p, subroot);
                    }
                }
            }
            return subroot;
            
        }

        protected void ReArrange(Person item, Node currentNode)
        {
            Node parentNode = currentNode.Parent;
            Node grandParentNode = parentNode.Parent;
            Node tempNode = grandParentNode;
            currentNode.Color = Node.Colors.Red;
            currentNode.LeftChild.Color = Node.Colors.Black;
            currentNode.RightChild.Color = Node.Colors.Black;

            if (parentNode.Color == Node.Colors.Red)
            {
                grandParentNode.Color = Node.Colors.Red;

                if(!item.Equals(grandParentNode.person))
                    parentNode = Rotate(item, grandParentNode);

                currentNode = Rotate(item, tempNode);
                currentNode.Color = Node.Colors.Black;
            }

            mRoot.RightChild.Color = Node.Colors.Black;
        }


        private Node Rotate(Person item, Node parentNode)
        {
            int value;

            if (!(item.Equals(parentNode.LeftChild.person)))
            {
                if(item.Equals(parentNode.LeftChild.person))
                    value=0;
                else if(item < parentNode.LeftChild.person)
                    value=-1;
                else
                    value=1;

                if (value < 0)
                    parentNode.LeftChild = this.Rotate(
                    parentNode.LeftChild, Node.Direction.Left);
                else
                    parentNode.LeftChild = this.Rotate(
                    parentNode.LeftChild, Node.Direction.Right);
                return parentNode.LeftChild;
            }
            else
            {
                if(item.Equals(parentNode.RightChild.person))
                    value=0;
                else if(item < parentNode.RightChild.person)
                    value=-1;
                else
                    value=1;
                if (value < 0)
                    parentNode.RightChild = this.Rotate(
                    parentNode.RightChild, Node.Direction.Right);
                else
                    parentNode.RightChild = this.Rotate(
                    parentNode.RightChild, Node.Direction.Right);
                return parentNode.RightChild;
            }
        }

        

        private Node Rotate(Node node, Node.Direction direction)
        {
            Node tempNode;

            if (direction == Node.Direction.Left)
            {
                tempNode = node.LeftChild;
                node.LeftChild= tempNode.RightChild;
                tempNode.RightChild = node;
                return tempNode;
            }

            else
            {
                tempNode = node.RightChild;
                node.RightChild = tempNode.LeftChild;
                tempNode.LeftChild = node;
                return tempNode;
            }
        }

        #endregion
    }
}

