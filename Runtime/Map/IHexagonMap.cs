using System.Collections.Generic;

namespace Osryden.HexagonFramework
{
    public interface IHexagonMap<THexagonTile>
        where THexagonTile : IHexagonTile
    {
        THexagonTile this[HexagonCoordinates coordinates] { get; set; }

        int Count { get; }
        IEnumerable<HexagonCoordinates> Coordinates { get; }
        IEnumerable<THexagonTile> Tiles { get; }

        void Clear();
        bool ContainsCoordinates(HexagonCoordinates coordinates);
        bool TryGetTile(HexagonCoordinates coordinates, out THexagonTile tile);
    }
}
