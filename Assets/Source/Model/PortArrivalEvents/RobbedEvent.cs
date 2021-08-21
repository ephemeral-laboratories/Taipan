using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class RobbedEvent: IPortArrivalEvent
    {
        public void Run(GameState state, IView view)
        {
            if (state.Cash <= 25000 || state.Random.Next(20) != 0) return;
            
            var robbed = state.Cash / 1.4 * state.Random.NextDouble();

            state.Cash -= robbed;
            GameLogic.UpdatePortStatistics(state);

            view.ShowTitle(Strings.CompradorsReport);
            view.ShowDetail(string.Format(Strings.YouGotRobbed, robbed.FancyNumbers()));

            GameLogic.SkippableDelay(state, 5);
        }

    }
}