using Assets.Scripts.Helpers;
using Mapbox.Unity.Utilities;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public sealed class PlaceAtLatLng : MonoBehaviour
    {
        public double Latitude;
        public double Longitude;

        private void Update()
        {
            transform.MoveToGeocoordinate(Latitude, Longitude, SceneManager.Instance.Map.CenterMercator, SceneManager.Instance.Map.WorldRelativeScale);
        }
    }
}