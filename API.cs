using MasterComputations.Data;
using Newtonsoft.Json;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
                List<Currency> retval = new List<Currency>();
                foreach (var x in res)
                {
                    Currency curr = new Currency();
                    curr.coin_type = x.coin_type;
                    curr.currency = x.currency;
                    curr.currency_long = x.currency_long;
                    curr.fee_precision = x.fee_precision;
                    curr.min_confirmations = x.min_confirmations;
                    curr.min_withdrawal_fee = x.min_withdrawal_fee;
                    curr.withdrawal_fee = x.withdrawal_fee;
                    retval.Add(curr);
                }
                return retval;
            }
        }
    }
}
