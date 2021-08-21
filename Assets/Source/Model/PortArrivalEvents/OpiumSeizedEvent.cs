using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class OpiumSeizedEvent: IPortArrivalEvent
    {
        public void Run(GameState state, IView view)
        {
            if (state.Ship.Port == Port.HongKong || state.Random.Next(18) != 0 ||
                !state.Ship.HasStored(CargoType.Opium))
                return;

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