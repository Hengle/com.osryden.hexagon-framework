namespace Osryden.HexagonFramework
{
    public interface IHexagonGeometry : IHexagonSize
    {
        HexagonOrientation Orientation { get; }
    }
}
