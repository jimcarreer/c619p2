using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hades
{

    class RedBlackTree
    {
        protected Node mRoot = null;
        protected int mNodeCount = -1;
        public int countNodes;
        public TimeSpan runTime;
        public DateTime startTime;
        public DateTime endTime;

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
            while (current != Node.Sentinal)
            {
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
            startTime = DateTime.Now;
            Node current = mRoot;
            countNodes = 0;
            countNodes = Search(current,start, end);

            return countNodes;
        }
        
        
        public Node Insert(Person p)
        {
            Node current = mRoot;
            Node parent = null;
            Node added = new Node(p, null, Node.Sentinal, Node.Sentinal);
            while (current != Node.Sentinal)
            {
                parent = current;
                if (p < current.Key)
                {
                    if (current.LeftMax.Key.Died <= added.Key.Died)
                        current.LeftMax = added;
                    current = current.LeftChild;

                }
                else
                {
                    if (current.RightMax.Key.Died <= added.Key.Died)
                        current.RightMax = added;
                    current = current.RightChild;
                }
            }
            added.Parent = parent;
            added.Color = Node.Colors.Red;
            mNodeCount++;
            if (parent != null)
            {
                if (p < parent.Key)
                {
                    parent.LeftChild = added;
                    parent.LeftMax = added;
                }
                else
                {
                    parent.RightChild = added;
                    parent.RightMax = added;
                }
            }
            else
                mRoot = added;
            added.LeftMax = added;
            added.RightMax = added;
            FixInsert(added);
            return added;
        }
        #endregion

        #region Protected Methods 
        protected int Search(Node current,DateTime Start, DateTime Stop)
        {
            
        if(current == Node.Sentinal)
        {
        return 0;
        }
        countNodes = 0;
        if (current.Key.Born <= Stop && current.Key.Died >= Start)
            countNodes++;

        if (current.Key.Born <= Stop && current.RightMax.Key.Died >= Start)
        {
            countNodes += Search(current.RightChild, Start, Stop);
        }
        if (current.LeftMax.Key.Died >= Start)
        {
            countNodes += Search(current.LeftChild, Start, Stop);
        }
            
            endTime = DateTime.Now;
            calcRunTime();
        
            return countNodes;
        }
        protected void calcRunTime()
        {
            runTime = endTime - startTime;
           
        }
        protected TimeSpan getSearchRunTime()
        {
            return runTime;
        }
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

            //Fix aug data
            if (x.LeftChild == Node.Sentinal)
                x.LeftMax = x;
            else
                x.LeftMax = y.RightMax;

            y.RightMax = x.MaxMax; 
            FixAncestorMax(y);
        }

        private static int mFixReachedRoot = 0;
        public int FixReachedRoot { get { return mFixReachedRoot; } }
        private static void FixAncestorMax(Node y)
        {
            //Go back up the chain and correct
            Node parent = y.Parent;
            while (parent != null)
            {
                if (y == parent.RightChild && y != y.MaxMax && parent.RightMax.Key.Died <= y.MaxMax.Key.Died)
                    parent.RightMax = y.MaxMax;
                else if (y == parent.LeftMax && y != y.MaxMax && parent.LeftMax.Key.Died <= y.MaxMax.Key.Died)
                    parent.LeftMax = y.MaxMax;
                else
                    break;
                y = parent;
                parent = y.Parent;
            }
            if (parent == null)
                mFixReachedRoot++;
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

            //Fix aug data
            if (x.RightChild == Node.Sentinal)
                x.RightMax = x;
            else
                x.RightMax = y.LeftMax;

            y.LeftMax = x.MaxMax;
            FixAncestorMax(y);
        }

        private void FixDelete(Node x)
        {
            while (x != mRoot && x.Color == Node.Colors.Black)
            {
                Console.WriteLine("At " + x.Key.ToString());
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

        public void Delete(Person p)
        {
            Node target = Search(p);
            if (target != null)
                Delete(target);
        }
        
        private void Delete(Node target)
        {
            Node child, successor;

            if (target.LeftChild == Node.Sentinal || target.RightChild == Node.Sentinal)
                successor = target;
            else
            {
                //Find left most child of the right subtree
                successor = target.RightChild;
                while (successor.LeftChild != Node.Sentinal) 
                    successor = successor.LeftChild;
            }

            if (successor.LeftChild != Node.Sentinal)
                child = successor.LeftChild;
            else
                child = successor.RightChild;

            child.Parent = successor.Parent;
            if (successor.Parent != null)
                if (successor == successor.Parent.LeftChild)
                    successor.Parent.LeftChild = child;
                else
                    successor.Parent.RightChild = child;
            else if(child != Node.Sentinal)
                mRoot = child;
           
            //fix aug data
            //target is inner node
            if (target.RightChild != Node.Sentinal || target.LeftChild != Node.Sentinal)
            {
                if (successor.Parent.RightChild == Node.Sentinal)
                    successor.Parent.RightMax = successor.Parent;
                else
                    successor.Parent.RightMax = successor.RightChild.MaxMax;

                if (successor.Parent.LeftChild == Node.Sentinal)
                    successor.Parent.LeftMax = successor.Parent;
                else
                    successor.Parent.LeftMax = successor.LeftChild.MaxMax;
                FixDeleteAncestorMax(successor.Parent, successor);
            }
            //target is leaf
            else
            {
                if (target == target.Parent.LeftMax)
                    target.Parent.LeftMax = target.Parent;
                else if (target == target.Parent.RightMax)
                    target.Parent.RightMax = target.Parent;
                FixDeleteAncestorMax(target.Parent, target);
            }


            if (successor != target)
                target.Key = successor.Key;
            if (successor.Color == Node.Colors.Black && child != Node.Sentinal)
                FixDelete(child);
            mNodeCount--;
        }

        private static void FixDeleteAncestorMax(Node inner, Node successor)
        {
            while(inner.Parent != null)
            {
                if (inner == inner.Parent.LeftChild && successor == inner.Parent.LeftMax)
                    inner.Parent.LeftMax = inner.MaxMax;
                else if (inner == inner.Parent.RightChild && successor == inner.Parent.RightMax)
                    inner.Parent.RightMax = inner.MaxMax;
                else
                    break;
                inner = inner.Parent;
            }
        }
        #endregion
    }
}

