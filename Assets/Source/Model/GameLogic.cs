using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Source.Model.PortActions;
using Source.Model.PortArrivalEvents;
using Source.View;

namespace Source.Model
{
    public class GameLogic
    {
        private static IView View { get; set; } = new StubbedView();

        private static void Main()
        {
            var state = InitialiseGame();
            MainLoop(state);
        }

        private static GameState InitialiseGame()
        {
            SplashIntro();

            var firmName = View.AskForFirmName(Strings.WhatWillYouNameYourFirm_1, Strings.WhatWillYouNameYourFirm_2);
            var initialState = CashOrGuns();
            var state = initialState.NewGame(firmName);

            // XXX: Maybe this should be elsewhere.
            SetPrices(state);
            
            return state;
        }

        private static readonly List<BasePortArrivalEvent> PortArrivalEvents =
        [
            new LiYuenExtortionEvent(),
            new McHenrysShipRepairsEvent(),
            new ElderBrotherWuDebtCollectionEvent(),
            new ElderBrotherWuDealEvent(),
            new OfferShipUpgradeEvent(),
            new OpiumSeizedEvent(),
            new WarehouseTheftEvent(),
            new LiYuensTrustEvent(),
            new LiYuenHasSentLieutenantEvent(),
            new GoodPricesEvent(),
            new RobbedEvent()
        ];

        private static readonly Dictionary<PortActivity, IPortAction> PortActions = new()
        {
            { PortActivity.Buy, new BuyAction() },
            { PortActivity.Sell, new SellAction() },
            { PortActivity.VisitBank, new VisitBankAction() },
            { PortActivity.TransferCargo, new TransferCargoAction() },
            { PortActivity.Retire, new RetireAction() }
        };
        
        private static void MainLoop(GameState state)
        {
            while (state.RunGameLoop)
            {
                UpdatePortStatistics(state);

                foreach (var portArrivalEvent in PortArrivalEvents)
                {
                    portArrivalEvent.MaybeRun(state, View);
                }

                // This loop repeats until the player quits trading and is not overloaded.
                while (true)
                {
                    var choice = PortChoices(state);
                    if (choice == PortActivity.QuitTrading)
                    {
                        if (state.Ship.IsOverloaded)
                        {
                            Overload(state);
                            continue;
                        }

                        break;
                    }

                    PortActions[choice].Run(state, View);

                    UpdatePortStatistics(state);
                }

                // It would be nicer if we could somehow refactor QuitTrading
                // to be just another action, but it currently also contains the
                // at-sea logic.
                QuitTrading(state);
            }
        }

        private static void SplashIntro()
        {
            View.ShowSplashUntilKeyPressed();
        }

        private static InitialState CashOrGuns()
        {
            // Still back to front. Should create both initial states upfront, 
            // send the two to the view to pick which.
            return View.SelectInitialState(Strings.CashOrGuns,
                InitialState.WithCash(),
                InitialState.WithGuns());
        }

        private static void SetPrices(GameState state)
        {
            var basePriceMultiplier = new Dictionary<CargoType, int>
            {
                { CargoType.Opium, 1000 },
                { CargoType.Silk, 100 },
                { CargoType.Arms, 10 },
                { CargoType.General, 1 }
            };

            var basePriceByPort = new Dictionary<Port, Dictionary<CargoType, int>>
            {
                {
                    Port.HongKong, new Dictionary<CargoType, int>
                    {
                        { CargoType.Opium, 11 }, { CargoType.Silk, 11 }, { CargoType.Arms, 12 },
                        { CargoType.General, 10 }
                    }
                },
                {
                    Port.Shanghai, new Dictionary<CargoType, int>
                    {
                        { CargoType.Opium, 16 }, { CargoType.Silk, 14 }, { CargoType.Arms, 16 },
                        { CargoType.General, 11 }
                    }
                },
                {
                    Port.Nagasaki, new Dictionary<CargoType, int>
                    {
                        { CargoType.Opium, 15 }, { CargoType.Silk, 15 }, { CargoType.Arms, 10 },
                        { CargoType.General, 12 }
                    }
                },
                {
                    Port.Saigon, new Dictionary<CargoType, int>
                    {
                        { CargoType.Opium, 14 }, { CargoType.Silk, 16 }, { CargoType.Arms, 11 },
                        { CargoType.General, 13 }
                    }
                },
                {
                    Port.Manila, new Dictionary<CargoType, int>
                    {
                        { CargoType.Opium, 12 }, { CargoType.Silk, 10 }, { CargoType.Arms, 13 },
                        { CargoType.General, 14 }
                    }
                },
                {
                    Port.Singapore, new Dictionary<CargoType, int>
                    {
                        { CargoType.Opium, 10 }, { CargoType.Silk, 13 }, { CargoType.Arms, 14 },
                        { CargoType.General, 15 }
                    }
                },
                {
                    Port.Batavia, new Dictionary<CargoType, int>
                    {
                        { CargoType.Opium, 13 }, { CargoType.Silk, 12 }, { CargoType.Arms, 15 },
                        { CargoType.General, 16 }
                    }
                }
            };

            var basePrices = basePriceByPort[state.Ship.Port];
            foreach (CargoType type in typeof(CargoType).GetEnumValues())
            {
                state.Prices[type] = Money.Of(basePrices[type]) / 2 * (state.Random.Next(3) + 1) * basePriceMultiplier[type];
            }
        }

