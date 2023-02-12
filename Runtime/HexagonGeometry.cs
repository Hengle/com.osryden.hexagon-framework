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
        /// The flat-topped hexagon orientation angle.
        /// </summary>
        public const float FLAT_TOP_ANGLE = 0;

        /// <summary>
        /// The pointy-topped hexagon orientation angle.
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
        /// Returns a flat-top orientation geometry with the size of 1.
        /// </summary>
        public static HexagonGeometry FlatTopGeometry { get; } = new HexagonGeometry(HexagonOrientation.FlatTop, 1);

        /// <summary>
        /// Returns a pointy-top orientation geometry with the size of 1.
        /// </summary>
        public static HexagonGeometry PointyTopGeometry { get; } = new HexagonGeometry(HexagonOrientation.PointyTop, 1);

        /// <summary>
        /// The hexagon orientation.
        /// </summary>
        public HexagonOrientation Orientation
        {
            get => m_Orientation;
            set => m_Orientation = value;
        }

        /// <summary>
        /// The hexagon circumradius.
        /// </summary>
        public float Size
        {
            get => m_Size;
            set => m_Size = value;
        }

        /// <summary>
        /// Returns the hexagon orientation angle.
        /// </summary>
        public float Angle => OrientationAngle(Orientation);

        /// <summary>
        /// Returns the hexagon width.
        /// </summary>
        public float Width => Size * 2;

        /// <summary>
        /// Returns the hexagon height.
        /// </summary>
        public float Height => Size * Mathf.Sqrt(3);

        /// <summary>
        /// Returns the horizontal distance between adjacent hexagons centers.
        /// </summary>
        public float HorizontalDistance => Width * 3 / 4;

        /// <summary>
        /// Returns the vertical distance between adjacent hexagons centers.
        /// </summary>
        public float VerticalDistance => Height;

        /// <summary>
        /// Returns all hexagon vertices.
        /// </summary>
        public IEnumerable<Vector3> Vertices
        {
            get
            {
                for (int i = 0; i < VERTICES; i++)
                    yield return Vertex(Size, Angle, i);
            }
        }

        /// <summary>
        /// Returns the vertex position of the specified <paramref name="index"/> relative to the <paramref name="size"/> and <paramref name="angle"/>.
        /// </summary>
        public static Vector3 Vertex(float size, float angle, int index)
        {
            if ((index < 0) || (index >= VERTICES))
                throw new ArgumentOutOfRangeException(nameof(index), index, $"The value ");

            float degrees = (60 * index) - angle;
            float radians = Mathf.PI / 180 * degrees;
            return new Vector3(size * Mathf.Cos(radians), 0, size * Mathf.Sin(radians));
        }

        /// <summary>
        /// Returns the angle of the specified <paramref name="orientation"/>.
        /// </summary>
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

        /// <summary>
        /// Returns the rotation of the <paramref name="angle"/> around the <paramref name="axis"/>.
        /// </summary>
        public static Quaternion RotationAxis(float angle, Vector3 axis)
        {
            return Quaternion.AngleAxis(angle, axis);
        }

        /// <summary>
        /// Returns the rotation of the <paramref name="orientation"/> around the <paramref name="axis"/>.
        /// </summary>
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
