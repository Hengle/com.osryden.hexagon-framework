using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    /// <summary>
    /// Representation of hexagon coordinates.
    /// </summary>
    [Serializable]
    public struct HexagonCoordinates : IEquatable<HexagonCoordinates>
    {
        [SerializeField] private int m_Q;
        [SerializeField] private int m_R;

        /// <summary>
        /// Creates a <see cref="HexagonCoordinates"/> with the specified <paramref name="q"/> and <paramref name="r"/> coordinates.
        /// </summary>
        public HexagonCoordinates(int q, int r)
        {
            m_Q = q;
            m_R = r;
        }

        /// <summary>
        /// Shorthand for <see cref="HexagonCoordinates"/>(0, 0).
        /// </summary>
        public static HexagonCoordinates Origin { get; } = new HexagonCoordinates(0, 0);

        /// <summary>
        /// Returns the Q, R, S coordinates using an index of 0, 1, 2 respectively.
        /// </summary>
        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Q;
                    case 1: return R;
                    case 2: return S;
                    default:
                        throw new IndexOutOfRangeException($"Invalid index: {index}!");
                }
            }
        }

        /// <summary>
        /// The Q-axis coordinate.
        /// </summary>
        public int Q => m_Q;

        /// <summary>
        /// The R-axis coordinate.
        /// </summary>
        public int R => m_R;

        /// <summary>
        /// The S-axis coordinate.
        /// </summary>
        public int S => -Q + R;

        /// <summary>
        /// The length of the Q, R, S coordinates.
        /// </summary>
        public int Length => (Mathf.Abs(Q) + Mathf.Abs(R) + Mathf.Abs(S)) / 2;

        /// <summary>
        /// Returns the adjacent coordinates in the specified <paramref name="direction"/>.
        /// </summary>
        public static HexagonCoordinates Adjacent(HexagonDirection direction)
        {
            return Adjacent((FlatTopHexagonDirection)direction);
        }

        /// <summary>
        /// Returns the adjacent coordinates in the specified <paramref name="direction"/>.
        /// </summary>
        public static HexagonCoordinates Adjacent(FlatTopHexagonDirection direction)
        {
            switch (direction)
            {
                case FlatTopHexagonDirection.North: return new HexagonCoordinates(0, 1);
                case FlatTopHexagonDirection.Northeast: return new HexagonCoordinates(1, 1);
                case FlatTopHexagonDirection.Southeast: return new HexagonCoordinates(1, 0);
                case FlatTopHexagonDirection.South: return new HexagonCoordinates(0, -1);
                case FlatTopHexagonDirection.Southwest: return new HexagonCoordinates(-1, -1);
                case FlatTopHexagonDirection.Northwest: return new HexagonCoordinates(-1, 0);
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Returns the adjacent coordinates in the specified <paramref name="direction"/>.
        /// </summary>
        public static HexagonCoordinates Adjacent(PointyTopHexagonDirection direction)
        {
            switch (direction)
            {
                case PointyTopHexagonDirection.Northeast: return new HexagonCoordinates(1, 1);
                case PointyTopHexagonDirection.East: return new HexagonCoordinates(1, 0);
                case PointyTopHexagonDirection.Southeast: return new HexagonCoordinates(0, -1);
                case PointyTopHexagonDirection.Southwest: return new HexagonCoordinates(-1, -1);
                case PointyTopHexagonDirection.West: return new HexagonCoordinates(-1, 0);
                case PointyTopHexagonDirection.Northwest: return new HexagonCoordinates(0, 1);
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Rounds a fractional coordinates.
        /// </summary>
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

        /// <summary>
        /// Linearly iterpolates between two coordinates.
        /// </summary>
        public static HexagonCoordinates Lerp(HexagonCoordinates a, HexagonCoordinates b, float t)
        {
            return Round(FractionalHexagonCoordinates.Lerp(a, b, t));
        }

        /// <summary>
        /// Returns the distance between two coordinates.
        /// </summary>
        public static int Distance(HexagonCoordinates a, HexagonCoordinates b)
        {
            return (a - b).Length;
        }

        /// <summary>
        /// Returns the position of the specified <paramref name="coordinates"/> relative to the <paramref name="geometry"/>.
        /// </summary>
        public static Vector3 Position(HexagonCoordinates coordinates, HexagonGeometry geometry)
        {
            float x = coordinates.Q * geometry.HorizontalDistance;
            float y = 0;
            float z = (coordinates.R + coordinates.S) / 2f * geometry.VerticalDistance;
            return HexagonGeometry.RotationAxis(geometry.Orientation, Vector3.up) * new Vector3(x, y, z);
        }

        /// <summary>
        /// Returns all adjacent coordinates.
        /// </summary>
        public static IEnumerable<HexagonCoordinates> Adjacents()
        {
            foreach (HexagonDirection direction in (HexagonDirection[])Enum.GetValues(typeof(HexagonDirection)))
                yield return Adjacent(direction);
        }

        /// <summary>
        /// Returns all adjacent coordinates of the specified <paramref name="coordinates"/>.
        /// </summary>
        public static IEnumerable<HexagonCoordinates> Adjacents(HexagonCoordinates coordinates)
        {
            foreach (HexagonCoordinates adjacent in Adjacents())
                yield return coordinates + adjacent;
        }

        /// <summary>
        /// Returns a line of coordinates of the specified <paramref name="length"/> in the <paramref name="direction"/> starting from <paramref name="origin"/>.
        /// </summary>
        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates origin, HexagonCoordinates direction, int length)
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

        /// <summary>
        /// Returns a line of coordinates of the specified <paramref name="length"/> in the <paramref name="direction"/> starting from <paramref name="origin"/>.
        /// </summary>
        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates origin, HexagonDirection direction, int length)
        {
            return Line(origin, Adjacent(direction), length);
        }

        /// <summary>
        /// Returns a line of coordinates of the specified <paramref name="length"/> in the <paramref name="direction"/> starting from <paramref name="origin"/>.
        /// </summary>
        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates origin, FlatTopHexagonDirection direction, int length)
        {
            return Line(origin, Adjacent(direction), length);
        }

        /// <summary>
        /// Returns a line of coordinates of the specified <paramref name="length"/> in the <paramref name="direction"/> starting from <paramref name="origin"/>.
        /// </summary>
        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates origin, PointyTopHexagonDirection direction, int length)
        {
            return Line(origin, Adjacent(direction), length);
        }

        /// <summary>
        /// Returns all coordinates within the <paramref name="range"/> from the <paramref name="center"/>.
        /// </summary>
        public static IEnumerable<HexagonCoordinates> Range(HexagonCoordinates center, int range)
        {
            if (range < 1)
                throw new ArgumentOutOfRangeException(nameof(range), range, "The minimum value is 1.");

            for (int q = -range; q <= range; q++)
                for (int r = Mathf.Max(-range, q - range); r <= Mathf.Min(range, q + range); r++)
                    yield return center + new HexagonCoordinates(q, r);
        }

        /// <summary>
        /// Returns all coordinates on the ring of the specified <paramref name="radius"/> from the <paramref name="center"/>.
        /// </summary>
        public static IEnumerable<HexagonCoordinates> Ring(HexagonCoordinates center, int radius)
        {
            if (radius < 1)
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "The minimum value is 1.");

            HexagonCoordinates coordinates = center + (Adjacent(HexagonDirection.Five) * radius);

            foreach (HexagonDirection direction in (HexagonDirection[])Enum.GetValues(typeof(HexagonDirection)))
                for (int i = 0; i < radius; i++)
                    yield return coordinates += Adjacent(direction);
        }

        /// <summary>
        /// Returns all coordinates within the <paramref name="radius"/> starting from the <paramref name="center"/>.
        /// </summary>
        public static IEnumerable<HexagonCoordinates> Spiral(HexagonCoordinates center, int radius)
        {
            if (radius < 1)
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "The minimum value is 1.");

            yield return center;

            for (int i = 1; i <= radius; i++)
                foreach (HexagonCoordinates coordinates in Ring(center, i))
                    yield return coordinates;
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
            return $"({Q}, {R}, {S})";
        }

        public static HexagonCoordinates operator +(HexagonCoordinates left, HexagonCoordinates right)
        {
            return new HexagonCoordinates(left.Q + right.Q, left.R + right.R);
        }

        public static HexagonCoordinates operator -(HexagonCoordinates left, HexagonCoordinates right)
        {
            return new HexagonCoordinates(left.Q - right.Q, left.R - right.R);
        }

        public static HexagonCoordinates operator *(int left, HexagonCoordinates right)
        {
            return new HexagonCoordinates(left * right.Q, left * right.R);
        }

        public static HexagonCoordinates operator *(HexagonCoordinates left, int right)
        {
            return new HexagonCoordinates(left.Q * right, left.R * right);
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
