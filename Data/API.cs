using MasterComputations.Data;
using Newtonsoft.Json;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

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
            public static List<Instrument> getInstruments(string symbol = "BTC", string kind = "option")
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                var apiInstance = new PublicApi(Configuration.Default);
                Object result = apiInstance.PublicGetInstrumentsGet(symbol);
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;
                return Parse.instruments(res, kind);
            }
            public static List<Tuple<long, double>> getHistVol(string symbol = "BTC")
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                var apiInstance = new PublicApi(Configuration.Default);
                Object result = apiInstance.PublicGetHistoricalVolatilityGet(symbol);
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;
                return Parse.hisVol(res);
            }
            public static Book getTicker(string name)
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                var apiInstance = new PublicApi(Configuration.Default);
                Object result = apiInstance.PublicTickerGet(name);
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;
                return Parse.ticker(res);
            }
            public static Book getBook(string name)
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                var apiInstance = new PublicApi(Configuration.Default);
                Object result = apiInstance.PublicGetOrderBookGet(name);
                dynamic stuff = JsonConvert.DeserializeObject(result.ToString());
                var res = stuff.result;
                return Parse.book(res);
            }
            //WithouAPI
            public static List<Instrument> getInstrumentsWA(string symbol = "BTC", string kind = "option", bool expired = false)
            {
                var encoding = Encoding.UTF8;
                var urlForAuth = @"https://www.deribit.com/api/v2/public/get_instruments"
                + "?currency=" + symbol + "&kind=" + kind + "&expired=" + expired.ToString().ToLower();
                var request = (HttpWebRequest)WebRequest.Create(urlForAuth);
                request.ContentType = "application/json";
                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                string read = "";
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.Default))
                    read = reader.ReadToEnd();
                dynamic stuff = JsonConvert.DeserializeObject(read);
                var res = stuff.result;
                return Parse.instruments(res, kind);
            }
            public static ChartData getChartDataWA(string name, long start, long end, string intervall)
            {
                var encoding = Encoding.UTF8;
                var urlForAuth = @"https://www.deribit.com/api/v2/public/get_tradingview_chart_data"
                + "?instrument_name=" + name + "&start_timestamp=" + start.ToString() + "&end_timestamp=" + end.ToString() + "&resolution=" + intervall;
                var request = (HttpWebRequest)WebRequest.Create(urlForAuth);
                request.ContentType = "application/json";
                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                string read = "";
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.Default))
                    read = reader.ReadToEnd();
                dynamic stuff = JsonConvert.DeserializeObject(read);
                var res = stuff.result;
                return Parse.chartData(res);
            }
            public static List<Trade> getTradesByInstrumentWA(string name, Int64 start, Int64 end, bool includeOld, int count)
            {
                var encoding = Encoding.UTF8;
                var urlForAuth = @"https://www.deribit.com/api/v2/public/get_last_trades_by_instrument_and_time"
                + "?instrument_name=" + name + "&end_timestamp=" + end.ToString() + "&count=" + count +
                "&include_old=" + includeOld.ToString().ToLower() + "&sorting=asc" + "&start_timestamp=" + start.ToString();
                var request = (HttpWebRequest)WebRequest.Create(urlForAuth);
                request.ContentType = "application/json";
                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                string read = "";
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.Default))
                    read = reader.ReadToEnd();
                dynamic stuff = JsonConvert.DeserializeObject(read);
                var res = stuff.result;
                return Parse.tradesByInst(res);
            }
            //TO-USE
            public static Trade getTradesByCurrencyWA(string symbol, long start, long end, bool includeOld, int count)
            {
                var encoding = Encoding.UTF8;
                var urlForAuth = @"https://www.deribit.com/api/v2/public/get_last_trades_by_currency_and_time"
                + "?currency=" + symbol + "&kind=option" + "&end_timestamp=" + end.ToString() + "&count=" + count +
                "&include_old=" + includeOld.ToString().ToLower() + "&sorting=asc" + "&start_timestamp=" + start.ToString();
                var request = (HttpWebRequest)WebRequest.Create(urlForAuth);
                request.ContentType = "application/json";
                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                string read = "";
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.Default))
                    read = reader.ReadToEnd();
                dynamic stuff = JsonConvert.DeserializeObject(read);
                var res = stuff.result;
                return Parse.tradesByInst(res);
            }
        }
    }
}

