using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackSplice
{
    class ScoutSorter
    {
        private System.Collections.ArrayList m_lstScouts;

        public ScoutSorter()
        {
            m_lstScouts = new System.Collections.ArrayList();
        }

        public ScoutSorter(Scout[] _objScouts)
        {
            m_lstScouts = new System.Collections.ArrayList();
            for (int i = 0; i < _objScouts.Length; i++)
            {
                this.insertScout(_objScouts[i].GetName(), _objScouts[i].GetTroopString());
            }
        }

        public void insertScout(string _strName, string _strTroop)
        {
            int _intTroop;

            try
            {
                _intTroop = int.Parse(_strTroop);
            }
            catch (FormatException)
            {
                _intTroop = int.MaxValue;
            }

            if (m_lstScouts.Count == 0)
            {
                Scout objAScout = new Scout(_strName, _strTroop);
                m_lstScouts.Add(objAScout);
            }
            else
            {
                // See if there is a scout with the same troop number as the new scout
                int intIndex = -1;
                foreach (Scout scout in m_lstScouts)
                {
                    if (scout.GetTroop() == _intTroop)
                    {
                        intIndex = m_lstScouts.IndexOf(scout);
                    }
                }

                // There is already a scout in the list with the same troop number as the new scout
                if (intIndex != -1)
                {
                    // Look for a place to put the new scout
                    int i = intIndex;
                    bool blnInserted = false;
                    while (i < m_lstScouts.Count)
                    {
                        Scout scout = (Scout)m_lstScouts[i];

                        if (scout.GetTroop() == _intTroop && string.Compare(scout.GetName(), _strName) >= 0 && !blnInserted)
                        {
                            Scout newScout = new Scout(_strName, _strTroop);
                            m_lstScouts.Insert(i, newScout);
                            blnInserted = true;
                        }
                        else if (scout.GetTroop() != _intTroop && !blnInserted)
                        {
                            Scout newScout = new Scout(_strName, _strTroop);
                            m_lstScouts.Insert(i, newScout);
                            blnInserted = true;
                        }
                        /*else if (i == m_lstScouts.Count - 1)
                        {
                            Scout newScout = new Scout(_strName, _intTroop);
                            m_lstScouts.Add(newScout);
                            blnInserted = true;
                        }*/

                        i++;
                    }

                    if (!blnInserted)
                    {
                        Scout theNewScout = new Scout(_strName, _strTroop);
                        m_lstScouts.Add(theNewScout);
                    }
                }
                else
                {
                    bool blnInserted = false;
                    int i = 0;
                    while (i < m_lstScouts.Count && !blnInserted)
                    {
                        Scout scout = (Scout)m_lstScouts[i];

                        if (_intTroop < scout.GetTroop())
                        {
                            Scout newScout = new Scout(_strName, _strTroop);
                            m_lstScouts.Insert(m_lstScouts.IndexOf(scout), newScout);
                            blnInserted = true;
                        }

                        i++;
                    }

                    if (!blnInserted)
                    {
                        Scout newScout = new Scout(_strName, _strTroop);
                        m_lstScouts.Add(newScout);
                    }
                }
            }
        }

        public void printList()
        {
            foreach (Scout scout in m_lstScouts)
            {
                System.Diagnostics.Debug.WriteLine(scout.GetName() + " " + scout.GetTroop());
            }
        }

        public Scout[] ToArray()
        {
            Scout[] scouts = new Scout[m_lstScouts.Count];

            for (int i = 0; i < m_lstScouts.Count; i++)
            {
                scouts[i] = (Scout)m_lstScouts[i];
            }

            return scouts;
        }
    }
}
