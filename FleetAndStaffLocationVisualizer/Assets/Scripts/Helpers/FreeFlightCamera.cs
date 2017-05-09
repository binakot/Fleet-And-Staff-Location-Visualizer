using Assets.Scripts.Utils;
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
        [Header("Rotation")]
        public float MinVerticalAngle = 20f;
        public float MaxVerticalAngle = 60f;
        public float Sensitivity = 0.25f;
        public bool RotateOnlyIfMouseDown = true;

        [Header("Movement")]
        public float MinHeight = 50f;
        public float MaxHeight = 300f;
        public float Speed = 100f;
        public float MaxSpeed = 1000f;
        public float Acceleration = 250f;
        public bool MovementStaysFlat = true;
        
        private Vector3 _rotate = Vector3.zero;
        private float _accelerationTime = 1f;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
                _rotate = Input.mousePosition;

            if (!RotateOnlyIfMouseDown || RotateOnlyIfMouseDown && Input.GetMouseButton(1))
            {
                _rotate = Input.mousePosition - _rotate;
                _rotate = new Vector3(-_rotate.y * Sensitivity, _rotate.x * Sensitivity, 0);
                _rotate = new Vector3(MathUtils.ClampAngle(transform.eulerAngles.x + _rotate.x, MinVerticalAngle, MaxVerticalAngle), transform.eulerAngles.y + _rotate.y, 0);
                transform.eulerAngles = _rotate;
                _rotate = Input.mousePosition;
            }

            var move = GetBaseInput();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _accelerationTime += Time.deltaTime;
                move = move * _accelerationTime * Acceleration;
                move.x = Mathf.Clamp(move.x, -MaxSpeed, MaxSpeed);
                move.y = Mathf.Clamp(move.y, -MaxSpeed, MaxSpeed);
                move.z = Mathf.Clamp(move.z, -MaxSpeed, MaxSpeed);
            }
            else
            {
                _accelerationTime = Mathf.Clamp(_accelerationTime * 0.5f, 1f, 1000f);
                move = move * Speed;
            }

            move = move * Time.deltaTime;
            var newPosition = transform.position;
            if (Input.GetKey(KeyCode.Space) || MovementStaysFlat && !(RotateOnlyIfMouseDown && Input.GetMouseButton(1)))
            {
                transform.Translate(move);
                newPosition.x = transform.position.x;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
            }
            else
            {
                transform.Translate(move);

                var temp = transform.position;
                temp.y = Mathf.Clamp(temp.y, MinHeight, MaxHeight);
                transform.position = temp;
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
