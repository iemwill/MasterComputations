using MasterComputations.Classes;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace MasterComputations.Models
{
    [ProtoContract]
    [ProtoInclude(500, typeof(Option))]
    [ProtoInclude(500, typeof(Model))]
    public class Model
    {
        [ProtoMember(1)]
        public Option option { get; set; }
        [ProtoMember(2)]
        public List<Tuple<DateTime, long>> rawModel { get; set; }

        public Model()
        {
            option = new Option(); rawModel = new List<Tuple<DateTime, long>>();
        }
    }
}
