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

    /// <summary>
    /// Represents the diagonals of a flat-top hexagon.
    /// </summary>
    public enum FlatTopHexagonDiagonal
    {
        /// <summary>
        /// Represents the northeast diagonal of a flat-top hexagon.
        /// </summary>
        Northeast = HexagonDiagonal.One,

        /// <summary>
        /// Represents the east diagonal of a flat-top hexagon.
        /// </summary>
        East = HexagonDiagonal.Two,

        /// <summary>
        /// Represents the southeast diagonal of a flat-top hexagon.
        /// </summary>
        Southeast = HexagonDiagonal.Three,

        /// <summary>
        /// Represents the southwest diagonal of a flat-top hexagon.
        /// </summary>
        Southwest = HexagonDiagonal.Four,

        /// <summary>
        /// Represents the west diagonal of a flat-top hexagon.
        /// </summary>
        West = HexagonDiagonal.Five,

        /// <summary>
        /// Represents the northwest diagonal of a flat-top hexagon.
        /// </summary>
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
