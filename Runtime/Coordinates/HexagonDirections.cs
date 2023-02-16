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

    /// <summary>
    /// Represents the adjacents directions of a flat-top hexagon.
    /// </summary>
    public enum FlatTopHexagonDirection
    {
        /// <summary>
        /// Represents the north adjacent direction of a flat-top hexagon.
        /// </summary>
        North = HexagonDirection.One,

        /// <summary>
        /// Represents the northeast adjacent direction of a flat-top hexagon.
        /// </summary>
        Northeast = HexagonDirection.Two,

        /// <summary>
        /// Represents the southeast adjacent direction of a flat-top hexagon.
        /// </summary>
        Southeast = HexagonDirection.Three,

        /// <summary>
        /// Represents the south adjacent direction of a flat-top hexagon.
        /// </summary>
        South = HexagonDirection.Four,

        /// <summary>
        /// Represents the southwest adjacent direction of a flat-top hexagon.
        /// </summary>
        Southwest = HexagonDirection.Five,

        /// <summary>
        /// Represents the northwest adjacent direction of a flat-top hexagon.
        /// </summary>
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
