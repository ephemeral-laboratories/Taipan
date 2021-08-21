using System;
using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class WarehouseTheftEvent: IPortArrivalEvent
    {
        public void Run(GameState state, IView view)
        {
            if (state.Random.Next(50) != 0 || !state.HongKongWarehouse.IsNotEmpty) return;
            
            foreach (CargoType type in Enum.GetValues(typeof(CargoType)))
            {
                var available = state.HongKongWarehouse.UnitsStored(type);
                var plundered = (int)(available * (0.8 / 1.8) * state.Random.NextDouble());
                state.HongKongWarehouse.RemoveCargo(type, plundered);
            }

            GameLogic.UpdatePortStatistics(state);

            view.ShowTitle(Strings.CompradorsReport);
            view.ShowDetail(Strings.LargeTheftFromWarehouse);

            GameLogic.SkippableDelay(state, 5);
        }
    }
}