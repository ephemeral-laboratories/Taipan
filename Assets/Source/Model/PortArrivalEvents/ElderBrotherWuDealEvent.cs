using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class ElderBrotherWuDealEvent: BasePortArrivalEvent
    {
        protected override bool ShouldRun(GameState state) => state.Ship.Port == Port.HongKong;

        protected override void Run(GameState state, IView view)
        {
            view.ShowTitle(Strings.CompradorsReport);
            view.ShowDetail(Strings.DoYouHaveBusinessWithElderBrotherWu);

            while (true)
            {
                var choice = view.AskYesNo();
                if (choice == YesNo.No)
                {
                    break;
                }

                if (state.Cash == 0 && state.CashAtBank == 0 && state.Ship.Guns == 0 && state.Ship.IsEmpty && state.HongKongWarehouse.IsEmpty)
                {
                    var i = Money.Of(state.Random.Next(1500) + 500);

                    state.BrotherWuBailoutCount++;
                    var j = Money.Of(state.Random.Next(2000) * state.BrotherWuBailoutCount + 1500);

                    while (true)
                    {
                        view.ShowTitle(Strings.CompradorsReport);
                        view.ShowDetail(string.Format(Strings.ElderBrotherIsAwareOfYourPlight, i, j));

                        choice = view.AskYesNo();
                        switch (choice)
                        {
                            case YesNo.Yes:
                                state.Cash += i;
                                state.Debt += j;
                                GameLogic.UpdatePortStatistics(state);
                                view.ShowTitle(Strings.CompradorsReport);
                                view.ShowDetail(Strings.VeryWellGoodJoss);
                                GameLogic.SkippableDelay(state, 5);
                                return;

                            case YesNo.No:
                                view.ShowTitle(Strings.CompradorsReport);
                                view.ShowDetail(Strings.VeryWellGameOver);
                                GameLogic.SkippableDelay(state, 5);
                                GameLogic.GameOver(state);
                                break;
                        }
                    }
                }

                if (state.Cash > 0 && state.Debt > 0)
                {
                    while (true)
                    {
                        view.ShowTitle(Strings.CompradorsReport);
                        view.ShowDetail(Strings.HowMuchToRepay);

                        var wu = view.AskUserForMoneyAmount();
                        if (wu < 0)
                        {
                            wu = state.Cash;
                        }

                        if (wu <= state.Cash)
                        {
                            state.Cash -= wu;
                            if (wu > state.Debt && state.Debt > 0)
                            {
                                state.Debt -= wu + 1;
                            }
                            else
                            {
                                state.Debt -= wu;
                            }

                            break;
                        }

                        view.ShowDetail("Taipan, you only have %s in cash.", state.Cash.FancyNumbers());
                        GameLogic.SkippableDelay(state, 5);
                    }
                }

                GameLogic.UpdatePortStatistics(state);

                for (;;)
                {
                    view.ShowTitle(Strings.CompradorsReport);
                    view.ShowDetail(Strings.HowMuchToBorrow);

                    var wu = view.AskUserForMoneyAmount();
                    if (wu < 0)
                    {
                        wu = state.Cash * 2;
                    }

                    if (wu <= state.Cash * 2)
                    {
                        state.Cash += wu;
                        state.Debt += wu;
                        break;
                    }
                    else
                    {
                        view.ShowDetail(Strings.HeWontLoanYouSoMuch);
                        GameLogic.SkippableDelay(state, 5);
                    }
                }

                GameLogic.UpdatePortStatistics(state);

                break;
            }

            if (state.Debt <= 20000 || state.Cash <= 0 || state.Random.Next(5) != 0) return;

            var num = state.Random.Next(3) + 1;

            state.Cash = Money.Zero;
            GameLogic.UpdatePortStatistics(state);

            view.ShowTitle(Strings.CompradorsReport);
            view.ShowDetail(string.Format(Strings.BadJossYouHaveBeenRobbed, num));

            GameLogic.SkippableDelay(state, 5);
        }
    }
}