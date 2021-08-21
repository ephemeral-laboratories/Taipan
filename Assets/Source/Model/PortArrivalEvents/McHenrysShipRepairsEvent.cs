using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class McHenrysShipRepairsEvent: IPortArrivalEvent
    {
        public void Run(GameState state, IView view)
        {
            if (state.Ship.Port != Port.HongKong || state.Ship.Damage <= 0) return;
            
            view.ShowTitle(Strings.CompradorsReport);
            view.ShowDetail(Strings.McHenryHasArrived);

            var choice = view.AskYesNo();

            if (choice != YesNo.Yes)
            {
                return;
            }

            var percent = (int)((double)state.Ship.Damage / state.Ship.Capacity * 100);
            var time = state.Calendar.MonthsSinceStart;

            var k = (time + 3) / 4;
            var br = (state.Random.Next(60 * k) + 25 * k) * state.Ship.Capacity / 50;
            var repairPrice = br * state.Ship.Damage + 1;

            view.ShowDetail(string.Format(Strings.HowMuchWillYeSpend, percent, repairPrice));

            while (true)
            {
                var amount = view.AskUserForMoneyAmount();
                if (amount < 0)
                {
                    amount = state.Cash;
                }
                else if (amount > state.Cash)
                {
                    continue;
                }

                state.Cash -= amount;
                state.Ship.Repair((int)((double)amount.Amount / br + 0.5));
                GameLogic.UpdatePortStatistics(state);
                break;
            }
        }
    }
}