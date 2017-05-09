namespace Assets.Scripts.Data.Model.Objects
{
    public sealed class Vehicle : MoveableObject
    {
        public string BrandModel;
        public string RegNumber;

        public override string ToString()
        {
            return string.Format("Vehicle: {0} with reg number {1}\r\n" +
                                 "Address: {2}\r\n" +
                                 "LatLng: {3:0.######}; {4:0.######}",
                BrandModel, RegNumber, GetNearestAddress(), Latitude, Longitude);
        }
    }
}