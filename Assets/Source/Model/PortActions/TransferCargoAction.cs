using Source.View;

namespace Source.Model.PortActions
{
    public class TransferCargoAction: IPortAction
    {
        public void Run(GameState state, IView view)
        {
            if (state.HongKongWarehouse.IsEmpty && state.Ship.IsEmpty)
            {
                view.ShowFeedback(Strings.YouHaveNoCargo);
                GameLogic.SkippableDelay(state, 5);
                return;
            }

            foreach (CargoType type in typeof(CargoType).GetEnumValues())
            {
                if (state.Ship.HasStored(type))
                {
                    while (true)
                    {
                        view.ShowTitle(Strings.CompradorsReport);
                        view.ShowDetail(string.Format(Strings.HowMuchToMove, type.LocalizedName()));

                        var amount = view.AskUserForNumber();
                        if (amount == -1)
                        {
                            amount = state.Ship.UnitsStored(type);
                        }

                        if (amount <= state.Ship.UnitsStored(type))
                        {
                            var inUse = state.HongKongWarehouse.TotalUnitsStored;
                            if (inUse + amount <= 10000)
                            {
                                state.Ship.RemoveCargo(type, amount);
                                state.HongKongWarehouse.AddCargo(type, amount);
                                break;
                            }

                            if (inUse == 10000)
                            {
                                view.ShowFeedback(Strings.WarehouseIsFull);
                            }
                            else
                            {
                                view.ShowFeedback(string.Format(Strings.WarehouseWillOnlyHoldX, 10000 - inUse));
                                GameLogic.SkippableDelay(state, 5);
                            }
                        }
                        else
                        {
                            view.ShowDetail(string.Format(Strings.YouHaveOnlyX, state.Ship.UnitsStored(type)));
                            GameLogic.SkippableDelay(state, 5);
                        }
                    }

                    GameLogic.UpdatePortStatistics(state);
                }

                if (state.HongKongWarehouse.HasStored(type))
                {
                    while (true)
                    {
                        view.ShowTitle(Strings.CompradorsReport);
                        view.ShowDetail(string.Format(Strings.HowMuchToMoveAboard, type.LocalizedName()));

                        var amount = view.AskUserForNumber();
                        if (amount == -1)
                        {
                            amount = state.HongKongWarehouse.UnitsStored(type);
                        }

                        if (amount <= state.HongKongWarehouse.UnitsStored(type))
                        {
                            state.HongKongWarehouse.RemoveCargo(type, amount);
                            state.Ship.AddCargo(type, amount);
                            break;
                        }

                        view.ShowDetail(string.Format(Strings.YouHaveOnlyX, state.HongKongWarehouse.UnitsStored(type)));
                        GameLogic.SkippableDelay(state, 5);
                    }

                    GameLogic.UpdatePortStatistics(state);
                }
            }
        }

    }
}