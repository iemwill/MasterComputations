using MasterComputations.Classes;
using MasterComputations.Computations;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MasterComputations.Data
{
    public class Load
    {
        public static Tuple<Dictionary<string, Option>, List<Option>, List<Option>, List<Currency>> onlinePublicAPI(bool justActive=false)
        {
            var btcOptions = new Dictionary<string, Option>();
            //Get supported currencies and options for BTC
            var currencies = API.Deribit.getCurrencies();
            var activeRaw = API.Deribit.getInstrumentsWA("BTC", "option", false);

            foreach (var x in activeRaw)
            {
                var add = new Option();
                add.raw = x;
                add.trades = API.Deribit.getTradesByInstrumentWA(x.instrument_name, x.creation_timestamp, x.expiration_timestamp, true, 1000);
                add.start = Helper.unixToDateTime(x.creation_timestamp / 1000);
                add.end = Helper.unixToDateTime(x.expiration_timestamp / 1000);
                add.active = x.is_active;
                add.name = x.instrument_name;
                btcOptions.Add(add.name, add);
            }
            if (!justActive)
            {
                var inactiveRaw = API.Deribit.getInstrumentsWA("BTC", "option", true);
                foreach (var x in inactiveRaw)
                {
                    var add = new Option();
                    add.raw = x;
                    add.trades = API.Deribit.getTradesByInstrumentWA(x.instrument_name, x.creation_timestamp, x.expiration_timestamp, true, 1000);
                    add.start = Helper.unixToDateTime(x.creation_timestamp / 1000);
                    add.end = Helper.unixToDateTime(x.expiration_timestamp / 1000);
                    add.active = x.is_active;
                    add.name = x.instrument_name;
                    btcOptions.Add(add.name, add);
                }
            }
            Save.currencies(currencies);
            Save.options(btcOptions);
            var inactive = new List<Option>(); var active = new List<Option>();
            foreach (var x in btcOptions.Values)
                if (x.active)
                    active.Add(x);
                else
                    inactive.Add(x);
            return new Tuple<Dictionary<string, Option>, List<Option>, List<Option>, List<Currency>>(btcOptions, active, inactive, currencies);
        }
        public static Tuple<Dictionary<string, Option>, List<Option>, List<Option>, List<Currency>> localPublicAPI()
        {
            var path = Application.StartupPath + "\\data\\bitcoin.options";
            Dictionary<string, Option> all = new Dictionary<string, Option>();
            using (var fs = File.OpenRead(path))
                all = Serializer.Deserialize<Dictionary<string, Option>>(fs);
            var inactive = new List<Option>();
            var active = new List<Option>();
            var curr = currencies();
            foreach (var x in all.Values)
                if (x.active)
                    active.Add(x);
                else
                    inactive.Add(x);
            return new Tuple<Dictionary<string, Option>, List<Option>, List<Option>, List<Currency>>(all, active, inactive, curr);
        }
        private static List<Currency> currencies()
        {
            var path = Application.StartupPath + "\\data\\currencies.deribit";
            List<Currency> currencies = new List<Currency>();
            using (var fs = File.OpenRead(path))
                currencies = Serializer.Deserialize<List<Currency>>(fs);
            return currencies;
        }


        //public static List<Instrument> activeOptionsBTC()
        //{

        //    var path = Application.StartupPath + "\\data\\activeOptionsBTC.will";
        //    List<Instrument> options = new List<Instrument>();
        //    using (var fs = File.OpenRead(path))
        //        options = Serializer.Deserialize<List<Instrument>>(fs);

        //    return options;
        //}
        //public static List<Instrument> inactiveOptionsBTC()
        //{

        //    var path = Application.StartupPath + "\\data\\inactiveOptionsBTC.will";
        //    List<Instrument> options = new List<Instrument>();
        //    using (var fs = File.OpenRead(path))
        //        options = Serializer.Deserialize<List<Instrument>>(fs);

        //    return options;
        //}
        //public static Dictionary<string, List<Book>> book()
        //{
        //    var path = Application.StartupPath + "\\data\\orderBook.will";
        //    Dictionary<string, List<Book>> orders = new Dictionary<string, List<Book>>();
        //    using (var fs = File.OpenRead(path))
        //        orders = Serializer.Deserialize<Dictionary<string, List<Book>>>(fs);

        //    return orders;
        //}
        //public static Dictionary<string, ChartData> chartData()
        //{
        //    var path = Application.StartupPath + "\\data\\chartData.will";
        //    Dictionary<string, ChartData> orders = new Dictionary<string, ChartData>();
        //    using (var fs = File.OpenRead(path))
        //        orders = Serializer.Deserialize<Dictionary<string, ChartData>>(fs);
        //    return orders;
        //}
    }
}
