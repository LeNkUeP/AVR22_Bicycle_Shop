using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine
{
    public struct Vector3
    {
        public static Vector3 up;
        public static Vector3 right;
        internal static int kEpsilon;
        internal float magnitude;
        internal float x;
        internal float y;
        internal float z;

        public Vector3(float x, float y, float z) 
        {
            magnitude = 0;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2) => v1;
        public static Vector3 operator +(float f, Vector3 v2) => v2;
        public static Vector3 operator -(Vector3 v1, Vector3 v2) => v1;
        public static Vector3 operator /(Vector3 v1, float f) => v1;
        public static Vector3 operator *(Vector3 v1, float f) => v1;
        public static Vector3 operator -(Vector3 v1) => v1;
    }
}
