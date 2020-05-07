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
    }
}
