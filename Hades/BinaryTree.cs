using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hades
{
    class BinaryTree
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

        public bool Search(Person p, out int depth)
        {
            depth = -1;
            return Search(mRoot, p, ref depth) != null;
        }

        public bool Search(Person p)
        {
            int depth = -1;
            return Search(mRoot, p, ref depth) != null;
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
        /// Code adapted from java code found at
        /// http://en.wikipedia.org/wiki/Binary_search_tree#Insertion
        /// </summary>
        protected virtual Node Insert(Node subroot, Person p)
        {
            if (subroot == null)
            {
                subroot = new Node(p);
                mNodeCount++;
                return subroot;
            }
            else
            {
                if (p < subroot.Key)
                {
                    subroot.LeftChild = Insert(subroot.LeftChild, p);
                    subroot.LeftChild.Parent = subroot;
                }
                else
                {
                    subroot.RightChild = Insert(subroot.RightChild, p);
                    subroot.RightChild.Parent = subroot;
                }
            }
            return subroot;
        }

        /// <summary>
        /// Code adapted from python code found at
        /// http://en.wikipedia.org/wiki/Binary_search_tree#Searching
        /// </summary>
        protected virtual Node Search(Node subroot, Person p, ref int depth)
        {
            depth++;
            if (subroot == null)
                return null;
            if (p < subroot.Key)
                return Search(subroot.LeftChild, p, ref depth);
            else if (p > subroot.Key)
                return Search(subroot.RightChild, p, ref depth);
            else
                return subroot;
        }
        #endregion
    }
}
