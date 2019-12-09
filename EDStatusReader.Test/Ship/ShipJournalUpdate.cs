using System;
using EDStatusReader.Elite;
using EDStatusReader.Elite.Journal;
using EDStatusReader.Ship;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace EDStatusReader.Test.Ship
{
    [TestClass]
    public class ShipJournalUpdate
    {
        private JournalParser journalParser = new JournalParser();
        private ShipStatus ship;

        public ShipJournalUpdate()
        {
            Init();
        }

        private void Init()
        {
            ship = new ShipStatus();
        }

        private void Apply(string s)
        {
            var header = JsonConvert.DeserializeObject<JournalHeader>(s);
            header._InputLine = s;

            journalParser.Parse(header, ship);
        }


        [TestMethod]
        public void DockingRequested()
        {
            Init();
            ship.DockingRequested = false;
            ship.RequestedDockingStation = string.Empty;
            ship.DockingGranted = false;
            ship.DockingDeniedReason = "a";
            ship.LandingPad = -1;

            Apply(@"{ 'timestamp':'2019 - 04 - 19T23: 52:19Z', 'event':'DockingRequested', 'MarketID':3230550272, 'StationName':'Boltzmann Gateway', 'StationType':'Coriolis' }");

            Assert.IsTrue(ship.DockingRequested);
            Assert.AreEqual("Boltzmann Gateway",ship.RequestedDockingStation);
            Assert.IsNull(ship.DockingGranted);
            Assert.AreEqual(string.Empty, ship.DockingDeniedReason);
            Assert.AreEqual(ship.LandingPad, 0);
        }
    }
}
