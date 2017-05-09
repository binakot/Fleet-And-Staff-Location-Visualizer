using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class MathUtils
    {
        public static float ClampAngle(float angle, float min, float max)
        {
            if (min < 0 && max > 0 && (angle > max || angle < min))
            {
                angle -= 360;
                if (angle > max || angle < min)
                {
                    return Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max)) ? min : max;
                }
            }
            else if (min > 0 && (angle > max || angle < min))
            {
                angle += 360;
                if (angle > max || angle < min)
                {
                    return Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max)) ? min : max;
                }
            }

            if (angle < min)
                return min;
            if (angle > max)
                return max;
            return angle;
        }
    }
}