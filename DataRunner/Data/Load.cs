using MasterComputations.Classes;
using MasterComputations.Computations;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;

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
                if (add.trades.Count == 1000)
                {
                        var period = x.expiration_timestamp - x.creation_timestamp;
                        var intervalDays = Convert.ToInt32(period / (60 * 60 * 24 * 1000));
                        List<Option> moreData = new List<Option>();
                        var newDataCount = 0;
                        for (var i = 0; i < intervalDays; i++)
                        {
                            var moreDoption = new Option();
                            long start = x.creation_timestamp;
                            long end = x.creation_timestamp + i * (60 * 60 * 24 * 1000);//(x.raw.creation_timestamp / 1000) + (x.raw.expiration_timestamp / 1000 - x.raw.creation_timestamp / 1000) / 2;
                            moreDoption.trades = API.Deribit.getTradesByInstrumentWA(x.instrument_name, start, end, true, 1000);
                            moreDoption.start = Helper.unixToDateTime(x.creation_timestamp / 1000);
                            moreDoption.end = Helper.unixToDateTime(x.expiration_timestamp / 1000);
                            newDataCount += moreDoption.trades.Count;
                            moreData.Add(moreDoption);
                        }
                        var filteredTrades = new List<Trade>();
                        foreach (var yes in add.trades)
                            filteredTrades.Add(yes);
                        foreach (var opt in moreData)
                            foreach (var tr in opt.trades)
                                if (!filteredTrades.Contains(tr))
                                    filteredTrades.Add(tr);
                        add.trades = filteredTrades;
                }
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
                    if (add.trades.Count == 1000)
                    {
                        var period = x.expiration_timestamp - x.creation_timestamp;
                        var intervalDays = Convert.ToInt32(period / (60 * 60 * 24 * 1000));
                        List<Option> moreData = new List<Option>();
                        var newDataCount = 0;
                        for (var i = 0; i < intervalDays; i++)
                        {
                            var moreDoption = new Option();
                            long start = x.creation_timestamp;
                            long end = x.creation_timestamp + i * (60 * 60 * 24 * 1000);//(x.raw.creation_timestamp / 1000) + (x.raw.expiration_timestamp / 1000 - x.raw.creation_timestamp / 1000) / 2;
                            moreDoption.trades = API.Deribit.getTradesByInstrumentWA(x.instrument_name, start, end, true, 1000);
                            moreDoption.start = Helper.unixToDateTime(x.creation_timestamp / 1000);
                            moreDoption.end = Helper.unixToDateTime(x.expiration_timestamp / 1000);
                            newDataCount += moreDoption.trades.Count;
                            moreData.Add(moreDoption);
                        }
                        var filteredTrades = new List<Trade>();
                        foreach (var yes in add.trades)
                            filteredTrades.Add(yes);
                        foreach (var opt in moreData)
                            foreach (var tr in opt.trades)
                                if (!filteredTrades.Contains(tr))
                                    filteredTrades.Add(tr);
                        add.trades = filteredTrades;
                    }
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
            var path = "~/Documents/data/bitcoin.options";
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
            var path = "~Documents/Documents/data/currencies.deribit";
            List<Currency> currencies = new List<Currency>();
            using (var fs = File.OpenRead(path))
                currencies = Serializer.Deserialize<List<Currency>>(fs);
            return currencies;
        }
    }
}
