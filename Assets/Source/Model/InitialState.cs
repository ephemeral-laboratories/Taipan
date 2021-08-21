namespace Source.Model
{
    public class InitialState
    {
        private InitialState(Money cash, Money debt, int hold, int guns, int liYuensTrust, int battleProbability)
        {
            Cash = cash;
            Debt = debt;
            Hold = hold;
            Guns = guns;
            LiYuensTrust = liYuensTrust;
            BattleProbability = battleProbability;
        }

        private Money Cash { get; }
        private Money Debt { get; }
        private int Hold { get; }
        private int Guns { get; }
        private int LiYuensTrust { get; }
        private int BattleProbability { get; }

        public static InitialState WithCash()
        {
            return new InitialState(Money.Of(400), Money.Of(5000), 60, 0, 0, 10);
        }

        public static InitialState WithGuns()
        {
            return new InitialState(Money.Zero, Money.Zero, 10, 5, 1, 7);
        }

        public GameState NewGame(string firmName)
        {
            var state = new GameState
            {
                FirmName = firmName,
                Cash = Cash,
                Debt = Debt,
                LiYuensTrust = LiYuensTrust,
                BattleProbability = BattleProbability,
                Ship =
                {
                    Capacity = Hold,
                }
            };
            state.Ship.AddGuns(Guns);
            state.Ship.Capacity = Hold;
            return state;
        }
    }
}