        public static void UpdatePortStatistics(GameState state)
        {
            var statusPercentage = 100 - (int)((float)state.Ship.Damage / state.Ship.Capacity * 100);

            View.ClearScreen();

            View.ShowPortStatsScreen();

            foreach (CargoType type in typeof(CargoType).GetEnumValues())
            {
                View.ShowWarehouseCargoQuantity(type, state.HongKongWarehouse.UnitsStored(type));
            }

            if (state.Ship.IsOverloaded)
            {
                View.ShowShipOverloaded();
            }
            else
            {
                View.ShowShipAvailable(state.Ship.Available);
            }

            foreach (CargoType type in typeof(CargoType).GetEnumValues())
            {
                View.ShowShipCargoQuantity(type, state.Ship.UnitsStored(type));
            }

            View.ShowCash(state.Cash.FancyNumbers());
            View.ShowWarehouseInUse(state.HongKongWarehouse.TotalUnitsStored);
            View.ShowWarehouseAvailable(state.HongKongWarehouse.Available);
            View.ShowShipGuns(state.Ship.Guns);
            View.ShowCashAtBank(state.CashAtBank.FancyNumbers());

            View.ShowBattleStatus(string.Format(
                Strings.MidMonthDateFormat, state.Calendar.Month.LocalizedName(), state.Calendar.Year));

            View.ShowPort(state.Ship.Port);
            View.ShowDebt(state.Debt.FancyNumbers());

            var status = StatusExtensions.StatusForPercentage(statusPercentage);
            View.ShowShipStatus(status, statusPercentage);
        }

        private static PortActivity PortChoices(GameState state)
        {
            View.ShowTitle(Strings.CompradorsReport);
            View.ShowDetail(
                "Taipan, present prices per unit here are\n   Opium:          Silk:\n   Arms:           General:\n");

            foreach (CargoType type in typeof(CargoType).GetEnumValues())
            {
                View.ShowPrice(type, state.Prices[type]);
            }

            return state.Ship.Port == Port.HongKong
                ? state.Cash + state.CashAtBank >= 1_000_000
                    ? View.AskUserForHomePortActivityIncludingRetirement(Strings.ShallIBuySellVisitBankTransferCargoQuitTradingOrRetire)
                    : View.AskUserForHomePortActivity(Strings.ShallIBuySellVisitBankTransferCargoOrQuitTrading)
                : View.AskUserForPortActivity(Strings.ShallIBuySellOrQuitTrading);
        }

        private static void QuitTrading(GameState state)
        {
            View.ShowTitle(Strings.CompradorsReport);
            View.ShowDetail(Strings.WhereToGo);

            while (true)
            {
                var destination = View.AskUserForPort();
                if (destination == state.Ship.Port)
                {
                    View.ShowFeedback(Strings.YouAreAlreadyHere);
                    SkippableDelay(state, 5);
                }
                else
                {
                    state.Ship.Port = destination;
                    break;
                }
            }
            
            AtSea(state);
        }
        
