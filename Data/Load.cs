using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterComputations.Data
{
    public class Load
    {
        public static Tuple<List<Currency>, List<WalletCurrency>, List<Currency>, List<Currency>, Wallet> Memory(
            string dictstruct, List<Currency> MarketCurrencies, List<WalletCurrency> accountBalances, List<Currency> BTCWatchlist, List<Currency> USDWatchlist, Wallet wallet)
        {
            //_MarketCurrencies
            using (var fs = File.OpenRead(dictstruct + "Settings\\Savings\\MarketCurrencies.will"))
                MarketCurrencies = Serializer.Deserialize<List<Currency>>(fs);
            //AccountBalances
            using (var fs = File.OpenRead(dictstruct + "Settings\\Savings\\Wallet.will"))
                accountBalances = Serializer.Deserialize<List<WalletCurrency>>(fs);
            //BTCWatchlist
            using (var fs = File.OpenRead(dictstruct + "Settings\\Savings\\BTCWatchlist.will"))
                BTCWatchlist = Serializer.Deserialize<List<Currency>>(fs);
            //USDWatchlist
            using (var fs = File.OpenRead(dictstruct + "Settings\\Savings\\USDWatchlist.will"))
                USDWatchlist = Serializer.Deserialize<List<Currency>>(fs);
            //Bittrex Wallet
            var path = Open.CSV(dictstruct + "Settings\\Savings\\Wallets");
            using (var fs = File.OpenRead(path))
                wallet = Serializer.Deserialize<Wallet>(fs);

            return new Tuple<List<Currency>, List<WalletCurrency>, List<Currency>, List<Currency>, Wallet>(MarketCurrencies, accountBalances, BTCWatchlist, USDWatchlist, wallet);
        }
    }
}
