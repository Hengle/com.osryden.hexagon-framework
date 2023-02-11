using System;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    public static class HexagonUtility
    {
        public static int Adjacents { get; } = 6;
        public static int Corners { get; } = 6;
        public static int Sides { get; } = 6;
        public static float FlatTopAngle { get; } = 0;
        public static float PointyTopAngle { get; } = -30;

        public static float GetAngle(HexagonOrientation orientation)
        {
            switch (orientation)
            {
                case HexagonOrientation.FlatTop: return FlatTopAngle;
                case HexagonOrientation.PointyTop: return PointyTopAngle;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static float GetSAxis(float q, float r)
        {
            return -q + r;
        }

        public static int GetSAxis(int q, int r)
        {
            return Mathf.RoundToInt(GetSAxis((float)q, (float)r));
        }

        public static Quaternion GetRotation(float angle, Vector3 axis)
        {
            return Quaternion.AngleAxis(angle, axis);
        }

        public static Quaternion GetRotation(HexagonOrientation orientation, Vector3 axis)
        {
            return GetRotation(GetAngle(orientation), axis);
        }
    }
}
