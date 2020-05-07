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
                // Change the user name for a subaccount
                Object result = apiInstance.PublicGetCurrenciesGet();
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;

                return Parse.currencies(res);
            }
            public static List<Instrument> getInstruments(string symbol="BTC", bool getFutures=false)
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                var apiInstance = new PublicApi(Configuration.Default);
                // Change the user name for a subaccount
                Object result = apiInstance.PublicGetInstrumentsGet(symbol);
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;
                return Parse.instruments(res, getFutures);
            }
        }
    }
}

