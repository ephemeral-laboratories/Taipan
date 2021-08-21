using Source.View;

namespace Source.Model.PortActions
{
    public class VisitBankAction: IPortAction
    {
        public void Run(GameState state, IView view)
        {
            Money amount;

            while (true)
            {
                view.ShowTitle(Strings.CompradorsReport);
                view.ShowDetail(Strings.HowMuchWillYouDeposit);

                amount = view.AskUserForMoneyAmount();
                if (amount < 0)
                {
                    amount = state.Cash;
                }

                if (amount <= state.Cash)
                {
                    state.Cash -= amount;
                    state.CashAtBank += amount;
                    break;
                }

                view.ShowDetail(string.Format(Strings.YouOnlyHaveXInCash, state.Cash.FancyNumbers()));
                GameLogic.SkippableDelay(state, 5);
            }

            GameLogic.UpdatePortStatistics(state);

            while (true)
            {
                view.ShowTitle(Strings.CompradorsReport);
                view.ShowDetail(Strings.HowMuchToWithdraw);

                amount = view.AskUserForMoneyAmount();
                if (amount < 0)
                {
                    amount = state.CashAtBank;
                }

                if (amount <= state.CashAtBank)
                {
                    state.Cash += amount;
                    state.CashAtBank -= amount;
                    break;
                }
                else
                {
                    view.ShowFeedback(string.Format(Strings.YouOnlyHaveXInTheBank, state.Cash.FancyNumbers()));
                    GameLogic.SkippableDelay(state, 5);
                }
            }

            GameLogic.UpdatePortStatistics(state);
        }
    }
}