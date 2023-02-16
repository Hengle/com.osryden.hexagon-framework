namespace Osryden.HexagonFramework
{
    /// <summary>
    /// Represents the diagonals of a hexagon.
    /// </summary>
    public enum HexagonDiagonal
    {
        /// <summary>
        /// Represents the first diagonal.
        /// </summary>
        One,

        /// <summary>
        /// Represents the second diagonal.
        /// </summary>
        Two,

        /// <summary>
        /// Represents the third diagonal.
        /// </summary>
        Three,

        /// <summary>
        /// Represents the fourth diagonal.
        /// </summary>
        Four,

        /// <summary>
        /// Represents the fifth diagonal.
        /// </summary>
        Five,

        /// <summary>
        /// Represents the sixth diagonal.
        /// </summary>
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
