using UnityEngine;

namespace Assets.Scripts.UI
{
    public sealed class FollowToGameObject : MonoBehaviour
    {
        public GameObject followedGameObject;

        private void Update()
        {
            transform.position = Camera.main.WorldToScreenPoint(followedGameObject.transform.position);
        }
    }
}
