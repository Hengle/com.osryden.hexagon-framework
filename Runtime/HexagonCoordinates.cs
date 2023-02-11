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
        public int S => -Q + R;
        public int Length => (Mathf.Abs(Q) + Mathf.Abs(R) + Mathf.Abs(S)) / 2;

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

        public static IEnumerable<HexagonCoordinates> Range(HexagonCoordinates center, int range)
        {
            if (range < 1)
                throw new ArgumentOutOfRangeException(nameof(range), range, "");

            for (int q = -range; q <= range; q++)
                for (int r = Mathf.Max(-range, q - range); r <= Mathf.Min(range, q + range); r++)
                    yield return center + new HexagonCoordinates(q, r);
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
