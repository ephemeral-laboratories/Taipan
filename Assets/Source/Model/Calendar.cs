namespace Source.Model
{
    public class Calendar
    {
        private const int EpochYear = 1860;

        private Calendar(int year, Month month)
        {
            Year = year;
            Month = month;
        }

        public static Calendar AtStart()
        {
            return new Calendar(EpochYear, Month.Jan);
        }
        
        public int Year { get; private set; }

        public Month Month { get; private set; }

        public int YearsSinceStart => Year - EpochYear;
        public int MonthsSinceYearStart => (int)Month;
        public int MonthsSinceStart => YearsSinceStart * 12 + MonthsSinceYearStart;

        public void Advance()
        {
            Month++;

            if (Month <= Month.Dec) return;

            Month = Month.Jan;
            Year++;
        }
    }
}