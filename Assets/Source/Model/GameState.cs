using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Source.Model.PortActions;
using Source.Model.PortArrivalEvents;
using Random = System.Random;
using Source.View;
using UnityEngine;

namespace Source.Model
{
    public class GameState
    {
        public Random Random { get; } = new Random();

        public string FirmName { get; set; }

        public Money Cash { get; set; } = Money.Zero;

        public Money CashAtBank { get; set; } = Money.Zero;

        public Money Debt { get; set; } = Money.Zero;
        
        public double EnemyCountMultiplier { get; set; } = 20.0;

        public double EnemyDamageMultiplier { get; set; } = 0.5;

        public Dictionary<CargoType, Money> Prices { get; } = new Dictionary<CargoType, Money>();

        public CargoStorage HongKongWarehouse { get; } = new CargoStorage(10_000);

        public Ship Ship { get; } = new Ship(60);

        public int BattleProbability { get; set; }

        public Calendar Calendar { get; set; } = Calendar.AtStart();

        public int LiYuensTrust { get; set; }

        public bool BrotherWuWarning { get; set; }

        public int BrotherWuBailoutCount { get; set; }

    }
}