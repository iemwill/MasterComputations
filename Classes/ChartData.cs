using ProtoBuf;
using System.Collections.Generic;

namespace OptionPricing.Data
{
    [ProtoContract]
    public class ChartData
    {
        [ProtoMember(1)]
        public List<double> close { get; set; }
        [ProtoMember(2)]
        public List<double> cost { get; set; }
        [ProtoMember(3)]
        public List<double> high { get; set; }
        [ProtoMember(4)]
        public List<double> low { get; set; }
        [ProtoMember(5)]
        public List<double> open { get; set; }
        [ProtoMember(6)]
        public string status { get; set; }
        [ProtoMember(7)]
        public List<long> ticks { get; set; }
        [ProtoMember(8)]
        public List<double> volume { get; set; }

        public ChartData()
        {
            close = new List<double>(); cost = new List<double>(); high = new List<double>(); low = new List<double>();
            open = new List<double>(); volume = new List<double>(); ticks = new List<long>(); status = "";
        } 
    }
}
