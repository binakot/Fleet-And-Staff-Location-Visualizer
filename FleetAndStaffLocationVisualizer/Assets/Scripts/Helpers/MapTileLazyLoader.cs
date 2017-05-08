using Mapbox.Unity.MeshGeneration;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    [RequireComponent(typeof(MapController))]
    public sealed class MapTileLazyLoader : MonoBehaviour
    {        
        public int Range = 3;

        private MapController _mapController;
        private Camera _camera;
        private Vector3 _cameraTarget;
        private Vector2 _currentTile;
        private Vector2 _cachedTile;
        private Ray _ray;
        private float _hitDistance;
        private Plane _yPlane;        
        
        private void Start()
        {
            _mapController = this.GetComponent<MapController>();
            _camera = Camera.main;
            _yPlane = new Plane(Vector3.up, Vector3.zero);
        }

        private void Update()
        {
            _ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (_yPlane.Raycast(_ray, out _hitDistance))
            {
                _cameraTarget = _ray.GetPoint(_hitDistance) / MapController.WorldScaleFactor;
                _currentTile = Conversions.MetersToTile(new Vector2d(MapController.ReferenceTileRect.Center.x + _cameraTarget.x, MapController.ReferenceTileRect.Center.y + _cameraTarget.z), _mapController.Zoom);
                if (_currentTile != _cachedTile)
                {
                    for (int i = -Range; i <= Range; i++)
                    {
                        for (int j = -Range; j <= Range; j++)
                        {
                            _mapController.Request(new Vector2(_currentTile.x + i, _currentTile.y + j), _mapController.Zoom);
                        }
                    }
                    _cachedTile = _currentTile;
                }
            }
        }
    }
}