using System;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    public static class HexagonUtility
    {
        public static float GetSAxis(float q, float r)
        {
            return -q + r;
        }

        public static int GetSAxis(int q, int r)
        {
            return Mathf.RoundToInt(GetSAxis((float)q, (float)r));
        }
    }
}
