using Cedeira.Essentials.NET.Diagnostics.Invariants;


namespace Cedeira.Essentials.NET.System.Range
{
    /// <summary>
    /// this class represents a date range with a start and end date
    /// </summary>
    public readonly struct DateRange
    {
        /// <summary>
        /// start date of the range
        /// </summary>
        public DateTime Start { get; }

        /// <summary>
        /// end date of the range
        /// </summary>
        public DateTime End { get; }
        /// <summary>
        /// duration of the range (end - start)
        /// </summary>
        public TimeSpan Duration => End - Start;

        /// <summary>
        /// creates a new instance of the <see cref="DateRange"/> class
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <exception cref="ArgumentException"></exception>
        public DateRange(DateTime start, DateTime end)
        {
            Invariants.For(start).IsNotNull();
            Invariants.For(end).IsNotNull();

            if (start > end)
            {
                throw new ArgumentException("The start date must be earlier than or equal to the end date.");
            }

            Start = start;
            End = end;
        }

        /// <summary>
        /// checks if the date is within the range
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool Contains(DateTime date)
        {
            return (date >= Start && date <= End);
        }

        /// <summary>
        /// checks if the range overlaps with another range
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool OverlapsWith(DateRange other)
        {
            return (Start < other.End && End > other.Start);
        }

        /// <summary>
        /// return the intersection of the range with another range
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public DateRange? Intersect(DateRange other)
        {
            if (!OverlapsWith(other))
            {
                return null;
            }

            var intersectionStart = (Start > other.Start ? Start : other.Start);
            var intersectionEnd = (End < other.End ? End : other.End);

            return new DateRange(intersectionStart, intersectionEnd);
        }

        /// <summary>
        /// returns the hash code of the range.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => HashCode.Combine(Start, End);

        /// <summary>
        /// returns true if the range is equal to another range
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DateRange left, DateRange right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// returns true if the range is not equal to another range
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DateRange left, DateRange right)
        {
            return !(left == right);
        }

        /// <summary>
        /// returns a string representation of the range.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Start:yyyy-MM-dd} to {End:yyyy-MM-dd}";
        }

        /// <summary>
        /// used to compare the range with another object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return (obj is DateRange other && Start == other.Start && End == other.End);
        }
    }
}
