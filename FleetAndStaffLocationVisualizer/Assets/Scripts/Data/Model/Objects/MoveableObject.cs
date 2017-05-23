using System.Collections;
using Assets.Scripts.Data.Interfaces;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Unity.Utilities;
using UnityEngine;

namespace Assets.Scripts.Data.Model.Objects
{
    public abstract class MoveableObject : BaseObject, IMovable
    {
        public float Speed;

        public GameObject TargetWayPoint;

        public void MoveTo(double lat, double lng, float speed, float course)
        {
            Latitude = lat;
            Longitude = lng;
            Course = course;
            Speed = speed;

            StartCoroutine(MoveToTargetPoint());
        }

        private IEnumerator MoveToTargetPoint()
        {
            TargetWayPoint.transform.MoveToGeocoordinate(Latitude, Longitude, MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor);

            var from = transform.position;
            var to = TargetWayPoint.transform.position;
            if (!from.Equals(to))
                transform.rotation = Quaternion.LookRotation(to - from); // Rotation based on the factual moving direction.

            var timer = 0f;
            while (timer < 1f)
            {
                timer += Time.deltaTime / DataManager.Instance.DataUpdateFrequency;
                transform.position = Vector3.Lerp(from, to, timer);
                yield return 0;
            }

            //transform.Rotate(Vector3.up, course); // Rotation based on the course value from data provider.

            UpdateNearestAddress();
        }
    }
}