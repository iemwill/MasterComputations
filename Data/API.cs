using MasterComputations.Data;
using Newtonsoft.Json;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using System;
using System.Collections.Generic;

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
            //TODO
            public static List<Instrument> getChartData()
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                var apiInstance = new PublicApi(Configuration.Default);
                // Change the user name for a subaccount
                Object result = apiInstance.PublicGetTradingviewChartDataGet("BTC-25DEC20-5000-C", 1526641200, 1535540400, "1D");
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;
                return res;
            }
        }
    }
}

