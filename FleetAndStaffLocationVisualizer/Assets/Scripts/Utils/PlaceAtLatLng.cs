using Mapbox.Unity.MeshGeneration;
using Mapbox.Unity.Utilities;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public sealed class PlaceAtLatLng : MonoBehaviour
    {
        public double Latitude;
        public double Longitude;

        private void Update()
        {
            transform.MoveToGeocoordinate(Latitude, Longitude, MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor);            
        }
    }
}
