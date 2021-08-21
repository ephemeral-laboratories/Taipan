using Source.Model;

namespace Source.View
{
    public interface IView
    {
        void ClearScreen();

        // Show splash screen, picture of a ship, press any key to start, you get the idea.
        // Kept information:
        // Created by: Art Canfil
        // TRS80 version by: Art Canfil
        // Programmed by: Jay Link jaylink1971@gmail.com
        void ShowSplashUntilKeyPressed();

        // Port stats layout:
        // Firm: %s, Hong Kong\n", firm
        //  ______________________________________
        // |Hong Kong Warehouse                   |     Date
        // |   Opium     [   ] In Use:            |
        // |   Silk      [   ]                    |
        // |   Arms      [   ] Vacant:            |   Location
        // |   General   [   ]                    |
        // |______________________________________|
        // |Hold               Guns               |     Debt
        // |   Opium     [   ]                    |
        // |   Silk      [   ]                    |
        // |   Arms      [   ]                    |  Ship Status
        // |   General   [   ]                    |
        // |______________________________________|
        // Cash:               Bank:
        // ________________________________________
        void ShowPortStatsScreen();
        
        string AskForFirmName(params string[] questionLines);

        void ShowTitle(string heading);

        void ShowDetail(params string[] detailRows);

        void ShowFeedback(string detail);

        void ShowBattleStatus(string detail);

        void DrawBlast(LorchaPosition position);

        void DrawLorcha(LorchaPosition position, Lorcha ship);

        void ClearLorcha(LorchaPosition position);

        // Trigger animation sinking the Lorcha.
        // In the original, 1/20 of the time, the animation runs 2x slower
        void SinkLorcha(LorchaPosition position);

        int PollForInputKeystroke();

        int AskUserForNumber();

        CargoType AskUserForCargoType();

        CargoToThrow AskUserForCargoTypeOrAll();

        YesNo AskYesNo();
        
        InitialState SelectInitialState(string question, InitialState a, InitialState b);

        void ShowPrice(CargoType cargoType, Money price);

        void WaitSecondsUnlessAKeyIsPressed(int seconds);

        Orders CheckForOrders();
        
        void ShowShipCount(int ships);

        void ShowShipsAttacking(string shipsAttackingOne);

        Port AskUserForPort();

        PortActivity AskUserForPortActivity(string question);
        
        PortActivity AskUserForHomePortActivity(string question);

        PortActivity AskUserForHomePortActivityIncludingRetirement(string question);

        // Renders the status string in inverse if status is critical or poor
        void ShowShipStatus(Status status, int statusPercentage);
        
        void ShowMoreShipsIndicator();
        
        void HideMoreShipsIndicator();
        
        void ShowPort(Port port);
        
        void ShowWarehouseCargoQuantity(CargoType type, long quantity);
        
        void ShowShipCargoQuantity(CargoType type, long quantity);
        
        void ShowShipAvailable(int available);
        
        void ShowShipOverloaded();
        
        void ShowWarehouseAvailable(long available);
        
        void ShowWarehouseInUse(long inUse);
        
        void ShowCash(string cash);
        
        void ShowCurrentOrders(string message);

        void ShowShipGuns(int shipGuns);
        
        void ShowCashAtBank(string cash);
        
        void ShowDebt(string cash);

        // In the original, displays in inverse,
        //                          
        // Y o u ' r e    a        
        //                         
        // M I L L I O N A I R E ! 
        //                         
        void ShowRetirement();
        
        // Classically, the screen flashes
        void ShowBeingHit();

        Money AskUserForMoneyAmount();

        // Your final status:
        // Net cash:  %s
        // Ship size: %d units with %d guns
        // You traded for %d year(s) and %d month(s)
        // Your score is %d.
        //
        // If Worse rating: "Have you considered a land based job?"
        // If Worst rating: "The crew has requested that you stay on shore for their safety!!"
        // Otherwise you get your position in the rating table highlighted:
        //   Your Rating:
        //    _______________________________
        //   |Ma Tsu         50,000 and over |
        //   |Master Taipan   8,000 to 49,999|
        //   |Taipan          1,000 to  7,999|
        //   |Compradore        500 to    999|
        //   |Galley Hand       less than 500|
        //   |_______________________________|
        //
        //   Play again?
        void ShowFinalStatsScreen(string finalEquity, int finalShipCapacity, int finalShipGuns,
            int years, int months,
            decimal finalScore, Rating rating);
    }
}