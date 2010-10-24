using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hades
{
    class BinaryTree
    {
        protected Node mRoot = null;

        #region Properties
        public Node Root
        {
            get { return mRoot; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Insertion entry method
        /// </summary>
        /// <param name="p"></param>
        public void Insert(Person p)
        {
            if (mRoot == null)
                mRoot = new Node(p);
            else
                Insert(mRoot, p);
        }

        /// <summary>
        /// Deletion entry method
        /// </summary>
        /// <param name="p"></param>
        public void Delete(Person p)
        {
            if (mRoot != null)
                Delete(mRoot, p);
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Code adapted from python code found at
        /// http://en.wikipedia.org/wiki/Binary_search_tree#Deletion
        /// <summary>
        protected void Delete(Node subroot, Person p)
        {
            if (subroot == mRoot)
            {

            }
            else if (p < subroot.Key)
                Delete(subroot.LeftChild, p);
            else if (p > subroot.Key)
                Delete(subroot.RightChild, p);
            else if (subroot.LeftChild != null && subroot.RightChild != null)
            {
                Node successor = LocalMin(subroot.RightChild);
                subroot.Key = successor.Key;
                successor.Replace(successor.RightChild);
            }
            else if (subroot.LeftChild != null && subroot.RightChild == null)
                subroot.Replace(subroot.LeftChild);
            else if (subroot.LeftChild == null && subroot.RightChild != null)
                subroot.Replace(subroot.RightChild);
            else
                subroot.Replace(null);
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
        protected Node Insert(Node subroot, Person p)
        {
            if (subroot == null)
            {
                subroot = new Node(p);
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
        #endregion
    }
}
