using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    [Serializable]
    public struct HexagonGeometry : IEquatable<HexagonGeometry>
    {
        [SerializeField] private HexagonOrientation m_Orientation;
        [SerializeField] private float m_Size;

        public HexagonGeometry(HexagonOrientation orientation, float size)
        {
            m_Orientation = orientation;
            m_Size = size;
        }

        public HexagonOrientation Orientation
        {
            get => m_Orientation;
            set => m_Orientation = value;
        }
        public float Size
        {
            get => m_Size;
            set => m_Size = value;
        }
        public float Angle => HexagonUtility.GetAngle(Orientation);
        public float Width => Size * 2;
        public float Height => Size * Mathf.Sqrt(3);
        public float HorizontalDistance => Width * 3 / 4;
        public float VerticalDistance => Height;
        public IEnumerable<Vector3> Corners
        {
            get
            {
                for (int i = 0; i < HexagonUtility.Corners; i++)
                {
                    float degrees = (60 * i) - Angle;
                    float radians = Mathf.PI / 180 * degrees;
                    yield return new Vector3(Size * Mathf.Cos(radians), 0, Size * Mathf.Sin(radians));
                }
            }
        }

        public override bool Equals(object other)
        {
            if (other is not HexagonGeometry)
                return false;

            return Equals((HexagonGeometry)other);
        }

        public bool Equals(HexagonGeometry other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Size, Orientation);
        }

        public override string ToString()
        {
            return $"{nameof(HexagonGeometry)}[orientation: {Orientation}; size: {Size}]";
        }

        public static bool operator ==(HexagonGeometry left, HexagonGeometry right)
        {
            if (left.Orientation != right.Orientation) return false;
            if (left.Size != right.Size) return false;
            return true;
        }

        public static bool operator !=(HexagonGeometry left, HexagonGeometry right)
        {
            return !(left == right);
        }
    }
}
