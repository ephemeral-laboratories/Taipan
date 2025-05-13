using Source.View;

namespace Source.Model.PortActions
{
    public class BuyAction: IPortAction
    {
        public void Run(GameState state, IView view)
        {
            view.ShowDetail(Strings.WhatDoYouWishMeToBuy);

            var type = view.AskUserForCargoType();

            int quantity;
            while (true)
            {
                var affordable = (int)(state.Cash / state.Prices[type]);
                view.ShowDetail(
                    string.Format(Strings.YouCanAffordX, affordable),
                    string.Format(Strings.HowMuchXShallIBuy, type.LocalizedName()));
                quantity = view.AskUserForNumber();
                if (quantity == -1)
                {
                    quantity = affordable;
                }

                if (quantity <= affordable)
                {
                    break;
                }
            }

            state.Cash -= state.Prices[type] * quantity;
            state.Ship.AddCargo(type, quantity);
        }
    }
}