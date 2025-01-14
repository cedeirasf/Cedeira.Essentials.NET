using Cedeira.Essentials.NET.Diagnostics.Invariants;


namespace Cedeira.Essentials.NET.System.Range
{
    public readonly struct DateRange
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public TimeSpan Duration => End - Start;

        public DateRange(DateTime start, DateTime end)
        {
            Invariants.For(start).IsNotNull();
            Invariants.For(end).IsNotNull();

            //todo:Incluir custom Invariant
            if (start > end)
            {
                throw new ArgumentException("The start date must be earlier than or equal to the end date.");
            }

            Start = start;
            End = end;
        }

        //verificar si una fecha específica está dentro del rango
        public bool Contains(DateTime date)
        {
            return (date >= Start && date <= End);
        }

        //validar solapamientos.
        public bool OverlapsWith(DateRange other)
        {
            return (Start < other.End && End > other.Start);
        }

        // Devuelve el rango comun entre dos rangos  
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

        public override int GetHashCode() => HashCode.Combine(Start, End);

        public static bool operator ==(DateRange left, DateRange right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(DateRange left, DateRange right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{Start:yyyy-MM-dd} to {End:yyyy-MM-dd}";
        }

        public override bool Equals(object? obj)
        {
            return (obj is DateRange other && Start == other.Start && End == other.End);
        }
    }
}
