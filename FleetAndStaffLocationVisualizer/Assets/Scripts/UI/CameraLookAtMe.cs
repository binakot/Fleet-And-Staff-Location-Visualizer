using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public sealed class CameraLookAtMe : MonoBehaviour
    {
        public GameObject target;

        private void Start()
        {
            var button = this.GetComponent<Button>();
            button.onClick.AddListener(() => MoveCameraToThisGameObject());
        }

        private void MoveCameraToThisGameObject()
        {
            Camera.main.transform.position = new Vector3(target.transform.position.x - 100f, Camera.main.transform.position.y, target.transform.position.z - 100f);
            Camera.main.transform.LookAt(target.transform);
        }
    }    
}
