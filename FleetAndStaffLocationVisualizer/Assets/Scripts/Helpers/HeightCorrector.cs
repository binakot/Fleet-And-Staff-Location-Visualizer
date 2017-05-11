using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public sealed class HeightCorrector : MonoBehaviour
    {
        private const float RaycastYOffset = 1000f;

        public string[] RaycastLayers = { "Terrain" };
        public Color DebugColor = Color.white;

        private void Update()
        {
            RaycastHit hit;
            var origin = transform.position + new Vector3(0, RaycastYOffset, 0);
            if (Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask(RaycastLayers)))
            {
                var targetLocation = hit.point;                
                transform.position = targetLocation;

                Debug.DrawLine(origin, hit.point, DebugColor);               
            }
        }
    }
}