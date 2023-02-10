using UnityEditor;

namespace Osryden.HexagonFramework.Editor
{
    [CustomPropertyDrawer(typeof(FractionalHexagonCoordinates))]
    public class FractionalHexagonCoordinatesDrawer : HexagonCoordinatesDrawerBase<float> { }
}
