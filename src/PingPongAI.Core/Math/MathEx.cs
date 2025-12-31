using System;

namespace PingPongAI.Core.Math
{
    public sealed class MathEx
    {
        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <typeparam name="T">A generic type derived from the 'System.IComparable' interface.</typeparam>
        /// <param name="value">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        private static T SetClamp<T>(T value, T first, T last)
            where T : IComparable
        {
            T min = (((IComparable)first).CompareTo(last) < 0) ? first : last;
            T max = (((IComparable)first).CompareTo(last) > 0) ? first : last;
            return (((IComparable)value).CompareTo(min) < 0)
                ? min
                : (((IComparable)value).CompareTo(max) > 0)
                ? max
                : value;
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static byte Clamp(byte n, byte first, byte last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static decimal Clamp(decimal n, decimal first, decimal last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static double Clamp(double n, double first, double last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static float Clamp(float n, float first, float last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static int Clamp(int n, int first, int last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static long Clamp(long n, long first, long last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static sbyte Clamp(sbyte n, sbyte first, sbyte last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static short Clamp(short n, short first, short last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static uint Clamp(uint n, uint first, uint last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static ulong Clamp(ulong n, ulong first, ulong last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static ushort Clamp(ushort n, ushort first, ushort last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static TimeSpan Clamp(TimeSpan n, TimeSpan first, TimeSpan last)
        {
            return SetClamp(n, first, last);
        }

        /// <summary>
        /// Forces the specified value between two limits.
        /// </summary>
        /// <param name="n">Base value.</param>
        /// <param name="first">The first value to compare.</param>
        /// <param name="last">The last value to compare.</param>
        /// <returns>Returns a value between 'first' and 'last'.</returns>
        public static DateTime Clamp(DateTime n, DateTime first, DateTime last)
        {
            return SetClamp(n, first, last);
        }
    }
}
