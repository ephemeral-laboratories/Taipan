using Source.View;

namespace Source.Model.PortActions
{
    public class SellAction: IPortAction
    {
        public void Run(GameState state, IView view)
        {
            view.ShowDetail("What do you wish me to sell, Taipan? ");

            var type = view.AskUserForCargoType();

            int quantity;
            while (true)
            {
                view.ShowDetail("How much %s shall I sell, Taipan: ", type.LocalizedName());
                quantity = view.AskUserForNumber();
                if (quantity == -1)
                {
                    quantity = state.Ship.UnitsStored(type);
                }

                if (state.Ship.UnitsStored(type) < quantity)
                {
                    continue;
                }

                state.Ship.RemoveCargo(type, quantity);
                break;
            }

            state.Cash += state.Prices[type] * quantity;
        }
    }
}