using System.Collections.Generic;

namespace Osryden.HexagonFramework
{
    public interface IHexagonMap<THexagonTile>
        where THexagonTile : IHexagonTile
    {
        THexagonTile this[HexagonCoordinates coordinates] { get; set; }

        int Count { get; }
        ICollection<HexagonCoordinates> Coordinates { get; }
        ICollection<THexagonTile> Tiles { get; }

        void Clear();
        bool ContainsCoordinates(HexagonCoordinates coordinates);
        bool TryGetTile(HexagonCoordinates coordinates, out THexagonTile tile);
    }
}
