using System;

namespace starikcetin.Eflatun.UnityCommon.Utils.Calculation
{
    /// <summary>
    /// A class that includes standard mathematical functions for types that doesn't included in Unity's Mathf class; such as double and long.
    /// Also includes some other useful mathematical functions.
    /// </summary>
    public static class MathUtils
    {
        public const int LayerMaskAll = ~0;
        public const int LayerMaskNone = 0;

        public static double ClampDouble(this double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        /// <summary>
        /// Truncates the decimal places of <paramref name="value"/> to fit <paramref name="decimalCount"/>.
        /// </summary>
        /// <example> 1.14d.LimitDecimals(1) --> 1.1d </example>
        public static double LimitDecimals(this double value, int decimalCount)
        {
            var rate = Math.Pow(10, decimalCount);
            return Math.Truncate(value * rate) / rate;

            // This below uses the rounding option, which is not precise:
            //double rounded = Math.Round(value, decimalCount, MidpointRounding.AwayFromZero);
            //return rounded;
        }

        /// <summary>
        /// Truncates the decimal places of <paramref name="value"/> to fit <paramref name="decimalCount"/>.
        /// </summary>
        /// <example> 1.14f.LimitDecimals(1) --> 1.1f </example>
        public static float LimitDecimals(this float value, int decimalCount)
        {
            var rate = Math.Pow(10, decimalCount);
            return (float) (Math.Truncate(value * rate) / rate);

            // This below uses the rounding option, which is not precise:
            //float rounded = (float)Math.Round(value, decimalCount, MidpointRounding.AwayFromZero);
            //return rounded;
        }

        /// <summary>
        /// Square root.
        /// </summary>
        public static float Sqrt(this float number)
        {
            return (float) Math.Sqrt(number);
        }

        /// <summary>
        /// Square root.
        /// </summary>
        public static float Sqrt(this int number)
        {
            return (float) Math.Sqrt(number);
        }

        /// <summary>
        /// Returns the <paramref name="exp"/>'th power of <paramref name="baseN"/>.
        /// </summary>
        /// <param name="baseN">Base.</param>
        /// <param name="exp">Exponent (Power).</param>
        /// <remarks>Float raised to float = float</remarks>
        public static float Pow(this float baseN, float exp)
        {
            return (float) Math.Pow(baseN, exp);
        }

        /// <summary>
        /// Returns the <paramref name="exp"/>'th power of <paramref name="baseN"/>.
        /// </summary>
        /// <param name="baseN">Base.</param>
        /// <param name="exp">Exponent (Power).</param>
        /// <remarks>Integer raised to float = float</remarks>
        public static float Pow(this int baseN, float exp)
        {
            return (float) Math.Pow(baseN, exp);
        }

        /// <summary>
        /// Returns the <paramref name="exp"/>'th power of <paramref name="baseN"/>.
        /// </summary>
        /// <param name="baseN">Base.</param>
        /// <param name="exp">Exponent (Power).</param>
        /// <remarks>Integer raised to integer = integer</remarks>
        public static int Pow(this int baseN, int exp)
        {
            return (int) Math.Pow(baseN, exp);
        }

        /// <summary>
        /// Normalizes the angle in degrees to 0-360 range.
        /// </summary>
        public static float NormalizeAngle(this float angle)
        {
            angle = (angle + 360) % 360;

            if (angle < 0)
            {
                angle += 360;
            }

            return angle;
        }

        /// <summary>
        /// <para>Calculates the shortest rotation direction from this angle to the one given in paranthesis.</para>
        /// <para>If return value is positive, shortest angle is positive way (counter-clockwise); if return value is negative shortest angle is negative way (clockwise).</para>
        /// <para>If return value is 0, this means angles are equal.</para>
        /// </summary>
        /// <param name="from">The angle to start from.</param>
        /// <param name="to"> The destination angle of rotation.</param>
        public static float ShortestRotationTo(this float from, float to)
        {
            //
            // Orginal Source: http://answers.unity.com/answers/556633/view.html
            // Modified and reformatted.
            //

            // If from or to is a negative, we have to recalculate them.
            // For an example, if from = -45 then from(-45) + 360 = 315.
            if (from < 0)
            {
                from += 360;
            }

            if (to < 0)
            {
                to += 360;
            }

            // Do not rotate if from == to.
            if (from == to ||
                from == 0 && to == 360 ||
                from == 360 && to == 0)
            {
                return 0;
            }

            // Pre-calculate left and right.
            float left = (360 - from) + to;
            float right = from - to;

            // If from < to, re-calculate left and right.
            if (from < to)
            {
                if (to > 0)
                {
                    left = to - from;
                    right = (360 - to) + from;
                }
                else
                {
                    left = (360 - to) + from;
                    right = to - from;
                }
            }

            // Determine the shortest direction.
            return (left <= right) ? left : (right * -1);
        }

        /// <summary>
        /// Determines if given mask includes given layer. Layer parameter must NOT be bit-shifted, bit-shifting is being done inside this method.
        /// </summary>
        public static bool MaskIncludes(this int mask, int layer)
        {
            int shifted = 1 << layer;
            return (mask & shifted) == shifted;
        }

        /// <summary>
        /// Mirrors the <paramref name="value"/> by <paramref name="origin"/>.
        /// </summary>
        public static int MirrorBy(this int value, int origin)
        {
            return origin + (origin - value);
        }

        /// <summary>
        /// Mirrors the <paramref name="value"/> by <paramref name="origin"/>.
        /// </summary>
        public static double MirrorBy(this double value, double origin)
        {
            return origin + (origin - value);
        }

        /// <summary>
        /// Mirrors the <paramref name="value"/> by <paramref name="origin"/>.
        /// </summary>
        public static float MirrorBy(this float value, float origin)
        {
            return origin + (origin - value);
        }
    }
}
