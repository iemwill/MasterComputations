using ProtoBuf;

namespace OptionPricing.Data
{
    [ProtoContract]
    [ProtoInclude(500, typeof(Stats))]
    [ProtoInclude(500, typeof(Greeks))]
    public class Book
    {
        [ProtoMember(1)]
        public float underlying_price { get; set; }
        [ProtoMember(2)]
        public string underlying_index { get; set; }
        [ProtoMember(3)]
        public long timestamp { get; set; }
        [ProtoMember(4)]
        public Stats stats { get; set; }
        [ProtoMember(5)]
        public string state { get; set; }
        [ProtoMember(6)]
        public float settlement_price { get; set; }
        [ProtoMember(7)]
        public float open_interest { get; set; }
        [ProtoMember(8)]
        public float min_price { get; set; }
        [ProtoMember(9)]
        public float max_price { get; set; }
        [ProtoMember(10)]
        public float mark_price { get; set; }
        [ProtoMember(11)]
        public float mark_iv { get; set; }
        [ProtoMember(12)]
        public float last_price { get; set; }
        [ProtoMember(13)]
        public float interest_rate { get; set; }
        [ProtoMember(14)]
        public float index_price { get; set; }
        [ProtoMember(15)]
        public string instrument_name { get; set; }
        [ProtoMember(16)]
        public float estimated_delivery_price { get; set; }
        [ProtoMember(17)]
        public long change_id { get; set; }
        [ProtoMember(18)]
        public Greeks greeks { get; set; }
        [ProtoMember(19)]
        public float best_bid_price { get; set; }
        [ProtoMember(20)]
        public float best_bid_amount { get; set; }
        [ProtoMember(21)]
        public float best_ask_price { get; set; }
        [ProtoMember(22)]
        public float best_ask_amount { get; set; }
        [ProtoMember(23)]
        public float best_ask_iv { get; set; }
        [ProtoMember(24)]
        public float best_bid_iv { get; set; }
        public Book()
        {
            min_price = new float(); max_price = new float(); mark_price = new float(); mark_iv = new float(); last_price = new float();
            interest_rate = new float(); index_price = new float(); estimated_delivery_price = new float(); open_interest = new float();
            best_bid_price = new float(); best_bid_amount = new float(); best_ask_price = new float(); best_ask_amount = new float();
            settlement_price = new float(); underlying_price = new float(); change_id = new long(); timestamp = new long();
            instrument_name = ""; underlying_index = ""; state = ""; greeks = new Greeks(); ; stats = new Stats();
            best_ask_iv = new float(); best_bid_iv = new float();
        }

    }
    [ProtoContract]
    public class Stats
    {
        [ProtoMember(1)]
        public float volume { get; set; }
        [ProtoMember(2)]
        public float price_change { get; set; }
        [ProtoMember(3)]
        public float low { get; set; }
        [ProtoMember(4)]
        public float high { get; set; }
        public Stats()
        {
            volume = new float(); price_change = new float(); low = new float(); high = new float();
        }

    }
    [ProtoContract]
    public class Greeks
    {
        [ProtoMember(1)]
        public float delta { get; set; }
        [ProtoMember(2)]
        public float gamma { get; set; }
        [ProtoMember(3)]
        public float rho { get; set; }
        [ProtoMember(4)]
        public float theta { get; set; }
        [ProtoMember(5)]
        public float vega { get; set; }

        public Greeks()
        {
            delta = new float(); gamma = new float(); rho = new float(); theta = new float(); vega = new float();
        }
    }
}
