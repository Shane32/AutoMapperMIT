#if NETSTANDARD2_0
using System.Runtime.CompilerServices;

namespace System
{
    internal readonly struct Index : IEquatable<Index>
    {
        private readonly int _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Index(int value, bool fromEnd = false)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Non-negative number required.");
            _value = fromEnd ? ~value : value;
        }

        private Index(int value) { _value = value; }

        public static Index Start => new Index(0);
        public static Index End => new Index(~0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Index(int value) => new Index(value);

        public bool IsFromEnd => _value < 0;
        public int Value => _value < 0 ? ~_value : _value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetOffset(int length)
        {
            int offset = _value;
            if (IsFromEnd)
                offset += length + 1;
            return offset;
        }

        public bool Equals(Index other) => _value == other._value;
        public override bool Equals(object value) => value is Index index && _value == index._value;
        public override int GetHashCode() => _value;
        public override string ToString() => IsFromEnd ? "^" + Value.ToString() : ((uint)Value).ToString();
    }

    internal readonly struct Range : IEquatable<Range>
    {
        public Index Start { get; }
        public Index End { get; }

        public Range(Index start, Index end) { Start = start; End = end; }

        public static Range StartAt(Index start) => new Range(start, Index.End);
        public static Range EndAt(Index end) => new Range(Index.Start, end);
        public static Range All => new Range(Index.Start, Index.End);

        public bool Equals(Range other) => Start.Equals(other.Start) && End.Equals(other.End);
        public override bool Equals(object value) => value is Range r && r.Equals(this);
        public override int GetHashCode() => HashCode.Combine(Start.GetHashCode(), End.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (int Offset, int Length) GetOffsetAndLength(int length)
        {
            int start = Start.GetOffset(length);
            int end = End.GetOffset(length);
            if ((uint)end > (uint)length || (uint)start > (uint)end)
                throw new ArgumentOutOfRangeException(nameof(length));
            return (start, end - start);
        }

        public override string ToString() => Start.ToString() + ".." + End.ToString();
    }
}
#endif
