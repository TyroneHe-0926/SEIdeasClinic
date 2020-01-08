//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//namespace Sandbox
//{
//    public class Vector2
//    {
//        public float x;
//        public float y;

//        public Vector2(float xval, float yval)
//        {
//            x = xval;
//            y = yval;
//        }


//        public Vector2(double xval, double yval)
//        {
//            x = (float)xval;
//            y = (float)yval;
//        }

//        public Vector2()
//        {

//        }


//        public static Vector2 operator+(Vector2 v1, Vector2 v2)
//        {
//            return new Vector2(v1.x + v2.x, v1.y + v2.y);
//        }

//        public static Vector2 operator -(Vector2 v1, Vector2 v2)
//        {
//            return new Vector2(v1.x - v2.x, v1.y - v2.y);
//        }

//        public static Vector2 operator -(Vector2 v1)
//        {
//            return new Vector2(-v1.x, -v1.y);
//        }

//        //adds components
//        public static float SumComponents(Vector2 v1)
//        {
//            return (v1.x + v1.y);
//        }

//        //to a given power
//        public static Vector2 PowComponents(Vector2 v1, float power)
//        {
//            return new Vector2(Mathf.Pow(v1.x, power), Mathf.Pow(v1.y, power));
//        }

//        //Square root
//        public static Vector2 SqrtComponents(Vector2 v1)
//        {
//            return (new Vector2(Mathf.Sqrt(v1.x), Mathf.Sqrt(v1.y)));
//        }

//        //Squares
//        public static Vector2 SqrComponents(Vector2 v1)
//        {
//            return (new Vector2(v1.x * v1.x, v1.y * v1.y));
//        }

//        //Sum of squares
//        public static float SumComponentSqrs(Vector2 v1)
//        {
//            Vector2 v2 = SqrComponents(v1);
//            return SumComponents(v2);
//        }

//        //less than
//        public static bool operator <(Vector2 v1, Vector2 v2)
//        {
//            return SumComponentSqrs(v1) < SumComponentSqrs(v2);
//        }

//        //less than or equal to
//        public static bool operator <=(Vector2 v1, Vector2 v2)
//        {
//            return SumComponentSqrs(v1) <= SumComponentSqrs(v2);
//        }

//        //greater than
//        public static bool operator >(Vector2 v1, Vector2 v2)
//        {
//            return SumComponentSqrs(v1) > SumComponentSqrs(v2);
//        }

//        //greater than or equal to
//        public static bool operator >=(Vector2 v1, Vector2 v2)
//        {
//            return SumComponentSqrs(v1) >= SumComponentSqrs(v2);
//        }

//        //equality
//        public static bool operator ==(Vector2 v1, Vector2 v2)

//        {
//            return v1.x == v2.x && v1.y == v2.y;


//        }

//        //inequality
//        public static bool operator !=(Vector2 v1, Vector2 v2)
//        {
//            return !(v1 == v2);
//        }

//        //mutliplication by scalar
//        public static Vector2 operator *(Vector2 v1, float s2)
//        {
//            return new Vector2(v1.x * s2, v1.y * s2);
//        }

//        public static Vector2 operator *(float s1, Vector2 v2)
//        {
//            return v2 * s1;
//        }

//        //Dot Product
//        public static float DotProduct(Vector2 v1, Vector2 v2)
//        {
//            return (v1.x * v2.x + v1.y * v2.y);
//        }

//        //Division
//        public static Vector2 operator /(Vector2 v1, float s2)
//        {
//            return (new Vector2(v1.x / s2, v1.y / s2));
//        }

//        //Magnitude
//        public static float Magnitude(Vector2 v1)
//        {
//            return Mathf.Sqrt(SumComponentSqrs(v1));

//        }

//        public static bool isNull(object v1)
//        {
//            return (v1 == null);

//        }
//    }
//}