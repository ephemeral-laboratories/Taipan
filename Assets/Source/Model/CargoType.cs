using System;
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
        public static string LocalizedName(this CargoType type)
        {
            return type switch
            {
                CargoType.Opium => Strings.Item_Opium,
                CargoType.Silk => Strings.Item_Silk,
                CargoType.Arms => Strings.Item_Arms,
                CargoType.General => Strings.Item_General,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}