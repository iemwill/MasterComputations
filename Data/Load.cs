using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MasterComputations.Data
{
    public class Load
    {
        public static List<Currency> currencies()
        {

            var path = Application.StartupPath + "\\data\\currencies.will";
            List<Currency> currencies = new List<Currency>();
            using (var fs = File.OpenRead(path))
                currencies = Serializer.Deserialize<List<Currency>>(fs);

            return currencies;
        }
        public static List<Instrument> activeOptionsBTC()
        {

            var path = Application.StartupPath + "\\data\\activeOptionsBTC.will";
            List<Instrument> options = new List<Instrument>();
            using (var fs = File.OpenRead(path))
                options = Serializer.Deserialize<List<Instrument>>(fs);

            return options;
        }
        public static List<Instrument> inactiveOptionsBTC()
        {

            var path = Application.StartupPath + "\\data\\inactiveOptionsBTC.will";
            List<Instrument> options = new List<Instrument>();
            using (var fs = File.OpenRead(path))
                options = Serializer.Deserialize<List<Instrument>>(fs);

            return options;
        }
        public static Dictionary<string, List<Book>> book()
        {
            var path = Application.StartupPath + "\\data\\orderBook.will";
            Dictionary<string, List<Book>> orders = new Dictionary<string, List<Book>>();
            using (var fs = File.OpenRead(path))
                orders = Serializer.Deserialize<Dictionary<string, List<Book>>>(fs);

            return orders;
        }
        public static Dictionary<string, ChartData> chartData()
        {

            var path = Application.StartupPath + "\\data\\chartData.will";
            Dictionary<string, ChartData> orders = new Dictionary<string, ChartData>();
            using (var fs = File.OpenRead(path))
                orders = Serializer.Deserialize<Dictionary<string, ChartData>>(fs);

            return orders;
        }
    }
}
