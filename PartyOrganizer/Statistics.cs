using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PartyOrganizer
{
    public static class Statistics
    {
        public static Stats RefreshStats(ObservableCollection<Guest> guestsList)
        {
            Stats tmp = new Stats()
            {
                guestCount = CalculateGuestsCount(guestsList),
                maleCount = CalculateMaleCount(guestsList),
                femaleCount = CalculateFemaleCount(guestsList),
                guestConfirmed = CalculateGuestsConfirmed(guestsList),
                maleConfirmed = CalculateMaleConfirmed(guestsList),
                femaleConfirmed = CalculateFemaleConfirmed(guestsList)
            };

            return tmp;
        }

        private static int CalculateGuestsCount(ObservableCollection<Guest> guestsList)
        {
            int guestsCount = guestsList.Count;
            return guestsCount;
        }

        private static int CalculateMaleCount(ObservableCollection<Guest> guestsList)
        {
            int maleCount = guestsList.Where(m => m.Sex == "Male").Count();
            return maleCount;
        }

        private static int CalculateFemaleCount(ObservableCollection<Guest> guestsList)
        {
            int femaleCount = guestsList.Where(m => m.Sex == "Female").Count();
            return femaleCount;
        }

        private static int CalculateGuestsConfirmed(ObservableCollection<Guest> guestsList)
        {
            int guestsConfirmed = guestsList.Where(m => m.Confirmed).Count();
            return guestsConfirmed;
        }

        private static int CalculateMaleConfirmed(ObservableCollection<Guest> guestsList)
        {
            int maleConfirmed = guestsList.Where(m => m.Sex == "Male").Where(m => m.Confirmed).Count();
            return maleConfirmed;
        }

        private static int CalculateFemaleConfirmed(ObservableCollection<Guest> guestsList)
        {
            int femaleConfirmed = guestsList.Where(m => m.Sex == "Female").Where(m => m.Confirmed).Count();
            return femaleConfirmed;
        }
    }
}
