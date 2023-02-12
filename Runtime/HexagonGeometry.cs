using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    /// <summary>
    /// Representation of hexagon geometry.
    /// </summary>
    [Serializable]
    public struct HexagonGeometry : IEquatable<HexagonGeometry>
    {
        /// <summary>
        /// The number of edges in a hexagon.
        /// </summary>
        public const int EDGES = 6;

        /// <summary>
        /// The number of vertices in a hexagon.
        /// </summary>
        public const int VERTICES = 6;

        /// <summary>
        /// The angle of the flat-top orientation.
        /// </summary>
        public const float FLAT_TOP_ANGLE = 0;

        /// <summary>
        /// The angle of the pointy-top orientation.
        /// </summary>
        public const float POINTY_TOP_ANGLE = -30;

        [SerializeField] private HexagonOrientation m_Orientation;
        [SerializeField] private float m_Size;

        public HexagonGeometry(HexagonOrientation orientation, float size)
        {
            m_Orientation = orientation;
            m_Size = size;
        }

        /// <summary>
        /// The orientation of the hexagon.
        /// </summary>
        public HexagonOrientation Orientation
        {
            get => m_Orientation;
            set => m_Orientation = value;
        }

        /// <summary>
        /// The circumradius of the hexagon.
        /// </summary>
        public float Size
        {
            get => m_Size;
            set => m_Size = value;
        }

        /// <summary>
        /// The orientation angle of the hexagon.
        /// </summary>
        public float Angle => OrientationAngle(Orientation);

        /// <summary>
        /// The width of the hexagon.
        /// </summary>
        public float Width => Size * 2;

        /// <summary>
        /// The height of the hexagon.
        /// </summary>
        public float Height => Size * Mathf.Sqrt(3);

        /// <summary>
        /// The horizontal distance between adjacent hexagons centers.
        /// </summary>
        public float HorizontalDistance => Width * 3 / 4;

        /// <summary>
        /// The vertical distance between adjacent hexagons centers.
        /// </summary>
        public float VerticalDistance => Height;

        /// <summary>
        /// Gets all vertices of the hexagon. 
        /// </summary>
        public IEnumerable<Vector3> Vertices
        {
            get
            {
                for (int i = 0; i < VERTICES; i++)
                    yield return Vertex(Size, Angle, i);
            }
        }

        public static Vector3 Vertex(float size, float angle, int i)
        {
            float degrees = (60 * i) - angle;
            float radians = Mathf.PI / 180 * degrees;
            return new Vector3(size * Mathf.Cos(radians), 0, size * Mathf.Sin(radians));
        }

        public static float OrientationAngle(HexagonOrientation orientation)
        {
            switch (orientation)
            {
                case HexagonOrientation.FlatTop: return FLAT_TOP_ANGLE;
                case HexagonOrientation.PointyTop: return POINTY_TOP_ANGLE;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static Quaternion RotationAxis(float angle, Vector3 axis)
        {
            return Quaternion.AngleAxis(angle, axis);
        }

        public static Quaternion RotationAxis(HexagonOrientation orientation, Vector3 axis)
        {
            return RotationAxis(OrientationAngle(orientation), axis);
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
