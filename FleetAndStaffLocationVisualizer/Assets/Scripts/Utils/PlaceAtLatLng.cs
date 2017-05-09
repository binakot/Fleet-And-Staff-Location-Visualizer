using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public sealed class PlaceAtLatLng : MonoBehaviour
    {
        public double Latitude;
        public double Longitude;

        private void Update()
        {
            transform.MoveToGeocoordinate(Latitude, Longitude, Vector2d.zero);            
        }
    }
}
