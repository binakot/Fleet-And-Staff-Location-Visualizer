namespace Assets.Scripts.Data.Interfaces
{
    public interface IMovable
    {
        void MoveTo(double lat, double lng, float speed, float course);
    }
}
