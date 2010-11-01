using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hades
     
{
    class Node
    {
        #region Members
        public enum Colors { Red, Black };
        public enum Direction { Left, Right };

        public static Node Sentinal = new Node();

        //Basic Binary Tree Data
        public Node LeftChild = null;
        public Node RightChild = null;
        //Red Black Tree Augmentation Data
        public Node Parent = null;
        public Colors Color = Colors.Black;
        //Interval Tree Augmentation Data
        public Node RightMax;
        public Node LeftMax;
        //Sweet data center
        public Person Key = null;

        //We actually use this when generating pretty graphs, not useful
        // in balancing the tree or answering queries.
        private Guid mGUID;
        #endregion

        #region Constructors
        private Node()
        {
            Key = new Person("SENTINAL");
            RightChild = this;
            LeftChild = this;
            Parent = null;
            Color = Colors.Black;
            LeftMax = this;
            RightMax = this;
        }

        public Node(Person p)
        {
            mGUID = Guid.NewGuid();
            Key = p;
            LeftMax = Node.Sentinal;
            RightMax = Node.Sentinal;
            Parent = null;
            LeftChild = null;
            RightChild = null;
        }

        public Node(Person p, Node parent, Node right, Node left)
            :this(p)
        {
            Parent = parent;
            RightChild = right;
            LeftChild = left;
        }

        #endregion

        #region Public Methods
        public virtual void ReplaceInParent(Node replacement)
        {
            if (this.Parent == null)
                throw new Exception("This node has no parent to be replaced in");

            if (this == this.Parent.LeftChild)
                Parent.LeftChild = replacement;
            else
                Parent.RightChild = replacement;
            if (replacement != null)
                replacement.Parent = this.Parent;
        }

        public Node MaxMax
        {
            get
            {
                if (this.Key.Died >= LeftMax.Key.Died && this.Key.Died >= RightMax.Key.Died)
                    return this;
                if (RightMax.Key.Died >= LeftMax.Key.Died)
                    return RightMax;
                else
                    return LeftMax;
            }
        }

        public virtual void NullifyFamily()
        {
            this.Parent = null;
            this.RightChild = null;
            this.LeftChild = null;
        }
        
        #endregion

        #region Various relations
        public Node GrandParent
        {
            get
            {
                if (this.Parent != null)
                    return this.Parent.Parent;
                else
                    return null;
            }
        }

        public Node Uncle
        {
            get
            {
                Node g = this.GrandParent;
                if (g == null)
                    return null;
                if (this.Parent == g.LeftChild)
                    return g.RightChild;
                else
                    return g.LeftChild;
            }
        }

        public Node Sibling
        {
            get
            {
                if(this.Parent == null)
                    return null;
                if (this == this.Parent.LeftChild)
                    return this.Parent.RightChild;
                else
                    return this.Parent.LeftChild;
            }
        }
        #endregion

        #region GraphViz Code Generater
        /// <summary>
        /// This function generates valid GraphViz dot graph notation code and
        /// writes it to the output stream.
        /// </summary>
        /// <param name="output">Stream to write generated code to</param>
        private void WriteDotCode(StreamWriter output)
        {
            //Must be sentinal
            if (this == Node.Sentinal)
                return;

            string name = "Node_" + mGUID.ToString("N");
            string label = "label=\"";
            if(!LeftMax.Key.IsAlive)
                label += "<f0> " + LeftMax.Key.Died.ToString("MM/dd/yyyy");
            else
                label += "<f0> PRESENT ";
            label += " | <f1> " + Key.ToString("G");
            if (!RightMax.Key.IsAlive)
                label += " | <f2> " + RightMax.Key.Died.ToString("MM/dd/yyyy");
            else
                label += " | <f2> PRESENT";
            label += "\", ";
            string options;
            if (Color == Colors.Black)
                options = "color=black";
            else
                options = "color=red";

            //Write the node's entry
            output.WriteLine(name + "[ " + label + options + " ];");

            //Fill in the subtrees
            if (RightChild != null && RightChild != Node.Sentinal)
                RightChild.WriteDotCode(output);
            if (LeftChild != null && LeftChild != Node.Sentinal)
                LeftChild.WriteDotCode(output);

            //Write structural code
            if (LeftChild != null && LeftChild != Node.Sentinal)
                output.WriteLine("\"" + name + "\":f0 -> \"Node_" + LeftChild.mGUID.ToString("N") + "\":f1");
            if (RightChild != null && RightChild != Node.Sentinal)
                output.WriteLine("\"" + name + "\":f2 -> \"Node_" + RightChild.mGUID.ToString("N") + "\":f1");
            if (RightChild == Node.Sentinal)
            {
                string rightguid = Guid.NewGuid().ToString("N");
                output.WriteLine("Node_" + rightguid + " [label=NULL, color=black]");
                output.WriteLine("\"" + name + "\":f2 -> \"Node_" + rightguid + "\"");
            }
            if (LeftChild == Node.Sentinal)
            {
                string leftguid = Guid.NewGuid().ToString("N");
                output.WriteLine("Node_" + leftguid + " [label=NULL, color=black]");
                output.WriteLine("\"" + name + "\":f0 -> \"Node_" + leftguid + "\"");
            }
        }

        /// <summary>
        /// Generates GraphViz dot graph notation code and writes it to an
        /// output file.
        /// </summary>
        /// <param name="output">Name of file to write generated code to</param>
        public void WriteDotGraph(string output)
        {
            StreamWriter writer = new StreamWriter(new FileStream(output, FileMode.Create));
            writer.WriteLine("digraph G");
            writer.WriteLine("{");
            writer.WriteLine("node [shape=record];");
            WriteDotCode(writer);
            writer.WriteLine("}");
            writer.Close();
        }
        #endregion
    }
}
