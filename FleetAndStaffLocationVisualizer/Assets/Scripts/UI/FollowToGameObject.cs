using Assets.Scripts.Data.Model.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public sealed class FollowToGameObject : MonoBehaviour
    {
        private const float YOffset = 5f;

        public GameObject followedGameObject;

        private BaseObject _object;
        private MeshRenderer _mesh;
        private Text _label;

        private void Start()
        {
            _object = followedGameObject.GetComponent<BaseObject>();
            _mesh = followedGameObject.GetComponentInChildren<MeshRenderer>();
            _label = this.GetComponentInChildren<Text>();
        }

        private void Update()
        {
            _label.text = _object.ToString().Trim();

            //transform.position = Camera.main.WorldToScreenPoint(followedGameObject.transform.position); // Center of the object.
            transform.position = Camera.main.WorldToScreenPoint(_mesh.bounds.max + new Vector3(0f, YOffset, 0f)); // Centered top of the object with some offset.
        }
    }
}