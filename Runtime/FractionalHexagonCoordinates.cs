using System;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    [Serializable]
    public struct FractionalHexagonCoordinates : IEquatable<FractionalHexagonCoordinates>
    {
        [SerializeField] private float m_Q;
        [SerializeField] private float m_R;

        public FractionalHexagonCoordinates(float q, float r)
        {
            m_Q = q;
            m_R = r;
        }

        public static FractionalHexagonCoordinates Origin { get; } = new FractionalHexagonCoordinates(0, 0);

        public float this[int index]
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

        public float Q => m_Q;
        public float R => m_R;
        public float S => -Q + R;
        public float Length => (Mathf.Abs(Q) + Mathf.Abs(R) + Mathf.Abs(S)) / 2;

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
            return $"({Q}, {R}, {S})";
        }

        public static FractionalHexagonCoordinates operator +(FractionalHexagonCoordinates left, FractionalHexagonCoordinates right)
        {
            return new FractionalHexagonCoordinates(left.Q + right.Q, left.R + right.R);
        }

        public static FractionalHexagonCoordinates operator -(FractionalHexagonCoordinates left, FractionalHexagonCoordinates right)
        {
            return new FractionalHexagonCoordinates(left.Q - right.Q, left.R - right.R);
        }

        public static FractionalHexagonCoordinates operator *(float left, FractionalHexagonCoordinates right)
        {
            return new FractionalHexagonCoordinates(left * right.Q, left * right.R);
        }

        public static FractionalHexagonCoordinates operator *(FractionalHexagonCoordinates left, float right)
        {
            return new FractionalHexagonCoordinates(left.Q * right, left.R * right);
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
            return new HexagonCoordinates(value.Q, value.R);
        }
    }
}
