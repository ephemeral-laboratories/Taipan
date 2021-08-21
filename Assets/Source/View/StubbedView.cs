using Source.Model;

namespace Source.View
{
    public class StubbedView: IView
    {
        public void ShowTitle(string heading)
        {
            throw new System.NotImplementedException();
        }

        public void ShowDetail(params string[] detailRows)
        {
            throw new System.NotImplementedException();
        }

        public void ShowSplashUntilKeyPressed()
        {
            throw new System.NotImplementedException();
        }

        public void SinkLorcha(LorchaPosition position)
        {
            throw new System.NotImplementedException();
        }

        public int PollForInputKeystroke()
        {
            throw new System.NotImplementedException();
        }

        public int AskUserForNumber()
        {
            throw new System.NotImplementedException();
        }

        public YesNo AskYesNo()
        {
            throw new System.NotImplementedException();
        }

        public InitialState SelectInitialState(string question, InitialState a, InitialState b)
        {
            throw new System.NotImplementedException();
        }

        public void ClearScreen()
        {
            throw new System.NotImplementedException();
        }

        public void ShowPortStatsScreen()
        {
            throw new System.NotImplementedException();
        }

        public void DrawBlast(LorchaPosition position)
        {
            throw new System.NotImplementedException();
        }

        public void DrawLorcha(LorchaPosition position, Lorcha ship)
        {
            throw new System.NotImplementedException();
        }

        public void ClearLorcha(LorchaPosition position)
        {
            throw new System.NotImplementedException();
        }

        public CargoType AskUserForCargoType()
        {
            throw new System.NotImplementedException();
        }

        public CargoToThrow AskUserForCargoTypeOrAll()
        {
            throw new System.NotImplementedException();
        }

        public void ShowPrice(CargoType cargoType, Money price)
        {
            throw new System.NotImplementedException();
        }

        public void WaitSecondsUnlessAKeyIsPressed(int seconds)
        {
            throw new System.NotImplementedException();
        }

        public Orders CheckForOrders()
        {
            throw new System.NotImplementedException();
        }

        public void ShowShipCount(int ships)
        {
            throw new System.NotImplementedException();
        }

        public void ShowShipsAttacking(string shipsAttackingOne)
        {
            throw new System.NotImplementedException();
        }

        public Port AskUserForPort()
        {
            throw new System.NotImplementedException();
        }

        public PortActivity AskUserForPortActivity(string question)
        {
            throw new System.NotImplementedException();
        }

        public PortActivity AskUserForHomePortActivity(string question)
        {
            throw new System.NotImplementedException();
        }

        public PortActivity AskUserForHomePortActivityIncludingRetirement(string question)
        {
            throw new System.NotImplementedException();
        }

        public void ShowShipStatus(Status status, int statusPercentage)
        {
            throw new System.NotImplementedException();
        }

        public void ShowMoreShipsIndicator()
        {
            throw new System.NotImplementedException();
        }

        public void HideMoreShipsIndicator()
        {
            throw new System.NotImplementedException();
        }

        public void ShowPort(Port port)
        {
            throw new System.NotImplementedException();
        }

        public void ShowWarehouseCargoQuantity(CargoType type, long quantity)
        {
            throw new System.NotImplementedException();
        }

        public void ShowShipCargoQuantity(CargoType type, long quantity)
        {
            throw new System.NotImplementedException();
        }

        public void ShowShipAvailable(int available)
        {
            throw new System.NotImplementedException();
        }

        public void ShowShipOverloaded()
        {
            throw new System.NotImplementedException();
        }

        public void ShowWarehouseAvailable(long available)
        {
            throw new System.NotImplementedException();
        }

        public void ShowWarehouseInUse(long inUse)
        {
            throw new System.NotImplementedException();
        }

        public void ShowCash(string cash)
        {
            throw new System.NotImplementedException();
        }

        public void ShowShipGuns(int shipGuns)
        {
            throw new System.NotImplementedException();
        }

        public void ShowCashAtBank(string cash)
        {
            throw new System.NotImplementedException();
        }

        public void ShowDebt(string cash)
        {
            throw new System.NotImplementedException();
        }

        public void ShowRetirement()
        {
            throw new System.NotImplementedException();
        }

        public void ShowBeingHit()
        {
            throw new System.NotImplementedException();
        }

        public Money AskUserForMoneyAmount()
        {
            throw new System.NotImplementedException();
        }

        public void ShowFinalStatsScreen(string finalEquity, int finalShipCapacity, int finalShipGuns, int years, int months,
            decimal finalScore, Rating rating)
        {
            throw new System.NotImplementedException();
        }

        public string AskForFirmName(string[] questionLines)
        {
            throw new System.NotImplementedException();
        }

        public void ShowFeedback(string detail)
        {
            throw new System.NotImplementedException();
        }

        public void ShowBattleStatus(string detail)
        {
            throw new System.NotImplementedException();
        }

        public void ShowCurrentOrders(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}