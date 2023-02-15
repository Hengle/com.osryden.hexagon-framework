using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    /// <summary>
    /// Represents cube coordinates for hexagons.
    /// </summary>
    [Serializable]
    public struct HexagonCoordinates : IEquatable<HexagonCoordinates>, IFormattable
    {
        [SerializeField] private int m_Q;
        [SerializeField] private int m_R;

        /// <summary>
        /// Creates a new <see cref="HexagonCoordinates"/> with the specified <paramref name="q"/> and <paramref name="r"/> coordinates.
        /// </summary>
        /// <param name="q">The value of the <see cref="Q"/> coordinate.</param>
        /// <param name="r">The value of the <see cref="R"/> coordinate.</param>
        public HexagonCoordinates(int q, int r)
        {
            m_Q = q;
            m_R = r;
        }

        /// <summary>
        /// Gets the coordinates of the origin.
        /// </summary>
        public static HexagonCoordinates Origin { get; } = new HexagonCoordinates(0, 0);

        #region Adjacents

        /// <summary>
        /// Gets the coordinates to the north direction adjacent of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopNorthAdjacent { get; } = new HexagonCoordinates(0, 1);

        /// <summary>
        /// Gets the coordinates to the northeast direction adjacent of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopNortheastAdjacent { get; } = new HexagonCoordinates(1, 1);

        /// <summary>
        /// Gets the coordinates to the southeast direction adjacent of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopSoutheastAdjacent { get; } = new HexagonCoordinates(1, 0);

        /// <summary>
        /// Gets the coordinates to the south direction adjacent of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopSouthAdjacent { get; } = new HexagonCoordinates(0, -1);

        /// <summary>
        /// Gets the coordinates to the southwest direction adjacent of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopSouthwestAdjacent { get; } = new HexagonCoordinates(-1, -1);

        /// <summary>
        /// Gets the coordinates to the northwest direction adjacent of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopNorthwestAdjacent { get; } = new HexagonCoordinates(-1, 0);

        /// <summary>
        /// Gets the coordinates to the northeast direction adjacent of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopNortheastAdjacent { get; } = new HexagonCoordinates(1, 1);

        /// <summary>
        /// Gets the coordinates to the east direction adjacent of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopEastAdjacent { get; } = new HexagonCoordinates(1, 0);

        /// <summary>
        /// Gets the coordinates to the southeast direction adjacent of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopSoutheastAdjacent { get; } = new HexagonCoordinates(0, -1);

        /// <summary>
        /// Gets the coordinates to the southwest direction adjacent of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopSouthwestAdjacent { get; } = new HexagonCoordinates(-1, -1);

        /// <summary>
        /// Gets the coordinates to the west direction adjacent of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopWestAdjacent { get; } = new HexagonCoordinates(-1, 0);

        /// <summary>
        /// Gets the coordinates to the northwest direction adjacent of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopNorthwestAdjacent { get; } = new HexagonCoordinates(0, 1);

        #endregion
        #region Diagonals

        /// <summary>
        /// Gets the coordinates to the northeast direction diagonal of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopNortheastDiagonal { get; } = new HexagonCoordinates(1, 2);

        /// <summary>
        /// Gets the coordinates to the east direction diagonal of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopEastDiagonal { get; } = new HexagonCoordinates(2, 1);

        /// <summary>
        /// Gets the coordinates to the southeast direction diagonal of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopSoutheastDiagonal { get; } = new HexagonCoordinates(1, -1);

        /// <summary>
        /// Gets the coordinates to the southwest direction diagonal of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopSouthwestDiagonal { get; } = new HexagonCoordinates(-1, -2);

        /// <summary>
        /// Gets the coordinates to the west direction diagonal of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopWestDiagonal { get; } = new HexagonCoordinates(-2, -1);

        /// <summary>
        /// Gets the coordinates to the northwest direction diagonal of the flat-top orientation.
        /// </summary>
        public static HexagonCoordinates FlatTopNorthwestDiagonal { get; } = new HexagonCoordinates(-1, 1);

        /// <summary>
        /// Gets the coordinates to the north direction diagonal of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopNorthDiagonal { get; } = new HexagonCoordinates(1, 2);

        /// <summary>
        /// Gets the coordinates to the northeast direction diagonal of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopNortheastDiagonal { get; } = new HexagonCoordinates(2, 1);

        /// <summary>
        /// Gets the coordinates to the southeast direction diagonal of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopSoutheastDiagonal { get; } = new HexagonCoordinates(1, -1);

        /// <summary>
        /// Gets the coordinates to the south direction diagonal of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopSouthDiagonal { get; } = new HexagonCoordinates(-1, -2);

        /// <summary>
        /// Gets the coordinates to the southwest direction diagonal of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopSouthwestDiagonal { get; } = new HexagonCoordinates(-2, -1);

        /// <summary>
        /// Gets the coordinates to the northwest direction diagonal of the pointy-top orientation.
        /// </summary>
        public static HexagonCoordinates PointyTopNorthwestDiagonal { get; } = new HexagonCoordinates(-1, 1);

        #endregion

        /// <summary>
        /// Gets the coordinate at the specified <paramref name="axis"/>.
        /// </summary>
        /// <param name="axis">The axis of the coordinate to get.</param>
        public int this[HexagonCoordinateAxis axis]
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
        public int Q => m_Q;

        /// <summary>
        /// Gets the R-axis coordinate.
        /// </summary>
        public int R => m_R;

        /// <summary>
        /// Gets the S-axis coordinate.
        /// </summary>
        public int S => -Q + R;

        /// <summary>
        /// Gets the length of the coordinates.
        /// </summary>
        public int Length => (Mathf.Abs(Q) + Mathf.Abs(R) + Mathf.Abs(S)) / 2;

        public static HexagonCoordinates Adjacent(HexagonDirection direction)
        {
            return Adjacent((FlatTopHexagonDirection)direction);
        }

        public static HexagonCoordinates Adjacent(FlatTopHexagonDirection direction)
        {
            switch (direction)
            {
                case FlatTopHexagonDirection.North: return FlatTopNorthAdjacent;
                case FlatTopHexagonDirection.Northeast: return FlatTopNortheastAdjacent;
                case FlatTopHexagonDirection.Southeast: return FlatTopSoutheastAdjacent;
                case FlatTopHexagonDirection.South: return FlatTopSouthAdjacent;
                case FlatTopHexagonDirection.Southwest: return FlatTopSouthwestAdjacent;
                case FlatTopHexagonDirection.Northwest: return FlatTopNorthwestAdjacent;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static HexagonCoordinates Adjacent(PointyTopHexagonDirection direction)
        {
            switch (direction)
            {
                case PointyTopHexagonDirection.Northeast: return PointyTopNortheastAdjacent;
                case PointyTopHexagonDirection.East: return PointyTopEastAdjacent;
                case PointyTopHexagonDirection.Southeast: return PointyTopSoutheastAdjacent;
                case PointyTopHexagonDirection.Southwest: return PointyTopSouthwestAdjacent;
                case PointyTopHexagonDirection.West: return PointyTopWestAdjacent;
                case PointyTopHexagonDirection.Northwest: return PointyTopNorthwestAdjacent;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static HexagonCoordinates Diagonal(HexagonDiagonal diagonal)
        {
            return Diagonal((FlatTopHexagonDiagonal)diagonal);
        }

        public static HexagonCoordinates Diagonal(FlatTopHexagonDiagonal diagonal)
        {
            switch (diagonal)
            {
                case FlatTopHexagonDiagonal.Northeast: return FlatTopNortheastDiagonal;
                case FlatTopHexagonDiagonal.East: return FlatTopEastDiagonal;
                case FlatTopHexagonDiagonal.Southeast: return FlatTopSoutheastDiagonal;
                case FlatTopHexagonDiagonal.Southwest: return FlatTopSouthwestDiagonal;
                case FlatTopHexagonDiagonal.West: return FlatTopWestDiagonal;
                case FlatTopHexagonDiagonal.Northwest: return FlatTopNorthwestDiagonal;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static HexagonCoordinates Diagonal(PointyTopHexagonDiagonal diagonal)
        {
            switch (diagonal)
            {
                case PointyTopHexagonDiagonal.North: return PointyTopNorthDiagonal;
                case PointyTopHexagonDiagonal.Northeast: return PointyTopNortheastDiagonal;
                case PointyTopHexagonDiagonal.Southeast: return PointyTopSoutheastDiagonal;
                case PointyTopHexagonDiagonal.South: return PointyTopSouthDiagonal;
                case PointyTopHexagonDiagonal.Southwest: return PointyTopSouthwestDiagonal;
                case PointyTopHexagonDiagonal.Northwest: return PointyTopNorthwestDiagonal;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static HexagonCoordinates Min(HexagonCoordinates a, HexagonCoordinates b)
        {
            return new HexagonCoordinates(Mathf.Min(a.Q, b.Q), Mathf.Min(a.R, b.R));
        }

        public static HexagonCoordinates Max(HexagonCoordinates a, HexagonCoordinates b)
        {
            return new HexagonCoordinates(Mathf.Max(a.Q, b.Q), Mathf.Max(a.R, b.R));
        }

        public static HexagonCoordinates Reflect(HexagonCoordinates coordinates, HexagonCoordinateAxis axis)
        {
            switch (axis)
            {
                case HexagonCoordinateAxis.Q: return new HexagonCoordinates(coordinates.Q, -coordinates.S);
                case HexagonCoordinateAxis.R: return new HexagonCoordinates(coordinates.S, coordinates.R);
                case HexagonCoordinateAxis.S: return new HexagonCoordinates(-coordinates.R, -coordinates.Q);
                default:
                    throw new InvalidOperationException();
            }
        }

        public static HexagonCoordinates Rotate(HexagonCoordinates coordinates, bool clockwise)
        {
            return clockwise ? new HexagonCoordinates(coordinates.R, coordinates.S) : new HexagonCoordinates(-coordinates.S, -coordinates.Q);
        }

        public static HexagonCoordinates Rotate(HexagonCoordinates coordinates, bool clockwise, int rotations)
        {
            if (rotations < 1)
                throw new ArgumentOutOfRangeException(nameof(rotations), rotations, "The minimum value is 1.");

            HexagonCoordinates result = coordinates;

            for (int i = 0; i < rotations; i++)
                result = Rotate(result, clockwise);

            return result;
        }

        public static HexagonCoordinates Round(FractionalHexagonCoordinates coordinates)
        {
            int q = Mathf.RoundToInt(coordinates.Q);
            int r = Mathf.RoundToInt(coordinates.R);
            int s = Mathf.RoundToInt(coordinates.S);

            float qDifference = Mathf.Abs(q - coordinates.Q);
            float rDifference = Mathf.Abs(r - coordinates.R);
            float sDifference = Mathf.Abs(s - coordinates.S);

            if ((qDifference > rDifference) && (qDifference > sDifference))
            {
                q = r - s;
            }
            else if (rDifference > sDifference)
            {
                r = q + s;
            }

            return new HexagonCoordinates(q, r);
        }

        public static HexagonCoordinates Lerp(HexagonCoordinates a, HexagonCoordinates b, float t)
        {
            return Lerp((FractionalHexagonCoordinates)a, (FractionalHexagonCoordinates)b, t);
        }

        public static HexagonCoordinates Lerp(FractionalHexagonCoordinates a, FractionalHexagonCoordinates b, float t)
        {
            return Round(FractionalHexagonCoordinates.Lerp(a, b, t));
        }

        public static int Distance(HexagonCoordinates a, HexagonCoordinates b)
        {
            return (a - b).Length;
        }

        public static Vector3 Position(HexagonCoordinates coordinates, HexagonGeometry geometry)
        {
            float x = coordinates.Q * geometry.HorizontalDistance;
            float y = 0;
            float z = (coordinates.R + coordinates.S) / 2f * geometry.VerticalDistance;
            return HexagonGeometry.RotationAxis(geometry.Orientation, Vector3.up) * new Vector3(x, y, z);
        }

        public static IEnumerable<HexagonCoordinates> Adjacents()
        {
            foreach (HexagonDirection direction in (HexagonDirection[])Enum.GetValues(typeof(HexagonDirection)))
                yield return Adjacent(direction);
        }

        public static IEnumerable<HexagonCoordinates> Adjacents(HexagonCoordinates coordinates)
        {
            foreach (HexagonCoordinates adjacent in Adjacents())
                yield return coordinates + adjacent;
        }

        public static IEnumerable<HexagonCoordinates> Diagonals()
        {
            foreach (HexagonDiagonal diagonal in (HexagonDiagonal[])Enum.GetValues(typeof(HexagonDiagonal)))
                yield return Diagonal(diagonal);
        }

        public static IEnumerable<HexagonCoordinates> Diagonals(HexagonCoordinates coordinates)
        {
            foreach (HexagonCoordinates diagonal in Diagonals())
                yield return coordinates + diagonal;
        }

        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates a, HexagonCoordinates b)
        {
            int distance = Distance(a, b);
            float step = 1 / (float)Mathf.Max(distance, 1);
            var aNudge = new FractionalHexagonCoordinates(a.Q + 1e-6f, a.R + 1e-6f);
            var bNudge = new FractionalHexagonCoordinates(b.Q + 1e-6f, b.R + 1e-6f);

            for (int i = 0; i <= distance; i++)
                yield return Lerp(aNudge, bNudge, i * step);
        }

        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates origin, int length, HexagonCoordinates direction)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), length, "The minimum value is 0.");

            yield return origin;

            if (length > 0)
            {
                HexagonCoordinates coordinates = origin;

                for (int i = 0; i < length; i++)
                    yield return coordinates += direction;
            }
        }

        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates origin, int length, HexagonDirection direction)
        {
            return Line(origin, length, Adjacent(direction));
        }

        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates origin, int length, FlatTopHexagonDirection direction)
        {
            return Line(origin, length, Adjacent(direction));
        }

        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates origin, int length, PointyTopHexagonDirection direction)
        {
            return Line(origin, length, Adjacent(direction));
        }

        public static IEnumerable<HexagonCoordinates> Range(HexagonCoordinates center, int range)
        {
            if (range < 0)
                throw new ArgumentOutOfRangeException(nameof(range), range, "The minimum value is 0.");

            for (int q = -range; q <= range; q++)
                for (int r = Mathf.Max(-range, q - range); r <= Mathf.Min(range, q + range); r++)
                    yield return center + new HexagonCoordinates(q, r);
        }

        public static IEnumerable<HexagonCoordinates> Rotations(HexagonCoordinates origin, bool clockwise, int rotations)
        {
            if (rotations < 1)
                throw new ArgumentOutOfRangeException(nameof(rotations), rotations, "The minimum value is 1.");

            for (int i = 1; i <= rotations; i++)
                yield return Rotate(origin, clockwise);
        }

        public static IEnumerable<HexagonCoordinates> Ring(HexagonCoordinates center, int radius)
        {
            if (radius < 1)
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "The minimum value is 1.");

            HexagonCoordinates coordinates = center + (Adjacent(HexagonDirection.Five) * radius);

            foreach (HexagonDirection direction in (HexagonDirection[])Enum.GetValues(typeof(HexagonDirection)))
                for (int i = 0; i < radius; i++)
                    yield return coordinates += Adjacent(direction);
        }

        public static IEnumerable<HexagonCoordinates> Spiral(HexagonCoordinates center, int radius)
        {
            if (radius < 0)
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "The minimum value is 0.");

            yield return center;

            if (radius > 0)
            {
                for (int i = 1; i <= radius; i++)
                    foreach (HexagonCoordinates coordinates in Ring(center, i))
                        yield return coordinates;
            }
        }

        public override bool Equals(object other)
        {
            if (other is not HexagonCoordinates)
                return false;

            return base.Equals((HexagonCoordinates)other);
        }

        public bool Equals(HexagonCoordinates other)
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

        public static HexagonCoordinates operator -(HexagonCoordinates value)
        {
            return new HexagonCoordinates(-value.Q, -value.R);
        }

        public static HexagonCoordinates operator +(HexagonCoordinates left, HexagonCoordinates right)
        {
            return new HexagonCoordinates(left.Q + right.Q, left.R + right.R);
        }

        public static HexagonCoordinates operator -(HexagonCoordinates left, HexagonCoordinates right)
        {
            return new HexagonCoordinates(left.Q - right.Q, left.R - right.R);
        }

        public static HexagonCoordinates operator *(HexagonCoordinates left, HexagonCoordinates right)
        {
            return new HexagonCoordinates(left.Q * right.Q, left.R * right.R);
        }

        public static HexagonCoordinates operator *(int left, HexagonCoordinates right)
        {
            return new HexagonCoordinates(left * right.Q, left * right.R);
        }

        public static HexagonCoordinates operator *(HexagonCoordinates left, int right)
        {
            return new HexagonCoordinates(left.Q * right, left.R * right);
        }

        public static HexagonCoordinates operator /(HexagonCoordinates left, int right)
        {
            return new HexagonCoordinates(left.Q / right, left.R / right);
        }

        public static bool operator ==(HexagonCoordinates left, HexagonCoordinates right)
        {
            return (left.Q == right.Q) && (left.R == right.R);
        }

        public static bool operator !=(HexagonCoordinates left, HexagonCoordinates right)
        {
            return !(left == right);
        }
    }
}
