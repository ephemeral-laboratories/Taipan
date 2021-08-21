namespace Source.Model
{
    // An enemy pirate ship
    // https://en.wikipedia.org/wiki/Lorcha_(boat)
    public class Lorcha
    {
        public Lorcha(int initialHitPoints)
        {
            HitPoints = initialHitPoints;
        }

        private int HitPoints { get; set; }

        public bool IsSunk => HitPoints <= 0;

        public void TakeDamage(int damage)
        {
            HitPoints -= damage;
        }
    }
}