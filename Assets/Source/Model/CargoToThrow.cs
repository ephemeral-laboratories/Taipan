using System;
using System.Collections.Generic;

namespace Source.Model
{
    public enum CargoToThrow: byte
    {
        Opium,
        Silk,
        Arms,
        General,
        Everything
    }

    internal static class CargoToThrowExtensions
    {
        public static string LocalizedName(this CargoToThrow type)
        {
            return type switch
            {
                CargoToThrow.Opium => Strings.Item_Opium,
                CargoToThrow.Silk => Strings.Item_Silk,
                CargoToThrow.Arms => Strings.Item_Arms,
                CargoToThrow.General => Strings.Item_General,
                CargoToThrow.Everything => Strings.Item_Everything,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}