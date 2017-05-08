using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public sealed class HeightCorrector : MonoBehaviour
    {
        public Color DebugColor = Color.white;

        private const float RaycastHeight = 1000f;        

        private void Start()
        {
            if (!"Ignore Raycast".Equals(LayerMask.LayerToName(gameObject.layer)))
                Debug.LogWarning("GameObject with HeightCorrector should be raycast transparent.");
        }

        private void Update()
        {
            RaycastHit hit;
            var origin = transform.position;
            origin.Set(origin.x, RaycastHeight, origin.z);
            if (Physics.Raycast(origin, Vector3.down, out hit))
            {
                var targetLocation = hit.point;                
                transform.position = targetLocation;

                Debug.DrawLine(origin, hit.point, DebugColor);               
            }
        }
    }
}