        private static void AtSea(GameState state)
        {
            View.ShowPort(Port.AtSea);
            View.ShowTitle(Strings.CaptainsReport);

            var battleResult = RunNormalBattleEvent(state, out var booty);

            battleResult = RunLiYuenBattleEvent(state, battleResult, ref booty);

            if (battleResult != BattleResult.NoBattle)
            {
                UpdatePortStatistics(state);
                View.ShowPort(Port.AtSea);

                View.ShowTitle(Strings.CaptainsReport);
                
                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch (battleResult)
                {
                    case BattleResult.Victory:
                        View.ShowDetail(string.Format(Strings.WeCapturedSomeBooty, booty.FancyNumbers()));
                        state.Cash += booty;
                        break;

                    case BattleResult.Retreat:
                        View.ShowDetail(Strings.WeMadeIt);
                        break;
                    
                    // Must be BattleResult.Sunk
                    default:
                        View.ShowDetail(Strings.TheBuggersGotUs);
                        SkippableDelay(state, 5);
                        GameOver(state);
                        state.RunGameLoop = false;
                        return;
                }

                SkippableDelay(state, 3);
            }

            StormAtSeaEvent(state);

            AdvanceTime(state);

            View.ShowDetail(string.Format(Strings.ArrivingAt, state.Ship.Port.LocalizedName()));

            SkippableDelay(state, 3);
        }

        private static BattleResult RunNormalBattleEvent(GameState state, out Money booty)
        {
            var result = BattleResult.NoBattle;
            booty = Money.Zero;

            if (state.Random.Next(state.BattleProbability) != 0) return result;

            var numShips = Math.Min(state.Random.Next(state.Ship.Capacity / 10 + state.Ship.Guns) + 1,
                9999);

            View.ShowDetail(string.Format(Strings.HostileShipsApproaching, numShips));

            SkippableDelay(state, 3);

            result = SeaBattle(state, BattleType.Generic, numShips, out booty);

            if (result != BattleResult.EnemyChasedOffByLiYuen) return result;

            UpdatePortStatistics(state);
            View.ShowPort(Port.AtSea);
            View.ShowTitle(Strings.CaptainsReport);
            View.ShowDetail(Strings.LiYuensFleetDroveThemOff);

            SkippableDelay(state, 3);

            return result;
        }

        private static BattleResult RunLiYuenBattleEvent(GameState state, BattleResult result, ref Money booty)
        {
            if ((result != BattleResult.NoBattle || state.Random.Next(4 + 8 * state.LiYuensTrust) != 0)
                && result != BattleResult.EnemyChasedOffByLiYuen)
                return result;

            View.ShowDetail(Strings.LiYuensPirates);
            SkippableDelay(state, 3);

            if (state.LiYuensTrust > 0)
            {
                View.ShowDetail(Strings.GoodJossTheyLetUsBe);
                SkippableDelay(state, 3);

                // C port from CymonsGames had a return here, but on consulting the original BASIC code,
                // the correct behaviour is to jump past the booty. One way to do that is to pretend
                // that there was no battle.
                return BattleResult.NoBattle;
            }

            var numShips = state.Random.Next(state.Ship.Capacity / 5 + state.Ship.Guns) + 5;
            View.ShowDetail(string.Format(Strings.XShipsOfLiYuensPirateFleet, numShips));
            SkippableDelay(state, 3);
            return SeaBattle(state, BattleType.LiYuen, numShips, out booty);
        }

        private static void StormAtSeaEvent(GameState state)
        {
            if (state.Random.Next(10) != 0) return;

            View.ShowDetail(Strings.Storm);
            SkippableDelay(state, 3);

            if (state.Random.Next(30) == 0)
            {
                View.ShowDetail(Strings.IThinkWereGoingDown);
                SkippableDelay(state, 3);

                // Compared with original logic again, seems to match.
                // ReSharper disable once PossibleLossOfFraction
                if (state.Ship.Damage / state.Ship.Capacity * 3.0 * state.Random.NextDouble() >= 1.0)
                {
                    View.ShowDetail(Strings.WereGoingDown);
                    SkippableDelay(state, 5);
                    GameOver(state);
                }
            }

            View.ShowDetail(Strings.WeMadeIt2);
            SkippableDelay(state, 3);

            if (state.Random.Next(3) != 0) return;

            var originalDestination = state.Ship.Port;
            while (state.Ship.Port == originalDestination)
            {
                state.Ship.Port = (Port)state.Random.Next(1, 8);
            }

            View.ShowDetail(string.Format(Strings.WeveBeenBlownOffCourseTo, state.Ship.Port.LocalizedName()));

            SkippableDelay(state, 3);
        }

        private static void AdvanceTime(GameState state)
        {
            state.Calendar.Advance();
            if (state.Calendar.Month == Month.Jan)
            {
                state.EnemyCountMultiplier += 100;
                state.EnemyDamageMultiplier += 0.5;
            }

            state.Debt += state.Debt * 0.1;
            state.CashAtBank += state.CashAtBank * 0.005;
            SetPrices(state);
        }

