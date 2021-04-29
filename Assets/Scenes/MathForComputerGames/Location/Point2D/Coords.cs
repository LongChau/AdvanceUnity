using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAdvance.Location
{
    public class Coords
    {
        float x;
        float y;
        float z;

        public Coords(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = -1;
        }

        public Coords(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Coords(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }

        public Vector3 ToVector3 => new Vector3(x, y, z);
        public Vector2 ToVector2 => new Vector2(x, y);

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Z { get => z; set => z = value; }

        public override string ToString()
        {
            return $"({x},{y},{z})";
        }

        static public void DrawPoint(Coords position, float width, Color colour)
        {
            GameObject line = new GameObject($"Point_{position.ToString()}");
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
            lineRenderer.material.color = colour;
            lineRenderer.positionCount = 2;
            // Draw a Rhombus - con thoi. 
            lineRenderer.SetPosition(0, new Vector3(position.x - width/3.0f, position.y - width/3.0f, position.z));
            lineRenderer.SetPosition(1, new Vector3(position.x + width / 3.0f, position.y + width / 3.0f, position.z));
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
        }

        static public void DrawLine(Coords start, Coords end, float width, Color colour)
        {
            GameObject line = new GameObject($"Point_{start.ToString()}");
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
            lineRenderer.material.color = colour;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(start.x, start.y, start.z));
            lineRenderer.SetPosition(1, new Vector3(end.x, end.y, end.z));
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
        }

        static public void DrawLine(Coords[] points, float width, Color colour)
        {
            GameObject line = new GameObject($"Point_{points.ToString()}");
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
            lineRenderer.material.color = colour;
            lineRenderer.positionCount = points.Length;
            for (int i = 0; i < points.Length; i++)
            {
                var point = points[i];
                lineRenderer.SetPosition(i, new Vector3(point.x, point.y, point.z));
            }
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
        }
    }
}
