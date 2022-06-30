using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backsplice
{
    public class CampProgram
    {
        public CampProgram(string _strName, string _strPeriod, int _intPeriodNumber)
        {
            Name = _strName;
            Period = _strPeriod;
            PeriodNumber = _intPeriodNumber;
        }

        public int PeriodNumber
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Period
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            CampProgram program = (CampProgram)obj;

            if (Name == program.Name && PeriodNumber == program.PeriodNumber && Period == program.Period)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int periodNumberHashCode = PeriodNumber.GetHashCode();
            int periodHashCode = Period == null ? 0 : Period.GetHashCode();
            int nameHashCode = Name == null ? 0 : Name.GetHashCode();

            return periodHashCode ^ periodNumberHashCode ^ nameHashCode;
        }
    }
}
