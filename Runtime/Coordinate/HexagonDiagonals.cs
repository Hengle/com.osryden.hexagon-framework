namespace Osryden.HexagonFramework
{
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
