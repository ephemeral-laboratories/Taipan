using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class WarehouseTheftEvent: BasePortArrivalEvent
    {
        protected override bool ShouldRun(GameState state) =>
            state.Random.Next(50) == 0 && state.HongKongWarehouse.IsNotEmpty;

        protected override void Run(GameState state, IView view)
        {
            foreach (CargoType type in typeof(CargoType).GetEnumValues())
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