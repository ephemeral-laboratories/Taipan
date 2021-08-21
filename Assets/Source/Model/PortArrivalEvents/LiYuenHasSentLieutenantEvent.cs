using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class LiYuenHasSentLieutenantEvent: IPortArrivalEvent
    {
        public void Run(GameState state, IView view)
        {
            if (state.Ship.Port == Port.HongKong || state.LiYuensTrust != 0 || state.Random.Next(4) == 0) return;
            
            view.ShowTitle(Strings.CompradorsReport);
            view.ShowDetail(Strings.LiYuenHasSentALieutenant);

            GameLogic.SkippableDelay(state, 3);
        }
    }
}