namespace Assets.Scripts.Data.Model.Objects
{
    public sealed class Employee : MoveableObject
    {
        public string Name;
        public string JobPosition;
        public string Department;

        public override string ToString()
        {
            return string.Format("Employee: {0} on position {1} from department {2}\r\n" +
                                 "Address: {3}\r\n" +
                                 "LatLng: {4:0.######}; {5:0.######}",
                Name, JobPosition, Department, GetNearestAddress(), Latitude, Longitude);
        }
    }
}