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
    }
}
