using ProtoBuf;
using System;

namespace MasterComputations.Data
{
    [ProtoContract]
    public class Instrument
    {
        [ProtoMember(1)]
        public float tick_size { get; set; }
        [ProtoMember(2)]
        public float taker_commission { get; set; }
        [ProtoMember(3)]
        public float strike { get; set; }
        [ProtoMember(4)]
        public string settlement_period { get; set; }
        [ProtoMember(5)]
        public string quote_currency { get; set; }
        [ProtoMember(6)]
        public string option_type { get; set; }
        [ProtoMember(7)]
        public float min_trade_amount { get; set; }
        [ProtoMember(8)]
        public float maker_commission { get; set; }
        [ProtoMember(9)]
        public string kind { get; set; }
        [ProtoMember(10)]
        public bool? is_active { get; set; }
        [ProtoMember(11)]
        public string instrument_name { get; set; }
        [ProtoMember(12)]
        public Int64 expiration_timestamp { get; set; }
        [ProtoMember(13)]
        public Int64 creation_timestamp { get; set; }
        [ProtoMember(14)]
        public float contract_size { get; set; }
        [ProtoMember(15)]
        public string base_currency { get; set; }
        [ProtoMember(16)]
        public float max_liquidation_commission { get; set; }
        [ProtoMember(17)]
        public int max_leverage { get; set; }
        public Instrument()
        {
            base_currency = ""; instrument_name = ""; kind = ""; option_type = ""; quote_currency = ""; settlement_period = "";
            contract_size = new float(); maker_commission = new float(); min_trade_amount = new float(); taker_commission = new float();
            strike = new float(); tick_size = new float(); max_liquidation_commission = new float();
            creation_timestamp = new Int64(); expiration_timestamp = new Int64(); max_leverage = new int();
            is_active = new bool();
        }
    }
}
