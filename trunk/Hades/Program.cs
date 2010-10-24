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
            BirthdayGenerator bg = new BirthdayGenerator(1832, 1923);
            Random rand = new Random();

            BinaryTree btree = new BinaryTree();
            for (int i = 0; i <= 100; i++)
            {
                int lifespan = rand.Next(25, 80);
                DateTime birthdate = bg.NextBirthday();
                DateTime deathdate = birthdate.AddYears(lifespan);
                Person p = new Person("Test" + i.ToString(), birthdate, deathdate);
                Console.WriteLine("Inserting value: " + p);
                btree.Insert(p);
            }
            btree.Root.WriteDotGraph("test.gvd");
            Console.ReadKey();
        }
    }
}
