using NUnit.Framework;
using System;

namespace Osryden.HexagonFramework.Tests
{
    [TestFixture]
    public class HexagonDirectionTests
    {
        [Test]
        public void TestNumberOfMembers()
        {
            int expectedNumberOfMembers = 6;
            int actualNumberOfMembers = Enum.GetNames(typeof(HexagonDirection)).Length;
            Assert.AreEqual(expectedNumberOfMembers, actualNumberOfMembers);
        }
    }
}
