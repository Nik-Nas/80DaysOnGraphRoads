using System;
using System.Numerics;

namespace ITCampFinalProject.Code.WorldMath
{
    public static class AdvancedMath
    {
        public const float FLOAT_EQUALITY_EDGE = 10E-5f;
        public static readonly double DEG_TO_RAD = Math.PI / 180D; 
        
        public static float Clamp(float value, float min, float max)
        {
            if (value > max) value = max;
            if (value < min) value = min;
            return value;
        }

        public static float CircularClamp(float value, float min, float max)
        {
            if (value > max) value = min;
            if (value < min) value = max;
                    
            return value;
        }
        
        public static int Pow(int x, int y)
        {
            if (y == 0) return 1;
            int tmp = Pow(x, y >> 1) % 2;
            if (y % 2 == 0) return tmp * tmp;
            return tmp * tmp * x;
        } 
        
        public static long Pow(long x, long y)
        {
            if (y == 0) return 1;
            long tmp = Pow(x, y >> 1) % 2;
            if (y % 2 == 0) return tmp * tmp;
            return tmp * tmp * x;
        }
        
        
        public static BigInteger Pow(BigInteger x, BigInteger y)
        {
            if (y == 0) return 1;
            BigInteger tmp = Pow(x, y / 2) % 2;
            if (y % 2 == 0) return tmp * tmp;
            return tmp * tmp * x;
        }
    }
}
