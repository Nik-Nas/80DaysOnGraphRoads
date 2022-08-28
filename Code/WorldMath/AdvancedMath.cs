using System;
using System.Numerics;

namespace ITCampFinalProject.Code.WorldMath
{
    public static class AdvancedMath
    {
        public const float FLOAT_EQUALITY_EDGE = 10E-5f;
        public const double DEG_TO_RAD = 0.01745329251994329576923690768489D; 
        public const double RAD_TO_DEG = 57.295779513082320876798154814105D; 
        
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
        
        public static float Pow(float x, float y)
        {
            if (y == 0) return 1;
            float tmp = Pow(x, y / 2) % 2;
            if (y % 2 == 0) return tmp * tmp;
            return tmp * tmp * x;
        }
        
        public static double Pow(double x, double y)
        {
            if (y == 0) return 1;
            double tmp = Pow(x, y / 2) % 2;
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
        
        public static float NextFloat(Random random)
        {
            double mantissa = random.NextDouble() * 2.0 - 1.0;
            // choose -149 instead of -126 to also generate subnormal floats (*)
            double exponent = Math.Pow(2.0f, random.Next(-126, 128));
            return (float) (mantissa * exponent);
        }
    }
}
