using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hades
{
    class Person
    {
        #region Members
        private bool mAlive = false;
        private DateTime mBirth = DateTime.MinValue;
        private DateTime mDeath = DateTime.MaxValue;
        private string mName = "NONE";
        #endregion

        #region Constructors
        public Person(string name, DateTime birth)
        {
            if (birth > DateTime.Now)
                throw new ArgumentException("Birth date can not be in the future");

            mName = name;
            mBirth = birth;
            mAlive = true;
        }

        public Person(string name, string birth)
            : this(name, DateTime.Parse(birth)) { }

        public Person(string name, DateTime birth, DateTime death)
        {
            if (death > DateTime.Now)
                throw new ArgumentException("Death date can not be in the future");
            else if (birth > DateTime.Now)
                throw new ArgumentException("Birth date can not be in the future");
            else if (death <= birth)
                throw new ArgumentException("Death date can not be before birth date");

            mName = name;
            mBirth = birth;
            mDeath = death;
            mAlive = false;
        }

        public Person(string name, string birth, string death)
            : this(name, DateTime.Parse(birth), DateTime.Parse(death)) { }
        #endregion

        #region Properties
        public int Age
        {
            get { return (int)(Math.Floor(LifeSpan.TotalDays / 365.25)) + 1; }
        }

        public DateTime Born
        {
            get { return mBirth; }
        }


        public DateTime Died
        {
            get { return mDeath; }
        }

        public bool IsAlive
        {
            get { return mAlive; }
        }

        public TimeSpan LifeSpan
        {
            get 
            {
                if (mAlive)
                    return DateTime.Now - mBirth;
                else
                    return mDeath - mBirth;
            }
        }

        public string Name
        {
            get { return mName; }
        }
        #endregion

        #region Overridden
        public override bool Equals(object obj)
        {
            Person p = obj as Person;
            if (p == null)
                return false;

            return Died == p.Died &&
                   Born == p.Born &&
                   IsAlive == p.IsAlive &&
                   Name == p.Name;
        }

        public override int GetHashCode()
        {
            int hash = Name.GetHashCode();
            return hash ^ Died.Year ^ Born.Year;
        }

        public override string ToString()
        {
            return ToString("N");
        }

        public string ToString(string format)
        {
            string death = mDeath.ToString("MM/dd/yyyy");
            string birth = mBirth.ToString("MM/dd/yyyy");
            if(mAlive)
                death = "PRESENT   ";
            switch (format)
            {
                case "N":
                    return String.Format("{0,3} : ", Age) + birth + " - " + death;
                case "G":
                    return birth + "\\n" + death;
                default:
                    throw new ArgumentException("Unrecognized format '" + format + "'");
            }

        }
        #endregion

        #region Operators
        static public bool operator ==(Person p1, Person p2) { return p1.Equals(p2); }
        static public bool operator !=(Person p1, Person p2) { return !p1.Equals(p2); }
        //For our purposes in the tree order is determined by the low end of the interval a.k.a
        // the person's birthdate.
        public static bool operator >(Person p1, Person p2) { return (p1.Born > p2.Born); }
        public static bool operator <(Person p1, Person p2) { return (p1.Born < p2.Born); }
        public static bool operator >=(Person p1, Person p2) { return (p1.Born >= p2.Born); }
        public static bool operator <=(Person p1, Person p2) { return (p1.Born <= p2.Born); }
        #endregion
    }

    class PersonReader
    {
        #region Members
        private StreamReader mReader = null;    
        #endregion

        #region Constructors
        public PersonReader(string file)
        {
            mReader = new StreamReader(new FileStream(file, FileMode.Open));
        }
        #endregion

        #region Public Methods
        public void Close()
        {
            mReader.Close();
        }

        public Person NextPerson()
        {
            string data = mReader.ReadLine();
            string[] elements = data.Split(',');
            Person p = null;
            if (elements.Length < 3 || elements[2].Trim() == "")
                p = new Person(elements[0], elements[1]);
            else
                p = new Person(elements[0], elements[1], elements[2]);
            return p;
        }
        #endregion
    }
}
