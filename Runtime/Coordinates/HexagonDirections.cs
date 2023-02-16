namespace Osryden.HexagonFramework
{
    /// <summary>
    /// Represents the adjacents directions of a hexagon.
    /// </summary>
    public enum HexagonDirection
    {
        /// <summary>
        /// Represents the first adjacent direction.
        /// </summary>
        One,

        /// <summary>
        /// Represents the second adjacent direction.
        /// </summary>
        Two,

        /// <summary>
        /// Represents the third adjacent direction.
        /// </summary>
        Three,

        /// <summary>
        /// Represents the fourth adjacent direction.
        /// </summary>
        Four,

        /// <summary>
        /// Represents the fifth adjacent direction.
        /// </summary>
        Five,

        /// <summary>
        /// Represents the sixth adjacent direction.
        /// </summary>
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
