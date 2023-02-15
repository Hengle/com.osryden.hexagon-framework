using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    [Serializable]
    public class HexagonalMapShape : HexagonMapShape
    {
        [SerializeField] private int m_Size;

        public HexagonalMapShape(HexagonCoordinates origin, int size) : base(origin)
        {
            m_Size = size;
        }

        public int Size => m_Size;

        protected override HashSet<HexagonCoordinates> Shape()
        {
            return HexagonCoordinates.Range(HexagonCoordinates.Origin, Size).ToHashSet();
        }
    }
}
