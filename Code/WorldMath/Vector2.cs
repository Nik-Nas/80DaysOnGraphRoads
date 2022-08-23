using System;

// ReSharper disable InconsistentNaming

namespace ITCampFinalProject.Code.WorldMath
{
    public struct Vector2 : IComparable<Vector2>
    {
        public static readonly Vector2 zero = new Vector2(0, 0);
        public static readonly Vector2 one = new Vector2(1, 1);
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float Length => (float) Math.Sqrt(x * x + y * y);

        public Vector2 Normalized => new Vector2(x / Length, y / Length);
        
        #region Vector Operations

        public static float Distance(Vector2 from, Vector2 to)
        {
            return (from - to).Length;
        }

        public static float Angle(Vector2 v1, Vector2 v2)
        {
            return (float) (Math.Acos(Dot(v1, v2 - v1)) * 180D / Math.PI);
        }

        public static float Dot(Vector2 v1, Vector2 v2)
        {
            return v1.x * v2.x + v1.y * v2.y;
        }

        public static Vector2 Cross(Vector2 v1)
        {
            return new Vector2(-v1.y, v1.x);
        }
        
        #endregion

        #region Operator Overriding
        
        public static Vector2 operator +(Vector2 addTo, Vector2 addFrom)
        {
            addFrom.x += addTo.x;
            addFrom.y += addTo.y;
            return addFrom;
        }

        public static Vector2 operator -(Vector2 subtractFrom, Vector2 subtrahend)
        {
            subtractFrom.x -= subtrahend.x;
            subtractFrom.y -= subtrahend.y;
            return subtractFrom;
        }
        
        public static Vector2 operator -(Vector2 negativeFrom)
        {
            negativeFrom.x *= -1;
            negativeFrom.y *= -1;
            return negativeFrom;
        }
        
        public static Vector2 operator /(Vector2 dividingVector, float coefficient)
        {
            if (coefficient == 0.0f) throw new DivideByZeroException("STOP!!! STOP!! DON'T DO THIS! DON'T DIVIDE BY ZERO! IT'S ILLEGAL!!!");
            dividingVector.x /= coefficient;
            dividingVector.y /= coefficient;
            return dividingVector;
        }
        
        public static Vector2 operator *(Vector2 multipliedVector, float coefficient)
        {
            multipliedVector.x *= coefficient;
            multipliedVector.y *= coefficient;
            return multipliedVector;
        }
        
        public static bool operator ==(Vector2 compareTo, Vector2 compareWith)
        {
            Vector2 compareToNormalized = compareTo.Normalized;
            Vector2 compareWithNormalized = compareWith.Normalized;
            return Math.Abs(compareTo.Length - compareWith.Length) < AdvancedMath.FLOAT_EQUALITY_EDGE &&
                   Math.Abs(compareToNormalized.x - compareWithNormalized.x) < AdvancedMath.FLOAT_EQUALITY_EDGE &&
                   Math.Abs(compareToNormalized.y - compareWithNormalized.y) < AdvancedMath.FLOAT_EQUALITY_EDGE;
        }

        public static bool operator !=(Vector2 compareTo, Vector2 compareWith)
        {
            return !(compareTo == compareWith);
        }

        public static bool operator >(Vector2 compareTo, Vector2 compareWith)
        {
            Vector2 compareToNormalized = compareTo.Normalized;
            Vector2 compareWithNormalized = compareWith.Normalized;
            return compareTo.Length > compareWith.Length &&
                   compareToNormalized.x > compareWithNormalized.x &&
                   compareToNormalized.y > compareWithNormalized.y;
        }
        
        public static bool operator >=(Vector2 compareTo, Vector2 compareWith)
        {
            Vector2 compareToNormalized = compareTo.Normalized;
            Vector2 compareWithNormalized = compareWith.Normalized;
            return compareTo.Length >= compareWith.Length &&
                   compareToNormalized.x >= compareWithNormalized.x &&
                   compareToNormalized.y >= compareWithNormalized.y;
        }

        public static bool operator <=(Vector2 compareTo, Vector2 compareWith)
        {
            Vector2 compareToNormalized = compareTo.Normalized;
            Vector2 compareWithNormalized = compareWith.Normalized;
            return compareTo.Length <= compareWith.Length &&
                   compareToNormalized.x <= compareWithNormalized.x &&
                   compareToNormalized.y <= compareWithNormalized.y; 
        }

        public static bool operator <(Vector2 compareTo, Vector2 compareWith)
        {
            return !(compareTo > compareWith);
        }
        
        public bool Equals(Vector2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }

        #endregion

        public int CompareTo(Vector2 other)
        {
            int xComparison = x.CompareTo(other.x);
            if (xComparison != 0) return xComparison;
            return y.CompareTo(other.y);
        }
    }
}