using Assets.Scripts.Data.Interfaces;

namespace Assets.Scripts.Data.Model.Objects
{
    public abstract class MoveableObject : BaseObject, IMovable
    {
        public float Speed;

        public void MoveTo(double lat, double lng, float speed, float course)
        {
            // TODO Implement a smooth movement animation from current location to the target one.
        }
    }
}