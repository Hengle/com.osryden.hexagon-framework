using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    /// <summary>
    /// Represents fractional cube coordinates for hexagons.
    /// </summary>
    [Serializable]
    public struct FractionalHexagonCoordinates : IEquatable<FractionalHexagonCoordinates>, IFormattable
    {
        [SerializeField] private float m_Q;
        [SerializeField] private float m_R;

        public FractionalHexagonCoordinates(float q, float r)
        {
            m_Q = q;
            m_R = r;
        }

        /// <summary>
        /// Gets the coordinates of the origin.
        /// </summary>
        public static FractionalHexagonCoordinates Origin { get; } = new FractionalHexagonCoordinates(0, 0);

        /// <summary>
        /// Gets the coordinate at the specified <paramref name="axis"/>.
        /// </summary>
        /// <param name="axis">The axis of the coordinate to get.</param>
        public float this[HexagonCoordinateAxis axis]
        {
            get
            {
                switch (axis)
                {
                    case HexagonCoordinateAxis.Q: return Q;
                    case HexagonCoordinateAxis.R: return R;
                    case HexagonCoordinateAxis.S: return S;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Gets the Q-axis coordinate.
        /// </summary>
        public float Q => m_Q;

        /// <summary>
        /// Gets the R-axis coordinate.
        /// </summary>
        public float R => m_R;

        /// <summary>
        /// Gets the S-axis coordinate.
        /// </summary>
        public float S => -Q + R;

        /// <summary>
        /// Gets the length of the coordinates.
        /// </summary>
        public float Length => (Mathf.Abs(Q) + Mathf.Abs(R) + Mathf.Abs(S)) / 2;

        public static FractionalHexagonCoordinates Min(FractionalHexagonCoordinates a, FractionalHexagonCoordinates b)
        {
            return new FractionalHexagonCoordinates(Mathf.Min(a.Q, b.Q), Mathf.Min(a.R, b.R));
        }

        public static FractionalHexagonCoordinates Max(FractionalHexagonCoordinates a, FractionalHexagonCoordinates b)
        {
            return new FractionalHexagonCoordinates(Mathf.Max(a.Q, b.Q), Mathf.Max(a.R, b.R));
        }

        public static FractionalHexagonCoordinates Reflect(FractionalHexagonCoordinates coordinates, HexagonCoordinateAxis axis)
        {
            switch (axis)
            {
                case HexagonCoordinateAxis.Q: return new FractionalHexagonCoordinates(coordinates.Q, -coordinates.S);
                case HexagonCoordinateAxis.R: return new FractionalHexagonCoordinates(coordinates.S, coordinates.R);
                case HexagonCoordinateAxis.S: return new FractionalHexagonCoordinates(-coordinates.R, -coordinates.Q);
                default:
                    throw new InvalidOperationException();
            }
        }

        public static FractionalHexagonCoordinates Rotate(FractionalHexagonCoordinates coordinates, bool clockwise)
        {
            return clockwise ? new FractionalHexagonCoordinates(coordinates.R, coordinates.S) : new FractionalHexagonCoordinates(-coordinates.S, -coordinates.Q);
        }

        public static FractionalHexagonCoordinates Rotate(FractionalHexagonCoordinates coordinates, bool clockwise, int rotations)
        {
            if (rotations < 1)
                throw new ArgumentOutOfRangeException(nameof(rotations), rotations, "The minimum value is 1.");

            FractionalHexagonCoordinates result = coordinates;

            for (int i = 0; i < rotations; i++)
                result = Rotate(result, clockwise);

            return result;
        }

        public static FractionalHexagonCoordinates Lerp(FractionalHexagonCoordinates a, FractionalHexagonCoordinates b, float t)
        {
            return new FractionalHexagonCoordinates(Mathf.Lerp(a.Q, b.Q, t), Mathf.Lerp(a.R, b.R, t));
        }

        public static float Distance(FractionalHexagonCoordinates a, FractionalHexagonCoordinates b)
        {
            return (a - b).Length;
        }

        public static Vector3 Position(FractionalHexagonCoordinates coordinates, HexagonGeometry geometry)
        {
            float x = coordinates.Q * geometry.HorizontalDistance;
            float y = 0;
            float z = (coordinates.R + coordinates.S) / 2f * geometry.VerticalDistance;
            return Quaternion.AngleAxis(geometry.Angle, Vector3.up) * new Vector3(x, y, z);
        }

        public static IEnumerable<FractionalHexagonCoordinates> Rotations(FractionalHexagonCoordinates origin, bool clockwise, int rotations)
        {
            if (rotations < 1)
                throw new ArgumentOutOfRangeException(nameof(rotations), rotations, "The minimum value is 1.");

            for (int i = 1; i <= rotations; i++)
                yield return Rotate(origin, clockwise);
        }

        public override bool Equals(object other)
        {
            if (other is not FractionalHexagonCoordinates)
                return false;

            return base.Equals((FractionalHexagonCoordinates)other);
        }

        public bool Equals(FractionalHexagonCoordinates other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return Q.GetHashCode() ^ (R.GetHashCode() << 2);
        }

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            formatProvider ??= CultureInfo.InvariantCulture.NumberFormat;
            return string.Format("({0}, {1}, {2})", Q.ToString(format, formatProvider), R.ToString(format, formatProvider), S.ToString(format, formatProvider));
        }

        public static FractionalHexagonCoordinates operator -(FractionalHexagonCoordinates value)
        {
            return new FractionalHexagonCoordinates(-value.Q, -value.R);
        }

        public static FractionalHexagonCoordinates operator +(FractionalHexagonCoordinates left, FractionalHexagonCoordinates right)
        {
            return new FractionalHexagonCoordinates(left.Q + right.Q, left.R + right.R);
        }

        public static FractionalHexagonCoordinates operator -(FractionalHexagonCoordinates left, FractionalHexagonCoordinates right)
        {
            return new FractionalHexagonCoordinates(left.Q - right.Q, left.R - right.R);
        }

        public static FractionalHexagonCoordinates operator *(FractionalHexagonCoordinates left, FractionalHexagonCoordinates right)
        {
            return new FractionalHexagonCoordinates(left.Q * right.Q, left.R * right.R);
        }

        public static FractionalHexagonCoordinates operator *(float left, FractionalHexagonCoordinates right)
        {
            return new FractionalHexagonCoordinates(left * right.Q, left * right.R);
        }

        public static FractionalHexagonCoordinates operator *(FractionalHexagonCoordinates left, float right)
        {
            return new FractionalHexagonCoordinates(left.Q * right, left.R * right);
        }

        public static FractionalHexagonCoordinates operator /(FractionalHexagonCoordinates left, float right)
        {
            return new FractionalHexagonCoordinates(left.Q / right, left.R / right);
        }

        public static bool operator ==(FractionalHexagonCoordinates left, FractionalHexagonCoordinates right)
        {
            return (left.Q == right.Q) && (left.R == right.R);
        }

        public static bool operator !=(FractionalHexagonCoordinates left, FractionalHexagonCoordinates right)
        {
            return !(left == right);
        }

        public static implicit operator FractionalHexagonCoordinates(HexagonCoordinates value)
        {
            return new FractionalHexagonCoordinates(value.Q, value.R);
        }
    }
}
