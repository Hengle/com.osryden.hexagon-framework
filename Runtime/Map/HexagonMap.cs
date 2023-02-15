using System.Collections.Generic;

namespace Osryden.HexagonFramework
{
    public class HexagonMap<THexagonTile> : IHexagonMap<THexagonTile>
        where THexagonTile : IHexagonTile
    {
        public HexagonMap(HexagonMapShape shape)
        {
            Data = new Dictionary<HexagonCoordinates, THexagonTile>();

            foreach (HexagonCoordinates coordinates in shape)
                Data.Add(coordinates, default);
        }

        public THexagonTile this[HexagonCoordinates coordinates]
        {
            get => Data[coordinates];
            set => Data[coordinates] = value;
        }

        public int Count => Data.Count;
        public IEnumerable<HexagonCoordinates> Coordinates => Data.Keys;
        public IEnumerable<THexagonTile> Tiles => Data.Values;
        private Dictionary<HexagonCoordinates, THexagonTile> Data { get; }

        public void Clear()
        {
            foreach (HexagonCoordinates coordinates in Coordinates)
                this[coordinates] = default;
        }

        public bool ContainsCoordinates(HexagonCoordinates coordinates)
        {
            return Data.ContainsKey(coordinates);
        }

        public bool TryGetTile(HexagonCoordinates coordinates, out THexagonTile tile)
        {
            return Data.TryGetValue(coordinates, out tile);
        }
    }
}
