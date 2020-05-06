using ProtoBuf;
using System.Data;


namespace MasterComputations.Data
{
        [ProtoContract]
        public class OptionData
        {
            [ProtoMember(1)]
            public bool? isCall { get; set; }
            [ProtoMember(2)]
            public DataTable rawData { get; set; }
            public OptionData() { isCall = null; rawData = new DataTable(); }
        }
}
