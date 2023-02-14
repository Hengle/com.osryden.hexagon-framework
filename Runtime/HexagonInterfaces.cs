namespace Osryden.HexagonFramework
{
    public interface IHexagonCoordinates<T>
    {
        T this[HexagonCoordinateAxis axis] { get; }

        T Q { get; }
        T R { get; }
        T S { get; }
        T Length { get; }
    }

    public interface IHexagonSize
    {
        float Size { get; }
        float Width { get; }
        float Height { get; }
        float HorizontalDistance { get; }
        float VerticalDistance { get; }
    }

    public interface IHexagonTile
    {
        HexagonCoordinates Coordinates { get; }
    }
}
