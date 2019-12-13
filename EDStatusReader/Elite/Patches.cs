using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDStatusReader.Elite
{
    public static class Patches
    {
        private static Dictionary<string, string> ShipTypes = new Dictionary<string, string>
        {
            ["adder"] = "Adder",
            ["asp_scout"] = "Asp Scout",
            ["asp"] = "Asp Explorer",
            ["diamondback"] = "Diamondback Sc",
            ["diamondbackxl"] = "Diamondback Ex",
            ["sidewinder"] = "Sidewinder",
            ["federation_corvette"] = "Corvette",
            ["cobramkiii"] = "Cobra III",
            ["cobramkiv"] = "Cobra IV",
            ["viper"] = "Viper III",
            ["viper_mkiv"] = "Viper IV",
            ["dolphin"] = "Dolphin",
            ["ferdelance"] = "FDL",
            ["eagle"] = "Eagle",
            ["vulture"] = "Vulture",
            ["krait_light"] = "Krait Phantom",
            ["independant_trader"] = "Keelback",
            ["python"] = "Python",
            ["anaconda"] = "Anaconda",
            ["type6"] = "Type-6",
            ["type9"] = "Type-9 Heavy",
            ["empire_eagle"] = "Imp Eagle",
            ["federation_gunship"] = "Fed Gunship",
            ["empire_courier"] = "Imp Courier",
            ["federation_fighter"] = "SLF Condor",
            ["empire_trader"] = "Imp Clipper",
            ["federation_dropship_mkii"] = "Fed Assault",
            ["independent_fighter"] = "SLF Taipan",
            ["empire_fighter"] = "SLF Gu-97",
            ["krait_mkii"] = "Krait",
            ["federation_dropship"] = "Fed Dropship",
            ["cutter"] = "Imp Cutter",
            ["type7"] = "Type-7",
            ["typex_2"] = "Crusader",
            ["typex_3"] = "Challenger",
        };

        public static string ShipName(string shipcode)
        {
            return ShipTypes.TryGetValue(shipcode.ToLowerInvariant(), out string name) ? name : null;
        }

    }
}
