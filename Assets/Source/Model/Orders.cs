using System.Collections.Generic;

namespace Source.Model
{
    public enum Orders
    {
        Fight = 1,
        Run = 2,
        ThrowCargo = 3
    }
    
    public static class OrdersExtensions
    {
        private static readonly Dictionary<Orders, string> Names = new Dictionary<Orders, string>
        {
            {Orders.Fight, Strings.Orders_Fight},
            {Orders.Run, Strings.Orders_Run},
            {Orders.ThrowCargo, Strings.Orders_ThrowCargo},
        };

        public static string LocalizedName(this Orders port)
        {
            return Names[port];
        }
    }
}