using ProtoBuf;

namespace OptionPricing.Data
{
    [ProtoContract]
    public class Trade
    {
        [ProtoMember(1)]
        public double amount { get; set; }
        [ProtoMember(2)]
        public string direction { get; set; }
        [ProtoMember(3)]
        public double index_price { get; set; }
        [ProtoMember(4)]
        public string instrument_name { get; set; }
        [ProtoMember(5)]
        public double implied_volatility { get; set; }
        [ProtoMember(6)]
        public string liquidatation { get; set; }
        [ProtoMember(7)]
        public double price { get; set; }
        [ProtoMember(8)]
        public int tick_direction { get; set; }
        [ProtoMember(9)]
        public long timestamp { get; set; }
        [ProtoMember(10)]
        public string trade_id { get; set; }
        [ProtoMember(12)]
        public int trade_seq { get; set; }

        public Trade()
        {
            amount = new double(); direction = ""; index_price = new double(); instrument_name = "";
            implied_volatility = new double(); liquidatation = ""; price = new double(); tick_direction = new int();
            timestamp = new long(); trade_id = ""; trade_seq = new int();
        }
    }
}
