using System.Collections;
using System.Linq;

namespace Backsplice
{
    class ScoutList : System.Collections.ArrayList
    {
        /// <summary>
        /// Provides a comparer to sort scouts alphabetically by troop
        /// </summary>
        /// <returns>a comparer</returns>
        public static IComparer SortScoutsAlphabeticallyByTroop()
        {
            return (IComparer)new SortScoutsAlphabeticallyByTroopHelper();
        }

        /// <summary>
        /// Provides a comparer to sort scouts alphabetically
        /// </summary>
        /// <returns>a comparer</returns>
        public static IComparer SortScoutsAlphabetically()
        {
            return (IComparer)new SortScoutsAlphabeticallyHelper();
        }

        /// <summary>
        /// Provides a comparer to sort scouts by troop
        /// </summary>
        /// <returns>a comparer</returns>
        public static IComparer SortScoutsByTroop()
        {
            return (IComparer)new SortScoutsByTroopHelper();
        }

        /// <summary>
        /// Prints the name and troop of all scouts in the list (useful for debugging)
        /// </summary>
        public void printList()
        {
            foreach (Scout scout in this)
            {
                System.Diagnostics.Debug.WriteLine(scout.GetName() + " " + scout.GetTroop());
            }
        }

        /// <summary>
        /// Sorts all scouts in the list according to the current sorting preference
        /// </summary>
        public override void Sort()
        {
            if (BackspliceMain.SortAlphabetically && BackspliceMain.SortByTroop)
            {
                Sort(SortScoutsAlphabeticallyByTroop());
            }
            else if (BackspliceMain.SortAlphabetically)
            {
                Sort(SortScoutsAlphabetically());
            }
            else
            {
                Sort(SortScoutsByTroop());
            }
        }

        /// <summary>
        /// Helper class that sorts scouts alphabetically by troop
        /// </summary>
        private class SortScoutsAlphabeticallyByTroopHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                Scout sScout1 = (Scout)a;
                Scout sScout2 = (Scout)b;

                if (sScout1.GetTroop() > sScout2.GetTroop())
                {
                    return 1;
                }
                else if (sScout1.GetTroop() < sScout2.GetTroop())
                {
                    return -1;
                }
                else
                {
                    return string.Compare(sScout1.GetName(), sScout2.GetName());
                }
            }
        }

        /// <summary>
        /// Helper class that sorts scouts alphabetically
        /// </summary>
        private class SortScoutsAlphabeticallyHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                Scout sScout1 = (Scout)a;
                Scout sScout2 = (Scout)b;

                return string.Compare(sScout1.GetName(), sScout2.GetName());
            }
        }

        /// <summary>
        /// Helper class that sorts scouts by troop
        /// </summary>
        private class SortScoutsByTroopHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                Scout sScout1 = (Scout)a;
                Scout sScout2 = (Scout)b;

                if (sScout1.GetTroop() > sScout2.GetTroop())
                {
                    return 1;
                }
                else if (sScout1.GetTroop() == sScout2.GetTroop())
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
