using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EDStatusReader.Output;
using System.Linq;
using System.Text;

namespace EDStatusReader.Test.Output
{
    [TestClass]
    public class LED7SegCommandTest
    {
        [TestMethod]
        public void led7_0()
        {
            AssertArray(new byte[] { 0, 10, 10, 10 }, LED7SegCommand.IntTo7Seg4(0));
        }

        [TestMethod]
        public void led7_1()
        {
            AssertArray(new byte[] { 1, 10, 10, 10 }, LED7SegCommand.IntTo7Seg4(1));
        }
        [TestMethod]
        public void led7_10()
        {
            AssertArray(new byte[] { 0, 1, 10, 10 }, LED7SegCommand.IntTo7Seg4(10));
        }
        [TestMethod]
        public void led7_100()
        {
            AssertArray(new byte[] { 0, 0, 1, 10 }, LED7SegCommand.IntTo7Seg4(100));
        }
        [TestMethod]
        public void led7_1000()
        {
            AssertArray(new byte[] { 0, 0, 0, 1 + 11 }, LED7SegCommand.IntTo7Seg4(1000));
        }
        [TestMethod]
        public void led7_1001()
        {
            AssertArray(new byte[] { 1, 0, 0, 1 + 11 }, LED7SegCommand.IntTo7Seg4(1001));
        }
        [TestMethod]
        public void led7_1010()
        {
            AssertArray(new byte[] { 0, 1, 0, 1 + 11 }, LED7SegCommand.IntTo7Seg4(1010));
        }
        [TestMethod]
        public void led7_9999()
        {
            AssertArray(new byte[] { 9, 9, 9, 9 + 11 }, LED7SegCommand.IntTo7Seg4(9999));
        }
        [TestMethod]
        public void led7_10000()
        {
            AssertArray(new byte[] { 0, 0, 0 + 11, 1 }, LED7SegCommand.IntTo7Seg4(10000));
        }
        [TestMethod]
        public void led7_100000()
        {
            AssertArray(new byte[] { 0, 0 + 11, 0 , 1 }, LED7SegCommand.IntTo7Seg4(100000));
        }
        [TestMethod]
        public void led7_1000000()
        {
            AssertArray(new byte[] { 0 + 11, 0, 0, 1 }, LED7SegCommand.IntTo7Seg4(1000000));
        }
        [TestMethod]
        public void led7_999999()
        {
            AssertArray(new byte[] { 9, 9 + 11, 9, 9 }, LED7SegCommand.IntTo7Seg4(999999));
        }

        public void AssertArray<T>(T[] expected, T[] actual)
        {
            if (expected == null) throw new ArgumentNullException("expected");
            if (actual == null) throw new ArgumentNullException("actual");

            bool fail = false;
            if (expected.Length != actual.Length)
            {
                fail = true;
            }
            else
            {
                for (int i = 0; i < expected.Length; i++)
                {
                    if (!expected[i].Equals(actual[i]))
                    {
                        fail = true;
                        break;
                    }
                }
            }

            if (fail)
            {
                var sb = new StringBuilder();

                sb.AppendLine("Arrays not equal:");
                sb.Append("Expected: " + string.Join(",", expected.Select(a => a.ToString())));
                sb.AppendLine();
                sb.Append("  Actual: " + string.Join(",", actual.Select(a => a.ToString())));
                sb.AppendLine();

                throw new AssertFailedException(sb.ToString());
            }


        }
    }
}
