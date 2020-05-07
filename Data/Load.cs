using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;

namespace MasterComputations.Data
{
    public class Load
    {
        public static List<Currency> currencies()
        {

            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Desktop\\MC\\currencies.will";
            List<Currency> currencies = new List<Currency>();
            using (var fs = File.OpenRead(path))
                currencies = Serializer.Deserialize<List<Currency>>(fs);

            return currencies;
        }
        public static List<Instrument> optionsBTC()
        {

            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Desktop\\MC\\optionsBTC.will";
            List<Instrument> options = new List<Instrument>();
            using (var fs = File.OpenRead(path))
                options = Serializer.Deserialize<List<Instrument>>(fs);

            return options;
        }
    }
}
