﻿using System;
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

        //Basic Binary Tree Data
        public Node LeftChild = null;
        public Node RightChild = null;
        //Red Black Tree Augmentation Data
        public Node Parent = null;
        public Colors Color = Colors.Black;
        //Interval Tree Augmentation Data
        public DateTime RightMax = DateTime.MaxValue;
        public DateTime LeftMax = DateTime.MaxValue;

        //Sweet data center
        private Person mKey = null;
        //We actually use this when generating pretty graphs, not useful
        // in balancing the tree or answering queries.
        private Guid mGUID;
        #endregion

        #region Constructors
        public Node(Person p)
        {
            mGUID = Guid.NewGuid();
            mKey = p;
        }

        public Node(Person p, Node parent, Node right, Node left)
            :this(p)
        {
            Parent = parent;
            RightChild = right;
            LeftChild = left;
        }
        #endregion

        #region Properties
        public Person Key { get { return mKey; } }
        #endregion

        #region GraphViz Code Generater
        /// <summary>
        /// This function generates valid GraphViz dot graph notation code and
        /// writes it to the output stream.
        /// </summary>
        /// <param name="output">Stream to write generated code to</param>
        private void WriteDotCode(StreamWriter output)
        {
            string name = "Node_" + mGUID.ToString("N");
            string label = "label=\"<f0> " + LeftMax.ToString("MM/dd/yyyy");
            label += " | <f1> " + Key.ToString("G");
            label += " | <f2> " + LeftMax.ToString("MM/dd/yyyy") + "\", ";
            string options;
            if (Color == Colors.Black)
                options = "color=black";
            else
                options = "color=red";

            //Write the node's entry
            output.WriteLine(name + "[ " + label + options + " ];");

            //Fill in the subtrees
            if (RightChild != null)
                RightChild.WriteDotCode(output);
            if (LeftChild != null)
                LeftChild.WriteDotCode(output);

            //Write structural code
            if (RightChild != null)
                output.WriteLine("\"" + name + "\":f0 -> \"Node_" + RightChild.mGUID.ToString("N") + "\":f1");
            if (LeftChild != null)
                output.WriteLine("\"" + name + "\":f2 -> \"Node_" + LeftChild.mGUID.ToString("N") + "\":f1");

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