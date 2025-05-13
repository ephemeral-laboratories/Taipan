using Source.View;

namespace Source.Model.PortArrivalEvents
{
    public class GoodPricesEvent: BasePortArrivalEvent
    {
        protected override bool ShouldRun(GameState state) => state.Random.Next(9) == 0;

        protected override void Run(GameState state, IView view)
        {
            var type = (CargoType)state.Random.Next(4);
            var direction = state.Random.Next(2);

            view.ShowTitle(Strings.CompradorsReport);
            string line2;
            if (direction == 0)
            {
                state.Prices[type] /= 5;
                line2 = string.Format(Strings.HasDroppedToX, state.Prices[type]);
            }
            else
            {
                state.Prices[type] *= state.Random.Next(5) + 5;
                line2 = string.Format(Strings.HasRisenToX, state.Prices[type]);
            }

            view.ShowDetail(
                string.Format(Strings.ThePriceOfX, type.LocalizedName()),
                line2);

            GameLogic.SkippableDelay(state, 3);
        }
    }
}