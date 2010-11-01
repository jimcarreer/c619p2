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
            for (int i = 0; i <= 400; i++)
            {
                Person p = pr.NextPerson();
                if (p == null)
                    break;
                if(i==5)
                pp = p;
                if (i==6)
                p2 = p;
                rb.Insert(p);
                //bt.Insert(p);
               // rb.Root.WriteDotGraph("rb-insert" + i + ".gvd");
                //bt.Root.WriteDotGraph("bt-insert" + i + ".gvd");
                
                //Console.WriteLine("Inserting " + p);
            }
            //bt.Root.WriteDotGraph("bt-final.gvd");
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
            Console.WriteLine("1910s-"+ct);
            DateTime start2 = new DateTime(1920, 1, 1);
            DateTime end2 = new DateTime(1929, 12, 31);
            int ct2 = rb.Search(start2, end2);
            Console.WriteLine("1920s-" + ct2);
            DateTime start3 = new DateTime(1930, 1, 1);
            DateTime end3 = new DateTime(1939, 12, 31);
            int ct3 = rb.Search(start3, end3);
            Console.WriteLine("1910s-" + ct3);
            DateTime start4 = new DateTime(1940, 1, 1);
            DateTime end4 = new DateTime(1949, 12, 31);
            int ct4 = rb.Search(start4, end4);
            Console.WriteLine("1940s- " + ct4);
            DateTime start5 = new DateTime(1950, 1, 1);
            DateTime end5 = new DateTime(1959, 12, 31);
            int ct5 = rb.Search(start5, end5);
            Console.WriteLine("1950s- " + ct5);
            DateTime start6 = new DateTime(1960, 1, 1);
            DateTime end6 = new DateTime(1969, 12, 31);
            int ct6 = rb.Search(start6, end6);
            Console.WriteLine("1960s-" + ct);
            rb.Root.WriteDotGraph("rb-serch.gvd");
            Console.WriteLine("Fix reached root: " + rb.FixReachedRoot.ToString());
            Console.WriteLine("Deleting " + pp.ToString() + " ... ");
            rb.Delete(pp);
            rb.Root.WriteDotGraph("rb-del1.gvd");
            Console.WriteLine("RB Tree : " + rb.NodeCount);
            Console.WriteLine("Deleting " + p2.ToString() + " ... ");
            rb.Delete(p2);
            rb.Root.WriteDotGraph("rb-del2.gvd");
            Console.WriteLine("RB Tree : " + rb.NodeCount);
            //Node search=rb.Search(pp);
            //Console.WriteLine("Searching for " + pp);
            //Console.WriteLine("Search node " + search.Key);
            //Node search2 = rb.Search(p2);
            //Console.WriteLine("Searching for " + p2.Name);
            //Console.WriteLine("Search node " + search2.Key.Name);
            //DateTime start = new DateTime(1910, 1, 1);
            //DateTime end = new DateTime(1919, 12, 31);
            //int ct = rb.Search(start, end);
            //Console.WriteLine(ct);
           
           // Console.WriteLine(search2.Key.Name + " was deleted");

            rb.Root.WriteDotGraph("rb-final.gvd");
            Console.ReadKey();

           
        }
    }
}
