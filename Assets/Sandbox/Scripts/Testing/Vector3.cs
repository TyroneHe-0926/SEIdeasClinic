//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//namespace Sandbox
//{
//    public class Vector3
//    {
//        public float x;
//        public float y;
//        public float z;

//        //constructor
//        public Vector3(float xval, float yval, float zval)
//        {
//            x = xval;
//            y = yval;
//            z = zval;
//        }

//        //addition
//        public static Vector3 operator +(Vector3 v1, Vector3 v2)
//        {
//            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
//        }

//        //subtraction
//        public static Vector3 operator -(Vector3 v1, Vector3 v2)
//        {
//            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
//        }

//        //negation
//        public static Vector3 operator -(Vector3 v1)
//        {
//            return new Vector3(-v1.x, -v1.y, -v1.z);
//        }

//        //adds components
//        public static float SumComponents(Vector3 v1)
//        {
//            return (v1.x + v1.y + v1.z);
//        }

//        //to a given power
//        public static Vector3 PowComponents(Vector3 v1, float power)
//        {
//            return new Vector3 (Mathf.Pow(v1.x, power), Mathf.Pow(v1.y, power), Mathf.Pow(v1.z, power));
//        }

//        //Square root
//        public static Vector3 SqrtComponents(Vector3 v1)
//        {
//            return (new Vector3 (Mathf.Sqrt(v1.x), Mathf.Sqrt(v1.y), Mathf.Sqrt(v1.z)));
//        }

//        //Squares
//        public static Vector3 SqrComponents(Vector3 v1)
//        {
//            return(new Vector3(v1.x * v1.x, v1.y * v1.y, v1.z * v1.z));
//        }

//        //Sum of squares
//        public static float SumComponentSqrs(Vector3 v1)
//        {
//            Vector3 v2 = SqrComponents(v1);
//            return SumComponents(v2);
//        }

//        //less than
//        public static bool operator <(Vector3 v1, Vector3 v2)
//        {
//            return SumComponentSqrs(v1) < SumComponentSqrs(v2);
//        }

//        //less than or equal to
//        public static bool operator <=(Vector3 v1, Vector3 v2)
//        {
//            return SumComponentSqrs(v1) <= SumComponentSqrs(v2);
//        }

//        //greater than
//        public static bool operator >(Vector3 v1, Vector3 v2)
//        {
//            return SumComponentSqrs(v1) > SumComponentSqrs(v2);
//        }

//        //greater than or equal to
//        public static bool operator >=(Vector3 v1, Vector3 v2)
//        {
//            return SumComponentSqrs(v1) >= SumComponentSqrs(v2);
//        }

//        //equality
//        public static bool operator ==(Vector3 v1, Vector3 v2)
//        {
//            if (v1 is Vector3 && v2 is Vector3)
//                return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
//            else
//                return false;
//        }

//        //inequality
//        public static bool operator !=(Vector3 v1, Vector3 v2)
//        {
//            return !(v1 == v2);
//        }

//        //mutliplication by scalar
//        public static Vector3 operator *(Vector3 v1, float s2)
//        {
//            return new Vector3 (v1.x * s2, v1.y * s2, v1.z * s2);
//        }

//        public static Vector3 operator *(float s1, Vector3 v2)
//        {
//            return v2 * s1;
//        }

//        //Cross Product
//        public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
//        {
//            return new Vector3 (v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
//        }

//        //Dot Product
//        public static float DotProduct(Vector3 v1, Vector3 v2)
//        {
//            return (v1.x * v2.x + v1.y * v2.y + v1.z * v2.z);
//        }

//        //Division
//        public static Vector3 operator /(Vector3 v1, float s2)
//        {
//            return (new Vector3 (v1.x / s2, v1.y / s2, v1.z / s2));
//        }

//        //Magnitude
//        public static float Magnitude(Vector3 v1)
//        {
//            return Mathf.Sqrt(SumComponentSqrs(v1));

//        }

//        //public override int GetHashCode()
//        //{
//        //    unchecked
//        //    {
//        //        var hashCode = this.x.GetHashCode();
//        //        hashCode = (hashCode * 397) ^ this.y.GetHashCode();
//        //        hashCode = (hashCode * 397) ^ this.z.GetHashCode();
//        //        return hashCode;
//        //    }
//        //}

//        public static bool isNull(object v1)
//        {
//            return (v1 == null);
           
//        }
//    }
//}