namespace Assets.Scripts.Data.Model.Objects
{
    public sealed class Vehicle : MoveableObject
    {
        public string BrandModel;
        public string RegNumber;

        public override string ToString()
        {
            return string.Format("{0} - {1}\r\n" +
                                 "Address: {2}\r\n" +
                                 "Location: {3:0.######}; {4:0.######}\r\n" +
                                 "Course: {5:0.##}, Speed: {6:0.##} km/h",
                BrandModel, RegNumber, Address, Latitude, Longitude, Course, Speed);
        }
    }
}