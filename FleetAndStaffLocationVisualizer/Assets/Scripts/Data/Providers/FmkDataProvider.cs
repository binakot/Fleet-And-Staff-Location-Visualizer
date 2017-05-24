using Assets.Scripts.Data.Interfaces;
using Assets.Scripts.Data.Model.Objects;
using Assets.Scripts.Data.Model.Telemetrics;
using Assets.Scripts.Data.Providers.Fmk;
using Assets.Scripts.Data.Providers.Fmk.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Providers
{
    /// <summary>
    /// The author is working in First Monitoring Company. http://firstmk.ru
    /// Current provider let to load navigation data from company's servers.
    /// This data is confidential, therefore the data transfer protocol is not present in this repository.
    /// </summary>
    public sealed class FmkDataProvider : IDataProvider
    {
        private FmkApiClient _client;

        public FmkDataProvider()
        {
            var credentials = Resources.Load<FmkCredentials>("Storages/FMK");
            _client = new FmkApiClient(credentials.host, credentials.port, credentials.Key, credentials.Login, credentials.Password);

            if (!_client.IsAuthSuccess())
                throw new Exception("Incorrect credentials");
        }

        public List<Building> LoadBuildings()
        {
            return new List<Building>(0); // Buildings are not supported.
        }

        public List<MoveableObject> LoadMoveableObjects()
        {
            var moveableObjects = new List<MoveableObject>(0);

            var objects = _client.GetDevices();
            foreach (var obj in objects)
            {
                if (obj is Device)
                {
                    var device = (Device)obj;
                    if (device.IsEmployee)
                    {
                        moveableObjects.Add(
                            BuildEmployee(device.Id, device.Latitude, device.Longitude, device.Speed, device.Bearing,
                                          device.Driver, device.RegSign, device.Model));
                    }
                    else
                    {
                        moveableObjects.Add(
                            BuildVehicle(device.Id, device.Latitude, device.Longitude, device.Speed, device.Bearing,
                                         device.Model, device.RegSign));
                    }
                }
            }

            return moveableObjects;
        }

        public List<TrackPoint> UpdateMoveableObjectLocations()
        {
            var trackPoints = new List<TrackPoint>(0);

            var objects = _client.GetDevices();
            foreach (var obj in objects)
            {
                if (obj is Device)
                {
                    var device = (Device)obj;
                    trackPoints.Add(
                        new TrackPoint(device.Id, device.Latitude, device.Longitude, device.Speed, device.Bearing));
                }
            }

            return trackPoints;
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