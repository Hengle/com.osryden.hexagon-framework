using UnityEditor;

namespace Osryden.HexagonFramework.Editor
{
    [CustomPropertyDrawer(typeof(HexagonCoordinates))]
    public class HexagonCoordinatesDrawer : HexagonCoordinatesDrawerBase<int> { }
}
