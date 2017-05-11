using Assets.Scripts.Data.Interfaces;
using UnityEngine;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Unity.Utilities;

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

            transform.MoveToGeocoordinate(Latitude, Longitude, MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor);
            transform.Rotate(Vector3.up, Course);
        }

        public string GetNearestAddress()
        {
            return "TODO"; // TODO Use geo-coding to get a nearest address by current location.
        }
    }
}