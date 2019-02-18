using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EDStatusReader.FileIO;

namespace EDStatusReader.Test
{
    [TestClass]
    public class JournalFileName_Test
    {
        public JournalFileName_Test()
        {
            
        }
        [TestMethod]
        public void Rejects_incorrect_prefix()
        {
            Assert.IsNull(JournalFileName.Parse("Zournal.190217195123.01.log"));
        }

        [TestMethod]
        public void Rejects_incorrect_suffix()
        {
            Assert.IsNull(JournalFileName.Parse("Journal.190217195123.01.pog"));
        }

        [TestMethod]
        public void Parses_date()
        {
            var sut = JournalFileName.Parse("Journal.190217195123.01.log");

            Assert.AreEqual(sut.StartDate, new DateTime(2019, 02, 17, 19, 51, 23));
        }

        [TestMethod]
        public void Parses_filenumber()
        {
            var sut = JournalFileName.Parse("Journal.190217195123.01.log");

            Assert.AreEqual(sut.FileNumber, 1);
        }

    }
}
