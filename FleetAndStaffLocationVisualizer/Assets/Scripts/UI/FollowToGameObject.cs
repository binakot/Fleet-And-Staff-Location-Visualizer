using UnityEngine;

namespace Assets.Scripts.UI
{
    public sealed class FollowToGameObject : MonoBehaviour
    {
        private const float YOffset = 5f;

        public GameObject followedGameObject;

        private MeshRenderer _mesh;

        private void Start()
        {
            _mesh = followedGameObject.GetComponentInChildren<MeshRenderer>();
        }

        private void Update()
        {
            //transform.position = Camera.main.WorldToScreenPoint(followedGameObject.transform.position); // Center of the object.
            transform.position = Camera.main.WorldToScreenPoint(_mesh.bounds.max + new Vector3(0f, YOffset, 0f)); // Centered top of the object with some offset.
        }
    }
}
