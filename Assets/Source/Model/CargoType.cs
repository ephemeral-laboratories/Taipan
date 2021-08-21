using System.Collections.Generic;
using Source;

namespace Source.Model
{
    public enum CargoType: byte
    {
        Opium,
        Silk,
        Arms,
        General
    }

    internal static class CargoTypeExtensions
    {
        private static readonly Dictionary<CargoType, string> Names = new Dictionary<CargoType,string>
        {
            { CargoType.Opium, Strings.Item_Opium },
            { CargoType.Silk, Strings.Item_Silk },
            { CargoType.Arms, Strings.Item_Arms },
            { CargoType.General, Strings.Item_General }
        };

        public static string LocalizedName(this CargoType type)
        {
            return Names[type];
        }
    }
}