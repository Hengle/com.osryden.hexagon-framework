using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    [Serializable]
    public class RectangularMapShape : HexagonMapShape
    {
        [SerializeField] private HexagonOrientation m_Orientation;
        [SerializeField, Min(0)] private int m_Top;
        [SerializeField, Min(0)] private int m_Bottom;
        [SerializeField, Min(0)] private int m_Left;
        [SerializeField, Min(0)] private int m_Right;

        public RectangularMapShape(HexagonCoordinates origin, int size) : base(origin)
        {
            m_Top = size;
            m_Bottom = size;
            m_Left = size;
            m_Right = size;
        }

        public RectangularMapShape(HexagonCoordinates origin, int top, int bottom, int left, int right) : base(origin)
        {
            m_Top = top;
            m_Bottom = bottom;
            m_Left = left;
            m_Right = right;
        }

        public HexagonOrientation Orientation => m_Orientation;
        public int Top => m_Top;
        public int Bottom => m_Bottom;
        public int Left => m_Left;
        public int Right => m_Right;

        protected override HashSet<HexagonCoordinates> Shape()
        {
            switch (Orientation)
            {
                case HexagonOrientation.FlatTop: return FlatTopOrientationShape();
                case HexagonOrientation.PointyTop: return PointyTopOrientationShape();
                default:
                    throw new InvalidOperationException();
            }
        }

        protected HashSet<HexagonCoordinates> FlatTopOrientationShape()
        {
            var shape = new HashSet<HexagonCoordinates>();

            for (int q = -Left; q <= Right; q++)
            {
                int qOffset = Mathf.FloorToInt(q / 2f);

                for (int r = -Bottom + qOffset; r <= Top + qOffset; r++)
                    shape.Add(new HexagonCoordinates(q, r));
            }

            return shape;
        }

        protected HashSet<HexagonCoordinates> PointyTopOrientationShape()
        {
            var shape = new HashSet<HexagonCoordinates>();

            for (int r = -Bottom; r <= Top; r++)
            {
                int rOffset = Mathf.FloorToInt(r / 2f);

                for (int q = -Left + rOffset; q <= Right + rOffset; q++)
                    shape.Add(new HexagonCoordinates(q, r));
            }

            return shape;
        }
    }
}
