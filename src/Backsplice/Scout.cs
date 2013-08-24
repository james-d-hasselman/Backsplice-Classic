using System;

namespace Backsplice
{
    public class Scout
    {
        private string m_strName;
        private string m_strTroopString;
        private int m_intTroop;

        public Scout(string _strName, string _strTroop)
        {
            m_strName = _strName;
            m_strTroopString = _strTroop;

            int troop;
            if (int.TryParse(_strTroop, out troop))
            {
                m_intTroop = troop;
            }
        }

        public string GetName()
        {
            return m_strName;
        }

        public int GetTroop()
        {
            return m_intTroop;
        }

        public string GetTroopString()
        {
            return m_strTroopString;
        }

        public override bool Equals(object obj)
        {
            Scout scoutObject = (Scout)obj;

            if (m_intTroop == scoutObject.GetTroop() && m_strName == scoutObject.GetName() && m_strTroopString == scoutObject.GetTroopString())
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int troopHashCode = m_intTroop.GetHashCode();
            int troopStringHashCode = m_strTroopString == null ? 0 : m_strTroopString.GetHashCode();
            int nameHashCode = m_strName == null ? 0 : m_strTroopString.GetHashCode();

            return troopHashCode ^ troopStringHashCode ^ nameHashCode;
        }
    }
}
