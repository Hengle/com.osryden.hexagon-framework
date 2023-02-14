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
}
