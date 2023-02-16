using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    /// <summary>
    /// Represents hexagon geometry.
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
        /// The angle of a flat-top orientation hexagon.
        /// </summary>
        public const float FLAT_TOP_ANGLE = 0;

        /// <summary>
        /// The angle of a pointy-top orientation hexagon.
        /// </summary>
        public const float POINTY_TOP_ANGLE = -30;

        [SerializeField] private HexagonOrientation m_Orientation;
        [SerializeField] private float m_Size;

        /// <summary>
        /// Create a new <see cref="HexagonGeometry"/> with the specified <paramref name="orientation"/> and <paramref name="size"/>.
        /// </summary>
        /// <param name="orientation">The orientation of the geometry.</param>
        /// <param name="size">The size of the geometry.</param>
        public HexagonGeometry(HexagonOrientation orientation, float size)
        {
            m_Orientation = orientation;
            m_Size = size;
        }

        /// <summary>
        /// Gets the geometry of a flat-top hexagon with the size of 1.
        /// </summary>
        public static HexagonGeometry FlatTopGeometry { get; } = new HexagonGeometry(HexagonOrientation.FlatTop, 1);

        /// <summary>
        /// Gets the geometry of a pointy-top hexagon with the size of 1.
        /// </summary>
        public static HexagonGeometry PointyTopGeometry { get; } = new HexagonGeometry(HexagonOrientation.PointyTop, 1);

        /// <summary>
        /// The orientation of the geometry.
        /// </summary>
        public HexagonOrientation Orientation
        {
            get => m_Orientation;
            set => m_Orientation = value;
        }

        /// <summary>
        /// The circumradius of the geometry.
        /// </summary>
        public float Size
        {
            get => m_Size;
            set => m_Size = value;
        }

        /// <summary>
        /// The angle of the geometry.
        /// </summary>
        public float Angle => OrientationAngle(Orientation);

        /// <summary>
        /// The width of the geometry.
        /// </summary>
        public float Width => Size * 2;

        /// <summary>
        /// The height of the geometry.
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
        /// Gets all vertices of the geometry.
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
        /// Returns the position of the specified vertex.
        /// </summary>
        /// <param name="size">The size of the geometry.</param>
        /// <param name="angle">The angle of the geometry.</param>
        /// <param name="index">The index of which vertex to get.</param>
        /// <returns>The vertex position.</returns>
        public static Vector3 Vertex(float size, float angle, int index)
        {
            float degrees = (60 * index) - angle;
            float radians = Mathf.PI / 180 * degrees;
            return new Vector3(size * Mathf.Cos(radians), 0, size * Mathf.Sin(radians));
        }

        /// <summary>
        /// Returns the angle of the specified <paramref name="orientation"/>.
        /// </summary>
        /// <param name="orientation">The orientation to get the angle from.</param>
        /// <returns>The orientation angle.</returns>
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
        /// Returns the rotation of the specified <paramref name="angle"/> around the <paramref name="axis"/>.
        /// </summary>
        /// <param name="angle">The angle to rotate by.</param>
        /// <param name="axis">The axis to rotate around.</param>
        /// <returns>The rotation.</returns>
        public static Quaternion RotationAxis(float angle, Vector3 axis)
        {
            return Quaternion.AngleAxis(angle, axis);
        }

        /// <summary>
        /// Returns the rotation of the specified <paramref name="orientation"/> angle around the <paramref name="axis"/>.
        /// </summary>
        /// <param name="orientation">The angle of the orientation to rotate by.</param>
        /// <param name="axis">The axis to rotate around.</param>
        /// <returns>The rotation.</returns>
        public static Quaternion RotationAxis(HexagonOrientation orientation, Vector3 axis)
        {
            return RotationAxis(OrientationAngle(orientation), axis);
        }

        /// <summary>
        /// Returns whether this instance and <paramref name="other"/> are equal.
        /// </summary>
        /// <param name="other">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="other"/> are equal; otherwise <see langword="false"/>.</returns>
        public override bool Equals(object other)
        {
            if (other is not HexagonGeometry)
                return false;

            return Equals((HexagonGeometry)other);
        }

        /// <summary>
        /// Returns whether this instance and <paramref name="other"/> are equal.
        /// </summary>
        /// <param name="other">The geometry to compare with the current instance.</param>
        /// <returns><see langword="true"/> if the current instance and <paramref name="other"/> are equal; otherwise <see langword="false"/>.</returns>
        public bool Equals(HexagonGeometry other)
        {
            return this == other;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Size, Orientation);
        }

        /// <summary>
        /// Returns the string representation of the current instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            return $"{nameof(HexagonGeometry)}[orientation: {Orientation}; size: {Size}]";
        }

        /// <summary>
        /// Returns whether two geometries are equal.
        /// </summary>
        /// <param name="left">The first geometry to compare.</param>
        /// <param name="right">The second geometry to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> is equal; otherwise <see langword="false"/>.</returns>
        public static bool operator ==(HexagonGeometry left, HexagonGeometry right)
        {
            if (left.Orientation != right.Orientation) return false;
            if (left.Size != right.Size) return false;
            return true;
        }

        /// <summary>
        /// Returns whether two geometries are not equal.
        /// </summary>
        /// <param name="left">The first geometry to compare.</param>
        /// <param name="right">The second geometry to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are not equal; otherwise <see langword="false"/>.</returns>
        public static bool operator !=(HexagonGeometry left, HexagonGeometry right)
        {
            return !(left == right);
        }
    }
}
