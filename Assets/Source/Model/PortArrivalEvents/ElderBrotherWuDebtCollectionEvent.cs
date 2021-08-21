using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class ElderBrotherWuDebtCollectionEvent: IPortArrivalEvent
    {
        public void Run(GameState state, IView view)
        {
            if (state.Ship.Port != Port.HongKong || state.Debt < 10000 || state.BrotherWuWarning) return;

            var braves = state.Random.Next(100) + 50;

            view.ShowDetail(string.Format(Strings.ElderBrotherWuSpiel_1, braves));

            GameLogic.SkippableDelay(state, 3);

            view.ShowDetail(Strings.ElderBrotherWuSpiel_2);

            GameLogic.SkippableDelay(state, 3);

            view.ShowDetail(Strings.ElderBrotherWuSpiel_3);

            GameLogic.SkippableDelay(state, 5);

            state.BrotherWuWarning = true;
        }
    }
}