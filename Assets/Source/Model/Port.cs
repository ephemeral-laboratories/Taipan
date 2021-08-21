using System.Collections.Generic;

namespace Source.Model
{
    public enum Port
    {
        // Special value for when not at a port of course
        AtSea,

        HongKong,
        ShangHai,
        Nagasaki,
        Saigon,
        Manila,
        Singapore,
        Batavia
    }

    public static class PortExtensions
    {
        private static readonly Dictionary<Port, string> Names = new Dictionary<Port, string>
        {
            {Port.AtSea, Strings.Location_AtSea},
            {Port.HongKong, Strings.Location_HongKong},
            {Port.ShangHai, Strings.Location_Shanghai},
            {Port.Nagasaki, Strings.Location_Nagasaki},
            {Port.Saigon, Strings.Location_Saigon},
            {Port.Manila, Strings.Location_Manila},
            {Port.Singapore, Strings.Location_Singapore},
            {Port.Batavia, Strings.Location_Batavia}
        };

        public static string LocalizedName(this Port port)
        {
            return Names[port];
        }
    }
}