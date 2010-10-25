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
            //BirthdayGenerator bg = new BirthdayGenerator(1832, 1923);
            //Random rand = new Random();

            PersonReader pr = new PersonReader("people.dat");
            Person testdelete = null;
            Person testdelete2 = null;
            RedBlackTree rbTree = new RedBlackTree();
            BinaryTree btree = new BinaryTree();
            Person p = null;
            int read = 0;
            for (int i = 0; i <= 20; i++)

            {
                //int lifespan = rand.Next(25, 80);
                //DateTime birthdate = bg.NextBirthday();
                //DateTime deathdate = birthdate.AddYears(lifespan);
                //p = new Person("Test" + i.ToString(), birthdate, deathdate);
                read++;
                p = pr.NextPerson();

                rbTree.Insert(p);

                if (p == null)
                    break;

                Console.WriteLine("Inserting " + p);
                if (i == 10)
                    testdelete = p;
                if(i==11)
                    testdelete2 = p;
               //btree.Insert(p);

                
            
            }

            
          
            //Search depth
            int depth = 0;
            //Initial tree
          // btree.Root.WriteDotGraph("initial.gvd");
           rbTree.Root.WriteDotGraph("initial.gvd");
            Console.WriteLine("Node Count: " + rbTree.NodeCount);
            
            /*
            //Remove testdelete, search before and after
            if (btree.Search(testdelete, out depth))
                Console.WriteLine("Found     " + testdelete + " with depth " + depth);
            else
                Console.WriteLine("Not Found " + testdelete + " with depth " + depth);
            Console.WriteLine("Removing  " + testdelete);
            btree.Delete(testdelete);
            if (btree.Search(testdelete, out depth))
                Console.WriteLine("Found     " + testdelete + " with depth " + depth);
            else
                Console.WriteLine("Not Found " + testdelete + " with depth " + depth);
            btree.Root.WriteDotGraph("test2.gvd");
            Console.WriteLine("Node Count " + btree.NodeCount);

            //Remove root, search before and after
            if (btree.Search(btree.Root.Key, out depth))
                Console.WriteLine("Found     " + btree.Root.Key + " with depth " + depth);
            else
                Console.WriteLine("Not Found " + btree.Root.Key + " with depth " + depth);
            Console.WriteLine("Removing  " + btree.Root.Key);
            Person oldroot = btree.Root.Key;
            btree.Delete(btree.Root.Key);
            if (btree.Search(oldroot, out depth))
                Console.WriteLine("Found     " + oldroot + " with depth " + depth);
            else
                Console.WriteLine("Not Found " + oldroot + " with depth " + depth);
            btree.Root.WriteDotGraph("test1.gvd");
            Console.WriteLine("Node Count " + btree.NodeCount);
            Console.ReadKey();
            

            //Remove testdelete, search before and after
            */
            if (rbTree.Search(testdelete2))
                Console.WriteLine("Found     " + testdelete2 + " Name: " + testdelete2.Name + " Color: " + rbTree.getColor(testdelete2));
            else
                Console.WriteLine("Not Found " + testdelete2);

            
            if (rbTree.Search(testdelete))
                Console.WriteLine("Found     " + testdelete+ " Name: "+ testdelete.Name+ " Color: "+rbTree.getColor(testdelete));
            else
                Console.WriteLine("Not Found " + testdelete);

            Console.WriteLine("Removing  " + testdelete);
            rbTree.Delete(testdelete);
            if (rbTree.Search(testdelete))
                Console.WriteLine("Found     " + testdelete);
            else
                Console.WriteLine("Not Found " + testdelete);
            //rbTree.Root.WriteDotGraph("deleteinner.gvd");
            
            if (rbTree.Search(rbTree.Root.Key))
                Console.WriteLine("Found     " + rbTree.Root.Key + " Color " + rbTree.Root.Color);
            else
                Console.WriteLine("Not Found " + rbTree.Root.Key);
            Console.WriteLine("Removing  " + rbTree.Root.Key + " Color " + rbTree.Root.Color);
            Person oldroot = rbTree.Root.Key;
            rbTree.Delete(rbTree.Root.Key);
            if (rbTree.Search(oldroot))
                Console.WriteLine("Found     " + oldroot);
            else
                Console.WriteLine("Not Found " + oldroot );
           rbTree.Root.WriteDotGraph("deleteroot.gvd");
            
            Console.WriteLine("DONE");
            Console.ReadKey();
            
        }
    }
}
