using Assets.Scripts.Data.Interfaces;
using Assets.Scripts.Data.Model.Objects;
using Assets.Scripts.Data.Model.Telemetrics;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Providers
{
    public sealed class TestDataProvider : IDataProvider
    {
        public List<Building> LoadBuildings()
        {
            return new List<Building>(0);
        }

        public List<MoveableObject> LoadMoveableObjects()
        {
            var moveableObjects = new List<MoveableObject>
            {
                BuildEmployee(0, 43.586955, 39.716298, 5f, 139.119f, "Kristin Garcia", "Accountant", "Accounting"),
                BuildEmployee(1, 43.585888, 39.719431, 10f, 29.307f, "Diane Woods", "Manager", "Customer service"),
                BuildEmployee(2, 43.589140, 39.720010, 0f, 30.2f, "Clifton Ryan", "Director", "Leadership"),
                BuildVehicle(3, 43.599800, 39.730257, 90f, -13.167f, "Toyota Celica", "A 111 BC 23"),
                BuildVehicle(4, 43.592829, 39.737904, 60f, -48.449f, "Honda Civic", "D 222 EF 93"),
                BuildVehicle(5, 43.584737, 39.738638, 45f, 115.004f, "Opel Astra", "G 333 HI 123")
            };
            return moveableObjects;
        }

        public List<TrackPoint> UpdateMoveableObjectLocations()
        {
            var locations = new List<TrackPoint>
            {
                new TrackPoint(0, 43.581387, 39.722955, 5f, 139.119f),
                new TrackPoint(1, 43.593074, 39.725001, 10f, 29.307f),
                new TrackPoint(2, 43.593620, 39.723616, 0f, 30.2f),
                new TrackPoint(3, 43.601215, 39.729797, 90f, -13.167f),
                new TrackPoint(4, 43.593942, 39.736162, 60f, -48.449f),
                new TrackPoint(5, 43.583654, 39.741845, 45f, 115.004f)
            };
            return locations;
        }

        private static Employee BuildEmployee(int id, double lat, double lng, float speed, float course,
            string name, string job, string department)
        {
            var go = new GameObject(string.Format("{0} ({1})", name, job));
            var employee = go.AddComponent<Employee>();
            employee.Id = id;
            employee.Latitude = lat;
            employee.Longitude = lng;
            employee.Speed = speed;
            employee.Course = course;
            employee.Name = name;
            employee.JobPosition = job;
            employee.Department = department;
            return employee;
        }

        private static Vehicle BuildVehicle(int id, double lat, double lng, float speed, float course,
            string brandModel, string regNumber)
        {
            var go = new GameObject(string.Format("{0} {1}", brandModel, regNumber));
            var vehicle = go.AddComponent<Vehicle>();
            vehicle.Id = id;
            vehicle.Latitude = lat;
            vehicle.Longitude = lng;
            vehicle.Speed = speed;
            vehicle.Course = course;
            vehicle.BrandModel = brandModel;
            vehicle.RegNumber = regNumber;
            return vehicle;
        }
    }
}