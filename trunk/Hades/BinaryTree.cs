using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hades
{
    class BinaryTree
    {
        protected Node mRoot = null;

        public Node Root
        {
            get { return mRoot; }
        }

        public void Insert(Person p)
        {
            if (mRoot == null)
                mRoot = new Node(p);
            else
                Insert(mRoot, p);
        }

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
    }
}
