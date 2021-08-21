using System.Collections.Generic;

namespace Source.Model
{
    public enum Month
    {
        Jan,
        Feb,
        Mar,
        Apr,
        May,
        Jun,
        Jul,
        Aug,
        Sep,
        Oct,
        Nov,
        Dec
    }

    public static class MonthExtensions
    {
        private static readonly Dictionary<Month, string> Names = new Dictionary<Month, string>
        {
            {Month.Jan, Strings.Months_1},
            {Month.Feb, Strings.Months_2},
            {Month.Mar, Strings.Months_3},
            {Month.Apr, Strings.Months_4},
            {Month.May, Strings.Months_5},
            {Month.Jun, Strings.Months_6},
            {Month.Jul, Strings.Months_7},
            {Month.Aug, Strings.Months_8},
            {Month.Sep, Strings.Months_9},
            {Month.Oct, Strings.Months_10},
            {Month.Nov, Strings.Months_11},
            {Month.Dec, Strings.Months_12}
        };

        public static string LocalizedName(this Month month)
        {
            return Names[month];
        }
    }
}