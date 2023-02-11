using System;

namespace Osryden.HexagonFramework
{
    public static class HexagonUtility
    {
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
                case HexagonOrientation.Custom: return 0;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
