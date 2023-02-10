namespace Osryden.HexagonFramework
{
    public interface IHexagonCoordinates<T>
    {
        T Q { get; }
        T R { get; }
        T S { get; }
        T Length { get; }
    }
}
