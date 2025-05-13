using System;
using System.Collections.Generic;
using System.Linq;

namespace Source.Model
{
    public class CargoStorage
    {
        public int Capacity { get; set; }

        private readonly Dictionary<CargoType, int> _storage = new();
        
        public int TotalUnitsStored
        {
            get
            {
                return _storage.Sum(x => x.Value);
            }
        }

        public virtual int Available => Capacity - TotalUnitsStored;

        public bool IsEmpty
        {
            get
            {
                return _storage.All(x => x.Value == 0);
            }
        }

        public bool IsNotEmpty => !IsEmpty;

        public CargoStorage(int capacity)
        {
            Capacity = capacity;
        }

        public bool HasStored(CargoType type)
        {
            return _storage[type] > 0;
        }

        public int UnitsStored(CargoType type)
        {
            return _storage[type];
        }

        public void AddCargo(CargoType type, int amount)
        {
            _storage[type] += amount;
        }

        public bool RemoveCargo(CargoType type, int quantity)
        {
            if (_storage[type] < quantity)
            {
                throw new ArgumentException("Assumes you checked before");
            }

            _storage[type] -= quantity;
            return true;
        }

        public void ClearAllCargo()
        {
            _storage.Clear();
        }
    }
}