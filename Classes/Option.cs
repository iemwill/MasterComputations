using OptionPricing.Data;
using ProtoBuf;
using System;
using System.Collections.Generic;


namespace OptionPricing.Classes
{
    [ProtoContract]
    [ProtoInclude(500, typeof(Instrument))]
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
        [ProtoMember(6)]
        public Instrument raw { get; set; }
        [ProtoMember(7)]
        public bool active { get; set; }

        public Option()
        {
            name = ""; start = new DateTime(); end = new DateTime(); trades = new List<Trade>(); orderBook = new List<Book>();
            raw = new Instrument(); active = new bool();
        }
        public static Tuple<long, long> getDateEx(List<Option> optionsBTC)
        {
            var minDateTime = long.MaxValue;
            var maxDateTime = long.MinValue;
            foreach (var x in optionsBTC)
            {
                if (x.raw.creation_timestamp < minDateTime)
                    minDateTime = x.raw.creation_timestamp;
                if (x.raw.expiration_timestamp > maxDateTime)
                    maxDateTime = x.raw.expiration_timestamp;
            }
            return new Tuple<long, long>(minDateTime, maxDateTime);
        }
    }
}
