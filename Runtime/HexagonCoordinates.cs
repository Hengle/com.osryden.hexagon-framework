using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    [Serializable]
    public struct HexagonCoordinates : IHexagonCoordinates<int>, IEquatable<HexagonCoordinates>
    {
        [SerializeField] private int m_Q;
        [SerializeField] private int m_R;

        public HexagonCoordinates(int q, int r)
        {
            m_Q = q;
            m_R = r;
        }

        public static HexagonCoordinates Origin { get; } = new HexagonCoordinates(0, 0);
        public static HexagonCoordinates FlatTopNorthAdjacent { get; } = new HexagonCoordinates(0, 1);
        public static HexagonCoordinates FlatTopNortheastAdjacent { get; } = new HexagonCoordinates(1, 1);
        public static HexagonCoordinates FlatTopSoutheastAdjacent { get; } = new HexagonCoordinates(1, 0);
        public static HexagonCoordinates FlatTopSouthAdjacent { get; } = new HexagonCoordinates(0, -1);
        public static HexagonCoordinates FlatTopSouthwestAdjacent { get; } = new HexagonCoordinates(-1, -1);
        public static HexagonCoordinates FlatTopNorthwestAdjacent { get; } = new HexagonCoordinates(-1, 0);
        public static HexagonCoordinates PointyTopNortheastAdjacent { get; } = new HexagonCoordinates(1, 1);
        public static HexagonCoordinates PointyTopEastAdjacent { get; } = new HexagonCoordinates(1, 0);
        public static HexagonCoordinates PointyTopSoutheastAdjacent { get; } = new HexagonCoordinates(0, -1);
        public static HexagonCoordinates PointyTopSouthwestAdjacent { get; } = new HexagonCoordinates(-1, -1);
        public static HexagonCoordinates PointyTopWestAdjacent { get; } = new HexagonCoordinates(-1, 0);
        public static HexagonCoordinates PointyTopNorthwestAdjacent { get; } = new HexagonCoordinates(0, 1);

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

        public int Q => m_Q;
        public int R => m_R;
        public int S => -Q + R;
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

        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates a, HexagonCoordinates b)
        {
            int distance = Distance(a, b);
            float step = 1 / (float)Mathf.Max(distance, 1);

            for (int i = 0; i <= distance; i++)
                yield return Lerp(a, b, i * step);
        }

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

        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates origin, HexagonDirection direction, int length)
        {
            return Line(origin, Adjacent(direction), length);
        }

        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates origin, FlatTopHexagonDirection direction, int length)
        {
            return Line(origin, Adjacent(direction), length);
        }

        public static IEnumerable<HexagonCoordinates> Line(HexagonCoordinates origin, PointyTopHexagonDirection direction, int length)
        {
            return Line(origin, Adjacent(direction), length);
        }

        public static IEnumerable<HexagonCoordinates> Range(HexagonCoordinates center, int range)
        {
            if (range < 1)
                throw new ArgumentOutOfRangeException(nameof(range), range, "The minimum value is 1.");

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
