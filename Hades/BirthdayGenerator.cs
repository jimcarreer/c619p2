using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hades
{
    class BirthdayGenerator
    {
        private static int[] DAYS_IN_MONTH = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        private Random mRS = null;
        private int mMnY = 0;
        private int mMxY = 0;

        public BirthdayGenerator(int YearMin, int YearMax, int Seed)
        {
            mRS = new Random(Seed);

            if (YearMax > DateTime.MaxValue.Year)
                throw new ArgumentException("Max year cannot exceed " + DateTime.MaxValue.Year);
            if (YearMin < DateTime.MinValue.Year)
                throw new ArgumentException("Minimum year cannot exceed " + DateTime.MinValue.Year);

            mMnY = YearMin;
            mMxY = YearMax;
            mRS = new Random(Seed);
        }

        public BirthdayGenerator(int YearMin, int YearMax) : this(YearMin, YearMax, 0)
        {
            mRS = new Random();
        }

        public DateTime NextBirthday()
        {
            int year  = mRS.Next(mMnY, mMxY);
            int month = mRS.Next(0, 11);
            int dmax  = DAYS_IN_MONTH[month];
            //Leap year calculation
            month++;
            if (month == 2)
            {
                if (year % 400 == 0)
                    dmax++;
                else if (year % 4 == 0)
                    dmax++;
            }
            int day   = mRS.Next(1, dmax+1);
            return new DateTime(year, month, day);
        }
    }
}
