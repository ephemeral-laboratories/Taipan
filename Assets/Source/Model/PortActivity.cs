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
        private static readonly Dictionary<PortActivity, string> Names = new Dictionary<PortActivity, string>
        {
            {PortActivity.Buy, Strings.PortActivity_Buy},
            {PortActivity.Sell, Strings.PortActivity_Sell},
            {PortActivity.VisitBank, Strings.PortActivity_VisitBank},
            {PortActivity.TransferCargo, Strings.PortActivity_TransferCargo},
            {PortActivity.QuitTrading, Strings.PortActivity_QuitTrading},
            {PortActivity.Retire, Strings.PortActivity_Retire},
        };

        public static string LocalizedName(this PortActivity port)
        {
            return Names[port];
        }
    }
}