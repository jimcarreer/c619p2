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
            Person pp=null;
            Person p2 = null;
            for (int i = 0; i <= 100; i++)
            {
                Person p = pr.NextPerson();
                if (p == null)
                    break;
                if(i==50)
                pp = p;
                if (i==76)
                p2 = p;
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
            Node search=rb.Search(pp);
            Console.WriteLine("Searching for " + pp);
            Console.WriteLine("Search node " + search.Key);
            Node search2 = rb.Search(p2);
            Console.WriteLine("Searching for " + p2.Name);
            Console.WriteLine("Search node " + search2.Key.Name);
            DateTime start = new DateTime(1910, 1, 1);
            DateTime end = new DateTime(1919, 12, 31);
            int ct = rb.Search(start, end);
            Console.WriteLine(ct);
            Console.ReadKey();
        }
    }
}
