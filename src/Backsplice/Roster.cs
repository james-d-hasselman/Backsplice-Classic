using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backsplice
{
    public abstract class Roster
    {
        public abstract void Open(String file);
        public abstract void Close();
        public abstract void Save();
        public abstract void AddScout(Scout scout);
        public abstract void RemoveScout(Scout scout);
        public abstract void SetPeriod(String period);
        public abstract void SetProgram(String program);
        public abstract void SetWeek(String week);
        public abstract void Sort(IComparer<Scout> scoutComparer);
        public abstract bool IsCompatible(String file);
    }
}
