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

        public int Q => m_Q;
        public int R => m_R;
        public int S => HexagonUtility.GetSAxis(Q, R);
        public int Length => (Mathf.Abs(Q) + Mathf.Abs(R) + Mathf.Abs(S)) / 2;

        public static HexagonCoordinates Adjacent(HexagonDirection direction)
        {
            return Adjacent((FlatTopHexagonDirection)direction);
        }

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

        public static int Distance(HexagonCoordinates a, HexagonCoordinates b)
        {
            return (a - b).Length;
        }

        public static Vector3 Position(HexagonCoordinates coordinates, float angle)
        {
            float x = coordinates.Q;
            float y = 0;
            float z = (coordinates.R + coordinates.S) / 2f;
            return Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(x, y, z);
        }

        public static Vector3 Position(HexagonCoordinates coordinates, HexagonOrientation orientation)
        {
            return Position(coordinates, HexagonUtility.GetAngle(orientation));
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

        public static IEnumerable<HexagonCoordinates> Range(HexagonCoordinates center, int range)
        {
            if (range < 1)
                throw new ArgumentOutOfRangeException(nameof(range), range, "The minimum value is 1.");

            for (int q = -range; q <= range; q++)
                for (int r = Mathf.Max(-range, q - range); r <= Mathf.Min(range, q + range); r++)
                    yield return center + new HexagonCoordinates(q, r);
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
