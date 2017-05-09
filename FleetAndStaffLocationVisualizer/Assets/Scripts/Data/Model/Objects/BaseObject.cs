using System;
using Assets.Scripts.Data.Interfaces;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

namespace Assets.Scripts.Data.Model.Objects
{
    public abstract class BaseObject : MonoBehaviour, IPlaceable
    {
        public int Id;
        public double Latitude;
        public double Longitude;
        public float Course;

        public virtual void PlaceTo(double lat, double lng, float course)
        {
            Latitude = lat;
            Longitude = lng;
            Course = course;

            transform.MoveToGeocoordinate(Latitude, Longitude, Vector2d.zero);
            transform.Rotate(Vector3.up, Course);
        }

        public string GetNearestAddress()
        {
            throw new NotImplementedException(); // TODO Use geo-coding to get a nearest address by current location.
        }
    }
}