using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    [Serializable]
    public class TriangularMapShape : HexagonMapShape
    {
        [SerializeField, Min(0)] private int m_Size;
        [SerializeField] private HexagonCoordinateAxis m_Axis;

        public TriangularMapShape(HexagonCoordinates origin, int size, HexagonCoordinateAxis axis) : base(origin)
        {
            m_Size = size;
            m_Axis = axis;
        }

        public int Size => m_Size;
        public HexagonCoordinateAxis Axis => m_Axis;

        protected override HashSet<HexagonCoordinates> Shape()
        {
            var shape = new HashSet<HexagonCoordinates>();

            for (int q = 0; q <= Size; q++)
                for (int r = 0; r <= Size - q; r++)
                    shape.Add(HexagonCoordinates.Reflect(new HexagonCoordinates(q, -r), Axis));

            return shape;
        }
    }
}
