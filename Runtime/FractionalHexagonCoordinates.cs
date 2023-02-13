using System;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    /// <summary>
    /// Representation of fractional hexagon coordinates.
    /// </summary>
    [Serializable]
    public struct FractionalHexagonCoordinates : IEquatable<FractionalHexagonCoordinates>
    {
        [SerializeField] private float m_Q;
        [SerializeField] private float m_R;

        /// <summary>
        /// Creates a <see cref="FractionalHexagonCoordinates"/> with the specified <paramref name="q"/> and <paramref name="r"/> coordinates.
        /// </summary>
        public FractionalHexagonCoordinates(float q, float r)
        {
            m_Q = q;
            m_R = r;
        }

        /// <summary>
        /// Shorthand for <see cref="FractionalHexagonCoordinates"/>(0, 0).
        /// </summary>
        public static FractionalHexagonCoordinates Origin { get; } = new FractionalHexagonCoordinates(0, 0);

        /// <summary>
        /// Returns the Q, R, S coordinates using an index of 0, 1, 2 respectively.
        /// </summary>
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

        /// <summary>
        /// The Q-axis coordinate.
        /// </summary>
        public float Q => m_Q;

        /// <summary>
        /// The R-axis coordinate.
        /// </summary>
        public float R => m_R;

        /// <summary>
        /// The S-axis coordinate.
        /// </summary>
        public float S => -Q + R;

        /// <summary>
        /// The length of the Q, R, S coordinates.
        /// </summary>
        public float Length => (Mathf.Abs(Q) + Mathf.Abs(R) + Mathf.Abs(S)) / 2;

        /// <summary>
        /// Linearly iterpolates between two coordinates.
        /// </summary>
        public static FractionalHexagonCoordinates Lerp(FractionalHexagonCoordinates a, FractionalHexagonCoordinates b, float t)
        {
            return new FractionalHexagonCoordinates(Mathf.Lerp(a.Q, b.Q, t), Mathf.Lerp(a.R, b.R, t));
        }

        /// <summary>
        /// Returns the distance between two coordinates.
        /// </summary>
        public static float Distance(FractionalHexagonCoordinates a, FractionalHexagonCoordinates b)
        {
            return (a - b).Length;
        }

        /// <summary>
        /// Returns the position of the specified <paramref name="coordinates"/> relative to the <paramref name="geometry"/>.
        /// </summary>
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
            return new FractionalHexagonCoordinates(value.Q, value.R);
        }
    }
}
