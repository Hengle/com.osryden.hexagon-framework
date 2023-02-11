using NUnit.Framework;
using System;

namespace Osryden.HexagonFramework.Tests
{
    [TestFixture]
    public class FlatTopHexagonDirectionTests
    {
        [TestCase(FlatTopHexagonDirection.North, HexagonDirection.One)]
        [TestCase(FlatTopHexagonDirection.Northeast, HexagonDirection.Two)]
        [TestCase(FlatTopHexagonDirection.Southeast, HexagonDirection.Three)]
        [TestCase(FlatTopHexagonDirection.South, HexagonDirection.Four)]
        [TestCase(FlatTopHexagonDirection.Southwest, HexagonDirection.Five)]
        [TestCase(FlatTopHexagonDirection.Northwest, HexagonDirection.Six)]
        public void TestFlatTopHexagonDirectionValues(FlatTopHexagonDirection flatTopDirection, HexagonDirection direction)
        {
            Assert.AreEqual((int)flatTopDirection, (int)direction);
        }

        [Test]
        public void TestNumberOfMembers()
        {
            int expectedNumberOfMembers = HexagonUtility.Sides;
            int actualNumberOfMembers = Enum.GetNames(typeof(FlatTopHexagonDirection)).Length;
            Assert.AreEqual(expectedNumberOfMembers, actualNumberOfMembers);
        }
    }
}
