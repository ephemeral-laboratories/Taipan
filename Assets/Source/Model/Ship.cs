namespace Source.Model
{
    // The ship's hold can be used for storing cargo. It also holds guns.
    // The more guns you have, the less space you have for cargo.
    public class Ship : CargoStorage
    {
        public int Damage { get; private set; }

        public int Guns { get; private set; }

        public Port Port { get; set; } = Port.HongKong;

        public override int Available => base.Available - Guns * 10;

        public bool IsOverloaded => Available < 0;
        
        public Ship(int capacity) : base(capacity)
        {
        }

        public void AddGun()
        {
            AddGuns(1);
        }

        public void AddGuns(int number)
        {
            Guns += number;
        }

        public void LoseGun()
        {
            Guns -= 1;
        }

        public void ExpandHoldBy(int extra)
        {
            Capacity += extra;
        }

        public void DropCargo(CargoType choice, int amount)
        {
            RemoveCargo(choice, amount);
        }

        public void TakeDamage(int amount)
        {
            Damage += amount;
        }

        public void Repair(int amount)
        {
            Damage -= amount;
            Damage = Damage < 0 ? 0 : Damage;
        }

        public void ResetDamage()
        {
            Damage = 0;
        }
    }
}