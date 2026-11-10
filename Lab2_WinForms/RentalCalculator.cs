using System;

namespace Lab2_WinForms
{
    public static class RentalCalculator
    {
        public const int NightRate = 500;
        public const int NormalRate = 1000;
        public const int PeakRate = 1500;

        private static readonly int[,] periods =
        {
            { 0, 7, NightRate },
            { 7, 14, NormalRate },
            { 14, 19, PeakRate },
            { 19, 21, NormalRate },
            { 21, 24, NightRate }
        };

        public static int HoursAtRate(int start, int end, int rate)
        {
            int hours = 0;

            for (int i = 0; i < periods.GetLength(0); i++)
            {
                if (periods[i, 2] != rate)
                    continue;

                int from = Math.Max(start, periods[i, 0]);
                int to = Math.Min(end, periods[i, 1]);

                if (to > from)
                    hours += to - from;
            }

            return hours;
        }

        public static int GetTotal(int start, int end)
        {
            return HoursAtRate(start, end, NightRate) * NightRate
                 + HoursAtRate(start, end, NormalRate) * NormalRate
                 + HoursAtRate(start, end, PeakRate) * PeakRate;
        }
    }
}
