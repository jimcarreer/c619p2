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
        public Node Root{ get { return mRoot; } }
        public int NodeCount { get { return mNodeCount; } }
        #endregion

        #region Constuctors
        public RedBlackTree()
        {
            mRoot = Node.Sentinal;
        }
        #endregion

        #region Public Methods
        public Node.Colors GetNodeColor(Person p)
        {
            int depth = -1;
            return GetNodeColor(mRoot, p, ref depth);
        }
        public Node Search(Person p)
        {
            Node current = mRoot;
            Node parent = null;
            while (current != Node.Sentinal)
            {
                parent = current;
                if (p < current.Key)
                    current = current.LeftChild;
                else if (p > current.Key)
                    current = current.RightChild;
                else if (p == current.Key)
                    return current;
                else
                    return null;
            }
            return null;
        }
        public int Search(DateTime start, DateTime end)
        {
            int countNodes=0;
            Node current = mRoot;
            Node parent = null;
            /*
            while (current != Node.Sentinal)
            {
                parent = current;
                if (start > current.Key.Died)
                    

            }
             * */
            return countNodes;
        }

        public Node Insert(Person p)
        {
            Node current = mRoot;
            Node parent = null;
            while (current != Node.Sentinal)
            {
                parent = current;
                if (p < current.Key)
                    current = current.LeftChild;
                else
                    current = current.RightChild;
            }
            Node added = new Node(p, parent, Node.Sentinal, Node.Sentinal);
            added.Color = Node.Colors.Red;
            mNodeCount++;
            if (parent != null)
            {
                if (p < parent.Key)
                    parent.LeftChild = added;
                else
                    parent.RightChild = added;
            }
            else
                mRoot = added;
            FixInsert(added);
            return added;
        }
        #endregion

        #region Protected Methods 
        protected Node.Colors GetNodeColor(Node subroot, Person p, ref int depth)
        {
            if (p == subroot.Key)
                return subroot.Color;
            else if (p > subroot.Key)
                return GetNodeColor(subroot.RightChild, p, ref depth);
            else
                return GetNodeColor(subroot.LeftChild, p, ref depth);
        }

        protected virtual void FixInsert(Node x)
        {
            while (x != mRoot && x.Parent.Color == Node.Colors.Red)
            {
                if (x.Parent == x.GrandParent.LeftChild)
                {
                    if (x.Uncle.Color == Node.Colors.Red)
                    {
                        x.Parent.Color = Node.Colors.Black;
                        x.Uncle.Color = Node.Colors.Black;
                        x.GrandParent.Color = Node.Colors.Red;
                        x = x.GrandParent;
                    }
                    else
                    {
                        if (x == x.Parent.RightChild)
                        {
                            x = x.Parent;
                            RotateLeft(x);
                        }
                        x.Parent.Color = Node.Colors.Black;
                        x.GrandParent.Color = Node.Colors.Red;
                        RotateRight(x.GrandParent);
                    }
                }
                else
                {
                    if (x.Uncle.Color == Node.Colors.Red)
                    {
                        x.Parent.Color = Node.Colors.Black;
                        x.Uncle.Color = Node.Colors.Black;
                        x.GrandParent.Color = Node.Colors.Red;
                        x = x.GrandParent;
                    }
                    else
                    {
                        if (x == x.Parent.LeftChild)
                        {
                            x = x.Parent;
                            RotateRight(x);
                        }
                        x.Parent.Color = Node.Colors.Black;
                        x.GrandParent.Color = Node.Colors.Red;
                        RotateLeft(x.GrandParent);
                    }
                }
            }
            mRoot.Color = Node.Colors.Black;
        }

        private void RotateRight(Node x)
        {
            Node y = x.LeftChild;
            x.LeftChild = y.RightChild;
            if (y.RightChild != Node.Sentinal) 
                y.RightChild.Parent = x;
            if (y != Node.Sentinal)
                y.Parent = x.Parent;
            if (x.Parent != null)
            {
                if (x == x.Parent.RightChild)
                    x.Parent.RightChild = y;
                else
                    x.Parent.LeftChild = y;
            }
            else
                mRoot = y;

            y.RightChild = x;
            if (x != Node.Sentinal)
                x.Parent = y;
        }

        private void RotateLeft(Node x)
        {
            Node y = x.RightChild;
            x.RightChild = y.LeftChild;
            if (y.LeftChild != Node.Sentinal)
                y.LeftChild.Parent = x;
            if (y != Node.Sentinal)
                y.Parent = x.Parent;
            if(x.Parent != null)
            {
                if(x == x.Parent.LeftChild)
                    x.Parent.LeftChild = y;
                else
                    x.Parent.RightChild = y;
            }
            else
                mRoot = y;

            y.LeftChild = x;
            if(x != Node.Sentinal)
                x.Parent = y;
        }

        private void FixDelete(Node x)
        {
            while (x != mRoot && x.Color == Node.Colors.Black)
            {
                if (x == x.Parent.LeftChild)
                {
                    Node w = x.Parent.RightChild;
                    if (w.Color == Node.Colors.Red)
                    {
                        w.Color = Node.Colors.Black;
                        x.Parent.Color = Node.Colors.Red;
                        RotateLeft(x.Parent);
                        w = x.Parent.RightChild;
                    }
                    if (w.LeftChild.Color == Node.Colors.Black && w.RightChild.Color == Node.Colors.Black)
                    {
                        w.Color = Node.Colors.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.RightChild.Color == Node.Colors.Black)
                        {
                            w.LeftChild.Color = Node.Colors.Black;
                            w.Color = Node.Colors.Red;
                            RotateLeft(x.Parent);
                            x = mRoot;
                        }
                    }
                }
                else
                {
                    Node w = x.Parent.LeftChild;
                    if (w.Color == Node.Colors.Red)
                    {
                        w.Color = Node.Colors.Black;
                        x.Parent.Color = Node.Colors.Red;
                        RotateRight(x.Parent);
                        w = x.Parent.LeftChild;
                    }
                    if (w.RightChild.Color == Node.Colors.Black && w.LeftChild.Color == Node.Colors.Black)
                    {
                        w.Color = Node.Colors.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.LeftChild.Color == Node.Colors.Black)
                        {
                            w.RightChild.Color = Node.Colors.Black;
                            w.Color = Node.Colors.Red;
                            RotateLeft(w);
                            w = x.Parent.LeftChild;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.Color = Node.Colors.Black;
                        w.LeftChild.Color = Node.Colors.Black;
                        RotateRight(x.Parent);
                        x = mRoot;
                    }
                }
            }
            x.Color = Node.Colors.Black;
        }

        public void Delete(Node z)
        {
            Node x, y;

            if (z.LeftChild == Node.Sentinal || z.RightChild == Node.Sentinal)
                y = z;
            else
            {
                y = z.RightChild;
                while (y.LeftChild != Node.Sentinal) y = y.LeftChild;
            }

            if (y.LeftChild != Node.Sentinal)
                x = y.LeftChild;
            else
                x = y.RightChild;

            x.Parent = y.Parent;
            if (y.Parent != null)
                if (y == y.Parent.LeftChild)
                    y.Parent.LeftChild = x;
                else
                    y.Parent.RightChild = x;
            else
                mRoot = x;

            if (y != z)
                z.Key = y.Key;

            if (y.Color == Node.Colors.Black)
                FixDelete(x);

        }
        #endregion
    }
}

