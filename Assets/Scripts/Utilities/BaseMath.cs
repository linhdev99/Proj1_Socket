using UnityEngine;

namespace BKSpeed
{
    public class BaseMath
    {
        public static float Round(float number, int count)
        {
            if (count <= 0) return Mathf.Round(number);
            return Mathf.Round(number * 10 * count) / count / 10;
        }
    }
}