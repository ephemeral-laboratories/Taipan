using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class OfferShipUpgradeEvent: BasePortArrivalEvent
    {
        protected override bool ShouldRun(GameState state) => state.Random.Next(4) == 0;

        protected override void Run(GameState state, IView view)
        {
            if (state.Random.Next(2) == 0)
            {
                NewShip(state, view);
            }
            else if (state.Ship.Guns < 1000)
            {
                NewGun(state, view);
            }
        }

        private static void NewShip(GameState state, IView view)
        {
            var time = state.Calendar.MonthsSinceStart;
            var amount = Money.Of(state.Random.Next(1000 * (time + 5) / 6) * (state.Ship.Capacity / 50) + 1000);

            if (state.Cash < amount)
            {
                return;
            }

            view.ShowTitle(Strings.CompradorsReport);
            // TODO: Should invert the text if damaged.
            view.ShowDetail(string.Format(
                state.Ship.Damage > 0
                    ? Strings.DoYouWishToTradeInYourShip_Damaged
                    : Strings.DoYouWishToTradeInYourShip_Fine,
                amount.FancyNumbers()));

            var choice = view.AskYesNo();
            if (choice == YesNo.Yes)
            {
                state.Cash -= amount;
                state.Ship.ExpandHoldBy(50);
                state.Ship.ResetDamage();
            }

            if (state.Random.Next(2) == 0 && state.Ship.Guns < 1000)
            {
                GameLogic.UpdatePortStatistics(state);
                NewGun(state, view);
            }

            GameLogic.UpdatePortStatistics(state);
        }

        private static void NewGun(GameState state, IView view)
        {
            var time = state.Calendar.MonthsSinceStart;
            var amount = Money.Of(state.Random.Next(1000 * (time + 5) / 6) + 500);

            if (state.Cash < amount || state.Ship.Available < 10)
            {
                return;
            }

            view.ShowTitle(Strings.CompradorsReport);
            view.ShowDetail(string.Format(Strings.DoYouWishToBuyAGun, amount.FancyNumbers()));

            var choice = view.AskYesNo();
            if (choice == YesNo.Yes)
            {
                state.Cash -= amount;
                state.Ship.AddGun();
            }

            GameLogic.UpdatePortStatistics(state);
        }
    }
}