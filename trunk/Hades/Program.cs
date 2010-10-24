using System;
using System.Collections.Generic;
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
            BinaryTree btree = new BinaryTree();
            Person p = null;
            for (int i = 0; i <= 8; i++)
            {
                //int lifespan = rand.Next(25, 80);
                //DateTime birthdate = bg.NextBirthday();
                //DateTime deathdate = birthdate.AddYears(lifespan);
                //p = new Person("Test" + i.ToString(), birthdate, deathdate);
                p = pr.NextPerson();
                Console.WriteLine("Inserting " + p);
                if (i == 5)
                    testdelete = p;
                btree.Insert(p);
            }
          
            //Search depth
            int depth = 0;
            //Initial tree
            btree.Root.WriteDotGraph("initial.gvd");
            
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
            btree.Root.WriteDotGraph("deleteinner.gvd");

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
            btree.Root.WriteDotGraph("deleteroot.gvd");
            Console.ReadKey();
        }
    }
}
