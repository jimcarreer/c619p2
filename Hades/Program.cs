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
                Console.WriteLine("Inserting value: " + p);
                if (i == 5)
                    testdelete = p;
                btree.Insert(p);
            }
            btree.Root.WriteDotGraph("test1.gvd");
            //Console.WriteLine("Removing " + testdelete);
            //btree.Delete(testdelete);
            //btree.Root.WriteDotGraph("test2.gvd");
            //Console.WriteLine("Removing " + btree.Root.Key);
            //btree.Delete(btree.Root.Key);
            //btree.Root.WriteDotGraph("test3.gvd");
            Console.ReadKey();
        }
    }
}
