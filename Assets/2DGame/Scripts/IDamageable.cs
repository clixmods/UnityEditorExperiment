namespace _2DGame.Scripts
{
    public interface IDamageable
    {
        public float Health { get; }
        public void DoDamage(int amount);
    }
}