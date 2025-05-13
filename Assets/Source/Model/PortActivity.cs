using System;
using System.Collections.Generic;

namespace Source.Model
{
    public enum PortActivity
    {
        Buy,
        Sell,
        VisitBank,
        TransferCargo,
        QuitTrading,
        Retire,
    }

    public static class PortActivityExtensions
    {
        public static string LocalizedName(this PortActivity activity)
        {
            return activity switch
            {
                PortActivity.Buy => Strings.PortActivity_Buy,
                PortActivity.Sell => Strings.PortActivity_Sell,
                PortActivity.VisitBank => Strings.PortActivity_VisitBank,
                PortActivity.TransferCargo => Strings.PortActivity_TransferCargo,
                PortActivity.QuitTrading => Strings.PortActivity_QuitTrading,
                PortActivity.Retire => Strings.PortActivity_Retire,
                _ => throw new ArgumentOutOfRangeException(nameof(activity), activity, null)
            };
        }
    }
}