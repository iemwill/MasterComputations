using ProtoBuf;

namespace MasterComputations.Data
{
    [ProtoContract]
    public class Currency
    {
        [ProtoMember(1)]
        public float withdrawal_fee { get; set; }
        [ProtoMember(2)]
        public float min_withdrawal_fee { get; set; }
        [ProtoMember(3)]
        public int min_confirmations { get; set; }
        [ProtoMember(4)]
        public int fee_precision { get; set; }
        [ProtoMember(5)]
        public string currency_long { get; set; }
        [ProtoMember(6)]
        public string currency { get; set; }
        [ProtoMember(7)]
        public string coin_type { get; set; }

        public Currency()
        {
            withdrawal_fee = new float(); min_withdrawal_fee = new float(); min_confirmations = new int(); fee_precision = new int();
            currency_long = ""; currency = ""; coin_type = "";
        }
    }
}
