using Mapbox.Unity.MeshGeneration;
using System;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    [RequireComponent(typeof(MapController))]
    public sealed class MapStyleSelector : MonoBehaviour
    {
        public MapStyleType MapStyle = MapStyleType.Stylized;

        public enum MapStyleType
        {
            Stylized,
            Realistic
        }

        public MapVisualization StylizedMapVisualization;
        public MapVisualization RealisticMapVisualization;

        // TODO Implement the real-time style changing. Now there is the pre-compile style changing only.
        private void Awake()
        {
            var mapController = GetComponent<MapController>();
            switch (MapStyle)
            {
                case MapStyleType.Stylized:
                    {
                        mapController.MapVisualization = StylizedMapVisualization;
                        mapController.Awake();

                        RenderSettings.fog = true;

                        break;
                    }

                case MapStyleType.Realistic:
                    {
                        mapController.MapVisualization = RealisticMapVisualization;
                        mapController.Awake();

                        RenderSettings.fog = false;

                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}