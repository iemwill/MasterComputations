using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MasterComputations.Data
{
    public class Parse
    {
        public static DataTable csv(string path, char seperator)
        {
            DataTable dt = new DataTable();

            using (var fs = File.OpenRead(path))
            {
                using (var reader = new StreamReader(fs))
                {
                    string readHeader = reader.ReadLine();
                    if (readHeader != null)
                    {
                        string[] header = readHeader.Split(seperator);
                        int nSignals = header.GetLength(0);

                        for (int j = 0; j < nSignals; j++)
                            dt.Columns.Add(header[j], typeof(string));

                        while (!reader.EndOfStream)
                        {
                            string sRead = reader.ReadLine();
                            string[] splitArray = sRead.Split(seperator);
                            var row = new object[nSignals];

                            for (int j = 0; j < nSignals; j++)
                                row[j] = splitArray[j];

                            dt.Rows.Add(row);
                        }
                    }
                }
            }
            return dt;
        }
        public static List<Currency> currencies(dynamic input)
        {
            List<Currency> retval = new List<Currency>();
            foreach (var x in input)
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
        public static List<Instrument> instruments(dynamic input, bool getFutures)
        {
            List<Instrument> options = new List<Instrument>();
            List<Instrument> futures = new List<Instrument>();
            foreach (var x in input)
            {
                Instrument instr = new Instrument();
                if (x.kind == "option")
                {
                    instr.tick_size = x.tick_size;
                    instr.taker_commission = x.taker_commission;
                    instr.strike = x.strike;
                    instr.quote_currency = x.quote_currency;
                    instr.option_type = x.option_type;
                    instr.min_trade_amount = x.min_trade_amount;
                    instr.maker_commission = x.maker_commission;
                    instr.kind = x.kind;
                    instr.is_active = x.is_active;
                    instr.instrument_name = x.instrument_name;
                    instr.expiration_timestamp = x.expiration_timestamp;
                    instr.creation_timestamp = x.creation_timestamp;
                    instr.contract_size = x.contract_size;
                    instr.settlement_period = x.settlement_period;
                    instr.base_currency = x.base_currency;
                    options.Add(instr);
                }
                else if (x.kind == "future")
                {
                    instr.tick_size = x.tick_size;
                    instr.taker_commission = x.taker_commission;
                    instr.quote_currency = x.quote_currency;
                    instr.min_trade_amount = x.min_trade_amount;
                    instr.maker_commission = x.maker_commission;
                    instr.kind = x.kind;
                    instr.is_active = x.is_active;
                    instr.instrument_name = x.instrument_name;
                    instr.expiration_timestamp = x.expiration_timestamp;
                    instr.creation_timestamp = x.creation_timestamp;
                    instr.contract_size = x.contract_size;
                    instr.base_currency = x.base_currency;
                    instr.max_liquidation_commission = x.max_liquidation_commission;
                    instr.max_leverage = x.max_leverage;
                    futures.Add(instr);
                }
            }
            if (!getFutures)
                return options;
            else
                return futures;
        }
        public static List<Tuple<long, double>> hisVol(dynamic input)
        {
            List<Tuple<long, double>> retval = new List<Tuple<long, double>>();
            foreach (var x in input)
            {
                retval.Add(new Tuple<long, double>(x[0].Value, x[1].Value));
            }
            return retval;
        }
        public static List<Tuple<long, double>> ticker(dynamic input)
        {
            List<Tuple<long, double>> retval = new List<Tuple<long, double>>();
            {
                {
                    {
                        //"underlying_price": 9936.78,
                        //  "underlying_index": "SYN.BTC-8MAY20",
                        //  "timestamp": 1588897318040,
                        //  "stats": {
                        //    "volume": null,
                        //    "price_change": null,
                        //    "low": null,
                        //    "high": null
                        //  },
                        //  "state": "open",
                        //  "settlement_price": 0.18,
                        //  "open_interest": 0.0,
                        //  "min_price": 0.075,
                        //  "max_price": 0.1465,
                        //  "mark_price": 0.1075835,
                        //  "mark_iv": 179.23,
                        //  "last_price": null,
                        //  "interest_rate": 0.0,
                        //  "instrument_name": "BTC-8MAY20-11000-P",
                        //  "index_price": 9936.57,
                        //  "greeks": {
                        //    "vega": 0.19434,
                        //    "theta": -17.4153,
                        //    "rho": -0.09339,
                        //    "gamma": 0.00013,
                        //    "delta": -0.97095
                        //  },
                        //  "estimated_delivery_price": 9936.57,
                        //  "bid_iv": 0.0,
                        //  "best_bid_price": 0.013,
                        //  "best_bid_amount": 0.5,
                        //  "best_ask_price": 0.6665,
                        //  "best_ask_amount": 0.1,
                        //  "ask_iv": 500.0
                    }
                }
                return new List<Tuple<long, double>>();
            }
        }
    }
}
