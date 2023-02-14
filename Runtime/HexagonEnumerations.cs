namespace Osryden.HexagonFramework
{
    public enum HexagonCoordinateAxis
    {
        Q,
        R,
        S
    }

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

    public enum HexagonDiagonal
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }

    public enum FlatTopHexagonDiagonal
    {
        Northeast = HexagonDiagonal.One,
        East = HexagonDiagonal.Two,
        Southeast = HexagonDiagonal.Three,
        Southwest = HexagonDiagonal.Four,
        West = HexagonDiagonal.Five,
        Northwest = HexagonDiagonal.Six
    }

    public enum PointyTopHexagonDiagonal
    {
        North = HexagonDiagonal.One,
        Northeast = HexagonDiagonal.Two,
        Southeast = HexagonDiagonal.Three,
        South = HexagonDiagonal.Four,
        Southwest = HexagonDiagonal.Five,
        Northwest = HexagonDiagonal.Six
    }
}
