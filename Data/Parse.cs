using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

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
        public static List<Instrument> instruments(dynamic input, string kind)
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
            if (kind == "option")
                return options;
            else
                return futures;
        }
        public static List<Tuple<long, double>> hisVol(dynamic input)
        {
            List<Tuple<long, double>> retval = new List<Tuple<long, double>>();
            foreach (var x in input)
                retval.Add(new Tuple<long, double>(x[0].Value, x[1].Value));
            return retval;
        }
        public static Book book(dynamic x)
        {
            try
            {
                Book bookToAdd = new Book();
                bookToAdd.change_id = x.change_id;
                bookToAdd.estimated_delivery_price = x.estimated_delivery_price;
                bookToAdd.greeks.vega = x.greeks.vega;
                bookToAdd.greeks.theta = x.greeks.theta;
                bookToAdd.greeks.rho = x.greeks.rho;
                bookToAdd.greeks.gamma = x.greeks.gamma;
                bookToAdd.greeks.delta = x.greeks.delta;
                bookToAdd.index_price = x.index_price;
                bookToAdd.instrument_name = x.instrument_name;
                bookToAdd.interest_rate = x.interest_rate;
                if (x.last_price != null)
                    bookToAdd.last_price = x.last_price;
                bookToAdd.mark_iv = x.mark_iv;
                bookToAdd.mark_price = x.mark_price;
                bookToAdd.max_price = x.max_price;
                bookToAdd.min_price = x.min_price;
                bookToAdd.open_interest = x.open_interest;
                if (x.settlement_price != null)
                    bookToAdd.settlement_price = x.settlement_price;
                bookToAdd.state = x.state;
                if (x.stats.high != null)
                    bookToAdd.stats.high = x.stats.high;
                if (x.stats.low != null)
                    bookToAdd.stats.low = x.stats.low;
                if (x.stats.price_change != null)
                    bookToAdd.stats.price_change = x.stats.price_change;
                if (x.stats.volume != null)
                    bookToAdd.stats.volume = x.stats.volume;
                bookToAdd.timestamp = x.timestamp;
                bookToAdd.underlying_index = x.underlying_index;
                bookToAdd.underlying_price = x.underlying_price;
                bookToAdd.best_ask_amount = x.best_ask_amount;
                bookToAdd.best_ask_price = x.best_ask_price;
                bookToAdd.best_bid_amount = x.best_bid_amount;
                bookToAdd.best_bid_price = x.best_bid_price;
                if(x.best_ask_iv!=null)
                    bookToAdd.best_ask_iv = x.best_ask_iv;
                if (x.best_bid_iv != null)
                    bookToAdd.best_bid_iv = x.best_bid_iv;
                return bookToAdd;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return new Book();
            }
        }
        public static Book ticker(dynamic x)
        {
            try
            {
                Book bookToAdd = new Book();
                bookToAdd.estimated_delivery_price = x.estimated_delivery_price;
                bookToAdd.greeks.vega = x.greeks.vega;
                bookToAdd.greeks.theta = x.greeks.theta;
                bookToAdd.greeks.rho = x.greeks.rho;
                bookToAdd.greeks.gamma = x.greeks.gamma;
                bookToAdd.greeks.delta = x.greeks.delta;
                bookToAdd.index_price = x.index_price;
                bookToAdd.instrument_name = x.instrument_name;
                bookToAdd.interest_rate = x.interest_rate;
                if (x.last_price != null)
                    bookToAdd.last_price = x.last_price;
                bookToAdd.mark_iv = x.mark_iv;
                bookToAdd.mark_price = x.mark_price;
                bookToAdd.max_price = x.max_price;
                bookToAdd.min_price = x.min_price;
                bookToAdd.open_interest = x.open_interest;
                if (x.settlement_price != null)
                    bookToAdd.settlement_price = x.settlement_price;
                bookToAdd.state = x.state;
                if (x.stats.high != null)
                    bookToAdd.stats.high = x.stats.high;
                if (x.stats.low != null)
                    bookToAdd.stats.low = x.stats.low;
                if (x.stats.price_change != null)
                    bookToAdd.stats.price_change = x.stats.price_change;
                if (x.stats.volume != null)
                    bookToAdd.stats.volume = x.stats.volume;
                bookToAdd.timestamp = x.timestamp;
                bookToAdd.underlying_index = x.underlying_index;
                bookToAdd.underlying_price = x.underlying_price;
                bookToAdd.best_ask_amount = x.best_ask_amount;
                bookToAdd.best_ask_price = x.best_ask_price;
                bookToAdd.best_bid_amount = x.best_bid_amount;
                bookToAdd.best_bid_price = x.best_bid_price;
                bookToAdd.best_ask_iv = x.best_ask_iv;
                bookToAdd.best_bid_iv = x.best_bid_iv;
                return bookToAdd;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return new Book();
            }
        }
        public static ChartData chartData(dynamic input)
        {
            ChartData toAdd = new ChartData();
            List<double> volumes = new List<double>();
            List<long> ticks = new List<long>();
            List<double> close = new List<double>();
            List<double> open = new List<double>();
            List<double> cost = new List<double>();
            List<double> high = new List<double>();
            List<double> low = new List<double>();
            if (input.status != "no_data")
            {
                var check = 0;
                foreach (var x in input)
                {
                    foreach (var y in x)
                    {
                        check += 1;
                    }
                    if (x.Name == "volume")
                    {
                        foreach (var y in x.Value)
                            volumes.Add(y.Value);
                    }
                    else if (x.Name == "cost")
                    {
                        foreach (var y in x.Value)
                            cost.Add(y.Value);
                    }
                    else if (x.Name == "low")
                    {
                        foreach (var y in x.Value)
                            low.Add(y.Value);
                    }
                    else if (x.Name == "high")
                    {
                        foreach (var y in x.Value)
                            high.Add(y.Value);
                    }
                    else if (x.Name == "close")
                    {
                        foreach (var y in x.Value)
                            close.Add(y.Value);
                    }
                    else if (x.Name == "open")
                    {
                        foreach (var y in x.Value)
                            open.Add(y.Value);
                    }
                    else if (x.Name == "ticks")
                    {
                        foreach (var y in x.Value)
                            ticks.Add(y.Value);
                    }
                }
                toAdd.high = high; toAdd.low = low; toAdd.open = open; toAdd.volume = volumes;
                toAdd.close = close; toAdd.cost = cost; toAdd.ticks = ticks;
            }
            else
                toAdd.status = "no_data";
            return toAdd;
        }
        public static List<Trade> tradesByInst(dynamic input)
        {
            var retval = new List<Trade>();

            foreach (var x in input.trades)
            {
                var toAdd = new Trade();
                toAdd.direction = x.direction;
                toAdd.implied_volatility = x.iv;
                toAdd.index_price = x.index_price;
                toAdd.instrument_name = x.instrument_name;
                try
                {
                    toAdd.liquidatation = x.liquidatation;
                }
                catch
                {
                    continue;
                }
                toAdd.amount = x.amount;
                toAdd.price = x.price;
                toAdd.tick_direction = x.tick_direction;
                toAdd.timestamp = x.timestamp;
                toAdd.trade_id = x.trade_id;
                toAdd.trade_seq = x.trade_seq;
                retval.Add(toAdd);
            }
            return retval;
        }
    }
}
