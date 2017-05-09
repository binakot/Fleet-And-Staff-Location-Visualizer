namespace Assets.Scripts.Data.Model.Telemetrics
{
    public sealed class TrackPoint
    {
        public int Id { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public float Speed { get; private set; }
        public float Course { get; private set; }

        public TrackPoint(int id, double lat, double lng, float speed, float course)
        {
            Id = id;
            Latitude = lat;
            Longitude = lng;
            Speed = speed;
            Course = course;
        }
    }
}