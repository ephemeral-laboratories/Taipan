using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class LiYuenExtortionEvent: BasePortArrivalEvent
    {
        protected override bool ShouldRun(GameState state) =>
            state.Ship.Port == Port.HongKong && state.LiYuensTrust == 0 && state.Cash > 0;

        protected override void Run(GameState state, IView view)
        {
            var time = state.Calendar.MonthsSinceStart;

            var i = 1.8;
            var j = 0;

            if (time > 12)
            {
                j = state.Random.Next(1000 * time) + 1000 * time;
                i = 1;
            }

            var amount = state.Cash / i * state.Random.NextDouble() + j;

            view.ShowTitle(Strings.CompradorsReport);
            view.ShowDetail(string.Format(Strings.LiYuenAsksXInDonation, amount));

            var choice = view.AskYesNo();
            if (choice == YesNo.Yes)
            {
                if (amount <= state.Cash)
                {
                    state.Cash -= amount;
                    state.LiYuensTrust = 1;
                }
                else
                {
                    view.ShowDetail(Strings.YouDoNotHaveEnoughCash);
                    GameLogic.SkippableDelay(state, 3);

                    view.ShowDetail(Strings.DoYouWantElderBrotherWuToMakeUpTheDifference);
                    choice = view.AskYesNo();
                    if (choice == YesNo.Yes)
                    {
                        amount -= state.Cash;
                        state.Debt += amount;
                        state.Cash = Money.Zero;
                        state.LiYuensTrust = 1;

                        view.ShowDetail(Strings.ElderBrotherHasGivenLiYuenTheDifference);
                        GameLogic.SkippableDelay(state, 5);
                    }
                    else
                    {
                        state.Cash = Money.Zero;
                        view.ShowDetail(Strings.ElderBrotherWuWillNotPayLiYuenTheDifference);
                        GameLogic.SkippableDelay(state, 5);
                    }
                }
            }

            GameLogic.UpdatePortStatistics(state);
        }
    }
}