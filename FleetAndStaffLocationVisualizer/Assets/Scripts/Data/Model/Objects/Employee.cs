namespace Assets.Scripts.Data.Model.Objects
{
    public sealed class Employee : MoveableObject
    {
        public string Name;
        public string JobPosition;
        public string Department;

        public override string ToString()
        {
            return string.Format("{0} ({1} from {2})\r\n" +
                                 "Address: {3}\r\n" +
                                 "Location: {4:0.######}; {5:0.######}\r\n" +
                                 "Course: {6:0.##}, Speed: {7:0.##} km/h",
                Name, JobPosition, Department, GetNearestAddress(), Latitude, Longitude, Course, Speed);
        }
    }
}