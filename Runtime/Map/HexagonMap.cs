using System.Collections.Generic;

namespace Osryden.HexagonFramework
{
    public class HexagonMap<THexagonTile> : IHexagonMap<THexagonTile>
        where THexagonTile : IHexagonTile
    {
        public THexagonTile this[HexagonCoordinates coordinates]
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public int Count => throw new System.NotImplementedException();
        public ICollection<HexagonCoordinates> Coordinates => throw new System.NotImplementedException();
        public ICollection<THexagonTile> Tiles => throw new System.NotImplementedException();

        public void Clear() => throw new System.NotImplementedException();

        public bool ContainsCoordinates(HexagonCoordinates coordinates) => throw new System.NotImplementedException();

        public bool TryGetTile(HexagonCoordinates coordinates, out THexagonTile tile) => throw new System.NotImplementedException();
    }
}
