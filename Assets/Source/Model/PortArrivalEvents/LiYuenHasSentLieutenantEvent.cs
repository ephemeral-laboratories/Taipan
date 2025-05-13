using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class LiYuenHasSentLieutenantEvent: BasePortArrivalEvent
    {
        protected override bool ShouldRun(GameState state) =>
            state.Ship.Port != Port.HongKong && state.LiYuensTrust == 0 && state.Random.Next(4) > 0;

        protected override void Run(GameState state, IView view)
        {
            view.ShowTitle(Strings.CompradorsReport);
            view.ShowDetail(Strings.LiYuenHasSentALieutenant);

            GameLogic.SkippableDelay(state, 3);
        }
    }
}