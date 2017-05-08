using UnityEngine;

namespace Assets.Scripts.Helpers
{
    /// <summary>
    /// Free flight camera.
    /// Controls:
    ///     WASD - movement
    ///     RMB dragging - rotation
    ///     Space - hold the height when moving & rotating.
    ///     
    /// TODO Implement Q and E for the height changing (similar to Unity scene's camera).
    /// </summary>
    public sealed class FreeFlightCamera : MonoBehaviour
    {
        public float Sensitivity = 0.25f;

        public float Speed = 100.0f;
        public float MaxSpeed = 1000.0f;
        public float Acceleration = 250.0f;

        public bool MovementStaysFlat = true;
        public bool RotateOnlyIfMouseDown = true;

        private Vector3 _lastMouse = new Vector3(255, 255, 255);
        private float _totalRun = 1.0f;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
                _lastMouse = Input.mousePosition;

            if (!RotateOnlyIfMouseDown || RotateOnlyIfMouseDown && Input.GetMouseButton(1))
            {
                _lastMouse = Input.mousePosition - _lastMouse;
                _lastMouse = new Vector3(-_lastMouse.y * Sensitivity, _lastMouse.x * Sensitivity, 0);
                _lastMouse = new Vector3(transform.eulerAngles.x + _lastMouse.x, transform.eulerAngles.y + _lastMouse.y, 0);
                transform.eulerAngles = _lastMouse;
                _lastMouse = Input.mousePosition;
            }

            var point = GetBaseInput();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _totalRun += Time.deltaTime;
                point = point * _totalRun * Acceleration;
                point.x = Mathf.Clamp(point.x, -MaxSpeed, MaxSpeed);
                point.y = Mathf.Clamp(point.y, -MaxSpeed, MaxSpeed);
                point.z = Mathf.Clamp(point.z, -MaxSpeed, MaxSpeed);
            }
            else
            {
                _totalRun = Mathf.Clamp(_totalRun * 0.5f, 1f, 1000f);
                point = point * Speed;
            }

            point = point * Time.deltaTime;
            var newPosition = transform.position;
            if (Input.GetKey(KeyCode.Space) || MovementStaysFlat && !(RotateOnlyIfMouseDown && Input.GetMouseButton(1)))
            {
                transform.Translate(point);
                newPosition.x = transform.position.x;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
            }
            else
            {
                transform.Translate(point);
            }
        }

        private static Vector3 GetBaseInput()
        {
            var velocity = new Vector3();
            if (Input.GetKey(KeyCode.W))
                velocity += new Vector3(0, 0, 1);
            if (Input.GetKey(KeyCode.S))
                velocity += new Vector3(0, 0, -1);
            if (Input.GetKey(KeyCode.A))
                velocity += new Vector3(-1, 0, 0);
            if (Input.GetKey(KeyCode.D))
                velocity += new Vector3(1, 0, 0);

            return velocity;
        }
    }
}