        private static void Overload(GameState state)
        {
            View.ShowTitle(Strings.CompradorsReport);
            View.ShowDetail(Strings.YourShipIsOverloaded);
            SkippableDelay(state, 5);
        }

        private static BattleResult SeaBattle(GameState state, BattleType battleType, int numShips, out Money booty)
        {
            var numOnScreen = 0;
            var shipsOnScreen = new Dictionary<LorchaPosition, Lorcha>();
            var time = state.Calendar.MonthsSinceStart;
            var s0 = numShips;
            var ok = 0;
            var ik = 1;

            booty = Money.Of(time / 4 * 1000 * numShips + state.Random.Next(1000) + 250);

            foreach (var position in LorchaPosition.All)
            {
                shipsOnScreen[position] = null;
            }

            var orders = View.CheckForOrders();
            FightStats(state, numShips, orders);

            while (numShips > 0)
            {
                var statusPercentage = (int)(100 - (double)state.Ship.Damage / state.Ship.Capacity * 100);
                if (statusPercentage <= 0)
                {
                    return BattleResult.Sunk;
                }

                View.ShowBattleStatus(string.Format(
                    Strings.CurrentSeaworthiness,
                    StatusExtensions.StatusForPercentage(statusPercentage),
                    statusPercentage));

                // Spawns more Lorcha
                // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
                foreach (var position in LorchaPosition.All)
                {
                    if (numShips <= numOnScreen) continue;
                    if (shipsOnScreen[position] != null) continue;
                    
                    Thread.Sleep(100);
                    var lorcha = new Lorcha((int)(state.EnemyCountMultiplier * state.Random.NextDouble() + 20));
                    shipsOnScreen[position] = lorcha;
                    View.DrawLorcha(position, lorcha);
                    numOnScreen++;
                }

                if (numShips > numOnScreen)
                {
                    View.ShowMoreShipsIndicator();
                }
                else
                {
                    View.HideMoreShipsIndicator();
                }

                // TODO: Any input during 3 seconds is fine. How to model?
                // orders = _view.AskForOrders();
                // SkippableDelay(3);
                View.ShowBattleStatus(Strings.WhatShallWeDo);
                orders = View.CheckForOrders();

                FightStats(state, numShips, orders);

                switch (orders)
                {
                    case Orders.Fight when state.Ship.Guns > 0:
                    {
                        var shipsSunk = 0;

                        ok = 3;
                        ik = 1;

                        View.ShowBattleStatus(Strings.WellFightEm);

                        SkippableDelay(state, 3);

                        View.ShowBattleStatus(Strings.WereFiringOnThem);

                        for (var i = 1; i <= state.Ship.Guns; i++)
                        {
                            if (shipsOnScreen.All(entry => entry.Value == null))
                            {
                                foreach (var position in LorchaPosition.All)
                                {
                                    if (numShips > numOnScreen)
                                    {
                                        if (shipsOnScreen[position] == null)
                                        {
                                            Thread.Sleep(100);
                                            var lorcha = new Lorcha((int)(state.EnemyCountMultiplier * state.Random.NextDouble() + 20));
                                            shipsOnScreen[position] = lorcha;
                                            View.DrawLorcha(position, lorcha);
                                            numOnScreen++;
                                        }
                                    }
                                }
                            }

                            if (numShips > numOnScreen)
                            {
                                View.ShowMoreShipsIndicator();
                            }
                            else
                            {
                                View.HideMoreShipsIndicator();
                            }

                            // Yikes, bit of an expensive way to choose but OK
                            LorchaPosition targeted;
                            do
                            {
                                targeted = LorchaPosition.All[state.Random.Next(LorchaPosition.All.Count)];
                            } while (shipsOnScreen[targeted] == null);

                            View.DrawBlast(targeted);
                            Thread.Sleep(100);

                            View.DrawLorcha(targeted, shipsOnScreen[targeted]);
                            Thread.Sleep(100);

                            View.DrawBlast(targeted);
                            Thread.Sleep(100);

                            View.DrawLorcha(targeted, shipsOnScreen[targeted]);
                            Thread.Sleep(100);

                            shipsOnScreen[targeted].TakeDamage(state.Random.Next(30) + 10);

                            if (shipsOnScreen[targeted].IsSunk)
                            {
                                numOnScreen--;
                                numShips--;
                                shipsSunk++;
                                shipsOnScreen[targeted] = null;

                                Thread.Sleep(100);

                                View.SinkLorcha(targeted);

                                if (numShips == numOnScreen)
                                {
                                    View.HideMoreShipsIndicator();
                                }

                                FightStats(state, numShips, orders);
                            }

                            if (numShips == 0)
                            {
                                i += state.Ship.Guns;
                            }
                            else
                            {
                                Thread.Sleep(500);
                            }
                        }

                        if (shipsSunk > 0)
                        {
                            View.ShowBattleStatus(string.Format(Strings.SunkXOfTheBuggers, shipsSunk));
                        }
                        else
                        {
                            View.ShowBattleStatus(Strings.HitEmButDidntSinkEm);
                        }

                        SkippableDelay(state, 3);

                        if (state.Random.Next(s0) > numShips * 0.6 / (int)battleType && numShips > 2)
                        {
                            var ran = state.Random.Next(numShips / 3 / (int)battleType) + 1;

                            numShips -= ran;
                            FightStats(state, numShips, orders);

                            View.ShowBattleStatus(string.Format(Strings.XRanAway, ran));

                            if (numShips <= 10)
                            {
                                foreach (var position in LorchaPosition.AllBackwards)
                                {
                                    if (numOnScreen <= numShips || shipsOnScreen[position] == null) continue;

                                    shipsOnScreen[position] = null;
                                    numOnScreen--;

                                    View.ClearLorcha(position);

                                    Thread.Sleep(100);
                                }

                                if (numShips == numOnScreen)
                                {
                                    View.HideMoreShipsIndicator();
                                }
                            }

                            SkippableDelay(state, 3);

                            orders = View.CheckForOrders();
                        }

                        break;
                    }
                    case Orders.Fight when state.Ship.Guns == 0:
                    {
                        View.ShowBattleStatus(Strings.WeHaveNoGuns);
                        SkippableDelay(state, 3);
                        break;
                    }
                    case Orders.ThrowCargo:
                    {
                        var amount = 0;

                        View.ShowDetail(string.Format(
                            Strings.YouHaveTheFollowingOnBoard,
                            state.Ship.UnitsStored(CargoType.Opium),
                            state.Ship.UnitsStored(CargoType.Silk),
                            state.Ship.UnitsStored(CargoType.Arms), 
                            state.Ship.UnitsStored(CargoType.General)));

                        View.ShowBattleStatus(Strings.WhatShallIThrowOverboard);

                        var choice = View.AskUserForCargoTypeOrAll();

                        int total;
                        if (choice != CargoToThrow.Everything)
                        {
                            View.ShowBattleStatus(Strings.HowMuch);
                            amount = View.AskUserForNumber();
                            if (state.Ship.HasStored((CargoType)choice) &&
                                (amount == -1 || amount > state.Ship.UnitsStored((CargoType)choice)))
                            {
                                amount = state.Ship.UnitsStored((CargoType)choice);
                            }

                            total = state.Ship.UnitsStored((CargoType)choice);
                        }
                        else
                        {
                            total = state.Ship.TotalUnitsStored;
                        }

                        if (total > 0)
                        {
                            View.ShowBattleStatus(Strings.LetsHopeWeLoseEm);
                            if (choice != CargoToThrow.Everything)
                            {
                                state.Ship.DropCargo((CargoType)choice, amount);
                                ok += amount / 10;
                            }
                            else
                            {
                                state.Ship.ClearAllCargo();
                                ok += total / 10;
                            }

                            View.ShowDetail("");
                            SkippableDelay(state, 3);
                        }
                        else
                        {
                            View.ShowBattleStatus(Strings.TheresNothingThere);
                            View.ShowDetail("");
                            SkippableDelay(state, 3);
                        }

                        break;
                    }
                }

                if (orders is Orders.Run or Orders.ThrowCargo)
                {
                    if (orders == Orders.Run)
                    {
                        View.ShowBattleStatus(Strings.WellRun);
                        SkippableDelay(state, 3);
                    }

                    ok += ik++;
                    if (state.Random.Next(ok) > state.Random.Next(numShips))
                    {
                        View.ShowBattleStatus(Strings.WeGotAwayFromEm);
                        SkippableDelay(state, 3);
                        numShips = 0;
                    }
                    else
                    {
                        View.ShowBattleStatus(Strings.CouldntLoseEm);
                        SkippableDelay(state, 3);

                        if (numShips > 2 && state.Random.Next(5) == 0)
                        {
                            var lost = state.Random.Next(numShips / 2) + 1;

                            numShips -= lost;
                            FightStats(state, numShips, orders);
                            View.ShowBattleStatus(string.Format(Strings.ButWeEscapedFromXOfEm, lost));

                            if (numShips <= 10)
                            {
                                foreach (var position in LorchaPosition.AllBackwards)
                                {
                                    if (numOnScreen <= numShips || shipsOnScreen[position] == null) continue;

                                    shipsOnScreen[position] = null;
                                    numOnScreen--;

                                    View.ClearLorcha(position);
                                    Thread.Sleep(100);
                                }

                                if (numShips == numOnScreen)
                                {
                                    View.HideMoreShipsIndicator();
                                }
                            }

                            SkippableDelay(state, 3);
                            orders = View.CheckForOrders();
                        }
                    }
                }

                if (numShips > 0)
                {
                    View.ShowBattleStatus(Strings.TheyreFiringOnUs);

                    SkippableDelay(state, 3);

                    View.ShowBeingHit();

                    FightStats(state, numShips, orders);
                    foreach (var position in LorchaPosition.All)
                    {
                        var ship = shipsOnScreen[position];
                        if (ship != null)
                        {
                            View.DrawLorcha(position, ship);
                        }
                    }

                    if (numShips > numOnScreen)
                    {
                        View.ShowMoreShipsIndicator();
                    }
                    else
                    {
                        View.HideMoreShipsIndicator();
                    }

                    View.ShowBattleStatus(Strings.WeveBeenHit);

                    SkippableDelay(state, 3);

                    var i = numShips > 15 ? 15 : numShips;
                    if (state.Ship.Guns > 0 && (state.Random.Next(100) < (float)state.Ship.Damage / state.Ship.Capacity * 100 ||
                                               (float)state.Ship.Damage / state.Ship.Capacity * 100 > 80))
                    {
                        i = 1;

                        state.Ship.LoseGun();

                        FightStats(state, numShips, orders);
                        View.ShowBattleStatus(Strings.TheBuggersHitAGun);
                        FightStats(state, numShips, orders);

                        SkippableDelay(state, 3);
                    }

                    state.Ship.TakeDamage(state.Random.Next((int)(state.EnemyDamageMultiplier * i * (int)battleType)) + i / 2);
                    if (battleType != BattleType.LiYuen && state.Random.Next(20) == 0)
                    {
                        return BattleResult.EnemyChasedOffByLiYuen;
                    }
                }
            }

            orders = View.CheckForOrders();
            if (orders == Orders.Fight)
            {
                FightStats(state, numShips, orders);
                View.ShowBattleStatus(Strings.WeGotEmAll);
                SkippableDelay(state, 3);

                return BattleResult.Victory;
            }
            else
            {
                return BattleResult.Retreat;
            }
        }

