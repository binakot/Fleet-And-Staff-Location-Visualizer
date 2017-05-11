using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public sealed class AppQuitOnEsc : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }
    }
}