using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Hades
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonReader pr = new PersonReader("people.dat");
            RedBlackTree rb = new RedBlackTree();
            BinaryTree bt = new BinaryTree();
            for (int i = 0; i <= 10; i++)
            {
                Person p = pr.NextPerson();
                if (p == null)
                    break;

                rb.Insert(p);
                bt.Insert(p);
                rb.Root.WriteDotGraph("rb-insert" + i + ".gvd");
                bt.Root.WriteDotGraph("bt-insert" + i + ".gvd");

                Console.WriteLine("Inserting " + p);
            }
            rb.Root.WriteDotGraph("rb-final.gvd");
            bt.Root.WriteDotGraph("bt-final.gvd");
            Console.WriteLine("RB Tree : " + rb.NodeCount);
            Console.WriteLine("BI Tree : " + bt.NodeCount);
            Console.ReadKey();
        }
    }
}
