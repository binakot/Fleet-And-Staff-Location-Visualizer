using Assets.Scripts.Data.Interfaces;
using UnityEngine;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Unity.Utilities;
using Mapbox.Unity;
using Mapbox.Geocoding;
using Mapbox.Utils;

namespace Assets.Scripts.Data.Model.Objects
{
    public abstract class BaseObject : MonoBehaviour, IPlaceable
    {
        public int Id;
        public double Latitude;
        public double Longitude;
        public float Course;
        public string Address;

        private Vector2d _coordinate;
        private ReverseGeocodeResource _resource;        

        private void Awake()
        {
            _coordinate = new Vector2d();
            _resource = new ReverseGeocodeResource(_coordinate);
        }        

        public void PlaceTo(double lat, double lng, float course)
        {
            Latitude = lat;
            Longitude = lng;
            Course = course;

            transform.MoveToGeocoordinate(Latitude, Longitude, MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor);
            transform.Rotate(Vector3.up, Course);

            UpdateNearestAddress();
        }

        protected void UpdateNearestAddress()
        {
            if (MapboxAccess.Instance.Geocoder == null)
                return;

            _coordinate.x = Latitude;
            _coordinate.y = Longitude;
            _resource.Query = _coordinate;
            MapboxAccess.Instance.Geocoder.Geocode(_resource, HandleGeocoderResponse);
        }

        private void HandleGeocoderResponse(ReverseGeocodeResponse res)
        {
            Address = res.Features[0].PlaceName;
        }
    }
}