using System;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    [Serializable]
    public struct HexagonGeometry : IEquatable<HexagonGeometry>
    {
        [SerializeField] private float m_Size;
        [SerializeField] private HexagonOrientation m_Orientation;
        [SerializeField] private float m_CustomAngle;

        public HexagonGeometry(float size, HexagonOrientation orientation)
        {
            m_Size = size;
            m_Orientation = orientation;
            m_CustomAngle = 0;
        }

        public HexagonGeometry(float size, float customAngle)
        {
            m_Size = size;
            m_Orientation = HexagonOrientation.Custom;
            m_CustomAngle = customAngle;
        }

        public float Size
        {
            get => m_Size;
            set => m_Size = value;
        }
        public HexagonOrientation Orientation
        {
            get => m_Orientation;
            set => m_Orientation = value;
        }
        public float CustomAngle
        {
            get => m_CustomAngle;
            set => m_CustomAngle = value;
        }
        public float Angle
        {
            get
            {
                switch (Orientation)
                {
                    case HexagonOrientation.FlatTop:
                    case HexagonOrientation.PointyTop:
                        return HexagonUtility.GetAngle(Orientation);
                    case HexagonOrientation.Custom:
                        return CustomAngle;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }
        public float Width => Size * 2;
        public float Height => Size * Mathf.Sqrt(3);
        public float HorizontalDistance => Width * 3 / 4;
        public float VerticalDistance => Height;

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
            return HashCode.Combine(Size, Orientation, CustomAngle);
        }

        public override string ToString()
        {
            return $"{nameof(HexagonGeometry)}[size: {Size}; orientation: {Orientation}; angle: {Angle}]";
        }

        public static bool operator ==(HexagonGeometry left, HexagonGeometry right)
        {
            if (left.Orientation != right.Orientation) return false;
            if (left.Size != right.Size) return false;
            if (left.CustomAngle != right.CustomAngle) return false;
            return true;
        }

        public static bool operator !=(HexagonGeometry left, HexagonGeometry right)
        {
            return !(left == right);
        }
    }
}
