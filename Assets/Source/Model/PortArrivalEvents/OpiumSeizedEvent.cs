using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class OpiumSeizedEvent: BasePortArrivalEvent
    {
        protected override bool ShouldRun(GameState state) =>
            state.Ship.Port != Port.HongKong && state.Random.Next(18) == 0 &&
            state.Ship.HasStored(CargoType.Opium);

        protected override void Run(GameState state, IView view)
        {
            var fine = state.Cash / 1.8 * state.Random.NextDouble() + 1;

            state.Ship.RemoveCargo(CargoType.Opium, state.Ship.UnitsStored(CargoType.Opium));
            state.Cash -= fine;

            GameLogic.UpdatePortStatistics(state);

            view.ShowTitle(Strings.CompradorsReport);
            view.ShowDetail(string.Format(Strings.AuthoritiesSeizedYourOpium, fine.FancyNumbers()));

            GameLogic.SkippableDelay(state, 5);
        }
    }
}