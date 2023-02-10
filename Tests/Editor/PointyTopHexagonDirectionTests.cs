using NUnit.Framework;

namespace Osryden.HexagonFramework.Tests
{
    [TestFixture]
    public class PointyTopHexagonDirectionTests
    {
        [TestCase(PointyTopHexagonDirection.Northeast, HexagonDirection.One)]
        [TestCase(PointyTopHexagonDirection.East, HexagonDirection.Two)]
        [TestCase(PointyTopHexagonDirection.Southeast, HexagonDirection.Three)]
        [TestCase(PointyTopHexagonDirection.Southwest, HexagonDirection.Four)]
        [TestCase(PointyTopHexagonDirection.West, HexagonDirection.Five)]
        [TestCase(PointyTopHexagonDirection.Northwest, HexagonDirection.Six)]
        public void TestFlatTopHexagonDirectionValues(PointyTopHexagonDirection pointyTopDirection, HexagonDirection direction)
        {
            Assert.AreEqual((int)pointyTopDirection, (int)direction);
        }
    }
}
