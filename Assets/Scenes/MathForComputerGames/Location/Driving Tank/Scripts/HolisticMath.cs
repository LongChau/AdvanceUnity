using System.Collections;
using System.Collections.Generic;
using UnityAdvance.Location;
using UnityEngine;

namespace UnityAdvance.SectionVector
{
    /// <summary>
    /// Mathematic basic without using Unity math support.
    /// </summary>
    public class HolisticMath
    {
        static public Coords GetNormal(Coords vector)
        {
            var magnitude = SquareRoot(Square(vector.X) + Square(vector.Y));
            return new Coords(vector.X / magnitude, vector.Y / magnitude);
        }

        static public float Distance(Coords p1, Coords p2)
        {
            return SquareRoot(Square(p1.X - p2.X) + Square(p1.Y - p2.Y));
        }

        static public float SquareRoot(float value) => Mathf.Sqrt(value);
        static public float Square(float value) => Mathf.Pow(value, 2);

        static public Vector2 Direction(Coords p1, Coords p2)
        {
            var direction = p2.ToVector2 - p1.ToVector2;
            var coord = new Coords(direction);
            return GetNormal(coord).ToVector2;
        }

        static public float Dot(Coords vector1, Coords vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y;
        }

        static public float Angle(Coords vector1, Coords vector2)
        {
            var dot = Dot(vector1, vector2);
            var distance = Distance(new Coords(0, 0, 0), vector1) * Distance(new Coords(0, 0, 0), vector2);
            return Mathf.Acos(dot / distance); // Radians. For degrees * 180/mathf.PI;
        }

        /// <summary>
        /// Find the rotate vector.
        /// </summary>
        /// <param name="vector">Starting vector</param>
        /// <param name="angle">Angle in radians</param>
        /// <returns></returns>
        static public Coords Rotate(Coords vector, float angle, bool clockwise) // in radians
        {
            if (clockwise)
            {
                // A trick to make it rotate anti-clockwise.
                angle = 2 * Mathf.PI - angle;
            }

            float xVal = vector.X * Mathf.Cos(angle) - vector.Y * Mathf.Sin(angle);
            float yVal = vector.X * Mathf.Sin(angle) + vector.Y * Mathf.Cos(angle);
            return new Coords(xVal, yVal, 0);
        }

        /// <summary>
        /// Find Cross product.
        /// </summary>
        /// <returns></returns>
        static public Coords Cross(Coords vec1, Coords vec2)
        {
            var x = vec1.Y * vec2.Z - vec1.Z * vec2.Y;
            var y = vec1.Z * vec2.X - vec1.X * vec2.Z;
            var z = vec1.X * vec2.Y - vec1.Y * vec2.X;
            return new Coords(x, y, z);
        }

        static public Coords LookAt2D(Coords forwardVector, Coords position, Coords focusPoint)
        {
            Coords direction = new Coords(focusPoint.X - position.X, focusPoint.Y - position.Y, position.Z);
            float angle = HolisticMath.Angle(forwardVector, direction);
            bool clockwise = false;
            if (Cross(forwardVector, direction).Z < 0)
                clockwise = true;
            Coords newDir = Rotate(forwardVector, angle, clockwise);
            return newDir;
        }

        /// <summary>
        /// Add vector to existing vector
        /// </summary>
        /// <returns></returns>
        static public Coords Translate(Coords position, Coords facing, Coords vector)
        {
            if (Distance(new Coords(0, 0, 0), vector) <= 0) return position;
            float angle = Angle(vector, facing);
            float worldAngle = Angle(vector, new Coords(0, 1, 0));
            bool isClockwise = Cross(vector, facing).Z < 0;
            vector = Rotate(vector, angle + worldAngle, isClockwise);

            float xVal = position.X + vector.X;
            float yVal = position.Y + vector.Y;
            float zVal = position.Z + vector.Z;
            return new Coords(xVal, yVal, zVal);
        }
    }
}
