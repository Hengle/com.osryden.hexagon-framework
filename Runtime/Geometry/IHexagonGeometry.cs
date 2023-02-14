using System.Collections.Generic;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    public interface IHexagonGeometry : IHexagonSize
    {
        HexagonOrientation Orientation { get; }
        float Angle { get; }
        IEnumerable<Vector3> Vertices { get; }
    }
}
