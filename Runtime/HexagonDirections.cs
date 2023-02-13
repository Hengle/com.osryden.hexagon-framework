namespace Osryden.HexagonFramework
{
    public enum HexagonDirection
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }

    public enum FlatTopHexagonDirection
    {
        North = HexagonDirection.One,
        Northeast = HexagonDirection.Two,
        Southeast = HexagonDirection.Three,
        South = HexagonDirection.Four,
        Southwest = HexagonDirection.Five,
        Northwest = HexagonDirection.Six
    }

    public enum PointyTopHexagonDirection
    {
        Northeast = HexagonDirection.One,
        East = HexagonDirection.Two,
        Southeast = HexagonDirection.Three,
        Southwest = HexagonDirection.Four,
        West = HexagonDirection.Five,
        Northwest = HexagonDirection.Six
    }
}
