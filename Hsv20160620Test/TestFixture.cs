using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Hsv20160620Test
{
    [TestFixture]
    public class TestFixture1
    {
        [TestCase(0, Result = 0)]
        public int MethodTest(int value)
        {
            return (value - value);
        }

        [TestCase(0)]
        public void ThrowExceptionTest(int value)
        {
            Assert.That("引数が不正です。" == Assert.Throws<ArgumentException>(() => { throw new ArgumentException("引数が不正です。"); }).Message);
        }

        [Test]
        public void TestTrue()
        {
            Assert.IsTrue(true);
        }

        //// This test fail for example, replace result or delete this test to see all tests pass
        //[Test]
        //public void TestFault()
        //{
        //    Assert.IsTrue(false);
        //}
    }
}
