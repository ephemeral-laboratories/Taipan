using System;
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
        public static string LocalizedName(this Month month)
        {
            return month switch
            {
                Month.Jan => Strings.Months_1,
                Month.Feb => Strings.Months_2,
                Month.Mar => Strings.Months_3,
                Month.Apr => Strings.Months_4,
                Month.May => Strings.Months_5,
                Month.Jun => Strings.Months_6,
                Month.Jul => Strings.Months_7,
                Month.Aug => Strings.Months_8,
                Month.Sep => Strings.Months_9,
                Month.Oct => Strings.Months_10,
                Month.Nov => Strings.Months_11,
                Month.Dec => Strings.Months_12,
                _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
            };
        }
    }
}