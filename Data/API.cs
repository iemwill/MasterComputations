using MasterComputations.Data;
using Newtonsoft.Json;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MasterComputations
{
    public class API
    {
        public class Deribit
        {
            public static List<Currency> getCurrencies()
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                var apiInstance = new PublicApi(Configuration.Default);
                Object result = apiInstance.PublicGetCurrenciesGet();
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;
                return Parse.currencies(res);
            }
            public static List<Instrument> getInstruments(string symbol = "BTC", bool getFutures = false)
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                var apiInstance = new PublicApi(Configuration.Default);
                Object result = apiInstance.PublicGetInstrumentsGet(symbol);
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;
                return Parse.instruments(res, getFutures);
            }
            public static List<Tuple<long, double>> getHistVol(string symbol="BTC")
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                var apiInstance = new PublicApi(Configuration.Default);
                Object result = apiInstance.PublicGetHistoricalVolatilityGet(symbol);
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;
                return Parse.hisVol(res);
            }
            public static List<Tuple<long, double>> getTicker(string name)
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                var apiInstance = new PublicApi(Configuration.Default);
                Object result = apiInstance.PublicTickerGet(name);
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;
                return Parse.ticker(res);
            }
            public static List<Instrument> getChartData(string name, int start, int end, string intervall)
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                var apiInstance = new MarketDataApi(Configuration.Default);
                // Change the user name for a subaccount
                Object result = apiInstance.PublicGetTradingviewChartDataGet(name, start, end, intervall);
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;
                return Parse.chartData(res);
            }
        }
    }
}

