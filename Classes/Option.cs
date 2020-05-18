using MasterComputations.Data;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace MasterComputations.Classes
{
    [ProtoContract]
    public class Option
    {
        [ProtoMember(1)]
        public string name { get; set; }
        [ProtoMember(2)]
        public DateTime start { get; set; }
        [ProtoMember(3)]
        public DateTime end { get; set; }
        [ProtoMember(4)]
        public List<Trade> trades { get; set; }
        [ProtoMember(5)]
        public List<Book> orderBook { get; set; }

        public Option()
        {
            name = ""; start = new DateTime(); end = new DateTime(); trades = new List<Trade>(); orderBook = new List<Book>();
        }
    }
}
