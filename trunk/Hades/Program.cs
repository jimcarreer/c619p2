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
            DateTime st = DateTime.Now;
            for (int i = 0; i <= 500; i++)
            {
                Person p = pr.NextPerson();
                if (p == null)
                    break;
                if(i==5)
                pp = p;
                if (i==6)
                p2 = p;
                rb.Insert(p);
            }
            DateTime stop = DateTime.Now;
            TimeSpan rbRun = stop-st;
            Console.WriteLine("RB Tree : " + rb.NodeCount);
            Console.WriteLine("Run Time : " + rbRun.TotalMilliseconds);
            Node search=rb.Search(pp);
            Console.WriteLine("Searching for " + pp);
            Console.WriteLine("Search node " + search.Key);
            Node search2 = rb.Search(p2);
            Console.WriteLine("Searching for " + p2.Name);
            Console.WriteLine("Search node " + search2.Key.Name);
            DateTime start = new DateTime(1900, 1, 1);
            DateTime end = new DateTime(1909, 12, 31);
            int ct = rb.Search(start, end);
            TimeSpan run = rb.runTime;
            Console.WriteLine("1900s-" + ct);
            Console.WriteLine("Run time: " + run.TotalMilliseconds+" Milliseconds");
            DateTime start1 = new DateTime(1910, 1, 1);
            DateTime end1 = new DateTime(1919, 12, 31);
            int ct1 = rb.Search(start, end);
            Console.WriteLine("1910s-"+ct1);
            TimeSpan run1 = rb.runTime;
            Console.WriteLine("Run time: " + run1.TotalMilliseconds + " Milliseconds");
            DateTime start2 = new DateTime(1920, 1, 1);
            DateTime end2 = new DateTime(1929, 12, 31);
            int ct2 = rb.Search(start2, end2);
            Console.WriteLine("1920s-" + ct2);
            TimeSpan run2 = rb.runTime;
            Console.WriteLine("Run time: " + run2.TotalMilliseconds + " Milliseconds");
            DateTime start3 = new DateTime(1930, 1, 1);
            DateTime end3 = new DateTime(1939, 12, 31);
            int ct3 = rb.Search(start3, end3);
            Console.WriteLine("1930s-" + ct3);
            TimeSpan run3 = rb.runTime;
            Console.WriteLine("Run time: " + run3.TotalMilliseconds + " Milliseconds");
            DateTime start4 = new DateTime(1940, 1, 1);
            DateTime end4 = new DateTime(1949, 12, 31);
            int ct4 = rb.Search(start4, end4);
            Console.WriteLine("1940s- " + ct4);
            TimeSpan run4 = rb.runTime;
            Console.WriteLine("Run time: " + run4.TotalMilliseconds + " Milliseconds");
            DateTime start5 = new DateTime(1950, 1, 1);
            DateTime end5 = new DateTime(1959, 12, 31);
            int ct5 = rb.Search(start5, end5);
            Console.WriteLine("1950s- " + ct5);
            TimeSpan run5 = rb.runTime;
            Console.WriteLine("Run time: " + run5.TotalMilliseconds + " Milliseconds");
            DateTime start6 = new DateTime(1960, 1, 1);
            DateTime end6 = new DateTime(1969, 12, 31);
            int ct6 = rb.Search(start6, end6);
            Console.WriteLine("1960s-" + ct6);
            TimeSpan run6 = rb.runTime;
            Console.WriteLine("Run time: " + run6.TotalMilliseconds + " Milliseconds");
            DateTime start7 = new DateTime(1970, 1, 1);
            DateTime end7 = new DateTime(1979, 12, 31);
            int ct7 = rb.Search(start7, end7);
            Console.WriteLine("1970s-" + ct7);
            TimeSpan run7 = rb.runTime;
            Console.WriteLine("Run time: " + run7.TotalMilliseconds + " Milliseconds");
            DateTime start8 = new DateTime(1980, 1, 1);
            DateTime end8 = new DateTime(1989, 12, 31);
            int ct8 = rb.Search(start8, end8);
            Console.WriteLine("1980s-" + ct8);
            TimeSpan run8 = rb.runTime;
            Console.WriteLine("Run time: " + run8.TotalMilliseconds + " Milliseconds");
            DateTime start9 = new DateTime(1990, 1, 1);
            DateTime end9 = new DateTime(1999, 12, 31);
            int ct9 = rb.Search(start9, end9);
            Console.WriteLine("1990s-" + ct9);
            TimeSpan run9 = rb.runTime;
            Console.WriteLine("Run time: " + run9.TotalMilliseconds + " Milliseconds");
            DateTime start10 = new DateTime(2000, 1, 1);
            DateTime end10 = new DateTime(2009, 12, 31);
            int ct10 = rb.Search(start10, end10);
            Console.WriteLine("2000s-" + ct10);
            TimeSpan run10 = rb.runTime;
            Console.WriteLine("Run time: " + run10.TotalMilliseconds + " Milliseconds");
            DateTime start11 = new DateTime(2010, 1, 1);
            DateTime end11 = new DateTime(2010, 11, 1);
            int ct11 = rb.Search(start11, end11);
            Console.WriteLine("2010-" + ct11);
            TimeSpan run11 = rb.runTime;
            Console.WriteLine("Run time: " + run11.TotalMilliseconds + " Milliseconds");
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
            rb.Root.WriteDotGraph("rb-final.gvd");
            Console.ReadKey();

           
        }
    }
}
