using System;
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
        public static string LocalizedName(this Orders orders)
        {
            return orders switch
            {
                Orders.Fight => Strings.Orders_Fight,
                Orders.Run => Strings.Orders_Run,
                Orders.ThrowCargo => Strings.Orders_ThrowCargo,
                _ => throw new ArgumentOutOfRangeException(nameof(orders), orders, null)
            };
        }
    }
}