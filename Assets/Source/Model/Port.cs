using System;
using System.Collections.Generic;

namespace Source.Model
{
    public enum Port
    {
        // Special value for when not at a port of course
        AtSea,

        HongKong,
        Shanghai,
        Nagasaki,
        Saigon,
        Manila,
        Singapore,
        Batavia
    }

    public static class PortExtensions
    {
        public static string LocalizedName(this Port port)
        {
            return port switch
            {
                Port.AtSea => Strings.Location_AtSea,
                Port.HongKong => Strings.Location_HongKong,
                Port.Shanghai => Strings.Location_Shanghai,
                Port.Nagasaki => Strings.Location_Nagasaki,
                Port.Saigon => Strings.Location_Saigon,
                Port.Manila => Strings.Location_Manila,
                Port.Singapore => Strings.Location_Singapore,
                Port.Batavia => Strings.Location_Batavia,
                _ => throw new ArgumentOutOfRangeException(nameof(port), port, null)
            };
        }
    }
}