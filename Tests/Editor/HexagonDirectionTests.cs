using NUnit.Framework;
using System;

namespace Osryden.HexagonFramework.Tests
{
    [TestFixture]
    public class HexagonDirectionTests
    {
        [TestCase(HexagonDirection.One, 0)]
        [TestCase(HexagonDirection.Two, 1)]
        [TestCase(HexagonDirection.Three, 2)]
        [TestCase(HexagonDirection.Four, 3)]
        [TestCase(HexagonDirection.Five, 4)]
        [TestCase(HexagonDirection.Six, 5)]
        public void TestHexagonDirectionValues(HexagonDirection direction, int value)
        {
            Assert.AreEqual((int)direction, value);
        }

        [Test]
        public void TestNumberOfMembers()
        {
            int expectedNumberOfMembers = 6;
            int actualNumberOfMembers = Enum.GetNames(typeof(HexagonDirection)).Length;
            Assert.AreEqual(expectedNumberOfMembers, actualNumberOfMembers);
        }
    }
}
