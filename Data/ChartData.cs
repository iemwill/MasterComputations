using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterComputations.Data
{
    [ProtoContract]
    public class ChartData
    {
        [ProtoMember(1)]
        public List<float> close { get; set; }
        [ProtoMember(2)]
        public List<float> cost { get; set; }
        [ProtoMember(3)]
        public List<float> high { get; set; }
        [ProtoMember(4)]
        public List<float> low { get; set; }
        [ProtoMember(5)]
        public List<float> open { get; set; }
        [ProtoMember(6)]
        public string status { get; set; }
        [ProtoMember(7)]
        public List<long> ticks { get; set; }
        [ProtoMember(8)]
        public List<float> volume { get; set; }

        public ChartData()
        {
            close = new List<float>(); cost = new List<float>(); high = new List<float>(); low = new List<float>();
            open = new List<float>(); volume = new List<float>(); ticks = new List<long>(); status = "";
        } 
    }
}
