using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            get { return (int)Math.Floor(LifeSpan.TotalDays / 365.25); }
        }

        public DateTime Birth
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
    }
}