        private static void FightStats(GameState state, int ships, Orders orders)
        {
            View.ShowShipCount(ships);

            // TODO: Big .NET problem with internationalisation of plural forms. Can ICU help here?
            View.ShowShipsAttacking(ships == 1
                ? string.Format(Strings.ShipsAttacking_One, ships)
                : string.Format(Strings.ShipsAttacking_Other, ships));

            View.ShowCurrentOrders(string.Format(Strings.YourOrdersAreTo, orders.LocalizedName()));
            View.ShowShipGuns(state.Ship.Guns);
        }

        public static void GameOver(GameState state)
        {
            var years = state.Calendar.YearsSinceStart;
            var months = state.Calendar.MonthsSinceYearStart;
            var time = state.Calendar.MonthsSinceStart;

            var totalEquity = state.Cash + state.CashAtBank - state.Debt;
            var finalScore = totalEquity.Amount / 100 / time;
            var rating = RatingExtensions.ForFinalCash(state.Cash);

            // TODO: More pluralisation woes
            View.ShowFinalStatsScreen(state.Cash.FancyNumbers(), state.Ship.Capacity, state.Ship.Guns, years, months, finalScore,
                rating);

            var choice = View.AskYesNo();
            if (choice == YesNo.Yes)
            {
                Main();
            }
        }

        public static void SkippableDelay(GameState state, int seconds)
        {
            View.WaitSecondsUnlessAKeyIsPressed(seconds);
        }
    }
}